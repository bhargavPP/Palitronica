namespace PalitronicaDemo.Model
{
    public class Customer
    {
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string city { get; set; }
        public string street { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }

    public class Item
    {
        public string ItemId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
