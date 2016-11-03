using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Flow
{
    public class CellPopulationOfInterest
    {
        private int m_Id;
        private string m_Description;

        public CellPopulationOfInterest(int id, string description)
        {
            this.m_Id = id;
            this.m_Description = description;
        }

        public int Id
        {
            get { return this.m_Id; }
            set { this.m_Id = value; }
        }

        public string Description
        {
            get { return this.m_Description; }
            set { this.m_Description = value; }
        }
    }
}
