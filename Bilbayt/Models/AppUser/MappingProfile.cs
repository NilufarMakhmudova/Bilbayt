using AutoMapper;

namespace Bilbayt.Models.AppUser
{
    /// <summary>
    ///     Mapping Profile for AutoMapper
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        ///     ctor
        /// </summary>
        public MappingProfile()
        {
            // Get
            CreateMap<Core.Entities.AppUser, AppUserModel>().ReverseMap();

            // Create
            CreateMap<Create.CreateAppUserCommand, Core.Entities.AppUser>();
        }

    }
}
