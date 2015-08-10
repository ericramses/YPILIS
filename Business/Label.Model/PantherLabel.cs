using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace YellowstonePathology.Business.Label.Model
{
    public class PantherLabel : Label
    {        
        private string m_MasterAccessionNo;
        private string m_PatientFirstName;
        private string m_PatientLastName;
        private string m_SpecimenDescription;
        private string m_PantherId;

        public PantherLabel(string masterAccessionNo, string pantherId, string patientFirstName, string patientLastName, string specimenDescription)
        {            
            this.m_MasterAccessionNo = masterAccessionNo;
            this.m_PantherId = pantherId;
            this.m_PatientFirstName = patientFirstName;
            this.m_PatientLastName = patientLastName;
            this.m_SpecimenDescription = specimenDescription;            
        }

        public override void DrawLabel(int x, int y, System.Drawing.Printing.PrintPageEventArgs e)
        {
            string largeRectangleString = this.TruncateString(this.m_MasterAccessionNo, 150) + ": " + this.TruncateString(this.m_PatientLastName + ", " + this.m_PatientFirstName, 150);         

            StringFormat stringFormatLargeRectangle = new StringFormat();
            stringFormatLargeRectangle.Alignment = StringAlignment.Near;
            stringFormatLargeRectangle.LineAlignment = StringAlignment.Center;
            stringFormatLargeRectangle.FormatFlags = StringFormatFlags.DirectionVertical;

            Rectangle rectangle = new Rectangle(40, 0, 15, 250);                        

            using (Font font = new Font("Verdana", 6, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(largeRectangleString, font, Brushes.Black, rectangle, stringFormatLargeRectangle);                
            }

            BarcodeLib.Barcode barcode = new BarcodeLib.Barcode();
            Image barcodeImage = barcode.Encode(BarcodeLib.TYPE.CODE128, this.m_PantherId, Color.Black, Color.White, 130, 30);

            barcodeImage.RotateFlip(RotateFlipType.Rotate90FlipX);
            e.Graphics.DrawImage(barcodeImage, new Point(55, 0));     
        }        
    }
}
