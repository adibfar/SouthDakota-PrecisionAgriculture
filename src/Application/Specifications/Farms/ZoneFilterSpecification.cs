using PAS.Application.Specifications.Base;
using PAS.Domain.Entities.Farms;

namespace PAS.Application.Specifications.Farms
{
    public class ZoneFilterSpecification : PasSpecification<Zone>
    {
        public ZoneFilterSpecification(string searchString)
        {
            Includes.Add(a => a.Farm);
            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = p => p.Name.Contains(searchString) || p.Description.Contains(searchString) || p.Farm.Name.Contains(searchString);
            }
            else
            {
                Criteria = p => true;
            }
        }
    }
}