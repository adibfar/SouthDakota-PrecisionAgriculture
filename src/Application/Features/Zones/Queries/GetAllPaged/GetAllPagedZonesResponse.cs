namespace PAS.Application.Features.Zones.Queries.GetAllPaged
{
    public class GetAllPagedZonesResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Farm { get; set; }
        public int FarmId { get; set; }
    }
}