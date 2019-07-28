using AutoMapper;
using BusinessTier.Models;
using ProjectManager.Api.Extension.DTO;
using ProjectManager.SharedKernel;

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
                .ForMember(x => x.EndDate, opt => opt.MapFrom(x => x.EndDate.DateToYYYYMMDD()))
                .ForMember(x => x.ManagerDisplayName, opt => opt.MapFrom(x => x.Manager.FirstName))
                .ForMember(x => x.TotalTasks, opt => opt.MapFrom(x => x.Tasks.Count));

                cfg.CreateMap<ProjectDto, Project>()
               .ForMember(x => x.StartDate, opt => opt.MapFrom(x => x.StartDate.YYYYMMDDToDate()))
               .ForMember(x => x.EndDate, opt => opt.MapFrom(x => x.EndDate.YYYYMMDDToDate()));

                cfg.CreateMap<Task, TaskDto>()
                .ForMember(x => x.StartDate, opt => opt.MapFrom(x => x.StartDate.DateToYYYYMMDD()))
                .ForMember(x => x.EndDate, opt => opt.MapFrom(x => x.EndDate.DateToYYYYMMDD()))
                .ForMember(x => x.ParentTaskName, opt => opt.MapFrom(x => x.ParentTask.Name))
                .ForMember(x => x.OwnerName, opt => opt.MapFrom(x => string.Join(" ",x.Owner.FirstName, x.Owner.LastName)))
                .ForMember(x => x.ProjectName, opt => opt.MapFrom(x => x.Project.Name));

                cfg.CreateMap<TaskDto, Task>()
               .ForMember(x => x.StartDate, opt => opt.MapFrom(x => x.StartDate.YYYYMMDDToDate()))
               .ForMember(x => x.EndDate, opt => opt.MapFrom(x => x.EndDate.YYYYMMDDToDate()));

                cfg.CreateMap<ParentTask, ParentTaskDto>();
                cfg.CreateMap<ParentTaskDto, ParentTask>();
            });
        }
    }
}