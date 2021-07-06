using System.Threading.Tasks;
using Bilbayt.Infrastructure.Identity.Models.Authentication;
using Bilbayt.Models.AppUser;
using Bilbayt.Models.Token;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bilbayt.Controllers
{
    /// <summary>
    ///     All token related actions.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UserController
    {
        private readonly IMediator _mediator;

        /// <summary>
        ///     ctor
        /// </summary>
        /// <param name="mediator"></param>
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: api/User/Authenticate
        /// <summary>
        ///     Validate that the user account is valid and return an auth token
        ///     to the requesting app for use in the api.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Authenticate")]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<TokenResponse> AuthenticateAsync([FromBody] Authenticate.AuthenticateCommand command)
        {
            var response = await _mediator.Send(command);
            return response.Resource;
        }


        // POST: api/User/Create
        /// <summary>
        ///    Create new user record in the Cosmos DB
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Create")]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<string> CreateAsync([FromBody] Create.CreateAppUserCommand command)
        {
            var response = await _mediator.Send(command);
            return response.Id;
        }


        // GET: api/User/Profile/5
        /// <summary>
        ///    Get
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize("Bearer")]
        [HttpGet("{id}", Name = "Profile")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<AppUserModel> GetProfileAsync(string id)
        {
            var response = await _mediator.Send(new Get.GetQuery(){Id = id});
            return response.Resource;
        }
    }
}
