using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Surgical
{
    public class PathologistSignoutPath
    {
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder m_SurgicalTestOrder;
        private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;
        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;

        private PathologistSignoutDialog m_PathologistSignoutDialog;
        private YellowstonePathology.Business.Rules.Surgical.WordSearchList m_PapCorrelationWordList;


        public PathologistSignoutPath(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder,
            YellowstonePathology.Business.Persistence.ObjectTracker objectTracker,
            YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_SurgicalTestOrder = surgicalTestOrder;
            this.m_ObjectTracker = objectTracker;
            this.m_SystemIdentity = systemIdentity;

            this.m_PathologistSignoutDialog = new PathologistSignoutDialog();

            this.m_PapCorrelationWordList = new Business.Rules.Surgical.WordSearchList();
            this.m_PapCorrelationWordList.Add(new Business.Rules.Surgical.WordSearchListItem("ECC", true, string.Empty));
            this.m_PapCorrelationWordList.Add(new Business.Rules.Surgical.WordSearchListItem("CERVIX", true, string.Empty));
            this.m_PapCorrelationWordList.Add(new Business.Rules.Surgical.WordSearchListItem("CERVICAL", true, string.Empty));
            this.m_PapCorrelationWordList.Add(new Business.Rules.Surgical.WordSearchListItem("VAGINAL", true, string.Empty));
            this.m_PapCorrelationWordList.Add(new Business.Rules.Surgical.WordSearchListItem("ENDOCERVICAL", true, string.Empty));
            this.m_PapCorrelationWordList.Add(new Business.Rules.Surgical.WordSearchListItem("BLADDER", true, string.Empty));
            this.m_PapCorrelationWordList.Add(new Business.Rules.Surgical.WordSearchListItem("THYROID", true, string.Empty));
            this.m_PapCorrelationWordList.Add(new Business.Rules.Surgical.WordSearchListItem("BREAST", true, string.Empty));
            this.m_PapCorrelationWordList.Add(new Business.Rules.Surgical.WordSearchListItem("GALLBLADDER", false, string.Empty));
        }

        public void Start()
        {
            if (this.ShowPapCorrelationPage() == false)
            {
                this.ShowPathologistSignoutPage1();
            }
            this.m_PathologistSignoutDialog.ShowDialog();
        }

        private bool ShowPapCorrelationPage()
        {
            bool result = false;
            if (this.m_SurgicalTestOrder.Final == false)
            {
                if (this.m_AccessionOrder.SpecimenOrderCollection.FindWordsInDescription(this.m_PapCorrelationWordList) == true)
                {
                    this.m_SurgicalTestOrder.PapCorrelationRequired = true;
                    PapCorrelationPage papCorrelationPage = new PapCorrelationPage(this.m_AccessionOrder, this.m_SurgicalTestOrder, this.m_ObjectTracker);
                    papCorrelationPage.Next += PapCorrelationPage_Next;
                    papCorrelationPage.Back += PapCorrelationPage_Back;
                    this.m_PathologistSignoutDialog.PageNavigator.Navigate(papCorrelationPage);
                    result = true;
                }
                else
                {
                    this.m_SurgicalTestOrder.PapCorrelationRequired = false;
                    this.m_SurgicalTestOrder.PapCorrelation = 0;
                }
            }
            return result;
        }

        private void PapCorrelationPage_Next(object sender, EventArgs e)
        {
            this.ShowPathologistSignoutPage1();
        }

        private void PapCorrelationPage_Back(object sender, EventArgs e)
        {
            this.m_PathologistSignoutDialog.Close();
        }

        private void ShowPathologistSignoutPage1()
        {
            PathologistSignoutPage1 pathologistSignoutPage1 = new PathologistSignoutPage1(this.m_AccessionOrder, this.m_SurgicalTestOrder, this.m_ObjectTracker);
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
            if (this.ShowPapCorrelationPage() == false)
            {
                this.m_PathologistSignoutDialog.Close();
            }
        }
    }
}
