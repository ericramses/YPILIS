using System;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Client.Model
{
    public class StandingOrderViewCollection : ObservableCollection<StandingOrderView>
    {
        public StandingOrderViewCollection()
        {
            StandingOrderCollection standingOrderCollection = StandingOrderCollection.GetAll();
            foreach(StandingOrder standingOrder in standingOrderCollection)
            {
                StandingOrderView view = new Model.StandingOrderView(standingOrder);
                this.Add(view);
            }
        }
    }
}
