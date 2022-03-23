// <copyright file="MapperProfile.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Northwind.Services.Blogging;
using Northwind.Services.EntityFramework;
using Northwind.Services.EntityFramework.Blogging.Entities;

namespace Northwind.Services.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using AutoMapper;
    using Northwind.Services.Employees;
    using Northwind.Services.Products;

    /// <summary>
    /// Mapper profile.
    /// </summary>
    public class MapperProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MapperProfile"/> class.
        /// </summary>
        public MapperProfile()
        {
            /*this.CreateMap<Product, ProductDTO>()
                .ForMember("ProductId", opt=>opt.MapFrom(m=>m.Id))
                .ForMember("ProductName", opt=> opt.MapFrom(m=>m.Name));
            this.CreateMap<ProductDTO, Product>()
                .ForMember("Id", opt => opt.MapFrom(m => m.ProductId))
                .ForMember("Name", opt => opt.MapFrom(m => m.ProductName)); ;
            this.CreateMap<ProductCategory, CategoryDTO>()
                .ForMember("CategoryId", opt => opt.MapFrom(m => m.Id))
                .ForMember("CategoryName", opt => opt.MapFrom(m => m.Name)); ;
            this.CreateMap<CategoryDTO, ProductCategory>()
                .ForMember("Id", opt => opt.MapFrom(m => m.CategoryId))
                .ForMember("Name", opt => opt.MapFrom(m => m.CategoryName)); ; ;
            this.CreateMap<Employee, EmployeeDTO>()
                .ForMember("EmployeeId", opt => opt.MapFrom(m => m.Id));
            this.CreateMap<EmployeeDTO, Employee>()
                .ForMember("Id", opt => opt.MapFrom(m => m.EmployeeId));*/
            this.CreateMap<BlogArticle, BlogArticleDTO>()
                .ForMember("BlogArticleId", opt => opt.MapFrom(m => m.Id))
                .ReverseMap();
        }
    }
}
