using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace YellowstonePathology.Business.Persistence
{
    public static class JSONWriter
    {
        public static string Write(object o)
        {
            Type type = o.GetType();
            StringBuilder result = new StringBuilder();
            JSONIndenter.Indent(result);
            JSONIndenter.IndentDepth = JSONIndenter.IndentDepth + 1;
            result.Append("{" + Environment.NewLine);
            PropertyInfo[] properties = o.GetType().GetProperties().
                Where(prop => Attribute.IsDefined(prop, typeof(PersistentProperty)) || Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).ToArray();
            foreach (PropertyInfo property in properties)
            {
                JSONIndenter.Indent(result);               
                Type dataType = property.PropertyType;
                if (dataType == typeof(string))
                {
                    WriteString(result, property, o);
                }
                else if (dataType == typeof(int))
                {
                    WriteNumber(result, property, o);
                }
                else if (dataType == typeof(double))
                {
                    WriteNumber(result, property, o);
                }
                else if (dataType == typeof(Nullable<int>))
                {
                    WriteNumber(result, property, o);
                }
                else if (dataType == typeof(DateTime))
                {
                    WriteDate(result, property, o);
                }
                else if (dataType == typeof(bool))
                {
                    WriteBoolean(result, property, o);
                }
                else if (dataType == typeof(Nullable<bool>))
                {
                    WriteBoolean(result, property, o);
                }
                else if (dataType == typeof(Nullable<DateTime>))
                {
                    WriteDate(result, property, o);
                }
                else
                {
                    throw new Exception("This Data Type is Not Implemented: " + dataType.Name);
                }
            }

            JSONIndenter.IndentDepth = JSONIndenter.IndentDepth - 1;
            if (properties.Length != 0)
            {
                result.Remove(result.Length - 3, 3);
            }

            result.AppendLine("}");
            return result.ToString();
        }

        private static string EscapeJSON(string json)
        {
            //json = json.Replace(Environment.NewLine, "\\n");
            json = json.Replace("\r", string.Empty);
            json = json.Replace("\n", "\\n");
            return json.Replace("\"", "\\\"");
        }

        private static void WriteString(StringBuilder result, PropertyInfo property, object o)
        {
            if (property.GetValue(o, null) != null)
            {
                //result.Append("\t\"" + property.Name + "\": \"" + EscapeJSON(property.GetValue(o, null).ToString()) + "\", \n");
                result.Append("\"" + property.Name + "\": \"" + EscapeJSON(property.GetValue(o, null).ToString()) + "\", \n");
            }
            else
            {
                //result.Append("\t\"" + property.Name + "\": null, \n");
                result.Append("\"" + property.Name + "\": null, \n");
            }
        }

        private static void WriteNumber(StringBuilder result, PropertyInfo property, object o)
        {
            if (property.GetValue(o, null) != null)
            {
                //result.Append("\t\"" + property.Name + "\": " + property.GetValue(o, null) + ", \n");
                result.Append("\"" + property.Name + "\": " + property.GetValue(o, null) + ", \n");
            }
            else
            {
                //result.Append("\t\"" + property.Name + "\": null, \n");
                result.Append("\"" + property.Name + "\": null, \n");
            }
        }

        private static void WriteDate(StringBuilder result, PropertyInfo property, object o)
        {
            if (property.GetValue(o, null) != null)
            {
                DateTime dotNetDate = DateTime.Parse(property.GetValue(o, null).ToString());
                string jsonDate = dotNetDate.Year.ToString() + "-" + dotNetDate.Month.ToString() + "-" + dotNetDate.Day.ToString() + "T" + dotNetDate.Hour.ToString() + ":" + dotNetDate.Minute + ":" + dotNetDate.Second.ToString() + "." + dotNetDate.Millisecond + "Z";
                //result.Append("\t\"" + property.Name + "\": \"" + jsonDate + "\", \n");
                result.Append("\"" + property.Name + "\": \"" + jsonDate + "\", \n");
            }
            else
            {
                //result.Append("\t\"" + property.Name + "\": null, \n");
                result.Append("\"" + property.Name + "\": null, \n");
            }
        }

        private static void WriteBoolean(StringBuilder result, PropertyInfo property, object o)
        {
            if (property.GetValue(o, null) != null)
            {
                string stringValue = property.GetValue(o, null).ToString();
                string jsonValue = null;
                if (stringValue.ToUpper() == "TRUE")
                {
                    jsonValue = "true";
                }
                else if (stringValue.ToUpper() == "FALSE")
                {
                    jsonValue = "false";
                }
                //result.Append("\t\"" + property.Name + "\": " + jsonValue + ", \n");
                result.Append("\"" + property.Name + "\": " + jsonValue + ", \n");
            }
            else
            {
                //result.Append("\t\"" + property.Name + "\": null, \n");
                result.Append("\"" + property.Name + "\": null, \n");
            }
        }        
    }
}
