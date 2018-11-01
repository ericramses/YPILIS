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

        public void Print(YellowstonePathology.Business.Test.AliquotOrderCollection aliquotOrderCollection, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            List<Cassette> cassettes = new List<Cassette>();
            foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in aliquotOrderCollection)
            {
                if (aliquotOrder.IsBlock() == true)
                {
                    if (aliquotOrder.LabelType == YellowstonePathology.Business.Specimen.Model.AliquotLabelType.DirectPrint == true)
                    {
                        Cassette cassette = new Cassette();
                        cassette.FromAliquotOrder(aliquotOrder, accessionOrder);
                        cassettes.Add(cassette);
                    }
                }
            }

            foreach (Cassette cassette in cassettes)
            {
                cassette.Print();
            }            
        }
    }
}
