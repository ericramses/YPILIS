using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace YellowstonePathology.Business.Test.HER2AmplificationByISH
{
    public class HER2AmplificationResult : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected HER2AmplificationByISHTestOrder m_HER2AmplificationByISHTestOrder;
        protected Her2AmplificationByIHC.PanelSetOrderHer2AmplificationByIHC m_PanelSetOrderHer2AmplificationByIHC;
        protected string m_Interpretation;
        protected HER2AmplificationResultEnum m_Result;

        private bool m_RequiresBlindedObserver;

        public HER2AmplificationResult(PanelSetOrderCollection panelSetOrderCollection)
        {
            Her2AmplificationByIHC.Her2AmplificationByIHCTest her2AmplificationByIHCTest = new Her2AmplificationByIHC.Her2AmplificationByIHCTest();
            HER2AmplificationByISH.HER2AmplificationByISHTest her2AmplificationByISHTest = new HER2AmplificationByISH.HER2AmplificationByISHTest();

            if(panelSetOrderCollection.Exists(her2AmplificationByISHTest.PanelSetId) == true)
            {
                this.m_HER2AmplificationByISHTestOrder = (HER2AmplificationByISH.HER2AmplificationByISHTestOrder)panelSetOrderCollection.GetPanelSetOrder(her2AmplificationByISHTest.PanelSetId);
            }

            if (panelSetOrderCollection.Exists(her2AmplificationByIHCTest.PanelSetId) == true)
            {
                this.m_PanelSetOrderHer2AmplificationByIHC = (Her2AmplificationByIHC.PanelSetOrderHer2AmplificationByIHC)panelSetOrderCollection.GetPanelSetOrder(her2AmplificationByIHCTest.PanelSetId);
            }
        }

        public string Interpretation
        {
            get { return m_Interpretation; }
            set
            {
                if (this.m_Interpretation != value)
                {
                    this.m_Interpretation = value;
                    NotifyPropertyChanged("Interpretation");
                }
            }
        }

        public HER2AmplificationResultEnum Result
        {
            get { return m_Result; }
            set
            {
                if (this.m_Result != value)
                {
                    this.m_Result = value;
                    NotifyPropertyChanged("Result");
                }
            }
        }

        public bool RequiresBlindedObserver
        {
            get { return m_RequiresBlindedObserver; }
            set
            {
                if (this.m_RequiresBlindedObserver != value)
                {
                    this.m_RequiresBlindedObserver = value;
                    NotifyPropertyChanged("RequiresBlindedObserver");
                }
            }
        }

        public virtual bool IsAMatch()
        {
            return false;
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public void HandleIHC()
        {
            if (this.m_PanelSetOrderHer2AmplificationByIHC != null && this.m_PanelSetOrderHer2AmplificationByIHC.Accepted == true)
            {
                if (this.m_PanelSetOrderHer2AmplificationByIHC.Score.Contains("0") || this.m_PanelSetOrderHer2AmplificationByIHC.Score.Contains("1+"))
                {
                    this.m_Result = HER2AmplificationResultEnum.Negative;
                }
                else if (this.m_PanelSetOrderHer2AmplificationByIHC.Score.Contains("2+"))
                {
                    this.m_RequiresBlindedObserver = true;
                }
                else if (this.m_PanelSetOrderHer2AmplificationByIHC.Score.Contains("3+"))
                {
                    this.m_Result = HER2AmplificationResultEnum.Positive;
                }
            }
        }
        public bool IsRecountNeeded()
        {
            bool result = false;
            if (this.m_RequiresBlindedObserver == true && this.m_HER2AmplificationByISHTestOrder.NumberOfObservers < 3) result = true;
            return result;
        }
    }
}
