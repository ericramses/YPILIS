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
using System.ComponentModel;

namespace YellowstonePathology.UI.Test
{
    /// <summary>
    /// Interaction logic for KRASExon23MutationResultPage.xaml
    /// </summary>
    public partial class KRASExon23MutationResultPage : UserControl, INotifyPropertyChanged, Business.Interface.IPersistPageChanges
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void NextEventHandler(object sender, EventArgs e);
        public event NextEventHandler Next;

        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;
        private string m_PageHeaderText;

        private YellowstonePathology.Business.Test.KRASExon23Mutation.KRASExon23MutationTestOrder m_KRASExon23MutationTestOrder;
        private string m_OrderedOnDescription;

        public KRASExon23MutationResultPage(YellowstonePathology.Business.Test.KRASExon23Mutation.KRASExon23MutationTestOrder krasExon23MutationTestOrder,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.Business.Persistence.ObjectTracker objectTracker,
            YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            this.m_KRASExon23MutationTestOrder = krasExon23MutationTestOrder;
            this.m_AccessionOrder = accessionOrder;
            this.m_SystemIdentity = systemIdentity;
            this.m_ObjectTracker = objectTracker;

            this.m_PageHeaderText = "KRAS Exon 2,3 Mutation Analysis Result For: " + this.m_AccessionOrder.PatientDisplayName;

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_KRASExon23MutationTestOrder.OrderedOn, this.m_KRASExon23MutationTestOrder.OrderedOnId);
            this.m_OrderedOnDescription = specimenOrder.Description;

            InitializeComponent();

            DataContext = this;
        }

        public string OrderedOnDescription
        {
            get { return this.m_OrderedOnDescription; }
        }

        public YellowstonePathology.Business.Test.KRASExon23Mutation.KRASExon23MutationTestOrder PanelSetOrder
        {
            get { return this.m_KRASExon23MutationTestOrder; }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public string PageHeaderText
        {
            get { return this.m_PageHeaderText; }
        }

        public bool OkToSaveOnNavigation(Type pageNavigatingTo)
        {
            return true;
        }

        public bool OkToSaveOnClose()
        {
            return true;
        }

        public void Save()
        {
            this.m_ObjectTracker.SubmitChanges(this.m_AccessionOrder);
        }

        public void UpdateBindingSources()
        {

        }

        private void HyperLinkNotDetected_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.KRASExon23Mutation.KRASExon23MutationResult result = new YellowstonePathology.Business.Test.KRASExon23Mutation.KRASExon23MutationResult();
            result.SetNotDetectedResults(this.m_KRASExon23MutationTestOrder);
            this.NotifyPropertyChanged("PanelSetOrder");
        }

        private void HyperLinkDetected_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.KRASExon23Mutation.KRASExon23MutationResult result = new YellowstonePathology.Business.Test.KRASExon23Mutation.KRASExon23MutationResult();
            result.SetDetectedResults(this.m_KRASExon23MutationTestOrder);
            this.NotifyPropertyChanged("PanelSetOrder");
        }

        private void HyperLinkShowDocument_Click(object sender, RoutedEventArgs e)
        {
            this.Save();
            YellowstonePathology.Business.Test.KRASExon23Mutation.KRASExon23MutationWordDocument report = new Business.Test.KRASExon23Mutation.KRASExon23MutationWordDocument();
            report.Render(this.m_AccessionOrder.MasterAccessionNo, this.m_KRASExon23MutationTestOrder.ReportNo, Business.Document.ReportSaveModeEnum.Draft);

            YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_KRASExon23MutationTestOrder.ReportNo);
            string fileName = YellowstonePathology.Business.Document.CaseDocument.GetDraftDocumentFilePath(orderIdParser);
            YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(fileName);
        }

        private void HyperLinkFinalizeResults_Click(object sender, RoutedEventArgs e)
        {
            Business.Audit.Model.AuditResult auditResult = this.m_KRASExon23MutationTestOrder.IsOkToFinalize(this.m_AccessionOrder);
            if (auditResult.Status == Business.Audit.Model.AuditStatusEnum.OK)
            {
                this.m_KRASExon23MutationTestOrder.Finalize(this.m_SystemIdentity.User);
            }
            else
            {
                MessageBox.Show(auditResult.Message);
            }
        }

        private void HyperLinkUnfinalResults_Click(object sender, RoutedEventArgs e)
        {
            Business.Rules.MethodResult methodResult = this.m_KRASExon23MutationTestOrder.IsOkToUnfinalize();
            if (methodResult.Success == true)
            {
                this.m_KRASExon23MutationTestOrder.Unfinalize();
            }
            else
            {
                MessageBox.Show(methodResult.Message);
            }
        }

        private void HyperLinkAcceptResults_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult result = this.m_KRASExon23MutationTestOrder.IsOkToAccept();
            if (result.Success == true)
            {
                this.m_KRASExon23MutationTestOrder.Accept(this.m_SystemIdentity.User);
            }
            else
            {
                MessageBox.Show(result.Message);
            }
        }

        private void HyperLinkUnacceptResults_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult result = this.m_KRASExon23MutationTestOrder.IsOkToUnaccept();
            if (result.Success == true)
            {
                this.m_KRASExon23MutationTestOrder.Unaccept();
            }
            else
            {
                MessageBox.Show(result.Message);
            }
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            if (this.Next != null) this.Next(this, new EventArgs());
        }
    }
}
