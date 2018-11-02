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
            this.Carousel.Columns.Add(new CarouselColumn("White", 1, 101, "White"));
            this.Carousel.Columns.Add(new CarouselColumn("Orange", 2, 102, "#FFFFC175"));
            this.Carousel.Columns.Add(new CarouselColumn("Yellow", 3, 103, "Yellow"));
            this.Carousel.Columns.Add(new CarouselColumn("Lilac", 4, 104, "#b666d2"));            
        }

        public override Cassette GetCassette()
        {
            return new GeneralDataCassette();
        }
    }
}
