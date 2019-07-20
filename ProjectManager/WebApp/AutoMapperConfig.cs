using AutoMapper;
using BusinessTier.Models;
using WebApp.DTO;
using WebApp.Helper;

namespace WebApp
{
    public static class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize((cfg) =>
            {
                cfg.CreateMap<User, UserDto>();

                cfg.CreateMap<Project, ProjectDto>()
                .ForMember(x => x.StartDate, opt => opt.MapFrom(x => x.StartDate.DateToYYYYMMDD()))
                .ForMember(x => x.EndDate, opt => opt.MapFrom(x => x.EndDate.DateToYYYYMMDD()));

                cfg.CreateMap<Task, TaskDto>()
                .ForMember(x => x.StartDate, opt => opt.MapFrom(x => x.StartDate.DateToYYYYMMDD()))
                .ForMember(x => x.EndDate, opt => opt.MapFrom(x => x.EndDate.DateToYYYYMMDD()));

                cfg.CreateMap<ParentTask, ParentTaskDto>();
            });
        }
    }
}