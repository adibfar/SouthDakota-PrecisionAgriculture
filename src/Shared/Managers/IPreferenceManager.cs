using PAS.Shared.Settings;
using System.Threading.Tasks;
using PAS.Shared.Wrapper;

namespace PAS.Shared.Managers
{
    public interface IPreferenceManager
    {
        Task SetPreference(IPreference preference);

        Task<IPreference> GetPreference();

        Task<IResult> ChangeLanguageAsync(string languageCode);
    }
}