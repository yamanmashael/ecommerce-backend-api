using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class User
    {

        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Surname { get; set; }

        public DateTime CreatedAt { get; set; }


        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string ResetToken { get; set; }
        public DateTime ResetTokenExpires { get; set; }
        public bool EmailConfirmed { get; set; } 
        public string EmailConfirmationToken { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }
        public ICollection<Address> Address { get; set; }
        public ICollection<Orders> Orders { get; set; }  
        public ICollection<Favorites> UserFavorites { get; set; }       


    }
}
