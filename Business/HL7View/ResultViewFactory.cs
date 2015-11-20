using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.HL7View
{
    public class ResultViewFactory
    {
        public static IResultView GetResultView(string reportNo, int clientId, bool testing)
        {            
            IResultView resultView = null;
            switch (clientId)
            {                
                case 558:
                case 820:
                case 723:
                case 33:
                case 1417:
                case 650:
                case 1421:
                case 649:
                case 230:
                case 123:
                case 126:
                case 242:
                case 253:
                case 1313:
                case 1096:
                case 287:
                case 968:
                case 250:
                case 57:
                case 313:
                case 1025:
                case 1321:    
                case 25:
                case 90:
                case 505:
                case 154:
                case 184:
                case 969:
                case 1422:
                    resultView = new Business.HL7View.EPIC.EpicResultView(reportNo, testing);                    
                    break;
                case 203: //Richard Taylor
                case 1177: //Spring Creek
                case 196: //Central Montana
                case 209: //Laura Bennett                
                case 954: // Barb Miller
                case 1471: //Marchion
                //case 219:
                    resultView = new HL7View.CMMC.CMMCResultView(reportNo);                    
                    break;
                case 1337:
                    resultView = new HL7View.CDC.MTDohResultView(reportNo);
                    break;
                case 1203:
                    resultView = new HL7View.ECW.ECWResultView(reportNo, testing);
                    break;
            }       
            return resultView;
        }
    }
}
