using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace cafeMvc.Models
{
    //represents each item in order
    public class OrderDetail
    {
         public int Id { get; set; }

        public int OrderId { get; set; }

        public int DishId { get; set; }
        public int Quantity { get; set; }
// Navigation properties
        public Order Order { get; set; }

        public  Dish Dish { get; set; }
    }
}