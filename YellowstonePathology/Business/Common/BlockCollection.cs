﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.IO;

namespace YellowstonePathology.Business.Common
{
	public class BlockCollection : ObservableCollection<YellowstonePathology.Business.Common.Block>
	{
		public BlockCollection()
        {

        }

		public void FromAliquotOrderItemCollection(YellowstonePathology.Business.Test.AliquotOrderCollection aliquotOrderCollection, string reportNo, string PatientInitials, string cassetteColor, bool printRequested)
		{
			foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in aliquotOrderCollection)
			{
				if (aliquotOrder.IsBlock())
				{
					this.Add(aliquotOrder, reportNo, PatientInitials, cassetteColor);
				}
			}

			if (printRequested == true)
			{
				this.SetPrintRequested(true);
			}
		}		

		private void SetPrintRequested(bool requested)
		{
			foreach (Block block in this)
			{
				block.PrintRequested = requested;
			}
		}

		private void Add(YellowstonePathology.Business.Test.AliquotOrder aliquotOrder, string reportNo, string patientInitials, string cassetColor)
		{
			YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(reportNo);
			Block block = null;
			if (orderIdParser.IsLegacyReportNo == true)
			{
				block = new BlockV1();
				block.ReportNo = reportNo;
			}
			else
			{
				block = new BlockV2();
				block.MasterAccessionNo = orderIdParser.MasterAccessionNo;
			}
			block.BlockId = aliquotOrder.AliquotOrderId;
			block.BlockTitle = aliquotOrder.PrintLabel;
			block.Verified = aliquotOrder.GrossVerified;
			block.PatientInitials = patientInitials;
            block.CassetteColor = cassetColor;
			this.Add(block);
		}

        public void SetCassetteColor(string cassetteColor)
        {            
            foreach (YellowstonePathology.Business.Common.Block block in this)
            {
                block.CassetteColor = cassetteColor;
            }
        }

		public void SetVerified(string blockId)
		{
			foreach (Block block in this)
			{
				if (block.BlockId == blockId)
				{
					block.Verified = true;
					break;
				}
			}
		}

        public void AddSlides(YellowstonePathology.Business.Slide.Model.SlideOrderCollection slideOrderCollection)
        {
            foreach (YellowstonePathology.Business.Common.Block block in this)
            {
                //block.AddSlides(slideOrderCollection);
            }
        }
	}
}
