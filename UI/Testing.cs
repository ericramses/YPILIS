using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.UI
{
    public class Testing
    {
        public delegate string MyCallBack1(string text);
        public MyCallBack1 OnCallBack1 { get; set; }

        public delegate string MyCallBack2(string text);
        public MyCallBack2 OnCallBack2 { get; set; }

        public Testing()
        {

        }

        public void DoIt()
        {
            string color = "Black";
            System.Windows.MessageBox.Show(color);
            if (OnCallBack1 != null)
            {
                string newColor = OnCallBack1(color);
                System.Windows.MessageBox.Show(newColor);
            }            
        }
    }
}
