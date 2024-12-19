using cafeMvc.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace cafeMvc.ViewModels 
{
    public class DishCreateViewModel
    {
        public string Name { get; set; } = string.Empty; // Default empty string
        public string ImageUrl { get; set; } = string.Empty;
        public double Price { get; set; } = 0;

        public List<int> SelectedIngredientIds { get; set; } = new List<int>();

        public List<SelectListItem> AvailableIngredients { get; set; } = new List<SelectListItem>();
    }
}