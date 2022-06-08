using PAS.Domain.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using PAS.Application.Interfaces.Chat;
using PAS.Application.Models.Chat;

namespace PAS.Infrastructure.Models.Identity
{
    public class User : IdentityUser<string>, IChatUser, IAuditableEntity<string>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string CreatedBy { get; set; }

        [Column(TypeName = "text")]
        public string ProfilePictureDataUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime? LastModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
        public bool IsActive { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public virtual ICollection<ChatHistory<User>> ChatHistoryFromUsers { get; set; }
        public virtual ICollection<ChatHistory<User>> ChatHistoryToUsers { get; set; }

        public User()
        {
            ChatHistoryFromUsers = new HashSet<ChatHistory<User>>();
            ChatHistoryToUsers = new HashSet<ChatHistory<User>>();
        }
    }
}