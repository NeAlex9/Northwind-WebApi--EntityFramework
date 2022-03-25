// <copyright file="MapperProfile.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using AutoMapper;
using Northwind.Services.Blogging;
using Northwind.Services.EntityFramework.Blogging.Entities;

namespace Northwind.Services.EntityFramework.Blogging
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
            this.CreateMap<BlogArticle, BlogArticleDTO>()
                .ForMember("BlogArticleId", opt=> opt.MapFrom(m=> m.Id))
                .ReverseMap();

            this.CreateMap<BlogComment, BlogCommentDTO>()
                .ForMember("BlogCommentId", opt => opt.MapFrom(m => m.Id))
                .ReverseMap();

            this.CreateMap<BlogArticleProduct, BlogArticleProductDTO>()
                .ForMember("BlogArticleProductId", opt => opt.MapFrom(m => m.Id))
                .ReverseMap();
        }
    }
}
