using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.HER2AmplificationRecount
{
    [PersistentClass("tblHER2AmplificationRecountTestOrder", "tblPanelSetOrder", "YPIDATA")]
    public class HER2AmplificationRecountTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
    {
        private string m_CellsCounted;
        private string m_CEP17Counted;
        private string m_HER2Counted;

        public HER2AmplificationRecountTestOrder()
        { }

        public HER2AmplificationRecountTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
            bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
        { }

        [PersistentProperty()]
        public string CellsCounted
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
        public string CEP17Counted
        {
            get { return this.m_CEP17Counted; }
            set
            {
                if (this.m_CEP17Counted != value)
                {
                    this.m_CEP17Counted = value;
                    this.NotifyPropertyChanged("CEP17Counted");
                }
            }
        }

        [PersistentProperty()]
        public string HER2Counted
        {
            get { return this.m_HER2Counted; }
            set
            {
                if (this.m_HER2Counted != value)
                {
                    this.m_HER2Counted = value;
                    this.NotifyPropertyChanged("HER2Counted");
                }
            }
        }

        public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("HER2 Amplification Recount: ");
            result.AppendLine("Cells Counted: " + this.m_CellsCounted);
            result.AppendLine("CEP17 Counted: " + this.m_CEP17Counted);
            result.AppendLine("HER2 Counted: " + this.m_HER2Counted);
            return result.ToString();
        }
    }
}
