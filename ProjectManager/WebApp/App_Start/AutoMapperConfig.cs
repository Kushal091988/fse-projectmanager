using AutoMapper;
using BusinessTier.Models;
using ProjectManager.Api.Extension.DTO;
using ProjectManager.Api.Extension.Helper;

namespace WebApp
{
    public static class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize((cfg) =>
            {
                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<UserDto, User>();

                cfg.CreateMap<Project, ProjectDto>()
                .ForMember(x => x.StartDate, opt => opt.MapFrom(x => x.StartDate.DateToYYYYMMDD()))
                .ForMember(x => x.EndDate, opt => opt.MapFrom(x => x.EndDate.DateToYYYYMMDD()));

                cfg.CreateMap<ProjectDto, Project>()
               .ForMember(x => x.StartDate, opt => opt.MapFrom(x => x.StartDate.YYYYMMDDToDate()))
               .ForMember(x => x.EndDate, opt => opt.MapFrom(x => x.EndDate.YYYYMMDDToDate()));

                cfg.CreateMap<Task, TaskDto>()
                .ForMember(x => x.StartDate, opt => opt.MapFrom(x => x.StartDate.DateToYYYYMMDD()))
                .ForMember(x => x.EndDate, opt => opt.MapFrom(x => x.EndDate.DateToYYYYMMDD()));

                cfg.CreateMap<TaskDto, Task>()
               .ForMember(x => x.StartDate, opt => opt.MapFrom(x => x.StartDate.YYYYMMDDToDate()))
               .ForMember(x => x.EndDate, opt => opt.MapFrom(x => x.EndDate.YYYYMMDDToDate()));

                cfg.CreateMap<ParentTask, ParentTaskDto>();
                cfg.CreateMap<ParentTaskDto, ParentTask>();
            });
        }
    }
}