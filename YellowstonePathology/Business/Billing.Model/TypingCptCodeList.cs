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
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("CPT85060")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("CPT85097")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("CPT88300")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("CPT88302")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("CPT88304")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("CPT88305")));   
            
            CptCode cpt88305 = CptCodeCollection.Instance.GetCPTCodeById("CPT88305");
            CptCode po88305 = CptCode.Clone(cpt88305);
            po88305.Modifier = "26";
            this.Add(new TypingCptCodeListItem(po88305));

            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("CPT88307")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("CPT88309")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("CPT88104")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("CPT88112")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("CPT88160")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("CPT88161")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("CPT88172")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("CPT88173")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("CPT88177")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("CPT88311")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("CPT88321")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("CPT88323")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("CPT88325")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("CPT88329")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("CPT88331")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("CPT88332")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("CPT88333")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("CPT88334")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("CPT88342")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("CPT88363")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCPTCodeById("CPT99000")));
        }
    }
}
