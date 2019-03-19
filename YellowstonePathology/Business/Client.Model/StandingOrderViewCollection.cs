using System;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Client.Model
{
    public class StandingOrderViewCollection : ObservableCollection<StandingOrderView>
    {
        public StandingOrderViewCollection(StandingOrderViewTypeEnum standingOrderViewType)
        {
            StandingOrderCollection standingOrderCollection = new Model.StandingOrderCollection();
            switch (standingOrderViewType)
            {
                case StandingOrderViewTypeEnum.All:
                    standingOrderCollection = StandingOrderCollection.GetAll();
                    break;
                case StandingOrderViewTypeEnum.HPVOnly:
                    standingOrderCollection = StandingOrderCollection.GetHPVStandingOrders();
                    break;
                case StandingOrderViewTypeEnum.HPV1618Only:
                    standingOrderCollection = StandingOrderCollection.GetHPV1618StandingOrders();
                    break;
            }

            foreach (StandingOrder standingOrder in standingOrderCollection)
            {
                StandingOrderView view = new Model.StandingOrderView(standingOrder);
                this.Add(view);
            }
        }
    }
}
