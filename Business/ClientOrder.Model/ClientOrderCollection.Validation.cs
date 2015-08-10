using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Shared.ExtensionMethods;

namespace YellowstonePathology.Business.ClientOrder.Model
{
	public partial class ClientOrderCollection
	{        		
		public YellowstonePathology.Shared.ValidationResult IsDomainValid()
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}
	}
}
