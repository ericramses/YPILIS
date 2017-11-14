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
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("cpt:85060")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("cpt:85097")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("cpt:88300")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("cpt:88302")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("cpt:88304")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("cpt:88305")));   
            
            CptCode cpt88305 = CptCodeCollection.Instance.GetCPTCodeById("cpt:88305");
            CptCode po88305 = CptCode.Clone(cpt88305);
            po88305.Modifier = "26";
            this.Add(new TypingCptCodeListItem(po88305));

            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("cpt:88307")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("cpt:88309")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("cpt:88104")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("cpt:88112")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("cpt:88160")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("cpt:88161")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("cpt:88172")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("cpt:88173")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("cpt:88177")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("cpt:88311")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("cpt:88321")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("cpt:88323")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("cpt:88325")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("cpt:88329")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("cpt:88331")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("cpt:88332")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("cpt:88333")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("cpt:88334")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("cpt:88342")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("cpt:88363")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("cpt:99000")));
        }
    }
}
