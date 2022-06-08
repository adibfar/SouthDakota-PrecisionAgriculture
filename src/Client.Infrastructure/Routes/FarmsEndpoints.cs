namespace PAS.Client.Infrastructure.Routes
{
    public static class FarmsEndpoints
    {
        public static string ExportFiltered(string searchString)
        {
            return $"{Export}?searchString={searchString}";
        }

        public static string Export = "api/v1/farms/export";

        public static string GetAll = "api/v1/farms";
        public static string Delete = "api/v1/farms";
        public static string Save = "api/v1/farms";
        public static string GetCount = "api/v1/farms/count";
    }
}