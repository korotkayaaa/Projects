using System;

namespace Products.Application.Interfaces
{
    public class ProductsFilterDto
    {
        public string Name { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public bool? Availability { get; set; }
        public DateTime? MinDateCreating { get; set; }
        public DateTime? MaxDateCreating { get; set; }
        public string UserName { get; set; }
    }
}