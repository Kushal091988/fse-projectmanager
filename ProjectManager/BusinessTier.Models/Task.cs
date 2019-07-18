using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessTier.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        //Foreign key for ParentTask
        //[ForeignKey("ParentTask")]
        public int ParentTaskId { get; set; }

        public virtual ParentTask ParentTask { get; set; }

        //Foreign key for user
        //[ForeignKey("Owner")]
        public int OwnerId { get; set; }

        public virtual User Owner { get; set; }

        //[ForeignKey("Project")]
        //Foreign key for Project
        public int ProjectId { get; set; }

        public virtual Project Project { get; set; }
    }
}