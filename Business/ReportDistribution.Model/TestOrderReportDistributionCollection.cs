using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Data;
using System.Xml.Serialization;

namespace YellowstonePathology.Business.ReportDistribution.Model
{
	public class TestOrderReportDistributionCollection : ObservableCollection<TestOrderReportDistribution>
	{        
        public TestOrderReportDistributionCollection()
        {
            
		}

        private void CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            
        }        

        public void ScheduleDistribution(DateTime timeToSchedule)
        {
            foreach (TestOrderReportDistribution testOrderReportDistribution in this)
            {
                testOrderReportDistribution.Distributed = false;
                testOrderReportDistribution.ScheduledDistributionTime = timeToSchedule;
            }
        }

        public void UnscheduleDistributions()
        {
            foreach (TestOrderReportDistribution testOrderReportDistribution in this)
            {
                testOrderReportDistribution.Distributed = false;
                testOrderReportDistribution.ScheduledDistributionTime = null;
            }
        }

        public void AddNext(string testOrderReportDistributionId, string objectId, string reportNo, int physicianId, string physicianName, int clientId, string clientName, string distributionType)
        {
            this.AddNext(testOrderReportDistributionId, objectId, reportNo, physicianId, physicianName, clientId, clientName, distributionType, null, false);
        }

        public void AddNext(string testOrderReportDistributionId, string objectId, string reportNo, int physicianId, string physicianName, int clientId, string clientName, string distributionType, string faxNumber, bool longDistance)
        {            
            YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution = new TestOrderReportDistribution(testOrderReportDistributionId, objectId, reportNo,
                physicianId, physicianName, clientId, clientName, distributionType, faxNumber, longDistance);
            this.Add(testOrderReportDistribution);
        }        

        public bool Exists(int physicianId, int clientId, string distributionType)
        {
            bool result = false;
            foreach (TestOrderReportDistribution testOrderReportDistribution in this)
            {
                if (testOrderReportDistribution.PhysicianId == physicianId && 
                    testOrderReportDistribution.ClientId == clientId && 
                    testOrderReportDistribution.DistributionType == distributionType)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool Exists(string distributionType)
        {
            bool result = false;
            foreach (TestOrderReportDistribution testOrderReportDistribution in this)
            {
                if (testOrderReportDistribution.DistributionType == distributionType)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool HasDistribution(string distributionType)
        {
            bool result = false;
            foreach (TestOrderReportDistribution testOrderReportDistribution in this)
            {
                if (testOrderReportDistribution.DistributionType == distributionType)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool HasDistributionAfter(DateTime dateTime)
        {
            bool result = false;
            foreach (TestOrderReportDistribution testOrderReportDistribution in this)
            {
                if (testOrderReportDistribution.TimeOfLastDistribution > dateTime)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool HasDistributedItems()
        {
            bool result = false;
            foreach (TestOrderReportDistribution testOrderReportDistribution in this)
            {
                if (testOrderReportDistribution.Distributed == true)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool HasDistributedItemsAfter(DateTime afterDate)
        {
            bool result = false;
            foreach (TestOrderReportDistribution testOrderReportDistribution in this)
            {
                if (testOrderReportDistribution.Distributed == true)
                {
                    if (testOrderReportDistribution.TimeOfLastDistribution >= afterDate)
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

        public bool HasUndistributedItems()
        {
            bool result = false;
            foreach (TestOrderReportDistribution testOrderReportDistribution in this)
            {
                if (testOrderReportDistribution.Distributed == false)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public void MarkAllAsNotDistributed()
        {            
            foreach (TestOrderReportDistribution testOrderReportDistribution in this)
            {
                testOrderReportDistribution.Distributed = false;
            }         
        }

        public void UnscheduleAll()
        {
            foreach (TestOrderReportDistribution testOrderReportDistribution in this)
            {
                testOrderReportDistribution.ScheduledDistributionTime = null;
            }         
        }
	}
}
