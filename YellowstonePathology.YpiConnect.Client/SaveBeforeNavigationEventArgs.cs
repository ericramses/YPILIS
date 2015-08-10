using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.YpiConnect.Client
{
    public class SaveBeforeNavigationEventArgs
    {
        private YellowstonePathology.YpiConnect.Client.PleaseWaitDialog m_PleaseWaitDialog;
        //private YellowstonePathology.YpiConnect.Contract.ShipmentSubmitter m_Submitter;
        private System.Windows.Controls.Frame m_Navigator;
        private System.Windows.Controls.Page m_PageToNavigateTo;
        private YellowstonePathology.Business.Rules.MethodResult m_MethodResult;

        public SaveBeforeNavigationEventArgs(YellowstonePathology.YpiConnect.Client.PleaseWaitDialog pleaseWaitDialog,
            //YellowstonePathology.YpiConnect.Contract.ShipmentSubmitter submitter,
            System.Windows.Controls.Frame navigator,
            System.Windows.Controls.Page pageToNavigateTo,
            YellowstonePathology.Business.Rules.MethodResult methodResult)
        {
            this.m_PleaseWaitDialog = pleaseWaitDialog;
            //this.m_Submitter = submitter;
            this.m_Navigator = navigator;
            this.m_PageToNavigateTo = pageToNavigateTo;
            this.m_MethodResult = methodResult;
        }

        public YellowstonePathology.YpiConnect.Client.PleaseWaitDialog PleaseWaitDialog
        {
            get { return this.m_PleaseWaitDialog; }
        }

        //public YellowstonePathology.YpiConnect.Contract.ShipmentSubmitter Submitter
        //{
        //    get { return this.m_Submitter; }
        //}

        public System.Windows.Controls.Frame Navigator
        {
            get { return this.m_Navigator; }
        }

        public System.Windows.Controls.Page PageToNavigateTo
        {
            get { return this.m_PageToNavigateTo; }
        }

        public YellowstonePathology.Business.Rules.MethodResult MethodResult
        {
            get { return this.m_MethodResult; }
            set { this.m_MethodResult = value; }
        }
    }
}
