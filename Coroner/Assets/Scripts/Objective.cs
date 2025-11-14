
namespace Coroner
{
    public class Objective
    {
        public string description;
        public bool completed;
        public Objective[] subObjectives;

        public Objective(string description, bool completed = false, Objective[] subObjectives = null)
        {
            this.description = description;
            this.completed = completed;
            this.subObjectives = subObjectives;
        }
    }
}
