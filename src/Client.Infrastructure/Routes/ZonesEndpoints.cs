using System.Linq;

namespace PAS.Client.Infrastructure.Routes
{
    public static class ZonesEndpoints
    {
        public static string GetAllPaged(int pageNumber, int pageSize, string searchString, string[] orderBy)
        {
            var url = $"api/v1/zones?pageNumber={pageNumber}&pageSize={pageSize}&searchString={searchString}&orderBy=";
            if (orderBy?.Any() == true)
            {
                foreach (var orderByPart in orderBy)
                {
                    url += $"{orderByPart},";
                }
                url = url[..^1]; // loose training ,
            }
            return url;
        }

        public static string ExportFiltered(string searchString)
        {
            return $"{Export}?searchString={searchString}";
        }

        public static string GetCount = "api/v1/zones/count";
        public static string Save = "api/v1/zones";
        public static string Delete = "api/v1/zones";
        public static string Export = "api/v1/zones/export";
    }
}