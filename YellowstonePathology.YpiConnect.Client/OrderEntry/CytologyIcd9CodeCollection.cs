using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace YellowstonePathology.YpiConnect.Client.OrderEntry
{
	public class CytologyIcd9CodeCollection : ObservableCollection<CytologyIcd9Code>
	{
		public CytologyIcd9CodeCollection()
		{
			CytologyIcd9Code code1 = new CytologyIcd9Code("V76.2", "Routine Screening Prep");
			this.Add(code1);
			CytologyIcd9Code code2 = new CytologyIcd9Code("V76.47", "Vag Screen, No Cervix");
			this.Add(code2);
			CytologyIcd9Code code3 = new CytologyIcd9Code("V76.49", "Screen Other Sites");
			this.Add(code3);
			CytologyIcd9Code code4 = new CytologyIcd9Code("V15.89", "High Risk Screening Prep");
			this.Add(code4);
			CytologyIcd9Code code5 = new CytologyIcd9Code(string.Empty, "Diagnostic Pap");
			this.Add(code5);

		}
	}
}
