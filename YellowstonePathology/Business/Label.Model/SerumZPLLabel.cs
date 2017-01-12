﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Label.Model
{
    public class SerumZPLLabel
    {
        public SerumZPLLabel()
        {

        }

        public static string GetCommands()
        {                        
            StringBuilder result = new StringBuilder();
            int xOffset = 0;

            result.Append("^XA");
            for (int i = 0; i < 4; i++)
            {                
                GetOne(result, xOffset);
                xOffset += 325;
            }
            
            result.Append("^XZ");
            return result.ToString();
        }

        private static void GetOne(StringBuilder result, int xOffset)
        {            
            result.Append("^FO" + (xOffset + 65) + ",030^ATN,50^FD" + "Serum" + "^FS");
            result.Append("^FO" + (xOffset + 30) + ",090^ATN,50^FD" + "84165-26" + "^FS");
            result.Append("^FO" + (xOffset + 170) + ",150^ATN,50^FD" + "MD" + "^FS");
            result.Append("^FO" + (xOffset + 30) + ",210^ATN,50^FD" + "    /    /17" + "^FS");
        }
    }
}