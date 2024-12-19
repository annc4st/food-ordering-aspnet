using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace cafeMvc.Models
{
    public class Customer
    {
        public int Id { get; set;}

         public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Address { get; set; }
        // public Discount? Discount { get; set; }
         // Foreign key to User
        public int UserId { get; set; }
         // Navigation property to User
        public User User { get; set; }
        public ICollection<Order> Orders { get; set; }

    }

}