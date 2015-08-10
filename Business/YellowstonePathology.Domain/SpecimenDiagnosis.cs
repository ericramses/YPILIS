using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Persistence;

namespace YellowstonePathology.Domain
{
    public class SpecimenDiagnosis
    {
        private string m_Description;
        private string m_Diagnosis;
        private string m_MainTerm;

        public SpecimenDiagnosis()
        {

        }

        [PersistentProperty()]
        public string Description
        {
            get { return this.m_Description; }
            set { this.m_Description = value; }
        }

        [PersistentProperty()]
        public string Diagnosis
        {
            get { return this.m_Diagnosis; }
            set { this.m_Diagnosis = value; }
        }

        public string MainTerm
        {
            get { return this.m_MainTerm; }
            set { this.m_MainTerm = value; }
        }
    }
}
