using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Bilbayt.Core.Exceptions;
using Bilbayt.Core.Interfaces.Persistence;
using FluentValidation;
using MediatR;

namespace Bilbayt.Models.AppUser
{
    /// <summary>
    ///     Get related query, validators, and handlers
    /// </summary>
    public class Get
    {
        /// <summary>
        ///     Model to Get an entity
        /// </summary>
        public class GetQuery : IRequest<QueryResponse>
        {
            /// <summary>
            ///     Id
            /// </summary>
            public string Id { get; set; }

        }

        /// <summary>
        ///     Query Response
        /// </summary>
        public class QueryResponse
        {
            /// <summary>
            ///     Resource
            /// </summary>
            public AppUserModel Resource { get; set; }
        }

        /// <summary>
        ///     Register Validation 
        /// </summary>
        public class GetAppUserQueryValidator : AbstractValidator<GetQuery>
        {
            /// <summary>
            ///     Validator ctor
            /// </summary>
            public GetAppUserQueryValidator()
            {
                RuleFor(x => x.Id)
                    .NotEmpty();
            }

        }


        /// <summary>
        ///     Handler
        /// </summary>
        public class QueryHandler : IRequestHandler<GetQuery, QueryResponse>
        {
            private readonly IAppUserRepository _repo;
            private readonly IMapper _mapper;

            /// <summary>
            ///     Ctor
            /// </summary>
            /// <param name="repo"></param>
            /// <param name="mapper"></param>
            public QueryHandler(IAppUserRepository repo,
                                  IMapper mapper)
            {
                this._repo = repo ?? throw new ArgumentNullException(nameof(repo));
                this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }

            /// <summary>
            ///     Handle
            /// </summary>
            /// <param name="query"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public async Task<QueryResponse> Handle(GetQuery query, CancellationToken cancellationToken)
            {
                var response = new QueryResponse();

                var entity = await _repo.GetItemAsync(query.Id);
                if (entity == null)
                {
                    throw new EntityNotFoundException(nameof(AppUser), query.Id);
                }

                response.Resource = _mapper.Map<AppUserModel>(entity);

                return response;
            }
        }
    }
}
