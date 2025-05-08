using coworking_space.BAL.Dtos.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coworking_space.BAL.Interaces {
    public interface IAuthService {
        Task<string?> LoginAsync(string email, string password);
        Task<string?> RegisterAsync(UserCreateDto dto);
    }
}
