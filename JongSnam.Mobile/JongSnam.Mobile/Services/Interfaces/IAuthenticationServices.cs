using System.Threading.Tasks;

namespace JongSnam.Mobile.Services.Interfaces
{
    public interface IAuthenticationServices
    {
        Task<bool> Login(string user, string password);
        Task<bool> Logut();
    }
}
