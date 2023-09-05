using AutoMapper;
using Sales.API.Models;
using Sales.API.Models.DTOs;

namespace Sales.API.SalesMappers
{
    public class SalesMappers : Profile 
    {
        public SalesMappers()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
        }
        
    }
}
