
using AutoMapper;
using CO_Working_Space;
using coworking_space.BAL.Dtos.UserDTO;
using coworking_space.BAL.DTOs;

namespace coworking_space.API.MappingProfiles
{
    public class MappingProfile:Profile
    {
      
            public MappingProfile()
            {
                CreateMap<User, UserReadDto>();
            }
        
    }
}
