using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Domain
{
	public partial class OrderableTestIcd9Code
	{
		#region Fields
		private int m_OrderableTestIcd9CodeId;
		private int m_OrderableTestId;
		private string m_Icd9Code;
		#endregion

		#region Properties
		public int OrderableTestIcd9CodeId
		{
			get { return this.m_OrderableTestIcd9CodeId; }
			set
			{
				if(this.m_OrderableTestIcd9CodeId != value)
				{
					this.m_OrderableTestIcd9CodeId = value;
					this.NotifyPropertyChanged("OrderableTestIcd9CodeId");
				}
			}
		}

		public int OrderableTestId
		{
			get { return this.m_OrderableTestId; }
			set
			{
				if(this.m_OrderableTestId != value)
				{
					this.m_OrderableTestId = value;
					this.NotifyPropertyChanged("OrderableTestId");
				}
			}
		}

		public string Icd9Code
		{
			get { return this.m_Icd9Code; }
			set
			{
				if(this.m_Icd9Code != value)
				{
					this.m_Icd9Code = value;
					this.NotifyPropertyChanged("Icd9Code");
				}
			}
		}

		#endregion
	}
}
