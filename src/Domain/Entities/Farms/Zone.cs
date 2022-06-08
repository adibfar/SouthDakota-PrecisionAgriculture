using PAS.Domain.Contracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace PAS.Domain.Entities.Farms
{
    public class Zone : AuditableEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int FarmId { get; set; }
        public virtual Farm Farm { get; set; }
    }
}