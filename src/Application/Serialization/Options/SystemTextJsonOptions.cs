using System.Text.Json;
using PAS.Application.Interfaces.Serialization.Options;

namespace PAS.Application.Serialization.Options
{
    public class SystemTextJsonOptions : IJsonSerializerOptions
    {
        public JsonSerializerOptions JsonSerializerOptions { get; } = new();
    }
}