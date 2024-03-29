﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectManager.Api.Extension.DTO
{
    public class ProjectDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
         
        [DataType(DataType.Date)]
        public string StartDate { get; set; }
         
        [DataType(DataType.Date)]
        public string EndDate { get; set; }

        [Required]
        public int Priority { get; set; }

        [Required]
        public int ManagerId { get; set; }

        public string ManagerDisplayName { get; set; }

        public int TotalTasks { get; set; }
        public int TotalCompletedTasks { get; set; }

        public bool IsSuspended { get; set; }
    }
}