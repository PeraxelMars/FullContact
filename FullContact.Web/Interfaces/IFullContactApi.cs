using System.Threading.Tasks;
using FullContact.Enteties;

namespace FullContact.Web.Interfaces
{
    public interface IFullContactApi
    {
        Task<FullContactPerson> LookupPersonByEmailAsync(string email);
    }
}
