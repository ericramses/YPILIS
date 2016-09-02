using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Task.Model
{
	public class TaskFedexShipment : Task
    {
        public TaskFedexShipment(string assignedTo, string description) 
            : base(assignedTo, description)
        {
            this.m_TaskId = "FDXSHPMNT";
            this.m_TaskName = "Fedex Shipment";
            this.m_Description = "Ship material via Fedex";            
        }       
	}
}
