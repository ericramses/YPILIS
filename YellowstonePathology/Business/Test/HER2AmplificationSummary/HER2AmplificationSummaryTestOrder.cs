using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.HER2AmplificationSummary
{
    [PersistentClass("tblHER2AmplificationSummaryTestOrder", "tblPanelSetOrder", "YPIDATA")]
    public class HER2AmplificationSummaryTestOrder : PanelSetOrder
    {
        private string m_Result;
        /*private int m_CellsCounted;
        private int m_NumberOfObservers;
        private int m_TotalChr17SignalsCounted;
        private int m_TotalHer2SignalsCounted;*/

        public HER2AmplificationSummaryTestOrder()
        { }

        public HER2AmplificationSummaryTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
            bool distribute)
            : base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
        { }

        [PersistentProperty()]
        public string Result
        {
            get { return this.m_Result; }
            set
            {
                if (this.m_Result != value)
                {
                    this.m_Result = value;
                    this.NotifyPropertyChanged("Result");
                }
            }
        }

        /*[PersistentProperty()]
        public int CellsCounted
        {
            get { return this.m_CellsCounted; }
            set
            {
                if (this.m_CellsCounted != value)
                {
                    this.m_CellsCounted = value;
                    this.NotifyPropertyChanged("CellsCounted");
                }
            }
        }

        [PersistentProperty()]
        public int NumberOfObservers
        {
            get { return this.m_NumberOfObservers; }
            set
            {
                if (this.m_NumberOfObservers != value)
                {
                    this.m_NumberOfObservers = value;
                    this.NotifyPropertyChanged("NumberOfObservers");
                }
            }
        }

        [PersistentProperty()]
        public int TotalChr17SignalsCounted
        {
            get { return this.m_TotalChr17SignalsCounted; }
            set
            {
                if (this.m_TotalChr17SignalsCounted != value)
                {
                    this.m_TotalChr17SignalsCounted = value;
                    this.NotifyPropertyChanged("TotalChr17SignalsCounted");
                }
            }
        }

        [PersistentProperty()]
        public int TotalHer2SignalsCounted
        {
            get { return this.m_TotalHer2SignalsCounted; }
            set
            {
                if (this.m_TotalHer2SignalsCounted != value)
                {
                    this.m_TotalHer2SignalsCounted = value;
                    this.NotifyPropertyChanged("TotalHer2SignalsCounted");
                }
            }
        }*/
    }
}
