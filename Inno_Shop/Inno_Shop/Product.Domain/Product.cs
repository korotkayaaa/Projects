using System;

namespace Product.Domain
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool Availabillity { get; set; }
        public Guid UserID { get; set; }
        public DateTime DateCreating { get; set; }
    }
}
