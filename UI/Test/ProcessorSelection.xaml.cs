using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Data;

namespace YellowstonePathology.UI.Test
{
    /// <summary>
    /// Interaction logic for ProcessorSelection.xaml
    /// </summary>
    public partial class ProcessorSelection : Window
    {
		YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;

		public ProcessorSelection(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
			this.m_AccessionOrder = accessionOrder;

            InitializeComponent();

			this.DataContext = this.m_AccessionOrder;
		}

		private void ButtonApply_Click(object sender, RoutedEventArgs e)
		{
            SetFixationData();
		}

		private void ButtonOK_Click(object sender, RoutedEventArgs e)
		{
            SetFixationData();
			DialogResult = true;
		}

        private void SetFixationData()
        {
			foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in this.m_AccessionOrder.SpecimenOrderCollection)
            {
                if (specimenOrder.ApplyFixation)
                {
					specimenOrder.CollectionDate = this.m_AccessionOrder.CollectionDate;
					specimenOrder.CollectionTime = this.m_AccessionOrder.CollectionTime;
					specimenOrder.ExactFixationStartTimeUnknown = this.m_AccessionOrder.ExactFixationStartTimeUnknown;
					specimenOrder.FixationStartTime = this.m_AccessionOrder.CurrentFixationStart;
					specimenOrder.FixationEndTime = this.m_AccessionOrder.CurrentFixationEnd;
					specimenOrder.FixationType = this.m_AccessionOrder.CurrentFixationType;
					specimenOrder.ClientFixation = this.m_AccessionOrder.CurrentClientFixation;
					specimenOrder.LabFixation = this.m_AccessionOrder.CurrentLabFixation;
					specimenOrder.SetFixationComment();
                }
            }
			this.m_AccessionOrder.NotifyPropertyChanged("");
        }		

        public void ButtonClose_Click(object sender, RoutedEventArgs args)
        {
			DialogResult = false;
			Close();
		}	

        public void RadioButton_IsChecked(object sender, RoutedEventArgs args)
        {
            RadioButton radioButton = (RadioButton)args.Source;
            this.SetFixationEndTime(radioButton);
			this.m_AccessionOrder.NotifyPropertyChanged("");
        }

        public void SetFixationEndTime(RadioButton radioButton)
        {            
            string tag = radioButton.Tag.ToString();
            string[] commaSplit = tag.Split(',');
            string when = commaSplit[0];

            DateTime startTime = DateTime.Parse(DateTime.Today.ToShortDateString() + " " + commaSplit[1]);
            int dayOfWeek = (int)DateTime.Today.DayOfWeek;
            switch (when)
            {
                case "Tomorrow":
                    startTime = startTime.AddDays(1);
                    break;
                case "Saturday":                    
                    startTime = startTime.AddDays(6 - dayOfWeek);
                    break;
                case "Sunday":
                    startTime = startTime.AddDays(7 - dayOfWeek);
                    break;
            }
            TimeSpan duration = this.GetTimeSpan(commaSplit[2]);
			this.m_AccessionOrder.CurrentFixationEnd = startTime.Add(duration);
		}

        private TimeSpan GetTimeSpan(string hrsMins)
        {
            string[] colonSplit = hrsMins.Split(':');
            int hours = Convert.ToInt16(colonSplit[0]);
            int minutes = Convert.ToInt16(colonSplit[1]);
            TimeSpan timeSpan = new TimeSpan(hours, minutes, 0);
            return timeSpan;
        }        

        public void TextBoxFixationStartTime_LostFocus(object sender, RoutedEventArgs args)
        {
            for(int i=0; i< this.ListBoxProcessors.Items.Count; i++)
            {
                if (this.ListBoxProcessors.Items[i].GetType().Name == "RadioButton")
                {
                    RadioButton radioButton = (RadioButton)this.ListBoxProcessors.Items[i];
                    if (radioButton.IsChecked == true)
                    {
                        this.SetFixationEndTime(radioButton);
                        break;
                    }
                }                
            }
        }

        private void MenuItemUseForFixationStartTime_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            switch (menuItem.Header.ToString())
            {
                case "Collection Time":
					this.m_AccessionOrder.CurrentFixationStart = this.m_AccessionOrder.CollectionDate;
                    break;
                case "Accession Time":
					this.m_AccessionOrder.CurrentFixationStart = this.m_AccessionOrder.AccessionDateTime;
                    break;
                case "12:00 AM":
					this.m_AccessionOrder.CurrentFixationStart = DateTime.Parse(DateTime.Today.ToShortDateString() + " 12:00 AM");
                    break;
            }            
        }
    }
}
