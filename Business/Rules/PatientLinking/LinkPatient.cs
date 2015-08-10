using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules.PatientLinking
{
	public class LinkPatient
	{
        protected YellowstonePathology.Business.Rules.Rule m_Rule;
        protected YellowstonePathology.Business.Rules.ExecutionStatus m_ExecutionStatus;
		protected YellowstonePathology.Business.ProcessingModeEnum m_ProcessingMode;
		protected YellowstonePathology.Business.DataContext.YpiDataBase m_DataContext;
		protected YellowstonePathology.Business.Repository.CytologyRepository m_Repository;
		protected YellowstonePathology.Business.Interface.IOrder m_IOrder;

		protected YellowstonePathology.Business.PatientObjects.PatientLinkingList m_PatientLinkingList;
		protected string m_PatientId;

		public LinkPatient(YellowstonePathology.Business.DataContext.YpiDataBase dataContext)
		{
			this.m_DataContext = dataContext;
			this.m_Repository = new YellowstonePathology.Business.Repository.CytologyRepository(m_DataContext);
            this.m_Rule = new YellowstonePathology.Business.Rules.Rule();

			this.m_Rule.ActionList.Add(this.DetermineId);
			this.m_Rule.ActionList.Add(this.LinkSelectedPatients);
			this.m_Rule.ActionList.Add(this.Save);
		}

		private void DetermineId()
		{
			bool needNewId = false;
			m_PatientId = string.Empty;
			foreach (PatientObjects.PatientLinkingListItem item in m_PatientLinkingList)
			{
				if (item.IsSelected == true && item.PatientId != null && item.PatientId.Length > 0 && item.PatientId != "0")
				{
					m_PatientId = item.PatientId;
					break;
				}
			}

			if (m_PatientId.Length == 0 || m_PatientId == "0" || Char.IsLetter(m_PatientId[0]))
			{
				needNewId = true;
			}
			else
			{
				foreach (YellowstonePathology.Business.PatientObjects.PatientLinkingListItem item in m_PatientLinkingList)
				{
					if (item.IsSelected == true && item.PatientId != null && item.PatientId != m_PatientId && item.PatientId != "0")
					{
						needNewId = true;
						break;
					}
				}
			}

			if (!needNewId)
			{
				foreach (YellowstonePathology.Business.PatientObjects.PatientLinkingListItem item in m_PatientLinkingList)
				{
					if (item.IsSelected == false && item.PatientId == m_PatientId)
					{
						needNewId = true;
						break;
					}
				}
			}

			if (needNewId)
			{
				YellowstonePathology.Business.DataContext.YpiDataBase dataContext = new YellowstonePathology.Business.DataContext.YpiDataBase(Properties.Settings.Default.CurrentConnectionString);
				YellowstonePathology.Business.Repository.CytologyRepository repository = new YellowstonePathology.Business.Repository.CytologyRepository(dataContext);
				YellowstonePathology.Business.Domain.Patient patient = repository.GetNewPatient();
				m_PatientId = patient.PatientIdString;
			}
		}

		private void LinkSelectedPatients()
		{
			foreach (PatientObjects.PatientLinkingListItem item in m_PatientLinkingList)
			{
				if (item.IsSelected == true && item.PatientId != this.m_PatientId)
				{
					if (item.MasterAccessionNo == this.m_IOrder.MasterAccessionNo)
					{
						this.m_IOrder.PatientId = this.m_PatientId;
					}
					else
					{
						this.UpdatePatientId(item);
					}
				}
			}
		}

		private void Save()
		{
			if (this.m_ProcessingMode == ProcessingModeEnum.Production)
			{
				this.m_DataContext.SubmitChanges();
			}
		}

		private void UpdatePatientId(PatientObjects.PatientLinkingListItem item)
		{
		}

        public void Execute(YellowstonePathology.Business.PatientObjects.PatientLinkingList patientLinkingList, YellowstonePathology.Business.Interface.IOrder iOrder, YellowstonePathology.Business.ProcessingModeEnum processMode, YellowstonePathology.Business.Rules.ExecutionStatus executionStatus)
		{
			this.m_PatientLinkingList = patientLinkingList;
			this.m_IOrder = iOrder;
			this.m_ProcessingMode = processMode;
			this.m_ExecutionStatus = executionStatus;
			this.m_Rule.Execute(executionStatus);
		}
	}
}
