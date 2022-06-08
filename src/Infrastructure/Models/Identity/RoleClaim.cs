using System;
using PAS.Domain.Contracts;
using Microsoft.AspNetCore.Identity;

namespace PAS.Infrastructure.Models.Identity
{
    public class RoleClaim : IdentityRoleClaim<string>, IAuditableEntity<int>
    {
        public string Description { get; set; }
        public string Group { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public virtual Role Role { get; set; }

        public RoleClaim() : base()
        {
        }

        public RoleClaim(string roleClaimDescription = null, string roleClaimGroup = null) : base()
        {
            Description = roleClaimDescription;
            Group = roleClaimGroup;
        }
    }
}