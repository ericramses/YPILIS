using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Label.Model
{    
    public class Carousel
    {
        private List<CarouselColumn> m_Columns;

        public Carousel()
        {
            this.m_Columns = new List<CarouselColumn>();
        }

        public List<CarouselColumn> Columns
        {
            get { return this.m_Columns; }
        }

        public bool Exists(BlockColorEnum blockColor)
        {
            bool result = false;
            foreach(CarouselColumn column in this.m_Columns)
            {
                if(column.BlockColor == blockColor)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public CarouselColumn GetColumn(BlockColorEnum blockColor)
        {
            CarouselColumn result = null;
            foreach (CarouselColumn column in this.m_Columns)
            {
                if (column.BlockColor == blockColor)
                {
                    result = column;
                    break;
                }
            }
            return result;
        }        
    }
}
