using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Task.Model
{
    public class TaskFax : Task
    {
        public TaskFax(string assignedTo, string description) 
            : base(assignedTo, description)
        {
            this.m_TaskId = "TSKFAX";
            this.m_TaskName = "Fax";
        }
    }
}
