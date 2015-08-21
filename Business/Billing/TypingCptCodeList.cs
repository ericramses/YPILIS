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
			this.Add(new TypingCptCodeListItem(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT85060()));
			this.Add(new TypingCptCodeListItem(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT85097()));
			this.Add(new TypingCptCodeListItem(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88300()));
			this.Add(new TypingCptCodeListItem(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88302()));
			this.Add(new TypingCptCodeListItem(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88304()));
			this.Add(new TypingCptCodeListItem(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88305()));
			this.Add(new TypingCptCodeListItem(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88307()));
			this.Add(new TypingCptCodeListItem(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88309()));
			this.Add(new TypingCptCodeListItem(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88104()));
			this.Add(new TypingCptCodeListItem(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88112()));
			this.Add(new TypingCptCodeListItem(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88160()));
			this.Add(new TypingCptCodeListItem(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88161()));
			this.Add(new TypingCptCodeListItem(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88172()));
			this.Add(new TypingCptCodeListItem(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88173()));
			this.Add(new TypingCptCodeListItem(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88177()));
			this.Add(new TypingCptCodeListItem(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88311()));
			this.Add(new TypingCptCodeListItem(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88321()));
			this.Add(new TypingCptCodeListItem(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88323()));
			this.Add(new TypingCptCodeListItem(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88325()));
			this.Add(new TypingCptCodeListItem(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88329()));
			this.Add(new TypingCptCodeListItem(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88331()));
			this.Add(new TypingCptCodeListItem(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88332()));
			this.Add(new TypingCptCodeListItem(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88333()));
			this.Add(new TypingCptCodeListItem(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88334()));
			this.Add(new TypingCptCodeListItem(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88342()));
			this.Add(new TypingCptCodeListItem(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88363()));
			this.Add(new TypingCptCodeListItem(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT99000()));
			//this.Add(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT99360());
			//this.Add(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT00000());
		}
	}
}
