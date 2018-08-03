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

namespace YellowstonePathology.UI.Surgical
{
    /// <summary>
    /// Interaction logic for StainOrder.xaml
    /// </summary>
    public partial class StainOrder : Window, INotifyPropertyChanged
    {
        public delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;

        List<CheckBox> m_StainsCheckBoxes;

        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;        
        private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;
        private string m_PanelOrderComment;

        private YellowstonePathology.UI.Login.FinalizeAccession.AliquotAndStainOrderView m_AliquotAndStainOrderView;
        private YellowstonePathology.Business.Visitor.StainAcknowledgementTaskOrderVisitor m_StainAcknowledgementTaskOrderVisitor;

        public StainOrder(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_PanelSetOrder = panelSetOrder;

            this.m_StainAcknowledgementTaskOrderVisitor = new Business.Visitor.StainAcknowledgementTaskOrderVisitor(this.m_PanelSetOrder);
            this.m_AliquotAndStainOrderView = new Login.FinalizeAccession.AliquotAndStainOrderView(this.m_AccessionOrder, this.m_PanelSetOrder);

            InitializeComponent();

            this.DataContext = this;
            this.Loaded += StainOrder_Loaded;
        }

        private void StainOrder_Loaded(object sender, RoutedEventArgs e)
        {     
            this.m_StainsCheckBoxes = new List<CheckBox>();
            this.InitializeCheckboxList("StainOrder", this.m_StainsCheckBoxes, this.MainGrid);
        }

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
        }

        public YellowstonePathology.UI.Login.FinalizeAccession.AliquotAndStainOrderView AliquotAndStainOrderView
        {
            get { return this.m_AliquotAndStainOrderView; }
        }

        public string PanelOrderComment
        {
            get { return this.m_PanelOrderComment; }
            set
            {
                if (this.m_PanelOrderComment != value)
                {
                    this.m_PanelOrderComment = value;
                    this.NotifyPropertyChanged("PanelOrderComment");
                }
            }
        }

        private void InitializeCheckboxList(string name, List<CheckBox> list, DependencyObject depObj)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                if(child is CheckBox)
                {
                    CheckBox checkBox = (CheckBox)child;
                    if(checkBox.Name.Contains(name))
                    {
                        list.Add(checkBox);
                    }
                }
                this.InitializeCheckboxList(name, list, child);
            }
        }

        private void ButtonOrder_Click(object sender, RoutedEventArgs e)
        {
            Business.Stain.Model.StainCollection stainsToOrder = this.GetStainsToOrder();
            

            ((MainWindow)Application.Current.MainWindow).MoveKeyboardInputToNext();

            YellowstonePathology.Business.Test.AliquotOrderCollection selectedAliquots = this.m_AliquotAndStainOrderView.GetSelectedAliquots();
            if (selectedAliquots.Count > 0)
            {
                YellowstonePathology.Business.Test.Model.TestCollection selectedTests = stainsToOrder.GetTestCollection();

                if (selectedTests.Count > 0)
                {
                    this.HandleOrderingTests(selectedAliquots, selectedTests);

                    this.m_StainAcknowledgementTaskOrderVisitor.TaskOrderDetailComment = this.m_PanelOrderComment;
                    this.m_AccessionOrder.TakeATrip(this.m_StainAcknowledgementTaskOrderVisitor);

                    this.ClearCheckBoxes();                    

                    this.m_AliquotAndStainOrderView.Refresh(false, this.m_PanelSetOrder);
                    this.NotifyPropertyChanged("AliquotAndStainOrderView");

                    this.m_PanelOrderComment = null;
                    this.NotifyPropertyChanged("PanelOrderComment");
                }
                else
                {
                    MessageBox.Show("There are no tests selected.");
                }
            }
            else
            {
                MessageBox.Show("There are no aliquots selected.");
            }
        }

        private void HandleOrderingTests(YellowstonePathology.Business.Test.AliquotOrderCollection selectedAliquots, YellowstonePathology.Business.Test.Model.TestCollection selectedTests)
        {
            foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in selectedAliquots)
            {
                foreach (YellowstonePathology.Business.Test.Model.Test test in selectedTests)
                {
                    bool orderAsDual = false;
                    bool acknowledgeOnOrder = false;

                    YellowstonePathology.Business.Visitor.OrderTestVisitor orderTestVisitorTest = new Business.Visitor.OrderTestVisitor(this.m_PanelSetOrder.ReportNo, test, test.OrderComment, this.m_PanelOrderComment, orderAsDual, aliquotOrder, acknowledgeOnOrder, false, this.m_AccessionOrder.TaskOrderCollection);
                    this.m_AccessionOrder.TakeATrip(orderTestVisitorTest);
                    this.m_StainAcknowledgementTaskOrderVisitor.AddTestOrder(orderTestVisitorTest.TestOrder);
                }
            }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClearCheckBoxes()
        {
            foreach (CheckBox checkBox in this.m_StainsCheckBoxes)
            {
                if (checkBox.IsChecked == true)
                {
                    checkBox.IsChecked = false;
                }
            }
        }

        private Business.Stain.Model.StainCollection GetStainsToOrder()
        {
            Business.Stain.Model.StainCollection result = new Business.Stain.Model.StainCollection();
            foreach(CheckBox checkBox in this.m_StainsCheckBoxes)
            {
                if(checkBox.IsChecked == true)
                {
                    string stainId = (string)checkBox.Tag;
                    Business.Stain.Model.Stain stain = Business.Stain.Model.StainCollection.Instance.GetStain(stainId);
                    result.Add(stain);
                }                
            }
            return result;
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
