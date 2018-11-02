using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Label.Model
{
    public class CassettePrinterGrossHobbit : CassettePrinter
    {
        public CassettePrinterGrossHobbit() : base("Gross Hobbit", @"\\gross-hobbit\Jobs")
        {
            this.Carousel.Columns.Add(new CarouselColumn("White", 1, 101, "White"));
            this.Carousel.Columns.Add(new CarouselColumn("Orange", 2, 102, "#FFFFC175"));
            this.Carousel.Columns.Add(new CarouselColumn("Yellow", 3, 103, "Yellow"));                       
            this.Carousel.Columns.Add(new CarouselColumn("Lilac", 4, 104, "#b666d2"));
            this.Carousel.Columns.Add(new CarouselColumn("Blue", 5, 105, "#bfefff"));
            this.Carousel.Columns.Add(new CarouselColumn("Pink", 6, 106, "#e5a3ad"));

            //this.Carousel.Columns.Add(new CarouselColumn("Green", 5, 105, "Green"));
            //this.Carousel.Columns.Add(new CarouselColumn("Orange", 6, 106, "#FFFFC175"));                                   
            //this.Carousel.Columns.Add(new CarouselColumn("White", 8, 108, "White"));
        }

        public override Cassette GetCassette()
        {
            return new GeneralDataCassette();
        }
    }
}
