﻿using AutoMapper;
using Entity.Dtos;
using Entity.Models;

namespace Api.Helpers.Mapping;

public class MappingService : Profile
{

    public MappingService()
    {
        CreateMap<StudentModel, StudentDto>().ReverseMap();
        CreateMap<CompanyModel, CompanyDto>().ReverseMap();

    }
}