using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.HPV1618
{
	[PersistentClass("tblPanelOrderHPV1618", "tblPanelOrder", "YPIDATA")]
	public class PanelOrderHPV1618 : PanelOrder
	{
		private string m_HPV16Result;
		private string m_HPV18Result;		

		public PanelOrderHPV1618()
		{
		}

		public PanelOrderHPV1618(string reportNo, string objectId, string panelOrderId, YellowstonePathology.Business.Panel.Model.Panel panel, int orderedById)
			: base(reportNo, objectId, panelOrderId, panel, orderedById)
		{

		}				

		[PersistentProperty()]
		public string HPV16Result
		{
			get { return this.m_HPV16Result; }
			set
			{
				if (this.m_HPV16Result != value)
				{
					this.m_HPV16Result = value;
					this.NotifyPropertyChanged("HPV16Result");
				}
			}
		}

		[PersistentProperty()]
		public string HPV18Result
		{
			get { return this.m_HPV18Result; }
			set
			{
				if (this.m_HPV18Result != value)
				{
					this.m_HPV18Result = value;
					this.NotifyPropertyChanged("HPV18Result");
				}
			}
		}		
	}
}
