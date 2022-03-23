// <copyright file="MapperProfile.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using AutoMapper;
using Northwind.Services.Blogging;
using NorthwindApiApp.Models.BlogArticleModel;

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
            this.CreateMap<BlogArticleToCreate, BlogArticle>();
        }
    }
}
