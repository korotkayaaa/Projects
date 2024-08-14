using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Common.Mappings;
using Users.Domain;

namespace Products.Application.Products.Queries.GetProductList.GetAllProductList
{
    public class ProductLookupDto : IMapWith<Product>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool Availabillity { get; set; }
        public DateTime DateCreating { get; set; }
        public string UserName { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductLookupDto>()
                .ForMember(productDto => productDto.Id,
                    opt => opt.MapFrom(product => product.Id))
                .ForMember(productDto => productDto.Name,
                    opt => opt.MapFrom(product => product.Name))
                .ForMember(productDto => productDto.Description,
                    opt => opt.MapFrom(product => product.Description))
                .ForMember(productDto => productDto.Price,
                    opt => opt.MapFrom(product => product.Price))
                .ForMember(productDto => productDto.Availabillity,
                    opt => opt.MapFrom(product => product.Availabillity))
                 .ForMember(productDto => productDto.DateCreating,
                    opt => opt.MapFrom(product => product.DateCreating))
                 .ForMember(productDto => productDto.UserName,
                    opt => opt.MapFrom(product => product.User.Name));
        }
    }
}
