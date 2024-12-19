using cafeMvc.ViewModels;


namespace cafeMvc.Models
{
    public class Cart
    {
        public List<CartItemViewModel> Items { get; set; } = new List<CartItemViewModel>();
        public double TotalAmount => Items.Sum(x => x.Total);

        public void AddItem(CartItemViewModel item)
        {
            var existingItem = Items.FirstOrDefault(x => x.DishId == item.DishId);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            } else {
                Items.Add(item);
            }
        }

        public void ClearCart() => Items.Clear();
    }
}