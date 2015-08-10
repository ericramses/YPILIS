using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace YellowstonePathology.Business.View
{
	[XmlType("ClientLocationViewCollection")]
	public class ClientLocationViewCollection : ObservableCollection<ClientLocationView>
	{
		public ClientLocationViewCollection()
		{
            
		}

        public void AddFavorites()
        {
            YellowstonePathology.Business.View.ClientLocationView svhSurgery = new Business.View.ClientLocationView(558, 694, "St. Vincent Healthcare", "Surgery");
            this.Add(svhSurgery);

            YellowstonePathology.Business.View.ClientLocationView svhGICenter = new Business.View.ClientLocationView(558, 695, "St. Vincent Healthcare", "GI Center");
            this.Add(svhGICenter);

            YellowstonePathology.Business.View.ClientLocationView bigSkyDerm = new Business.View.ClientLocationView(1203, 580, "Big Sky Dermatology", "Medical Records");
            this.Add(bigSkyDerm);

            YellowstonePathology.Business.View.ClientLocationView cmmc = new Business.View.ClientLocationView(219, 84, "CMMC - Laboratory", "Medical Records");
            this.Add(cmmc);

            YellowstonePathology.Business.View.ClientLocationView ysc = new Business.View.ClientLocationView(660, 276, "Yellowstone Surgery Center", "Medical Records");
            this.Add(ysc);

            YellowstonePathology.Business.View.ClientLocationView hrh = new Business.View.ClientLocationView(723, 281, "Holy Rosary Healthcare Lab", "Medical Records");
            this.Add(hrh);

            YellowstonePathology.Business.View.ClientLocationView yd = new Business.View.ClientLocationView(14, 2, "Yellowstone Dermatology", "Medical Records");
            this.Add(yd);

            YellowstonePathology.Business.View.ClientLocationView yellowstoneUrology = new Business.View.ClientLocationView(184, 54, "St. Vincent Healthcare Urology", "Medical Records");
            this.Add(yellowstoneUrology);

            YellowstonePathology.Business.View.ClientLocationView westParkHospital = new Business.View.ClientLocationView(553, 205, "West Park Hospital", "Medical Records");
            this.Add(westParkHospital);

            YellowstonePathology.Business.View.ClientLocationView billingsObgyn = new Business.View.ClientLocationView(54, 8, "Billings OB/GYN Associates", "Medical Records");
            this.Add(billingsObgyn);

            YellowstonePathology.Business.View.ClientLocationView svhRadiology = new Business.View.ClientLocationView(558, 699, "St. Vincent Radiology", "Radiology");
            this.Add(svhRadiology);

            YellowstonePathology.Business.View.ClientLocationView yellowstoneBreastCenter = new Business.View.ClientLocationView(126, 20, "Yellowstone Breast Center", "Medical Records");
            this.Add(yellowstoneBreastCenter);

            YellowstonePathology.Business.View.ClientLocationView svhDermatology = new Business.View.ClientLocationView(1321, 686, "Montana Dermatology", "Medical Records");
            this.Add(svhDermatology);

            YellowstonePathology.Business.View.ClientLocationView stJames = new Business.View.ClientLocationView(278, 136, "St. James Healthcare - Pathology", "Medical Records");
            this.Add(stJames);

            YellowstonePathology.Business.View.ClientLocationView tallman = new Business.View.ClientLocationView(579, 223, "Tallman Dermatology", "Medical Records");
            this.Add(tallman);
        }

		public void AddRecent(YellowstonePathology.Business.Client.Model.Client client)
        {
            if (this.Count <= 20) // Limit list to 20
            {
                bool exists = false;
                foreach (ClientLocationView item in this)
                {
                    if (item.ClientId == client.ClientId)
                    {
                        exists = true;
                        break;
                    }
                }
                if (exists == false)
                {
                    ClientLocationView clientLocationView = new ClientLocationView(client.ClientId, 0, client.ClientName, "Medical Records");
                    this.Add(clientLocationView);
                }
            }
        }
	}
}
