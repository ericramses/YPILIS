using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Task.Model
{
    public class TaskMicrodisectionForMolecular : Task
    {
        public TaskMicrodisectionForMolecular()
        {
            this.m_TaskId = "MDFMPREP";
            this.m_TaskName = "Microdisection for Molecular";
            this.m_Description = "Microdisection for Molecular";
            this.m_AssignedTo = TaskAssignment.Histology;            
        }
    }
}
