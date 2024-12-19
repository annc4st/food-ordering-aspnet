namespace cafeMvc.Models 
{ public class DishIngredient
    {
        public int DishId { get; set; }
        public Dish Dish { get; set; } = null!; // Use null forgiving operator because it's set by EF Core

        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; } = null!;
    }
}