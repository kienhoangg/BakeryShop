using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop_Application.AutoMapper
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(
                c =>
                {
                    c.AddProfile(new DomainToViewModelMappingProfile());
                    c.AddProfile(new ViewModelToDomainMappingProfile());
                });
        }
    }
}
