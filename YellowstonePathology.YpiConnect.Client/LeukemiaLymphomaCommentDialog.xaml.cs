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
using System.Windows.Shapes;

namespace YellowstonePathology.YpiConnect.Client
{   
    public partial class LeukemiaLymphomaCommentDialog : Window
    {
		private YellowstonePathology.YpiConnect.Contract.Flow.FlowCommentCollection m_FlowCommentCollection;
		private YellowstonePathology.YpiConnect.Contract.Flow.FlowComment m_FlowComment;

		public LeukemiaLymphomaCommentDialog(YellowstonePathology.YpiConnect.Contract.Flow.FlowCommentCollection flowCommentCollection)
        {
			this.m_FlowCommentCollection = flowCommentCollection;

            InitializeComponent();

			this.DataContext = m_FlowCommentCollection;            
        }

		public YellowstonePathology.YpiConnect.Contract.Flow.FlowComment SelectedComment
		{
			get { return this.m_FlowComment; }
		}

        private void ButtonSelect_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            if (this.ListViewComments.SelectedItem != null)
            {
				this.m_FlowComment = (YellowstonePathology.YpiConnect.Contract.Flow.FlowComment)this.ListViewComments.SelectedItem;
				this.Close();
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

		private void ListViewComments_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			this.DialogResult = true;
			if (this.ListViewComments.SelectedItem != null)
			{
				this.m_FlowComment = (YellowstonePathology.YpiConnect.Contract.Flow.FlowComment)this.ListViewComments.SelectedItem;
				this.Close();
			}
		}        
    }
}
