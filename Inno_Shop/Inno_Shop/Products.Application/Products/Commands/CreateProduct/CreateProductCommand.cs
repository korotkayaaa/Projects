using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool Availabillity { get; set; }
        public string NameUser { get; set; }
    }
}
