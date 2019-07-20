using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectManager.Api.Extension.DTO
{
    public class TaskDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public string StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public string EndDate { get; set; }

        [Required]
        public int ParentTaskId { get; set; }

        [Required]
        public int OwnerId { get; set; }

        [Required]
        public int ProjectId { get; set; }
    }
}