﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Domain.Cytology
{
	public class CytologyReportCommentCollection : ObservableCollection<CytologyReportComment>
	{
		public CytologyReportCommentCollection()
		{
		}
	}
}
