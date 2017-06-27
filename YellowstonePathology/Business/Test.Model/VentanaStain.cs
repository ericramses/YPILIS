using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.Model
{
    public class VentanaStain
    {
        private string m_Id;
        private string m_Name;
        private string m_YPIId;
        private string m_Type;
        private string m_Color;

        public VentanaStain(string id, string name, string ypiId, string type, string color)
        {
            this.m_Id = id;
            this.m_Name = name;
            this.m_YPIId = ypiId;
            this.m_Type = type;
            this.m_Color = color;
        }

        public string Id
        {
            get { return this.m_Id; }
        }

        public string Name
        {
            get { return this.m_Name; }
        }

        public string YPIId
        {
            get { return this.m_YPIId; }
        }

        public string Type
        {
            get { return this.m_Type; }
        }

        public string Color
        {
            get { return this.m_Color; }
        }
    }
}
