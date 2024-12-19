using System.ComponentModel.DataAnnotations;
 

namespace cafeMvc.Models
{
    public class User 
    {
        public int UserId { get; set; }
        [EmailAddress]
        public string Email { get; set; }
         public string PasswordHash { get; set; } // storing hashed password
        public string Salt { get; set; }  
       // Navigation property for many-to-many relationship
        public List<Role> Roles { get; set; } = new List<Role>();
        public Customer Customer { get; set; }

    }
}