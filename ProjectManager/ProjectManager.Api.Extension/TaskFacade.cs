﻿using AutoMapper;
using DataAccess.Repositories.Intefaces;
using ProjectManager.Api.Extension.DTO;
using ProjectManager.Api.Extension.Interfaces;
using ProjectManager.SharedKernel;
using ProjectManager.SharedKernel.Enums;
using ProjectManager.SharedKernel.FilterCriteria;
using System;
using System.Collections.Generic;
using System.Linq;

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
                throw new InvalidOperationException("task does not exists");
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
            var tasks = _taskRepository.GetAll()
                                       .OrderByDescending(p => p.Id);

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
                task.StatusId = (int)TaskStatusEnum.InProgress;
                _taskRepository.Add(task);
            }
            else
            {
                //update task
                task.Name = taskDto.Name;
                task.StartDate = taskDto.StartDate.YYYYMMDDToDate();
                task.EndDate = taskDto.EndDate.YYYYMMDDToDate();
                task.ParentTaskId = taskDto.ParentTaskId;
                task.Priority = taskDto.Priority;
            }
            _taskRepository.SaveChanges();

            return taskDto;
        }

        /// <summary>
        /// update task state
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="statusId"></param>
        /// <returns></returns>
        public bool UpdateTaskState(int taskId, int statusId)
        {
            var task = _taskRepository.Get(taskId);
            if (task == null)
            {
                throw new InvalidOperationException("Task does not exists");
            }
            else
            {
                if (task.StatusId == statusId) throw new InvalidOperationException("Task state is already up to date");
                //update project
                Enum.TryParse<TaskStatusEnum>(statusId.ToString(), out TaskStatusEnum taskStatusEnum);
                task.StatusId = (int)taskStatusEnum;
            }
            _taskRepository.SaveChanges();
            return true;
        }
    }
}