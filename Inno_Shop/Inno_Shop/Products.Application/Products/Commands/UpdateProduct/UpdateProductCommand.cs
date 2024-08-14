using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public bool? Availabillity { get; set; }
        public DateTime? DateCreating { get; set; }
        public Guid? UserId { get; set; }
    }
}
