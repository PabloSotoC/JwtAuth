using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Perficient.Training.JwtAuthentication
{

    public interface ILoginService
    {

        string GenerateJwt(User userInfo);
    }


}