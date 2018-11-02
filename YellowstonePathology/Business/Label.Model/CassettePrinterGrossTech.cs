using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Label.Model
{
    public class CassettePrinterGrossTech : CassettePrinter
    {
        //\\10.1.1.84\Jobs\

        public CassettePrinterGrossTech() : base("Gross Tech", @"\\10.1.1.84\Jobs")
        {
            this.Carousel.Columns.Add(new CarouselColumn("Yellow", 1, 101, "Yellow"));
            this.Carousel.Columns.Add(new CarouselColumn("White", 2, 102, "White"));
            this.Carousel.Columns.Add(new CarouselColumn("Orange", 3, 103, "#FFFFC175"));
            this.Carousel.Columns.Add(new CarouselColumn("Lilac", 4, 104, "#b666d2"));            
        }

        public override Cassette GetCassette()
        {
            return new GeneralDataCassette();
        }
    }
}
