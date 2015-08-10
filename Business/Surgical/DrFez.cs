using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Surgical
{
    public class DrFez : Processor
    {
        public DrFez()
        {
            this.m_Name = "Dr. Fez";
            this.m_ProcessorRunCollection.Add(new ProcessorRun("DRFTO", "Dr. Fez, Today, Overnight", new TimeSpan(17, 0, 0), new TimeSpan(3, 10, 0), "3hrs 10min", ProcessorRunDayEnum.Today));
            this.m_ProcessorRunCollection.Add(new ProcessorRun("DRFTMLM", "Dr. Fez, Tomorrow, Long Mini", new TimeSpan(5, 0, 0), new TimeSpan(0, 40, 0), "40min", ProcessorRunDayEnum.Tomorrow));
            this.m_ProcessorRunCollection.Add(new ProcessorRun("DRFTMSM", "Dr. Fez, Tomorrow, Short Mini", new TimeSpan(5, 0, 0), new TimeSpan(0, 20, 0), "20min", ProcessorRunDayEnum.Tomorrow));
            this.m_ProcessorRunCollection.Add(new ProcessorRun("DRFTMO", "Dr. Fez, Tomorrow, Overnight", new TimeSpan(17, 0, 0), new TimeSpan(3, 10, 0), "3hrs 10min", ProcessorRunDayEnum.Tomorrow));
        }
    }
}