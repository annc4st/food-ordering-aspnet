using System.ComponentModel.DataAnnotations;


namespace cafeMvc.ViewModels
{
    // ViewModel for displaying cart items in the shopping cart view
    public class CartItemViewModel
    {
        // public string CartItemId { get; set; }
        // public string CartId { get; set; }
        public int DishId { get; set; }      // ID of the dish
        public string DishName { get; set; } // Name of the dish
        public int Quantity { get; set; }    // Quantity selected by the user
        public double Price { get; set; }   // Price per dish
        public double Total => Quantity * Price; // Total cost for this item
    }
}