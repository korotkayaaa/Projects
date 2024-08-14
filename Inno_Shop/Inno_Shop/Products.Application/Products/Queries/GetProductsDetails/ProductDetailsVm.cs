using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Common.Mappings;
using Users.Domain;

namespace Products.Application.Products.Queries.GetProductsDetails
{
    public class ProductDetailsVm : IMapWith<Product>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool Availabillity { get; set; }
        public  DateTime DateCreating  { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductDetailsVm>()
                .ForMember(productVm => productVm.Id,
                    opt => opt.MapFrom(product => product.Id))
                .ForMember(productVm => productVm.Name,
                    opt => opt.MapFrom(product => product.Name))
                .ForMember(productVm => productVm.Description,
                    opt => opt.MapFrom(product => product.Description))
                .ForMember(productVm => productVm.Price,
                    opt => opt.MapFrom(product => product.Price))
                .ForMember(productVm => productVm.Availabillity,
                    opt => opt.MapFrom(product => product.Availabillity));
        }
    }
}
