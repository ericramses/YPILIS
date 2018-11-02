using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Label.Model
{
    public class CassettePrinter
    {
        private string m_Name;
        private string m_Path;
        private Carousel m_Carousel;

        public CassettePrinter(string name, string path)
        {
            this.m_Name = name;
            this.m_Path = path;
            this.m_Carousel = new Carousel();
        }            

        public string Name
        {
            get { return this.m_Name; }
        }

        public string Path
        {
            get { return this.m_Path; }
        }

        public Carousel Carousel
        {
            get { return this.m_Carousel; }
        }

        public virtual Cassette GetCassette()
        {
            throw new Exception("Not Implemented Hers.");
        }

        public void Print(YellowstonePathology.Business.Test.AliquotOrderCollection aliquotOrderCollection, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {            
            CassettePrinterCollection printers = new CassettePrinterCollection();
            CassettePrinter printer = printers.GetPrinter(Business.User.UserPreferenceInstance.Instance.UserPreference.CassettePrinter);            

            List<Cassette> cassettes = new List<Cassette>();
            foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in aliquotOrderCollection)
            {
                if (aliquotOrder.IsBlock() == true)
                {
                    if (aliquotOrder.LabelType == YellowstonePathology.Business.Specimen.Model.AliquotLabelType.DirectPrint == true)
                    {
                        if(printer != null)
                        {
                            Cassette cassette = printer.GetCassette();
                            cassette.FromAliquotOrder(aliquotOrder, accessionOrder);
                            string line = cassette.GetLine();
                            string fileName = System.IO.Path.Combine(this.m_Path, System.Guid.NewGuid().ToString() + cassette.GetFileExtension());

                            try
                            {
                                using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileName))
                                {
                                    file.Write(line + "\r\n");
                                    aliquotOrder.Printed = true;
                                }
                            }
                            catch (Exception e)
                            {
                                System.Windows.MessageBox.Show(fileName + ": " + e.Message, "Cassette Printer Location.", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Exclamation);
                            }
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("The cassette printer is not selected.");
                        }                    
                    }
                }
            }                       
        }        
    }
}
