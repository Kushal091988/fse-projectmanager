using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectManager.Api.Extension.DTO
{
    public class ParentTaskDto
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
    }
}