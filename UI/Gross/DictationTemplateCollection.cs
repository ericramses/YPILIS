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
            result.Add(new ProstateTURTemplate());            
            return result;
        }               
    }
}
