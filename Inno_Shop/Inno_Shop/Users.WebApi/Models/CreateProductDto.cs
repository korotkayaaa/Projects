using AutoMapper;
using Products.Application.Products.Commands.CreateProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.Application.Common.Mappings;

namespace Users.WebApi.Models
{
    public class CreateProductDto : IMapWith<CreateProductCommand>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool Availabillity { get; set; }
        public string NameUser { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateProductDto, CreateProductCommand>()
                .ForMember(productCommand => productCommand.Name,
                opt => opt.MapFrom(productDto => productDto.Name))
                .ForMember(productCommand => productCommand.Description,
                opt => opt.MapFrom(productDto => productDto.Description))
                .ForMember(productCommand => productCommand.Price,
                opt => opt.MapFrom(productDto => productDto.Price))
                 .ForMember(productCommand => productCommand.Availabillity,
                opt => opt.MapFrom(productDto => productDto.Availabillity))
                  .ForMember(productCommand => productCommand.NameUser,
                opt => opt.MapFrom(productDto => productDto.NameUser));
        }
    }
}
