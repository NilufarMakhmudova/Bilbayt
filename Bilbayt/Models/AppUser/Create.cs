using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Bilbayt.Core.Interfaces.Email;
using Bilbayt.Core.Interfaces.Password;
using Bilbayt.Core.Interfaces.Persistence;
using Bilbayt.Core.Specifications;
using FluentValidation;
using MediatR;

namespace Bilbayt.Models.AppUser
{
    /// <summary>
    ///     Create related commands, validators and handlers
    /// </summary>
    public class Create
    {
        /// <summary>
        ///     Model to create an entity
        /// </summary>
        public class CreateAppUserCommand : IRequest<CommandResponse>
        {
            /// <summary>
            ///     First name
            /// </summary>
            public string FirstName { get; set; }
            /// <summary>
            ///     First name
            /// </summary>
            public string LastName { get; set; }
            /// <summary>
            ///     Username
            /// </summary>
            public string Username { get; set; }
            /// <summary>
            ///     Password
            /// </summary>
            public string Password { get; set; }
             /// <summary>
            ///     Confirmed password
            /// </summary>
            public string ConfirmedPassword { get; set; }
             /// <summary>
             ///     Full name
             /// </summary>
             public string FullName => $"{FirstName} {LastName}";
        }

        /// <summary>
        ///     Command Response
        /// </summary>
        public class CommandResponse
        {
            /// <summary>
            ///     Item Id
            /// </summary>
            public string Id { get; set; }
        }

        /// <summary>
        ///     Register Validation 
        /// </summary>
        public class CreateAppUserCommandValidator : AbstractValidator<CreateAppUserCommand>
        {
            private readonly IAppUserRepository _repo;

            /// <summary>
            ///     Validator ctor
            /// </summary>
            public CreateAppUserCommandValidator(IAppUserRepository repo)
            {
                this._repo = repo ?? throw new ArgumentNullException(nameof(repo));

                RuleFor(x => x.FirstName)
                    .NotEmpty()
                    .Length(0, 100);
                RuleFor(x => x.LastName)
                    .NotEmpty()
                    .Length(0, 100);
                RuleFor(x => x.Username)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty()
                    .EmailAddress().WithMessage("A valid email is required")
                    .MustAsync(HasUniqueUsername).WithMessage("User name must be unique");
                RuleFor(x => x.Password)
                    .NotEmpty();
                RuleFor(x => x.ConfirmedPassword)
                    .NotEmpty()
                    .Equal(x => x.Password);
            }

            /// <summary>
            ///     Check uniqueness
            /// </summary>
            /// <param name="username"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public async Task<bool> HasUniqueUsername(string username, CancellationToken cancellationToken)
            {
                var specification = new AppUserSearchSpecification(username,
                                                                      exactSearch: true);

                var entities = await _repo.GetItemsAsync(specification);

                return entities == null || !entities.Any();

            }
        }


        /// <summary>
        ///     Handler
        /// </summary>
        public class CommandHandler : IRequestHandler<CreateAppUserCommand, CommandResponse>
        {
            private readonly IAppUserRepository _repo;
            private readonly IMapper _mapper;
            private readonly IEmailService _emailService;
            private readonly IPasswordManager _passwordManager;

            /// <summary>
            ///     Ctor
            /// </summary>
            /// <param name="repo"></param>
            /// <param name="mapper"></param>
            /// <param name="emailService"></param>
            public CommandHandler(IAppUserRepository repo,
                                  IMapper mapper, 
                                  IEmailService emailService, 
                                  IPasswordManager passwordManager)
            {
                this._repo = repo ?? throw new ArgumentNullException(nameof(repo));
                this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
                this._emailService = emailService;
                this._passwordManager = passwordManager;
            }

            /// <summary>
            ///     Handle
            /// </summary>
            /// <param name="command"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public async Task<CommandResponse> Handle(CreateAppUserCommand command, CancellationToken cancellationToken)
            {
                var response = new CommandResponse();
                var entity = _mapper.Map<Core.Entities.AppUser>(command);

                //hash password
                entity.Password = _passwordManager.GetHashPassword(entity.Password);
                await _repo.AddItemAsync(entity);

                response.Id = entity.Id;

                //send welcoming email
                var subject = "Welcome to Bilbayt!";
                var body =
                    $"Dear {command.FullName}, </br> Your registration in bilbayt.com was successful. Thank you for using our services. </br> Best regards, </br> bilbayt.com team";

                await _emailService.SendEmailAsync(command.Username, command.FullName, subject, body);

                return response;
            }
        }
    }
}
