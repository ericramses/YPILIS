using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.InformalConsult
{
	[PersistentClass("tblInformalConsultTestOrder", "tblPanelSetOrder", "YPIDATA")]
	public class InformalConsultTestOrder : PanelSetOrder
	{
        private string m_Comment;

		public InformalConsultTestOrder()
		{
            
		}

		public InformalConsultTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
			bool distribute)
            : base(masterAccessionNo, reportNo, objectId, panelSet, distribute)
		{
            
		}


        [PersistentProperty()]
        public string Comment
        {
            get { return this.m_Comment; }
            set
            {
                if (this.m_Comment != value)
                {
                    this.m_Comment = value;
                    this.NotifyPropertyChanged("Comment");
                }
            }
        }

        public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			return this.m_Comment;
		}
	}
}
