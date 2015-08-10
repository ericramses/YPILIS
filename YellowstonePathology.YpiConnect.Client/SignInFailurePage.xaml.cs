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
    /// Interaction logic for SignInFailurePage.xaml
    /// </summary>
	public partial class SignInFailurePage : Page, YellowstonePathology.Shared.Interface.IPersistPageChanges
    {
        public SignInFailurePage()
        {
            InitializeComponent();            
        }

		public void Save()
		{

		}

		public bool OkToSaveOnNavigation(Type pageNavigatingTo)
		{
			return false;
		}

		public bool OkToSaveOnClose()
		{
			return false;
		}

		public void UpdateBindingSources()
		{
		}
	}
}
