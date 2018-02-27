﻿using System;
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
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace YellowstonePathology.UI.Billing
{	
	public partial class PanelSetOrderCPTCodeEntryPage : UserControl, INotifyPropertyChanged 
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

		public delegate void BackEventHandler(object sender, EventArgs e);
		public event BackEventHandler Back;

        private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;
        private YellowstonePathology.Business.Test.PanelSetOrderCPTCode m_PanelSetOrderCPTCode;
		private YellowstonePathology.Business.Billing.Model.CptCodeCollection m_CptCodeCollection;

        public PanelSetOrderCPTCodeEntryPage(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder, int clientId)
        {
            this.m_PanelSetOrder = panelSetOrder;
            this.m_PanelSetOrderCPTCode = this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.GetNextItem(panelSetOrder.ReportNo);
            this.m_PanelSetOrderCPTCode.Quantity = 1;
            this.m_PanelSetOrderCPTCode.ClientId = clientId;
            this.m_PanelSetOrderCPTCode.EntryType = "Manual Entry";
            this.m_PanelSetOrderCPTCode.CodeableType = "Billable Test";

            this.GetCptCodeCollection();

            InitializeComponent();			
			DataContext = this;
		}

        public YellowstonePathology.Business.Test.PanelSetOrderCPTCode PanelSetOrderCPTCode
        {
            get { return this.m_PanelSetOrderCPTCode; }
        }

		public YellowstonePathology.Business.Billing.Model.CptCodeCollection CptCodeCollection
		{
			get { return this.m_CptCodeCollection; }
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}		

		public bool OkToSaveOnNavigation(Type pageNavigatingTo)
		{
			return true;
		}

		public bool OkToSaveOnClose()
		{
			return true;
		}

		public void Save(bool releaseLock)
		{
			
		}

		public void UpdateBindingSources()
		{
		}

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
            if (this.OkToAddPanelSetOrderCPTCode() == true)
            {
                this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.Add(this.m_PanelSetOrderCPTCode);
                if (this.Next != null) this.Next(this, new EventArgs());
            }			
		}

        private bool OkToAddPanelSetOrderCPTCode()
        {
            bool result = true;
            string message = null;

            if (this.m_PanelSetOrderCPTCode.Quantity < 1)
            {
                result = false;
                message = "The quantity is not valid.";
            }
            if (string.IsNullOrEmpty(this.m_PanelSetOrderCPTCode.CPTCode) == true)
            {
                result = false;
                message = "The CPT code cannot be blank";
            }

            if (result == false)
            {
                MessageBox.Show(message);
            }

            return result;
        }

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{
			if (this.Back != null) this.Back(this, new EventArgs());
		}

		private void ListViewCptCodes_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this.ListViewCptCodes.SelectedItem != null)
			{
				YellowstonePathology.Business.Billing.Model.CptCode cptCode = (YellowstonePathology.Business.Billing.Model.CptCode)this.ListViewCptCodes.SelectedItem;
				this.m_PanelSetOrderCPTCode.CPTCode = cptCode.Code;
				this.m_PanelSetOrderCPTCode.Modifier = cptCode.Modifier == null ? null : cptCode.Modifier.Modifier;
				this.m_PanelSetOrderCPTCode.CodeType = cptCode.CodeType.ToString();
			}
		}
        
        private void GetCptCodeCollection()
        {
            this.m_CptCodeCollection = Store.AppDataStore.Instance.CPTCodeCollection.Clone();
            var codes = new[] {
                new { code = "3126F", modifier="1P" },
                new { code = "3126F", modifier="8P" },
                new { code = "3260F", modifier="1P" },
                new { code = "3260F", modifier="8P" },
                new { code = "3267F", modifier="1P" },
                new { code = "3267F", modifier="8P" },
                new { code = "3394F", modifier="8P" },
                new { code = "81220", modifier="26" },
                new { code = "81240", modifier="26" },
                new { code = "81241", modifier="26" },
                new { code = "81261", modifier="26" },
                new { code = "81270", modifier="26" },                
                new { code = "81291", modifier="26" },                
                new { code = "88312", modifier="TC" },
                new { code = "88313", modifier="TC" },
                new { code = "88342", modifier="TC" },
                new { code = "88343", modifier="TC" },
                new { code = "88344", modifier="TC" },
                new { code = "88360", modifier="TC" },
                new { code = "G0461", modifier="TC" },
                new { code = "G0462", modifier="TC" }
            };            
            
            foreach (var item in codes)
            {
                this.m_CptCodeCollection.AddCloneWithModifier(item.code, item.modifier);
            }
            this.m_CptCodeCollection = Business.Billing.Model.CptCodeCollection.GetSorted(this.m_CptCodeCollection);
        }
    }
}
