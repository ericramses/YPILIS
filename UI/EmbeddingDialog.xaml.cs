using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for EmbeddingDialog.xaml
    /// </summary>
    public partial class EmbeddingDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private DateTime m_WorkDate;
        private YellowstonePathology.Business.Test.AliquotOrderCollection m_AliquotOrderCollection;

        public EmbeddingDialog()
        {
            this.m_WorkDate = DateTime.Today;
            this.m_AliquotOrderCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetEmbeddingCollection(this.WorkDate);
            InitializeComponent();
            this.DataContext = this;
        }

        public DateTime WorkDate
        {
            get { return this.m_WorkDate; }
            set
            {
                if(this.m_WorkDate != value)
                {
                    this.m_WorkDate = value;
                    this.NotifyPropertyChanged("WorkDate");
                }
            }
        }

        public YellowstonePathology.Business.Test.AliquotOrderCollection AliquotOrderCollection
        {
            get { return this.m_AliquotOrderCollection; }
        }

        private void ButtonAccessionOrderBack_Click(object sender, RoutedEventArgs e)
        {
            this.WorkDate = this.WorkDate.AddDays(-1);
            this.m_AliquotOrderCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetEmbeddingCollection(this.WorkDate);
            this.NotifyPropertyChanged("AliquotOrderCollection");
        }

        private void ButtonAccessionOrderForward_Click(object sender, RoutedEventArgs e)
        {
            this.WorkDate = this.WorkDate.AddDays(1);
            this.m_AliquotOrderCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetEmbeddingCollection(this.WorkDate);
            this.NotifyPropertyChanged("AliquotOrderCollection");
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
