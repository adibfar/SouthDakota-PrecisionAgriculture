using PAS.Application.Specifications.Base;
using PAS.Domain.Entities.Farms;

namespace PAS.Application.Specifications.Farms
{
    public class FarmFilterSpecification : PasSpecification<Farm>
    {
        public FarmFilterSpecification(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = p => p.Name.Contains(searchString) || p.Description.Contains(searchString);
            }
            else
            {
                Criteria = p => true;
            }
        }
    }
}
