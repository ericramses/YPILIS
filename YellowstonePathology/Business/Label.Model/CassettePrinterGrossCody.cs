using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Label.Model
{
    public class CassettePrinterGrossCody : CassettePrinter
    {
        public CassettePrinterGrossCody() : base("Gross Cody", @"\\10.1.1.84\Jobs\")
        {
            this.Carousel.Columns.Add(new CarouselColumn(BlockColorEnum.Yellow, 1, 101));
            this.Carousel.Columns.Add(new CarouselColumn(BlockColorEnum.White, 2, 102));
            this.Carousel.Columns.Add(new CarouselColumn(BlockColorEnum.Orange, 3, 103));
            this.Carousel.Columns.Add(new CarouselColumn(BlockColorEnum.Lilac, 4, 104));            
        }
    }
}
