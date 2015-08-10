using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace YellowstonePathology.UI.PatientLinking
{
	/// <summary>
	/// Interaction logic for PatientLinkingControl.xaml
	/// </summary>
    /// 

   /* public delegate void LinkingNavigationEventHandler(object sender, EventArgs e);

	public partial class PatientLinkingControl : UserControl
	{
        public event LinkingNavigationEventHandler LinkingNavigate;

		private ObservableCollection<YellowstonePathology.Business.Interface.IPatientLinking> m_PatientLinkings;
		private YellowstonePathology.Business.Interface.IOrder m_ISpecimenLog;

		public PatientLinkingControl()
		{
			m_PatientLinkings = new ObservableCollection<YellowstonePathology.Business.Interface.IPatientLinking>();
			InitializeComponent();
			this.DataContext = m_PatientLinkings;

            this.listViewLinkingList.PreviewKeyUp += new KeyEventHandler(listViewLinkingList_PreviewKeyUp);
            //this.listViewLinkingList.KeyUp += new KeyEventHandler(listViewLinkingList_KeyUp);
            this.listViewLinkingList.Loaded += new RoutedEventHandler(listViewLinkingList_Loaded);            
		}

        public void listViewLinkingList_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                EventArgs eventArgs = new EventArgs();
                this.LinkingNavigate(this, eventArgs);
            }
            if (e.Key == Key.Enter)
            {
                if (this.listViewLinkingList.SelectedItem != null)
                {
                    ObservableCollection<YellowstonePathology.Business.Interface.IPatientLinking> selectedItems = new ObservableCollection<YellowstonePathology.Business.Interface.IPatientLinking>();
                    foreach (YellowstonePathology.Business.Interface.IPatientLinking item in this.listViewLinkingList.SelectedItems)
                    {
                        selectedItems.Add(item);
                    }

                    YellowstonePathology.Business.Rules.PatientLinking.LinkPatient linkPatient = new YellowstonePathology.Business.Rules.PatientLinking.LinkPatient();
                    YellowstonePathology.Business.Rules.ExecutionStatus executionStatus = new YellowstonePathology.Business.Rules.ExecutionStatus();
                    //linkPatient.Execute(selectedItems, YellowstonePathology.Business.ProcessingModeEnum.Production, executionStatus, this.m_dat);
                    if (executionStatus.Halted)
                    {
                        MessageBox.Show(executionStatus.ExecutionMessages[0].Message, executionStatus.ExecutionMessages[0].Name);
                        return;
                    }
                }
            }
        }

        private void listViewLinkingList_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void listViewLinkingList_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.listViewLinkingList.Items.Count != 0)
            {
                this.listViewLinkingList.SelectedIndex = 0;
                this.listViewLinkingList.MoveFocus(new TraversalRequest(FocusNavigationDirection.Down));

            }
            else
            {
                Keyboard.Focus(this.listViewLinkingList);
            }
        }

		public ObservableCollection<YellowstonePathology.Business.Interface.IPatientLinking> PatientLinkings
		{
			get { return this.m_PatientLinkings; }
			set	{ this.m_PatientLinkings = value; }
		}

		public YellowstonePathology.Business.Interface.IOrder ISpecimenLog
		{
			get { return this.m_ISpecimenLog; }
			set { this.m_ISpecimenLog = value; }
		}        		
	}*/
}
