using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.YpiConnect.Client.OrderEntry
{
    public class PhysicianCollectionView : INotifyPropertyChanged
    {
        public delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;

        YellowstonePathology.Business.Client.Model.PhysicianCollection m_Source;        
        ObservableCollection<YellowstonePathology.Business.Client.Model.Physician> m_View;

        public PhysicianCollectionView(YellowstonePathology.Business.Client.Model.PhysicianCollection physicianCollection)
        {
            this.m_Source = physicianCollection;
            this.SortByLastName();
        }

        public void SortByLastName()
        {
            List<YellowstonePathology.Business.Client.Model.Physician> list = 
                this.m_Source.OrderBy(x => x.LastName).ToList<YellowstonePathology.Business.Client.Model.Physician>();
            this.m_View = new ObservableCollection<YellowstonePathology.Business.Client.Model.Physician>(list);
        }

        public void FilterByLastName(string characters)
        {
            var query = from item in this.m_Source where item.LastName.ToUpper().StartsWith(characters.ToUpper()) orderby item.LastName select item;
            this.m_View = new ObservableCollection<YellowstonePathology.Business.Client.Model.Physician>(query.ToList<YellowstonePathology.Business.Client.Model.Physician>());
            this.NotifyPropertyChanged("View");
        }

        public YellowstonePathology.Business.Client.Model.PhysicianCollection Source
        {
            get { return this.m_Source; }            
        }

        public ObservableCollection<YellowstonePathology.Business.Client.Model.Physician> View
        {
            get { return this.m_View; }
        }

        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
