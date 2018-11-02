using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Label.Model
{
    public class CassettePrinterGrossCody : CassettePrinter
    {
        public CassettePrinterGrossCody() : base("Gross Cody", @"\\10.33.33.57\CassettePrinter\")
        {
            this.Carousel.Columns.Add(new CarouselColumn("Pink", 1, 1, "Pink"));
            this.Carousel.Columns.Add(new CarouselColumn("Green", 2, 1, "Green"));            
        }

        public override Cassette GetCassette()
        {
            return new ThermoFisherCassette();
        }
    }
}
