using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Surgical
{
    public class ProcessorRunCollection : ObservableCollection<ProcessorRun>
    {
        public ProcessorRunCollection()
        {

        }               

        public static ProcessorRunCollection GetAll()
        {
            ProcessorRunCollection result = new ProcessorRunCollection();

            DateTime yesterdayAt5 = DateTime.Parse(DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd") + "T17:00");
            DateTime todayAtNoon = DateTime.Parse(DateTime.Today.ToString("yyyy-MM-dd") + "T12:00");

            result.Add(new ProcessorRun("Chong, Overnight", yesterdayAt5, new TimeSpan(2, 30, 0)));
            result.Add(new ProcessorRun("Cheech, Overnight", yesterdayAt5, new TimeSpan(3, 10, 0)));
            result.Add(new ProcessorRun("Long Mini", todayAtNoon, new TimeSpan(0, 60, 0)));
            result.Add(new ProcessorRun("Short Mini", todayAtNoon, new TimeSpan(0, 30, 0)));                        
            return result;
        }
    }
}
