namespace Basket.API.Models
{
    public class ShoppingCart
    {
        // instead of using Guid we use the username as the id
        public string UserName { get; set; } = default!;

        public List<ShoppingCartItem> Items { get; set; } = new ();

        public double TotalPrice => Items.Sum(x => x.Price * x.Quantity);


        public ShoppingCart(string userName)
        {
            UserName = userName;
            
        }

        // Required for Mappig
        public ShoppingCart()
        {
            
        }




    }
}
