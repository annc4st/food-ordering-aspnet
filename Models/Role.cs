using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace cafeMvc.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
       // Navigation property for many-to-many relationship
        public List<User> Users { get; set; } = new List<User>();
    }
}