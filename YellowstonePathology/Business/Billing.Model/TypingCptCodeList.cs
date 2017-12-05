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
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("85060")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("85097")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("88300")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("88302")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("88304")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("88305")));   
            
            CptCode cpt88305 = CptCodeCollection.GetCPTCode("88305");
            CptCode po88305 = CptCode.Clone(cpt88305);
            po88305.Modifier = "26";
            this.Add(new TypingCptCodeListItem(po88305));

            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("88307")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("88309")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("88104")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("88112")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("88160")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("88161")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("88172")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("88173")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("88177")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("88311")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("88321")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("88323")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("88325")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("88329")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("88331")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("88332")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("88333")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("88334")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("88342")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("88363")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.GetCPTCode("cpt:99000")));
        }
    }
}
