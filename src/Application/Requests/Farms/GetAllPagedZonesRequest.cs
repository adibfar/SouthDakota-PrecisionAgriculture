namespace PAS.Application.Requests.Farms
{
    public class GetAllPagedZonesRequest : PagedRequest
    {
        public string SearchString { get; set; }
    }
}