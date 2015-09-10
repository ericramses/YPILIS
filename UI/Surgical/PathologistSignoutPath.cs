using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Surgical
{
    public class PathologistSignoutPath
    {
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder m_PanelSetOrder;
        private PathologistSignoutDialog m_PathologistSignoutDialog;

        public PathologistSignoutPath(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder panelSetOrder)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_PanelSetOrder = panelSetOrder;
            this.m_PathologistSignoutDialog = new PathologistSignoutDialog();
        }

        public void Start()
        {
            this.ShowPathologistSignoutPage1();
            this.m_PathologistSignoutDialog.ShowDialog();
        }

        private void ShowPathologistSignoutPage1()
        {
            PathologistSignoutPage1 pathologistSignoutPage1 = new PathologistSignoutPage1();
            pathologistSignoutPage1.Next += PathologistSignoutPage1_Next;
            pathologistSignoutPage1.Back += PathologistSignoutPage1_Back;
            this.m_PathologistSignoutDialog.PageNavigator.Navigate(pathologistSignoutPage1);
        }

        private void PathologistSignoutPage1_Next(object sender, EventArgs e)
        {
            this.m_PathologistSignoutDialog.Close();
        }

        private void PathologistSignoutPage1_Back(object sender, EventArgs e)
        {
            this.m_PathologistSignoutDialog.Close();
        }
    }
}
