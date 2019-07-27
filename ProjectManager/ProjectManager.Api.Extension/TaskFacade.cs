using AutoMapper;
using DataAccess.Repositories.Intefaces;
using ProjectManager.Api.Extension.DTO;
using ProjectManager.Api.Extension.Interfaces;
using ProjectManager.SharedKernel;
using ProjectManager.SharedKernel.FilterCriteria;
using System;
using System.Collections.Generic;

namespace ProjectManager.Api.Extension
{
    public class TaskFacade : ITaskFacade
    {
        private readonly ITaskRepository _taskRepository;

        public TaskFacade(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public FilterResult<TaskDto> Query(FilterState filterState)
        {
            var filterResult = _taskRepository.Query(filterState);

            if (filterResult != null)
            {
                var tasks = Mapper.Map<List<TaskDto>>(filterResult.Data);
                return new FilterResult<TaskDto>
                {
                    Total = filterResult.Total,
                    Data = tasks
                };
            }

            return null;
        }

        /// <summary>
        /// get task
        /// </summary>
        /// <param name="id">task id</param>
        /// <returns>task for the given id</returns>
        public TaskDto Get(int id)
        {
            var task = _taskRepository.Get(id);
            if (task == null)
            {
                throw new InvalidOperationException("user does not exists");
            }

            var taskDto = Mapper.Map<TaskDto>(task);
            return taskDto;
        }

        /// <summary>
        /// delete task
        /// </summary>
        /// <param name="id"></param>
        /// <returns>flag to know if deleted</returns>
        public bool Delete(int id)
        {
            var task = _taskRepository.Get(id);
            if (task == null)
            {
                throw new InvalidOperationException("task does not exists so could not be deleted");
            }

            _taskRepository.Remove(task);
            _taskRepository.SaveChanges();

            return true;
        }

        /// <summary>
        /// Get all tasks
        /// </summary>
        /// <returns>tasks list</returns>
        public List<TaskDto> GetAll()
        {
            var tasks = _taskRepository.GetAll();
            var taskDtos = Mapper.Map<List<TaskDto>>(tasks);

            return taskDtos;
        }

        /// <summary>
        /// either create or update provided task
        /// </summary>
        /// <param name="taskDto"></param>
        /// <returns></returns>
        public TaskDto Update(TaskDto taskDto)
        {
            var task = _taskRepository.Get(taskDto.Id);
            if (task == null)
            {
                //create task
                task = Mapper.Map<BusinessTier.Models.Task>(taskDto);
                _taskRepository.Add(task);
            }
            else
            {
                //update task
                task.Name = taskDto.Name;
                task.StartDate = taskDto.StartDate.YYYYMMDDToDate();
                task.EndDate = taskDto.EndDate.YYYYMMDDToDate();
                task.ProjectId = taskDto.ProjectId;
            }
            _taskRepository.SaveChanges();

            return taskDto;
        }
    }
}