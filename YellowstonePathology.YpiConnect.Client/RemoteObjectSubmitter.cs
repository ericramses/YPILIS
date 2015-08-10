using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.YpiConnect.Client
{
    public class RemoteObjectSubmitter
    {
		public void Submit(YellowstonePathology.Domain.ClientOrder.Model.ClientOrder clientOrder, YellowstonePathology.Persistence.ObjectTracker objectTracker)
        {
				YellowstonePathology.YpiConnect.Proxy.SubmitterServiceProxy proxy = new Proxy.SubmitterServiceProxy();
				YellowstonePathology.Persistence.SubmissionResult submissionResult = proxy.Submit(clientOrder, objectTracker);

				if (submissionResult.Success == true)
				{
					objectTracker.Deregister(clientOrder);
					objectTracker.RegisterObject(clientOrder);
				}
				else
				{
					System.Windows.MessageBox.Show("There was an error saving the data.");
				}
        }

		public void Submit(YellowstonePathology.Domain.ClientOrder.Model.Shipment shipment, YellowstonePathology.Persistence.ObjectTracker objectTracker)
		{
			YellowstonePathology.YpiConnect.Proxy.SubmitterServiceProxy proxy = new Proxy.SubmitterServiceProxy();
			YellowstonePathology.Persistence.SubmissionResult submissionResult = proxy.Submit(shipment, objectTracker);

			if (submissionResult.Success == false)
			{
				objectTracker.Deregister(shipment);
				objectTracker.RegisterObject(shipment);
			}
			else
			{
				System.Windows.MessageBox.Show("There was an error saving the data.");
			}
		}
	}
}
