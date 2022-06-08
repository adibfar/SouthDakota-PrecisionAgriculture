using System.Linq;
using PAS.Shared.Constants.Localization;
using PAS.Shared.Settings;

namespace PAS.Server.Settings
{
    public record ServerPreference : IPreference
    {
        public string LanguageCode { get; set; } = LocalizationConstants.SupportedLanguages.FirstOrDefault()?.Code ?? "en-US";

        //TODO - add server preferences
    }
}