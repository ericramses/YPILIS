using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Gross
{
    public class TemplateNotFound : DictationTemplate
    {
        public TemplateNotFound()
        {
            this.m_TemplateName = "Template Not Found.";            
        }
    }

    public class ProstateNeedleCoreTemplate : DictationTemplate
    {
        public ProstateNeedleCoreTemplate()
        {
            this.m_TemplateName = "Prostate Specimen";
            this.m_Text = "Specimen *SPECIMENNUMBER* is received in a B-plus-filled container labeled \"*PATIENTNAME*\"" +
                " and consists of *NUMBEROFFRAGMENTS* tan-pink cylindrical tissue fragments *MEASURING/AGGREGATING*. " +
                "The specimen is submitted entirely in *COLOR* cassette \"*CASSETTENUMBER*\".";

            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Specimen Number", "*SPECIMENNUMBER*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Patient Name", "*PATIENTNAME*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Number Of Fragments", "*NUMBEROFFRAGMENTS*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Measuring/Agregating", "*MEASURING/AGGREGATING*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Color", "*COLOR*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Number", "*CASSETTENUMBER*"));
            
            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.ProstateNeedleBiopsy nb = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.ProstateNeedleBiopsy();                        
            this.m_SpecimenCollection.Add(nb);            
        }
    }

    public class ProstateTURTemplate : DictationTemplate
    {
        public ProstateTURTemplate()
        {
            this.m_TemplateName = "Prostate Specimen";
            this.m_Text = "Specimen *SPECIMENNUMBER* is received fresh container labeled \"*PATIENTNAME*\"" +
                " and consists of (*NUMBEROFFRAGMENTS*) tan-pink irregularly shaped, rough and ragged tissue fragments(s) weighing in aggregate *WEIGHING*, and measuring/aggregating to *MEASURING/AGGREGATING*. " +
                "The specimen is submitted *ENTIRELY/PARTIALLY* in (*COLOR*) cassette \"*CASSETTENUMBER*\"";

            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Specimen Number", "*SPECIMENNUMBER*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Patient Name", "*PATIENTNAME*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Weight Of Fragments", "*WEIGHING*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Number Of Fragments", "*NUMBEROFFRAGMENTS*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Measuring/Agregating", "*MEASURING/AGGREGATING*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Color", "*COLOR*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Number", "*CASSETTENUMBER*"));

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.ProstateTUR tur = new Business.Specimen.Model.SpecimenDefinition.ProstateTUR();
            this.m_SpecimenCollection.Add(tur);
        }
    }

    public class GITemplate : DictationTemplate
    {
        public GITemplate()
        {
            this.m_TemplateName = "GI Specimen";
            this.m_Text = "The specimen *SPECIMENNUMBER* is received in one formalin filled container labeled \"*PATIENTNAME*\"" +
                " and consists of *NUMBEROFFRAGMENTS* fragments of tan-pink tissue measuring in aggregate *MEASURING/AGGREGATING*. " +
                "The specimen is filtered through a fine mesh bag and entirely submitted in *COLOR* cassette \"*CASSETTENUMBER*\".";

            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Specimen Number", "*SPECIMENNUMBER*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Patient Name", "*PATIENTNAME*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Number Of Fragments", "*NUMBEROFFRAGMENTS*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Measuring/Agregating", "*MEASURING/AGGREGATING*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Color", "*COLOR*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Number", "*CASSETTENUMBER*"));
            
            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.GIBiopsy gi = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.GIBiopsy();                        
            this.m_SpecimenCollection.Add(gi);            
        }
    }

    public class AppendixExcisionTemplate : DictationTemplate
    {
        public AppendixExcisionTemplate()
        {
            this.m_TemplateName = "Appendix Excision";
            this.m_Text = "The specimen is received in one formalin filled container labeled \"Igeland, Theodor - appendix\" is an appendix that measures 6.1 in " +
                "length and a diameter of 1.2 cm.  The serosal surface is tan-gray with some exudate present and prominent vessels are seen.  There is a tan-gray " +
                "mesoappendix present.  It measures 4.0 X 1.5 X 1.0 cm and is detached.  The appendix is serially sectioned and the lumen is filled with a small amount " +
                "of red material.  There is a perforation seen measuring 0.2 X 0.2 cm and it is close to the proximal end.  Representative pieces are submitted to orange " +
                "cassettes \"1A\" and \"1B\".   RS/CJN/co";

            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Specimen Number", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Recieved In", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Container Labeling", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Measurements", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Serosal Surface", ""));            
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Attached Mesoappendix: Measurements and Description", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Pertinent abnormalities", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Luminal contents", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Mucosal surface", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Number of Cassettes", ""));

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.AppendixExcision appendixExcision = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.AppendixExcision();
            this.m_SpecimenCollection.Add(appendixExcision);            
        }
    }
}
