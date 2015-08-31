using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Gross
{
    public class DictationTemplateCollection : ObservableCollection<DictationTemplate>
    {
        public DictationTemplateCollection()
        {

        }        

        public DictationTemplate GetTemplate(string specimenId)
        {
            DictationTemplate result = new TemplateNotFound();
            if (string.IsNullOrEmpty(specimenId) == false)
            {
                foreach (DictationTemplate dictationTemplate in this)
                {
                    if (dictationTemplate.SpecimenCollection.Exists(specimenId) == true)
                    {
                        result = dictationTemplate;
                        break;
                    }
                }
            }
            return result;
        }

        public static DictationTemplateCollection GetAll()
        {
            DictationTemplateCollection result = new DictationTemplateCollection();
            result.Add(new AppendixExcisionTemplate());
            result.Add(new ProstateNeedleCoreTemplate());
            result.Add(new ProstateTURTemplate());
            result.Add(new GITemplate());
            result.Add(new SkinShavePunchMiscTemplate());
            result.Add(new SkinExcisionTemplate());
            result.Add(new GallbladderExcisionTemplate());
            result.Add(new TonsilExcisionTemplate());
            result.Add(new POCTemplate());
            result.Add(new BreastReductionTemplate());
            result.Add(new ECCTemplate());
            result.Add(new EMBTemplate());
            result.Add(new CervicalBiopsyTemplate());
            //result.Add(new LEEPExcisionTemplate());
            //result.Add(new PlacentaTemplate());
            //result.Add(new UnterusTemplate());
            //result.Add(new NeedleCoreBiopsyTemplate());
            return result;
        }               
    }
}
