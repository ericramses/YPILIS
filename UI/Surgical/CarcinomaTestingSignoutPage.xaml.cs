using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YellowstonePathology.UI.Surgical
{
    /// <summary>
    /// Interaction logic for CarcinomaTestingSignoutPage.xaml
    /// </summary>
    public partial class CarcinomaTestingSignoutPage : UserControl, YellowstonePathology.Business.Interface.IPersistPageChanges
    {
        public delegate void NextEventHandler(object sender, EventArgs e);
        public event NextEventHandler Next;

        public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;

        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;
        private YellowstonePathology.Business.Surgical.CarcinomaMeasure m_CarcinomaMeasure;
        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;

        public CarcinomaTestingSignoutPage(YellowstonePathology.Business.Surgical.CarcinomaMeasure carcinomaMeasure,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.Business.Persistence.ObjectTracker objectTracker,
            YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            this.m_CarcinomaMeasure = carcinomaMeasure;
            this.m_AccessionOrder = accessionOrder;
            this.m_ObjectTracker = objectTracker;
            this.m_SystemIdentity = systemIdentity;

            InitializeComponent();
            DataContext = this;
        }

        public void Save()
        {
            this.m_ObjectTracker.SubmitChanges(this.m_AccessionOrder);
        }

        public bool OkToSaveOnNavigation(Type pageNavigatingTo)
        {
            return true;
        }

        public bool OkToSaveOnClose()
        {
            return true;
        }

        public void UpdateBindingSources()
        {
        }

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
        }

        public YellowstonePathology.Business.Surgical.CarcinomaMeasure CarcinomaMeasure
        {
            get { return this.m_CarcinomaMeasure; }
        }
        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            this.Next(this, new EventArgs());
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Back(this, new EventArgs());
        }
    }
}
