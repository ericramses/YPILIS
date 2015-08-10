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

namespace YellowstonePathology.UI.MolecularTesting
{
    /// <summary>
    /// Interaction logic for NewBatchDate.xaml
    /// </summary>

    public partial class PanelOrderBatch : System.Windows.Window
    {
		YellowstonePathology.Business.Panel.Model.PanelOrderBatch m_PanelOrderBatch;
		YellowstonePathology.Business.BatchTypeListItem m_BatchTypeListItem;
		string m_Description;		
		Nullable<DateTime> m_RunDate;
		DateTime m_BatchDate;

		public PanelOrderBatch(YellowstonePathology.Business.BatchTypeListItem batchTypeListItem)
        {
			this.m_BatchTypeListItem = batchTypeListItem;
			BatchDate = DateTime.Today;
			Description = batchTypeListItem.BatchTypeDescription;
            InitializeComponent();
            this.DataContext = this;
        }

		public string Description
		{
			get { return this.m_Description; }
			set { this.m_Description = value; }
		}

		public DateTime BatchDate
		{
			get { return this.m_BatchDate; }
			set { this.m_BatchDate = value; }
		}

		public Nullable<DateTime> RunDate
		{
			get { return this.m_RunDate; }
			set { this.m_RunDate = value; }
		}

		public YellowstonePathology.Business.Panel.Model.PanelOrderBatch CurrentPanelOrderBatch
		{
			get { return this.m_PanelOrderBatch; }
		}

        private void ButtonOk_Click(object sender, RoutedEventArgs args)
        {
            this.m_PanelOrderBatch = new Business.Panel.Model.PanelOrderBatch();
			this.m_PanelOrderBatch.BatchTypeId = this.m_BatchTypeListItem.BatchTypeId;
			this.m_PanelOrderBatch.Description = this.Description;
			this.m_PanelOrderBatch.RunDate = this.RunDate;
			this.m_PanelOrderBatch.BatchDate = this.BatchDate;
			this.Close();
        }
    }
}