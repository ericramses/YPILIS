/*
 * Created by SharpDevelop.
 * User: william.copland
 * Date: 1/6/2016
 * Time: 9:11 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace YellowstonePathology.UI.Test
{
	/// <summary>
	/// Description of BCellEnumerationResultPath.
	/// </summary>
	public class BCellEnumerationResultPath : ResultPath
	{
		BCellEnumerationResultPage m_ResultPage;
		YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		YellowstonePathology.Business.Test.BCellEnumeration.BCellEnumerationTestOrder m_PanelSetOrder;
		private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;

		public BCellEnumerationResultPath(string reportNo,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker,
			YellowstonePathology.UI.Navigation.PageNavigator pageNavigator)
            : base(pageNavigator)
        {
			this.m_AccessionOrder = accessionOrder;
			this.m_PanelSetOrder = (YellowstonePathology.Business.Test.BCellEnumeration.BCellEnumerationTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
			this.m_ObjectTracker = objectTracker;
		}

        protected override void ShowResultPage()
		{
			this.m_ResultPage = new BCellEnumerationResultPage(this.m_PanelSetOrder, this.m_AccessionOrder, this.m_ObjectTracker, this.m_SystemIdentity);
			this.m_ResultPage.Next += new BCellEnumerationResultPage.NextEventHandler(ResultPage_Next);
			this.m_PageNavigator.Navigate(this.m_ResultPage);
		}

		private void ResultPage_Next(object sender, EventArgs e)
		{
			this.Finished();
		}
	}
}
