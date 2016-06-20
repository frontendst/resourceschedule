using System.Linq;

namespace ResourceManagementSystem2.Models
{
    public class ProgrammerService
    {
        private readonly DbContext context;

        public ProgrammerService(DbContext context)
        {
            this.context = context;
        }

        public ProgrammerService()
        {
            context = new DbContext();
        }

        public Programmer[] GetProgrammers()
        {
            return context.Programmers.ToArray();
        }

        public Project[] GetProjects()
        {
            return context.Projects.ToArray();
        }
    }
}