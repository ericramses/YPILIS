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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for AmendmentPage.xaml
    /// </summary>
    public partial class AmendmentPage : UserControl, INotifyPropertyChanged, Business.Interface.IPersistPageChanges
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;
        public delegate void FinishEventHandler(object sender, EventArgs e);
        public event FinishEventHandler Finish;

        private YellowstonePathology.Business.User.SystemUserCollection m_AmendmentSigners;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;
        private YellowstonePathology.Business.Amendment.Model.Amendment m_Amendment;
        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;

        public AmendmentPage(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.Business.Persistence.ObjectTracker objectTracker,
            YellowstonePathology.Business.Amendment.Model.Amendment amendment,
            YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_ObjectTracker = objectTracker;
            this.m_Amendment = amendment;
            this.m_SystemIdentity = systemIdentity;
            this.AmendmentSigners = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetUsersByRole(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.AmendmentSigner, true);

            InitializeComponent();

            DataContext = this;
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

        public void Save()
        {
            this.m_ObjectTracker.SubmitChanges(this.m_AccessionOrder);
        }

        public void UpdateBindingSources()
        {

        }

        public YellowstonePathology.Business.Amendment.Model.Amendment Amendment
        {
            get { return this.m_Amendment; }
            set
            {
                this.m_Amendment = value;
                NotifyPropertyChanged("Amendment");
            }
        }

        public YellowstonePathology.Business.User.SystemUserCollection AmendmentSigners
        {
            get { return this.m_AmendmentSigners; }
            set
            {
                this.m_AmendmentSigners = value;
                NotifyPropertyChanged("AmendmentSigners");
            }
        }

        public void GridSelectedAmendment_KeyUp(object sender, KeyEventArgs args)
        {
            if (args.Key == Key.F7)
            {
                this.CheckSpelling();
            }
        }

        public void ComboBoxAmendmentUsers_SelectionChanged(object sender, RoutedEventArgs args)
        {
            if (this.m_Amendment != null && this.comboBoxAmendmentUsers.SelectedItem != null)
            {
                YellowstonePathology.Business.User.SystemUser systemUserItem = (YellowstonePathology.Business.User.SystemUser)this.comboBoxAmendmentUsers.SelectedItem;
                this.m_Amendment.PathologistSignature = systemUserItem.Signature;
            }
        }

        private void CheckSpelling()
        {
            YellowstonePathology.Business.Common.SpellChecker spellCheck = new YellowstonePathology.Business.Common.SpellChecker();
            YellowstonePathology.Business.Common.AmendmentSpellCheckList amendmentSpellCheckList = new YellowstonePathology.Business.Common.AmendmentSpellCheckList(this.m_Amendment);
            spellCheck.CheckSpelling(amendmentSpellCheckList);
            MessageBox.Show("Spell check is complete.");
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Back(this, new EventArgs());
        }

        private void ButtonFinalize_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.ExecutionStatus executionStatus = new YellowstonePathology.Business.Rules.ExecutionStatus();
            YellowstonePathology.Business.Rules.Amendment.FinalAmendment finalAmendment = new YellowstonePathology.Business.Rules.Amendment.FinalAmendment();
            finalAmendment.Execute(this.m_Amendment, executionStatus, this.m_SystemIdentity);
            if (executionStatus.Halted)
            {
                YellowstonePathology.Business.Rules.RuleExecutionStatus ruleExecutionStatus = new YellowstonePathology.Business.Rules.RuleExecutionStatus();
                ruleExecutionStatus.PopulateFromLinqExecutionStatus(executionStatus);
                RuleExecutionStatusDialog dialog = new RuleExecutionStatusDialog(ruleExecutionStatus);
                dialog.ShowDialog();
                return;
            }
            NotifyPropertyChanged("Amendment");
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Finish(this, new EventArgs());
        }
    }
}
