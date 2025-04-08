using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.application.Dtos;
using Task.application.Response;

namespace Task.repository.Data.Repos
{
    public interface IUserRepo
    {
        #region Registeration

        Task<bool> RegisterAsync(RegisterDto user);

        #endregion

        #region Login
        Task<AuthResult> LoginAsync(LoginDto user);


        #endregion


    }
}
