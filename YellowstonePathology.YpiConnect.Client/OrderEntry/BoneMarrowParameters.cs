using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.YpiConnect.Client.OrderEntry
{
	public class BoneMarrowParameters
	{
		private bool m_HavePeripheralBlood;
		private bool m_HaveBoneMarrowCore;
		private bool m_HaveBoneMarrowAspirate;
		private string m_BoneMarrowCoreContainerId;
		private string m_BoneMarrowAspirateContainerId;
		private string m_PeripheralBloodContainerId;
		private int m_TotalPeripheralSmearSlides;
		private int m_TotalAspirateSlides;

		private List<string> m_ScanComments;

		public BoneMarrowParameters()
		{
			this.m_ScanComments = new List<string>();
		}

		public bool HavePeripheralBlood
		{
			get { return this.m_HavePeripheralBlood; }
			set { this.m_HavePeripheralBlood = value; }
		}

		public bool HaveBoneMarrowCore
		{
			get { return this.m_HaveBoneMarrowCore; }
			set { this.m_HaveBoneMarrowCore = value; }
		}

		public bool HaveBoneMarrowAspirate
		{
			get { return this.m_HaveBoneMarrowAspirate; }
			set { this.m_HaveBoneMarrowAspirate = value; }
		}

		public string BoneMarrowCoreContainerId
		{
			get { return this.m_BoneMarrowCoreContainerId; }
			set { this.m_BoneMarrowCoreContainerId = value; }
		}

		public string BoneMarrowAspirateContainerId
		{
			get { return this.m_BoneMarrowAspirateContainerId; }
			set { this.m_BoneMarrowAspirateContainerId = value; }
		}

		public string PeripheralBloodContainerId
		{
			get { return this.m_PeripheralBloodContainerId; }
			set { this.m_PeripheralBloodContainerId = value; }
		}

		public string PeripheralBloodDetailDescription
		{
			get { return "Peripheral Blood"; }
		}

		public string BoneMarrowCoreDetailDescription
		{
			get { return "Bone Marrow Core"; }
		}

		public string BoneMarrowAspirateDetailDescription
		{
			get { return "Bone Marrow Aspirate"; }
		}

		public string PeripheralBloodScanComment
		{
			get { return "Please scan the peripheral blood"; }
		}

		public string BoneMarrowCoreScanComment
		{
			get { return "Please scan the bone marrow core"; }
		}

		public string BoneMarrowAspirateScanComment
		{
			get { return "Please scan the aspirate"; }
		}

		public void SetBoneMarrowAspirateContainerId(string containerId)
		{
			this.m_BoneMarrowAspirateContainerId = containerId;
		}

		public int TotalPeripheralSmearSlides
		{
			get { return this.m_TotalPeripheralSmearSlides; }
			set { this.m_TotalPeripheralSmearSlides = value; }
		}

		public int TotalAspirateSlides
		{
			get { return this.m_TotalAspirateSlides; }
			set { this.m_TotalAspirateSlides = value; }
		}

		public bool NeedsPeripheralBloodScan
		{
			get
			{
				bool result = false;
				if (this.HavePeripheralBlood && string.IsNullOrEmpty(this.PeripheralBloodContainerId) == true)
				{
					result = true;
				}
				return result;
			}
		}

		public bool NeedsBoneMarrowCoreScan
		{
			get
			{
				bool result = false;
				if (this.HaveBoneMarrowCore && string.IsNullOrEmpty(this.BoneMarrowCoreContainerId) == true)
				{
					result = true;
				}
				return result;
			}
		}

		public bool NeedsBoneMarrowAspirateScan
		{
			get
			{
				bool result = false;
				if (this.HaveBoneMarrowAspirate && string.IsNullOrEmpty(this.BoneMarrowAspirateContainerId) == true)
				{
					result = true;
				}
				return result;
			}
		}
	}
}
