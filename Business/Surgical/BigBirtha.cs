using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Surgical
{
    public class BigBirtha : Processor
    {
        public BigBirtha()
        {
            this.m_Name = "Big Birtha";
            this.m_ProcessorRunCollection.Add(new ProcessorRun("BBTO", "Big Birtha, Today, Overnight", new TimeSpan(17, 0, 0), new TimeSpan(2, 30, 0), "2hrs 30min", ProcessorRunDayEnum.Today));
            this.m_ProcessorRunCollection.Add(new ProcessorRun("BBTMLM", "Big Birtha, Tomorrow, Long Mini", new TimeSpan(5, 0, 0), new TimeSpan(0, 40, 0), "40min", ProcessorRunDayEnum.Tomorrow));
            this.m_ProcessorRunCollection.Add(new ProcessorRun("BBTMSM", "Big Birtha, Tomorrow, Short Mini", new TimeSpan(5, 0, 0), new TimeSpan(0, 20, 0), "20min", ProcessorRunDayEnum.Tomorrow));
            this.m_ProcessorRunCollection.Add(new ProcessorRun("BBTMO", "Big Birtha, Tomorrow, Overnight", new TimeSpan(17, 0, 0), new TimeSpan(2, 30, 0), "2hrs 30min", ProcessorRunDayEnum.Tomorrow));
            this.m_ProcessorRunCollection.Add(new ProcessorRun("BBS", "Big Birtha, Sunday", new TimeSpan(17, 0, 0), new TimeSpan(2, 30, 0), "2hrs 30min", ProcessorRunDayEnum.Sunday));            
        }
    }
}