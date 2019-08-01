﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.TechnicalOnly
{
	public class TechnicalOnlyTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
		public TechnicalOnlyTest()
		{
			this.m_PanelSetId = 31;
			this.m_PanelSetName = "Technical Only";
            this.m_Abbreviation = "TCHONLY";
            this.m_CaseType = YellowstonePathology.Business.CaseType.Technical;
			this.m_HasTechnicalComponent = true;			
			this.m_HasProfessionalComponent = false;
			this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.PublishedDocument;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterT();
            this.m_Active = true;
            this.m_ExpectedDuration = new TimeSpan(7, 0, 0, 0);

            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.TechnicalOnly.TechnicalOnlyTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.ReferenceLabReport).AssemblyQualifiedName;
			this.m_AllowMultiplePerAccession = true;
            this.m_NeverDistribute = true;            
			this.m_AcceptOnFinal = true;
            this.m_HasNoOrderTarget = true;
            this.m_MonitorPriority = MonitorPriorityNormal;

            this.m_TechnicalComponentFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");            
            this.m_TechnicalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());
		}
	}
}
