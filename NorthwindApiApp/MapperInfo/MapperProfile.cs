// <copyright file="MapperProfile.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using AutoMapper;
using Northwind.Services.Blogging;
using Northwind.Services.Employees;
using NorthwindApiApp.Models.BlogArticleModel;
using NorthwindApiApp.Models.BlogArticleModels;

namespace NorthwindApiApp.MapperInfo
{
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
            this.CreateMap<Employee, BlogArticleToReadAllItems>()
                .ForMember("AuthorName", opt => opt.MapFrom(m => $"{m.FirstName} {m.LastName}, {m.Title}"))
                .ForMember("AuthorId", opt => opt.MapFrom(m => m.Id))
                .ForMember("Id", opt=>opt.Ignore())
                .ReverseMap();
            this.CreateMap<Employee, BlogArticleToReadItem>()
                .ForMember("AuthorId", opt => opt.MapFrom(m => m.Id))
                .ForMember("AuthorName", opt => opt.MapFrom(m => $"{m.FirstName} {m.LastName}, {m.Title}"))
                .ForMember("Id", opt => opt.Ignore())
                .ReverseMap();
            this.CreateMap<BlogArticleToCreate, BlogArticle>().ReverseMap();
            this.CreateMap<BlogArticleToReadItem, BlogArticle>()
                .ForMember("Id", opt=>opt.MapFrom(m=>m.Id))
                .ForMember("PublicationDate", opt=>opt.MapFrom(m=>m.Posted))
                .ReverseMap();
            this.CreateMap<BlogArticleToReadAllItems, BlogArticle>()
                .ForMember("Id", opt => opt.MapFrom(m => m.Id))
                .ForMember("PublicationDate", opt => opt.MapFrom(m => m.Posted))
                .ReverseMap();
            this.CreateMap<BlogArticleToUpdate, BlogArticle>().ReverseMap();
        }
    }
}
