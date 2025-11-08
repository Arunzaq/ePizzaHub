namespace ePizza.UI.Models.ApiResponses
{
    
        public class GetCartResponseModel
        {
            public Guid Id { get; set; }
            public int UserId { get; set; }
            public DateTime CreatedDate { get; set; }
            public decimal Total { get; set; }
            public decimal Tax { get; set; }
            public decimal GrandTotal { get; set; }
            public List<CartItemresponse> Items { get; set; }
        }

    public class CartItemresponse
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string ItemName { get; set; }
        public string ImageUrl { get; set; }
        public decimal ItemTotal
        {
            get
            {
                return Quantity * UnitPrice;
            }
        }
    }
    
}
