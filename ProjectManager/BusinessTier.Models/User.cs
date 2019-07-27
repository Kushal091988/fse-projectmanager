using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessTier.Models
{
    public class User
    {
        public User()
        {
            this.Tasks = new HashSet<Task>();
            this.Projects = new HashSet<Project>();
        }
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmployeeId { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}
