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
using System.IO;

namespace YellowstonePathology.UI.Cytology
{
    /// <summary>
    /// Interaction logic for ClientLetters.xaml
    /// </summary>
    public partial class ClientLetters : Window
    {
        StringBuilder m_LetterText;

        public ClientLetters()
        {
            System.Reflection.Assembly assembly = this.GetType().Assembly;
            TextReader textReader = new StreamReader(assembly.GetManifestResourceStream("YellowstonePathology.UI.Cytology.CytologyClientLetterText.txt"));
            this.m_LetterText = new StringBuilder(textReader.ReadToEnd());

            InitializeComponent();

            this.TextBoxLetterText.Text = this.m_LetterText.ToString();
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
