
using PUCorporate.Model.Model;
using PUCorporate.Model.DTO;
using PUCorporate.DataAccessLayer.Services.Interfaces;
using PUCorporate.Model.Model.ViewModel;

namespace PUCorporateAPI.Services.Services
{
    public class AuthData : IAuthData
    {
        private readonly IGeneric _service;
        public AuthData(IGeneric service)
        {
            _service = service;
        }
        public Task CreateAdminandUser(User model)
        {
            //return _service.SaveData("APP_USER_INSERTNEWUSER_STP", new
            //{
            //    Model = JsonConvert.SerializeObject(model)
            //}


            //);
            string firstName = model.FirstName;
            string lastName = model.LastName;
            string loginName = model.LoginName;
            string emailAddress = model.Email;
            string password = model.Password;
            string loginstatus = "Active";

            return _service.SaveData("APP_USER_INSERTNEWUSER_STP", 
             new
             {
                 firstName,
                 lastName,
                 loginName,
                 emailAddress,
                 password,
                 loginstatus
             }


           );
        }

        public async Task<User?> GetDataforAuth(User user)
        {
            string P_LoginName = user.LoginName;
            string P_Password = user.Password;
            string P_PassKey = "SQL SERVER 2008 APEX";
            string Token = user.Token;
            string WebToken = user.Jwt;

            var result = await _service.LoadData<User, dynamic>("STP_APP_USER_VerifyUser",
                new {P_LoginName,
                    P_Password,
                    P_PassKey,
                    Token,
                    WebToken
                });

            return result.FirstOrDefault();
        }

      
    }
}
