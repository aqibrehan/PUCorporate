
using PUCorporate.Model.DTO;
using PUCorporate.Model.Model;

namespace PUCorporate.DataAccessLayer.Services.Interfaces
{
    public interface IAuthData
    {
        Task CreateAdminandUser(User model);
      
        Task<User?> GetDataforAuth(User user);
     
    }
}
