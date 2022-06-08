using PAS.Domain.Contracts;

namespace PAS.Domain.Entities.Farms
{
    public class Farm : AuditableEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}