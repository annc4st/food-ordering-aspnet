namespace cafeMvc.Models 
{
    public class Ingredient {
       public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<DishIngredient> DishIngredients { get; set; } = new List<DishIngredient>();
    }
}