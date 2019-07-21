using BusinessTier.Models;
using DataAccess.Repositories.Intefaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
       
    }
}
