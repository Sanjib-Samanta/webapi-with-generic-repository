using AutoMapper;
using MyStore.Database;
using MyStoreApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStoreApp.Repository
{
    public class ProductMappingProfile : Profile  
        
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductModel>().ReverseMap();
        }
    }

}
