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
using System.Text.RegularExpressions;

namespace YellowstonePathology.UI.Stain
{
    /// <summary>
    /// Interaction logic for StainEditDialog.xaml
    /// </summary>
    public partial class StainEditDialog : Window
    {
        public delegate void AcceptEventHandler(object sender, EventArgs e);
        public event AcceptEventHandler Accept;

        private Business.Stain.Model.Stain m_Stain;

        public StainEditDialog(Business.Stain.Model.Stain stain)
        {
            this.m_Stain = stain;
            InitializeComponent();
            DataContext = this;
        }
        public StainEditDialog()
        {
            this.m_Stain = new Business.Stain.Model.Stain();
            InitializeComponent();
            DataContext = this;
        }

        public Business.Stain.Model.Stain Stain
        {
            get { return this.m_Stain; }
            set { this.m_Stain = value; }
        }

        public void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = this.CanSave();
            if (methodResult.Success == true)
            {
                Business.Stain.Model.StainCollection.SetInRedis(this.m_Stain);
                //this.Accept(this, new EventArgs());
                Close();
            }
            else
            {
                MessageBox.Show(methodResult.Message);
            }
        }

        private YellowstonePathology.Business.Rules.MethodResult CanSave()
        {
            YellowstonePathology.Business.Rules.MethodResult result = new Business.Rules.MethodResult();
            string id = this.DetermineStainId();
            if (this.m_Stain.VentanaBenchMarkId == 0)
            {
                result.Success = false;
                result.Message = "The Ventana BenchMark Id must be a number greater than 0.";
            }
            if(string.IsNullOrEmpty(this.m_Stain.StainId) == true)
            {
                if(YellowstonePathology.Business.Stain.Model.StainCollection.Instance.Exists(id) == true)
                {
                    result.Success = false;
                    result.Message = "The Stain Id is not unique. Add a number or a consonent to the Name.";
                }
                else
                {
                    this.m_Stain.StainId = id;
                }
            }
            return result;
        }

        private string DetermineStainId()
        {
            string lower = this.m_Stain.StainName.ToLower();
            char first = lower[0];
            char last = lower[lower.Length - 1];
            string pattern = @"[aeiou]";
            Regex regex = new Regex(pattern);
            string result = regex.Replace(lower, "");
            if (first != result[0]) result = first + result;
            if (last != result[result.Length - 1]) result = result + last;
            pattern = @"[)(/\\,\.\s -]";
            regex = new Regex(pattern);
            result = regex.Replace(result, "");
            return result;
        }
    }
}
