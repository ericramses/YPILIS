using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.YpiConnect.Client.OrderEntry
{
	public class SpecimenOrderEntryPath
	{
		public delegate void ReturnEventHandler(object sender, Shared.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;

		private YellowstonePathology.Business.ClientOrder.Model.ClientOrder m_ClientOrder;		
		private YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail m_ClientOrderDetail;
		private ClientOrderDetailWizardModeEnum m_ClientOrderDetailWizardMode;
		private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;

		public SpecimenOrderEntryPath(YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder,
			YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail,
			ClientOrderDetailWizardModeEnum clientOrderDetailWizardMode,
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker)
		{			
			this.m_ClientOrder = clientOrder;
			this.m_ClientOrderDetail = clientOrderDetail;
			this.m_ClientOrderDetailWizardMode = clientOrderDetailWizardMode;
			this.m_ObjectTracker = objectTracker;
		}

		public void Start()
		{
			this.StartSpecimenOrderEntryPath();
		}

		private void StartSpecimenOrderEntryPath()
		{
            switch (this.m_ClientOrderDetail.OrderTypeCode)
            {                                
                case "SRGCL":             
                    this.StartSurgicalSpecimenPath();
                    break;                                                    
				case "PLCNT":
					this.StartPlacentaSpecimenPath();
					break;
                default:
                    this.StartGenericSpecimenPath();
                    break;
            }            
		}

		private void StartGenericSpecimenPath()
		{
			GenericSpecimenPath genericSpecimenPath = new GenericSpecimenPath(this.m_ClientOrder, this.m_ClientOrderDetail, this.m_ClientOrderDetailWizardMode);
			genericSpecimenPath.Return += new GenericSpecimenPath.ReturnEventHandler(Page_Return);
			genericSpecimenPath.Start();
		}

		private void StartSurgicalSpecimenPath()
		{
            YellowstonePathology.Business.ClientOrder.Model.SurgicalClientOrder surgicalClientOrder = (YellowstonePathology.Business.ClientOrder.Model.SurgicalClientOrder)this.m_ClientOrder;
			SurgicalSpecimenPath surgicalSpecimenPath = new SurgicalSpecimenPath(surgicalClientOrder, (YellowstonePathology.Business.ClientOrder.Model.SurgicalClientOrderDetail)this.m_ClientOrderDetail, this.m_ClientOrderDetailWizardMode, this.m_ObjectTracker);
			surgicalSpecimenPath.Return += new SurgicalSpecimenPath.ReturnEventHandler(Page_Return);
			surgicalSpecimenPath.Start();
		}

		private void StartPlacentaSpecimenPath()
		{
			YellowstonePathology.Business.ClientOrder.Model.SurgicalClientOrder surgicalClientOrder = (YellowstonePathology.Business.ClientOrder.Model.SurgicalClientOrder)this.m_ClientOrder;
			PlacentaSpecimenPath placentaSpecimenPath = new PlacentaSpecimenPath(surgicalClientOrder, (YellowstonePathology.Business.ClientOrder.Model.PlacentaClientOrderDetail)this.m_ClientOrderDetail, this.m_ClientOrderDetailWizardMode, this.m_ObjectTracker);
			placentaSpecimenPath.Return += new PlacentaSpecimenPath.ReturnEventHandler(Page_Return);
			placentaSpecimenPath.Start();
		}

		//private void StartThinPrepPapSpecimenPath()
		//{
		//	YellowstonePathology.Business.ClientOrder.Model.CytologyClientOrder cytologyClientOrder = (YellowstonePathology.Business.ClientOrder.Model.CytologyClientOrder)this.m_ClientOrder;
		//	ThinPrepPapSpecimenPath thinPrepPapSpecimenPath = new ThinPrepPapSpecimenPath(cytologyClientOrder, this.m_ClientOrderDetail, this.m_ClientOrderDetailWizardMode);
		//	thinPrepPapSpecimenPath.Return += new ThinPrepPapSpecimenPath.ReturnEventHandler(Page_Return);
		//	thinPrepPapSpecimenPath.Start();
		//}

		private void Page_Return(object sender, Shared.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case Shared.PageNavigationDirectionEnum.Back:
					Return(this, e);
					break;
				case Shared.PageNavigationDirectionEnum.Next:
					Return(this, e);
					break;
			}
		}
	}
}
