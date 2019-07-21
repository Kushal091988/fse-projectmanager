using AutoMapper;
using BusinessTier.Models;
using DataAccess.Repositories.Intefaces;
using ProjectManager.Api.Extension.DTO;
using ProjectManager.Api.Extension.Helper;
using ProjectManager.Api.Extension.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Api.Extension
{
    public class ProjectFacade : IProjectFacade
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectFacade(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        /// <summary>
        /// get project
        /// </summary>
        /// <param name="id">project id</param>
        /// <returns>project for the given id</returns>
        public ProjectDto Get(int id)
        {
            var project = _projectRepository.Get(id);
            if (project == null)
            {
                throw new InvalidOperationException("user does not exists");
            }

            var projectDto = Mapper.Map<ProjectDto>(project);
            return projectDto;
        }


        /// <summary>
        /// delete project
        /// </summary>
        /// <param name="id"></param>
        /// <returns>flag to know if deleted</returns>
        public bool Delete(int id)
        {
            var project = _projectRepository.Get(id);
            if (project == null)
            {
                throw new InvalidOperationException("project does not exists");
            }

            if (project.Tasks?.Count>0)
            {
                throw new InvalidOperationException("project has associated tasks so could not be deleted.");
            }

            _projectRepository.Remove(project);
            _projectRepository.SaveChanges();

            return true;
        }


        /// <summary>
        /// Get all projects
        /// </summary>
        /// <returns>projects list</returns>
        public List<ProjectDto> GetAll()
        {
            var projects = _projectRepository.GetAll();
            var projectDtos = Mapper.Map<List<ProjectDto>>(projects);

            return projectDtos;
        }

        /// <summary>
        /// either create or update provided project
        /// </summary>
        /// <param name="projectDto"></param>
        /// <returns></returns>
        public ProjectDto Update(ProjectDto projectDto)
        {
            var project = _projectRepository.Get(projectDto.Id);
            if (project == null)
            {
                //create project
                project = Mapper.Map<Project>(projectDto);
                _projectRepository.Add(project);
            }
            else
            {
                //update project
                project.Name = projectDto.Name;
                project.StartDate = projectDto.StartDate.YYYYMMDDToDate();
                project.EndDate = projectDto.EndDate.YYYYMMDDToDate();
                project.Priority = projectDto.Priority;
            }
            _projectRepository.SaveChanges();

            return projectDto;
        }
    }
}
