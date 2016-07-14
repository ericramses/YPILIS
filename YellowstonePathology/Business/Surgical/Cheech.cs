using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Surgical
{
    public class Cheech : Processor
    {
        public Cheech()
        {
            this.m_Name = "Cheech";
            this.m_ProcessorRunCollection.Add(new ProcessorRun("CHCHTO", "Cheech, Today, Overnight", new TimeSpan(17, 0, 0), new TimeSpan(3, 10, 0), "3hrs 10min", ProcessorRunDayEnum.Today));
            this.m_ProcessorRunCollection.Add(new ProcessorRun("CHCHTMLM", "Cheech, Tomorrow, Long Mini", new TimeSpan(5, 0, 0), new TimeSpan(0, 40, 0), "40min", ProcessorRunDayEnum.Tomorrow));
            this.m_ProcessorRunCollection.Add(new ProcessorRun("CHCHTMSM", "Cheech, Tomorrow, Short Mini", new TimeSpan(5, 0, 0), new TimeSpan(0, 20, 0), "20min", ProcessorRunDayEnum.Tomorrow));
            this.m_ProcessorRunCollection.Add(new ProcessorRun("CHCHTMO", "Cheech, Tomorrow, Overnight", new TimeSpan(17, 0, 0), new TimeSpan(2, 10, 0), "3hrs 10min", ProcessorRunDayEnum.Tomorrow));
            this.m_ProcessorRunCollection.Add(new ProcessorRun("CHCHS", "Cheech, Sunday", new TimeSpan(17, 0, 0), new TimeSpan(2, 30, 0), "2hrs 30min", ProcessorRunDayEnum.Sunday));            
        }
    }
}