using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace YellowstonePathology.Business.Reports
{    
    public partial class RetrospectiveReviewReport : FixedDocument
    {
        private List<string> m_ReportNoCollection;

        public RetrospectiveReviewReport(DateTime actionDate)
        {
            this.m_ReportNoCollection = new List<string>();

            DateTime finalDate = actionDate.AddDays(-1);
            if (actionDate.DayOfWeek == DayOfWeek.Monday)
            {
                finalDate = finalDate.AddDays(-3);
            }

            Business.ReportNoCollection finalCases = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetSurgicalFinal(finalDate);
            int count = finalCases.Count;
            double tenPercentOfCount = Math.Round((count * .1), 0);

            Random rnd = new Random();
            for (int i=0; i<tenPercentOfCount; i++)
            {                
                int nextRnd = rnd.Next(0, count - 1);
                this.m_ReportNoCollection.Add(finalCases[nextRnd].Value);
            }            

            InitializeComponent();
            this.DataContext = this;
        }

        public List<string> ReportNoCollection
        {
            get { return this.m_ReportNoCollection; }
        }
    }
}
