
using PAS.Application.Interfaces.Serialization.Settings;
using Newtonsoft.Json;

namespace PAS.Application.Serialization.Settings
{
    public class NewtonsoftJsonSettings : IJsonSerializerSettings
    {
        public JsonSerializerSettings JsonSerializerSettings { get; } = new();
    }
}