using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace YellowstonePathology.YpiConnect.Contract.Flow
{
    [DataContract]
    public class FlowAccessionSubmitter
    {
		private YellowstonePathology.YpiConnect.Contract.Flow.FlowAccessionCollection m_FlowAccessionCollection;
		private Collection<YellowstonePathology.YpiConnect.Contract.Flow.FlowAccession> m_FlowAccessionCollectionChanges;
		private Collection<YellowstonePathology.YpiConnect.Contract.Flow.FlowLeukemia> m_FlowLeukemiaCollectionChanges;
		private Collection<YellowstonePathology.YpiConnect.Contract.Flow.FlowMarker> m_FlowMarkerCollectionChanges;
		private Collection<Domain.PanelSetOrder> m_PanelSetOrderCollectionChanges;

		private YellowstonePathology.Business.Domain.Persistence.PropertyReaderFilterEnum m_PropertyReaderFilterEnum;
        private SubmitterModeEnum m_Mode;

		public FlowAccessionSubmitter()
		{
            this.m_Mode = SubmitterModeEnum.BeginEnd;
		}

        [DataMember]
		public Collection<YellowstonePathology.YpiConnect.Contract.Flow.FlowAccession> FlowAccessionCollectionChanges
        {
			get { return this.m_FlowAccessionCollectionChanges; }
			set { this.m_FlowAccessionCollectionChanges = value; }
        }

		[DataMember]
		public Collection<YellowstonePathology.YpiConnect.Contract.Domain.PanelSetOrder> PanelSetOrderCollectionChanges
		{
			get { return this.m_PanelSetOrderCollectionChanges; }
			set { this.m_PanelSetOrderCollectionChanges = value; }
		}

		[DataMember]
		public Collection<YellowstonePathology.YpiConnect.Contract.Flow.FlowLeukemia> FlowLeukemiaCollectionChanges
		{
			get { return this.m_FlowLeukemiaCollectionChanges; }
			set { this.m_FlowLeukemiaCollectionChanges = value; }
		}

		[DataMember]
		public Collection<YellowstonePathology.YpiConnect.Contract.Flow.FlowMarker> FlowMarkerCollectionChanges
		{
			get { return this.m_FlowMarkerCollectionChanges; }
			set { this.m_FlowMarkerCollectionChanges = value; }
		}

		[DataMember]
		public YellowstonePathology.Business.Domain.Persistence.PropertyReaderFilterEnum PropertyReaderFilterEnum
		{
			get { return this.m_PropertyReaderFilterEnum; }
			set { this.m_PropertyReaderFilterEnum = value; }
		}

		[DataMember]
		public SubmitterModeEnum Mode
		{
			get { return this.m_Mode; }
			set { this.m_Mode = value; }
		}

        public bool HasChanges()
        {
            bool result = false;
			if (this.m_FlowAccessionCollectionChanges.Count > 0) result = true;
			if (this.m_PanelSetOrderCollectionChanges.Count > 0) result = true;
			if (this.m_FlowLeukemiaCollectionChanges.Count > 0) result = true;
			if (this.m_FlowMarkerCollectionChanges.Count > 0) result = true;
			return result;
        }

        private void SetChanges()
        {
			this.SetDeletes();
			this.m_FlowAccessionCollection.AddChanged(this.m_FlowAccessionCollectionChanges);
			foreach (FlowAccession flowAccession in this.m_FlowAccessionCollection)
			{
				flowAccession.PanelSetOrderCollection.AddChanged(this.m_PanelSetOrderCollectionChanges);
				foreach(Domain.PanelSetOrder panelSetOrder in flowAccession.PanelSetOrderCollection)
				{
					Domain.PanelSetOrderLeukemiaLymphoma panelSetOrderLeukemiaLymphoma = (Domain.PanelSetOrderLeukemiaLymphoma)panelSetOrder;
					panelSetOrderLeukemiaLymphoma.FlowMarkerCollection.AddChanged(this.m_FlowMarkerCollectionChanges);
					if (panelSetOrderLeukemiaLymphoma.HasChanges())
					{
						FlowLeukemia flowLeukemia = new FlowLeukemia(panelSetOrderLeukemiaLymphoma);
						this.m_FlowLeukemiaCollectionChanges.Add(flowLeukemia);
					}
				}
			}
        }

		private void SetDeletes()
		{
			foreach (FlowAccession flowAccession in this.m_FlowAccessionCollection.DeletedItems)
			{
				flowAccession.PanelSetOrderCollection.AddDeleted(this.m_PanelSetOrderCollectionChanges);
				foreach (Domain.PanelSetOrder panelSetOrder in flowAccession.PanelSetOrderCollection)
				{
					((Domain.PanelSetOrderLeukemiaLymphoma)panelSetOrder).FlowMarkerCollection.AddDeleted(this.m_FlowMarkerCollectionChanges);
				}
			}
		}

		public void BeginSubmit(YellowstonePathology.YpiConnect.Contract.Flow.FlowAccessionCollection flowAccessionCollection, YellowstonePathology.Business.Domain.Persistence.PropertyReaderFilterEnum propertyReaderFilterEnum)
		{
			this.m_FlowAccessionCollection = flowAccessionCollection;
			this.m_PropertyReaderFilterEnum = propertyReaderFilterEnum;

			this.m_FlowAccessionCollectionChanges = new Collection<FlowAccession>();
			this.m_PanelSetOrderCollectionChanges = new Collection<Domain.PanelSetOrder>();
			this.m_FlowLeukemiaCollectionChanges = new Collection<FlowLeukemia>();
			this.m_FlowMarkerCollectionChanges = new Collection<FlowMarker>();
			this.SetChanges();
		}

		public void Submit(YellowstonePathology.YpiConnect.Contract.Flow.IFlowAccessionGateway flowAccessionGateway)
        {
			flowAccessionGateway.SubmitChanges(this.m_FlowAccessionCollectionChanges, this.m_PropertyReaderFilterEnum);
			flowAccessionGateway.SubmitChanges(this.m_PanelSetOrderCollectionChanges, this.m_PropertyReaderFilterEnum);
			flowAccessionGateway.SubmitChanges(this.m_FlowLeukemiaCollectionChanges, this.m_PropertyReaderFilterEnum);
			flowAccessionGateway.SubmitChanges(this.m_FlowMarkerCollectionChanges, this.m_PropertyReaderFilterEnum);
			if (this.Mode == SubmitterModeEnum.Normal)
            {
                this.ResetTracking();
            }
        }

		public void EndSubmit()
		{
			this.ResetTracking();
		}

		public void ResetTracking()
		{
			this.m_FlowAccessionCollection.Reset(YellowstonePathology.Business.Domain.Persistence.CollectionTrackingResetModeEnum.KeyPropertyDataPresentAfterInsert);
			this.m_FlowAccessionCollection[0].PanelSetOrderCollection.Reset(YellowstonePathology.Business.Domain.Persistence.CollectionTrackingResetModeEnum.KeyPropertyDataPresentAfterInsert);
			foreach (FlowAccession flowAccession in this.m_FlowAccessionCollection)
			{
				foreach (Domain.PanelSetOrder panelSetOrder in flowAccession.PanelSetOrderCollection)
				{
					((Domain.PanelSetOrderLeukemiaLymphoma)panelSetOrder).SetOriginalValues();
					((Domain.PanelSetOrderLeukemiaLymphoma)panelSetOrder).FlowMarkerCollection.Reset(YellowstonePathology.Business.Domain.Persistence.CollectionTrackingResetModeEnum.KeyPropertyDataNotPresentAfterInsert);
				}
			}
		}
	}
}
