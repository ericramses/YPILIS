using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
	public class TypingCptCodeList : ObservableCollection<TypingCptCodeListItem>
	{
		public TypingCptCodeList()
		{
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("85060", null)));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("85097", null)));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("88300", null)));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("88302", null)));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("88304", null)));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("88305", null)));   
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("88305", "26")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("88307", null)));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("88309", null)));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("88104", null)));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("88112", null)));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("88160", null)));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("88161", null)));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("88172", null)));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("88173", null)));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("88177", null)));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("88311", null)));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("88321", null)));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("88323", null)));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("88325", null)));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("88329", null)));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("88331", null)));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("88332", null)));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("88333", null)));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("88334", null)));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("88342", null)));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("88363", null)));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("99000", null)));
        }
    }
}
