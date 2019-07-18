using System;
using System.Collections.Generic;

namespace BusinessTier.Models
{
    public class ParentTask
    {
        public ParentTask()
        {
            this.Tasks = new HashSet<Task>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}