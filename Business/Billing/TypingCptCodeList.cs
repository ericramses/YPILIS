using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace YellowstonePathology.Business.Billing
{
    public class TypingCptCodeList : ObservableCollection<TypingCptCodeListItem>
    {
        public TypingCptCodeList()
        {            
            this.Add(new TypingCptCodeListItem("85060"));
            this.Add(new TypingCptCodeListItem("85097"));
            this.Add(new TypingCptCodeListItem("88300"));
            this.Add(new TypingCptCodeListItem("88302"));            
            this.Add(new TypingCptCodeListItem("88304"));
            this.Add(new TypingCptCodeListItem("88305"));            
            this.Add(new TypingCptCodeListItem("88307"));
            this.Add(new TypingCptCodeListItem("88309"));
            this.Add(new TypingCptCodeListItem("88104"));
            this.Add(new TypingCptCodeListItem("88112"));
            this.Add(new TypingCptCodeListItem("88160"));
            this.Add(new TypingCptCodeListItem("88161"));
            this.Add(new TypingCptCodeListItem("88172"));
            this.Add(new TypingCptCodeListItem("88173"));
            this.Add(new TypingCptCodeListItem("88177"));
            this.Add(new TypingCptCodeListItem("88311"));
            this.Add(new TypingCptCodeListItem("88321"));
            this.Add(new TypingCptCodeListItem("88323"));
            this.Add(new TypingCptCodeListItem("88325"));
            this.Add(new TypingCptCodeListItem("88329"));
            this.Add(new TypingCptCodeListItem("88331"));
            this.Add(new TypingCptCodeListItem("88332"));
            this.Add(new TypingCptCodeListItem("88333"));
            this.Add(new TypingCptCodeListItem("88334"));
            this.Add(new TypingCptCodeListItem("88342"));
            this.Add(new TypingCptCodeListItem("88363"));            
            this.Add(new TypingCptCodeListItem("99000"));
            this.Add(new TypingCptCodeListItem("99360"));
            this.Add(new TypingCptCodeListItem("00000"));            
        }
    }    
}
