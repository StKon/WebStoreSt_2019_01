using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Domain.Dto;
using WebStore.Domain.Entities;

namespace WebStore.Services.Map
{
    public static class ProductDto2Product
    {
        public static ProductDto Map(this Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                ImageUrl = product.ImageUrl,
                Order = product.Order,
                Price = product.Price,
                Brand = product.Brand is null ? 
                (product.BrandId is null ? null :
                  new BrandDto
                  {
                      Id = (int) product.BrandId,
                      Name = string.Empty
                  })
                :
                  new BrandDto
                  {
                      Id = product.Brand.Id,
                      Name = product.Brand.Name
                  },
                Section = product.Section is null ? 
                  new SectionDto
                  {
                      Id = (int) product.SectionId,
                      Name = string.Empty
                  }
                :
                  new SectionDto
                  {
                      Id = product.Section.Id,
                      Name = product.Section.Name
                  }
            };
        }

        public static Product Map(this ProductDto product)
        {
            return new Product
            {
                Id = product.Id,
                Name = product.Name,
                ImageUrl = product.ImageUrl,
                Order = product.Order,
                Price = product.Price,
                BrandId = product.Brand?.Id,                
                SectionId = product.Section.Id
            };
        }
    }

}
