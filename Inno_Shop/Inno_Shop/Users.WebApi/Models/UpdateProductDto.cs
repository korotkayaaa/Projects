using AutoMapper;
using Products.Application.Products.Commands.UpdateProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.Application.Common.Mappings;

namespace Users.WebApi.Models
{
    public class UpdateProductDto : IMapWith<UpdateProductCommand>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool Availabillity { get; set; }
        public DateTime DateCreating { get; set; }
        public Guid UserId { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateProductDto, UpdateProductCommand>()
                .ForMember(productCommand => productCommand.Name,
                opt => opt.MapFrom(productDto => productDto.Name))
                .ForMember(productCommand => productCommand.Description,
                opt => opt.MapFrom(productDto => productDto.Description))
                .ForMember(productCommand => productCommand.Price,
                opt => opt.MapFrom(productDto => productDto.Price))
                 .ForMember(productCommand => productCommand.Availabillity,
                opt => opt.MapFrom(productDto => productDto.Availabillity))
                  .ForMember(productCommand => productCommand.DateCreating,
                opt => opt.MapFrom(productDto => productDto.DateCreating))
                    .ForMember(productCommand => productCommand.Id,
                opt => opt.MapFrom(productDto => productDto.Id))
                      .ForMember(productCommand => productCommand.UserId,
                opt => opt.MapFrom(productDto => productDto.UserId));
        }
    }
}
