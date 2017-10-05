using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{    
    public class Ki67MelanADualStain : DualStain
    {      
        public Ki67MelanADualStain()            
        {
            this.m_TestId = "KI67MA";
			this.m_TestName = "Ki-67, Semi-quantitative/Melan A";
            this.m_TestAbbreviation = "Ki-67/MelanA";
            this.m_FirstTest = new Ki67SemiQuantitative();
            this.m_SecondTest = new MelanA();
            this.m_IsDualOrder = true;
            this.m_NeedsAcknowledgement = true;
			this.m_DepricatedFirstTestId = "225";
        }     
    } 
}
