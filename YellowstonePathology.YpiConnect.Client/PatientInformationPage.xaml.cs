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

namespace YellowstonePathology.YpiConnect.Client
{
    /// <summary>
	/// Interaction logic for ReportBrowserListPage.xaml
    /// </summary>
    public partial class PatientInformationPage : Page
    {
        YellowstonePathology.YpiConnect.Contract.IPatientData m_PatientData;

        public PatientInformationPage(YellowstonePathology.YpiConnect.Contract.IPatientData patientData)
        {
            this.m_PatientData = patientData;
            InitializeComponent();
            this.DataContext = patientData;
        }        
    }
}
