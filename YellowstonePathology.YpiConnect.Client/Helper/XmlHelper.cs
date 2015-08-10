using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace YellowstonePathology.YpiConnect.Client.Helper
{        
    public class XmlHelper
    {

        public static void SetValue(XElement xElement, string xPathStatement, string elementValue)
        {            
            if (DoesElementExist(xElement, xPathStatement) == false)
            {
                string[] slashSplit = xPathStatement.Split('/');
                XElement parentElement = xElement.XPathSelectElement('/' + slashSplit[1]);
                AddElement(parentElement, slashSplit[2]);       
            }

            XElement element = xElement.XPathSelectElement(xPathStatement);
            element.Value = elementValue;
        }
        
        public static void AddElement(XElement parentElement, string childName)
        {                                    
            string elementString = string.Format("<{0}></{0}>", childName);
            XElement newElement = XElement.Parse(elementString);            
            parentElement.Add(newElement);
        }

        public static string GetStringValue(XElement xElement, string xPathStatement, string defaultValue)
        {
            string result = defaultValue;
            if (DoesElementExist(xElement, xPathStatement) == true)
            {
                if (DoesElementHaveValue(xElement, xPathStatement) == true)
                {
                    result = xElement.XPathSelectElement(xPathStatement).Value.ToString();
                }
            }            
            return result;
        }

        public static bool GetBooleanValue(XElement xElement, string xPathStatement, bool defaultValue)
        {
            bool result = defaultValue;
            if (DoesElementExist(xElement, xPathStatement) == true)
            {
                if (DoesElementHaveValue(xElement, xPathStatement) == true)
                {
                    result = Convert.ToBoolean(xElement.XPathSelectElement(xPathStatement).Value);
                }
            }
            return result;
        }

        public static int GetIntValue(XElement xElement, string xPathStatement, int defaultValue)
        {
            int result = defaultValue;
            if (DoesElementExist(xElement, xPathStatement) == true)
            {
                if (DoesElementHaveValue(xElement, xPathStatement) == true)
                {
                    result = Convert.ToInt32(xElement.XPathSelectElement(xPathStatement).Value);
                }
            }
            return result;
        }

        public static bool DoesElementHaveValue(XElement xElement, string xPathStatement)
        {
            bool result = false;
            if (string.IsNullOrEmpty(xElement.XPathSelectElement(xPathStatement).Value) == false)
            {
                result = true;
            } 
            return result;
        }

        public static bool DoesElementExist(XElement xElement, string xPathStatement)
        {
            bool result = false;
            if (xElement.XPathSelectElement(xPathStatement) != null)
            {
                result = true;
            } 
            return result;
        }
    }
}
