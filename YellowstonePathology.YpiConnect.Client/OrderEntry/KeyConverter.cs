using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace YellowstonePathology.YpiConnect.Client.OrderEntry
{
    public class KeyConverter
    {
        public static string HandleKeyboardInput(Key key, string text)
        {
            string result = string.Empty;
            if (key == Key.Back)
            {
                if (text.Length > 1)
                {
                    result = text.Remove(text.Length - 1, 1);
                }
            }
            else
            {
                result = text + KeyConverter.ToCharacter(key);
            }
            return result;
        }

        public static string ToCharacter(Key key)
        {
            string result = string.Empty;

            string keyString = key.ToString();
            if (keyString.Length == 1)
            {
                result = key.ToString();
            }

            if (keyString.Length == 2)
            {
                if(keyString.StartsWith("D"))
                {
                    result = keyString.Substring(1,1);                    
                }
            }

            if (keyString == "OemMinus")
            {
                result = "-";
            }

            if (keyString.Length == 7)
            {
                if (keyString.StartsWith("NumPad"))
                {
                    result = keyString.Substring(6, 1);
                }
            }            
            return result;
        }
    }
}
