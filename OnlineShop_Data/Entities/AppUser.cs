using Microsoft.AspNetCore.Identity;
using OnlineShop_Data.Enums;
using OnlineShop_Data.Interfaces;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop_Data.Entities
{
    [Table("AppUsers")]
    public class AppUser : IdentityUser<Guid>, IDateTracking, ISwitchable
    {
        public string FullName { get; set; }

        public DateTime? BirthDay { set; get; }

        public decimal Balance { get; set; }

        public string Avatar { get; set; }

        public Status Status { get; set; }
        public DateTime CreatedDate { get;set; }
        public DateTime ModifiedDate { get;set; }
    }
}