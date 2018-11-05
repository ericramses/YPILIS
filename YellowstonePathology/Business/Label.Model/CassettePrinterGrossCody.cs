using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Label.Model
{
    public class CassettePrinterGrossCody : CassettePrinter
    {
        public CassettePrinterGrossCody() : base("Gross Cody")
        {
            this.Carousel.Columns.Add(new CarouselColumn("Pink", 1, 1, "Pink", CODYPRINTERPATH));
            this.Carousel.Columns.Add(new CarouselColumn("Green", 2, 1, "Green", CODYPRINTERPATH));            
        }

        public override Cassette GetCassette()
        {
            return new ThermoFisherCassette();
        }
    }
}
