using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SimpleRESTAPI.Models;

namespace SimpleRESTAPI.DTO
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Course, CourseDTO>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<Instructor, InstructorDTO>();
            CreateMap<CourseAddDTO, Course>();

            CreateMap<CourseDTO, Course>();
            CreateMap<CategoryDTO, Category>();
            CreateMap<InstructorDTO, Instructor>();

            CreateMap<AspUserDTO, AspUser>();
            CreateMap<AspUser, AspUserDTO>();
        }
    }
    
}