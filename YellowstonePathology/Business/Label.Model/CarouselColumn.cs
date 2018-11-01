using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Label.Model
{
    public class CarouselColumn
    {
        private BlockColorEnum m_BlockColor;
        private int m_ColumnNumber;
        private int m_PrinterCode;
                
        public CarouselColumn(BlockColorEnum blockColor, int columnNumber, int printerCode)
        {
            this.m_BlockColor = blockColor;
            this.m_ColumnNumber = columnNumber;
            this.m_PrinterCode = printerCode;
        }

        public BlockColorEnum BlockColor
        {
            get { return this.m_BlockColor; }
        }

        public int ColumnNumber
        {
            get { return this.m_ColumnNumber; }
        }

        public int PrinterCode
        {
            get { return this.m_PrinterCode; }
        }
    }
}
