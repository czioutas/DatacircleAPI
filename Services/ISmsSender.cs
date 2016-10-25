using System.Threading.Tasks;

namespace DatacircleAPI.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
