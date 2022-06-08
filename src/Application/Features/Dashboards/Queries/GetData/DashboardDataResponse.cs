using System.Collections.Generic;

namespace PAS.Application.Features.Dashboards.Queries.GetData
{
    public class DashboardDataResponse
    {
        public int ZoneCount { get; set; }
        public int FarmCount { get; set; }
        public int UserCount { get; set; }
        public int RoleCount { get; set; }
        public List<ChartSeries> DataEnterBarChart { get; set; } = new();
        public Dictionary<string, double> ZoneByFarmPieChart { get; set; }
    }

    public class ChartSeries
    {
        public ChartSeries() { }

        public string Name { get; set; }
        public double[] Data { get; set; }
    }

}