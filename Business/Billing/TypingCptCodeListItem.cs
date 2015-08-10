using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace YellowstonePathology.Business.Billing
{
    public class TypingCptCodeListItem : ListItem
    {
        string m_CptCode;        
        int m_Quantity;

        public TypingCptCodeListItem(string cptCode)
        {
            this.m_CptCode = cptCode;            
            this.m_Quantity = 1;
        }

        public string CptCode
        {
            get { return this.m_CptCode; }
            set
            {
                if (value != this.m_CptCode)
                {
                    this.m_CptCode = value;
                    this.NotifyPropertyChanged("CptCode");
                }
            }
        }        

        public int Quantity
        {
            get { return this.m_Quantity; }
            set
            {
                if (value != this.m_Quantity)
                {
                    this.m_Quantity = value;
                    this.NotifyPropertyChanged("Quantity");
                }
            }
        }
    }
}
