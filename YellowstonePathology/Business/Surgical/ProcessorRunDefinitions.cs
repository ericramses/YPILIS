using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Surgical
{

    //this.m_ProcessorRunCollection.Add(new ProcessorRun("CHCHTO", "Cheech, Today, Overnight", new TimeSpan(17, 0, 0), new TimeSpan(3, 10, 0), "3hrs 10min", ProcessorRunDayEnum.Today));
    //this.m_ProcessorRunCollection.Add(new ProcessorRun("CHCHTMLM", "Cheech, Tomorrow, Long Mini", new TimeSpan(5, 0, 0), new TimeSpan(0, 40, 0), "40min", ProcessorRunDayEnum.Tomorrow));
    //this.m_ProcessorRunCollection.Add(new ProcessorRun("CHCHTMSM", "Cheech, Tomorrow, Short Mini", new TimeSpan(5, 0, 0), new TimeSpan(0, 20, 0), "20min", ProcessorRunDayEnum.Tomorrow));
    //this.m_ProcessorRunCollection.Add(new ProcessorRun("CHCHTMO", "Cheech, Tomorrow, Overnight", new TimeSpan(17, 0, 0), new TimeSpan(2, 10, 0), "3hrs 10min", ProcessorRunDayEnum.Tomorrow));
    //this.m_ProcessorRunCollection.Add(new ProcessorRun("CHCHS", "Cheech, Sunday", new TimeSpan(17, 0, 0), new TimeSpan(2, 30, 0), "2hrs 30min", ProcessorRunDayEnum.Sunday));            

    public class CheechTodayOvernight : ProcessorRun
    {
        public CheechTodayOvernight()
        {
            //this.m_ProcessorRunCollection.Add(new ProcessorRun("CHCHTO", "Cheech, Today, Overnight", new TimeSpan(17, 0, 0), new TimeSpan(3, 10, 0), "3hrs 10min", ProcessorRunDayEnum.Today));
            this.m_ProcessorRunId = "CHCHTO";
            this.m_Name = "Cheech, Today, Overnight";
            this.m_StartTime = new TimeSpan(17, 0, 0);
            this.m_FixationTime = new TimeSpan(3, 10, 0);
            this.m_FixationTimeString = "3hrs 10min";
            this.m_ProcessorRunDay = ProcessorRunDayEnum.Today;
        }        
    }

    public class CheechTomorrowLongMini : ProcessorRun
    {        
        public CheechTomorrowLongMini()
        {
            //this.m_ProcessorRunCollection.Add(new ProcessorRun("CHCHTMLM", "Cheech, Tomorrow, Long Mini", new TimeSpan(5, 0, 0), new TimeSpan(0, 40, 0), "40min", ProcessorRunDayEnum.Tomorrow));
            this.m_ProcessorRunId = "CHCHTMLM";
            this.m_Name = "Cheech, Tomorrow, Long Mini";
            this.m_StartTime = new TimeSpan(5, 0, 0);
            this.m_FixationTime = new TimeSpan(0, 40, 0);
            this.m_FixationTimeString = "40min";
            this.m_ProcessorRunDay = ProcessorRunDayEnum.Tomorrow;
        }
    }

    public class CheechTomorrowShortMini : ProcessorRun
    {        
        public CheechTomorrowShortMini()
        {
            //this.m_ProcessorRunCollection.Add(new ProcessorRun("CHCHTMSM", "Cheech, Tomorrow, Short Mini", new TimeSpan(5, 0, 0), new TimeSpan(0, 20, 0), "20min", ProcessorRunDayEnum.Tomorrow));
            this.m_ProcessorRunId = "CHCHTMSM";
            this.m_Name = "Cheech, Tomorrow, Short Mini";
            this.m_StartTime = new TimeSpan(5, 0, 0);
            this.m_FixationTime = new TimeSpan(0, 20, 0);
            this.m_FixationTimeString = "20min";
            this.m_ProcessorRunDay = ProcessorRunDayEnum.Tomorrow;
        }
    }

    public class CheechTomorrowOvernight : ProcessorRun
    {        
        public CheechTomorrowOvernight()
        {
            //this.m_ProcessorRunCollection.Add(new ProcessorRun("CHCHTMO", "Cheech, Tomorrow, Overnight", new TimeSpan(17, 0, 0), new TimeSpan(2, 10, 0), "3hrs 10min", ProcessorRunDayEnum.Tomorrow));
            this.m_ProcessorRunId = "CHCHTMO";
            this.m_Name = "Cheech, Tomorrow, Overnight";
            this.m_StartTime = new TimeSpan(17, 0, 0);
            this.m_FixationTime = new TimeSpan(2, 10, 0);
            this.m_FixationTimeString = "3hrs 10min";
            this.m_ProcessorRunDay = ProcessorRunDayEnum.Tomorrow;
        }
    }

    public class CheechSunday : ProcessorRun
    {        
        public CheechSunday()
        {
            //this.m_ProcessorRunCollection.Add(new ProcessorRun("CHCHS", "Cheech, Sunday", new TimeSpan(17, 0, 0), new TimeSpan(2, 30, 0), "2hrs 30min", ProcessorRunDayEnum.Sunday));            
            this.m_ProcessorRunId = "CHCHS";
            this.m_Name = "Cheech, Sunday";
            this.m_StartTime = new TimeSpan(17, 0, 0);
            this.m_FixationTime = new TimeSpan(2, 30, 0);
            this.m_FixationTimeString = "2hrs 30min";
            this.m_ProcessorRunDay = ProcessorRunDayEnum.Sunday;
        }
    }
}
