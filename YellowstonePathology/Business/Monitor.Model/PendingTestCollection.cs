using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Monitor.Model
{
    public class PendingTestCollection : ObservableCollection<PendingTest>
    {
        public PendingTestCollection()
        {

        }

        public PendingTestCollection SortByDifference()
        {
            PendingTestCollection result = new PendingTestCollection();
            List<PendingTest> sortedList = this.OrderBy(x => x.State).ThenBy(x => x.Difference.TotalHours).ToList();
            foreach (PendingTest pendingTest in sortedList)
            {
                result.Add(pendingTest);
            }
            return result;
        }

        public void SetState()
        {
            foreach (PendingTest test in this)
            {
                test.SetState();
            }
        }

        public PendingTestCollection GetCriticalTestsForMonitorPriority(string monitorPriority)
        {
            PendingTestCollection result = new Model.PendingTestCollection();
            YellowstonePathology.Business.PanelSet.Model.PanelSetCollection pendingCollection = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetMonitorPriorityTests(monitorPriority);
            foreach (PendingTest pendingTest in this)
            {
                if (pendingTest.State == MonitorStateEnum.Critical)
                {
                    if (pendingCollection.Exists(pendingTest.PanelSetId) == true)
                    {
                        result.Add(pendingTest);
                    }
                }
            }
            return result;
        }
    }
}
