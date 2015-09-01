﻿using System;
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
            this.m_Text = "onsists of *NUMBEROFFRAGMENTS* tan-pink cylindrical tissue fragments *MEASURING/AGGREGATING*. " +
                "The specimen is filtered through a fine mesh bag and entirely submitted to *COLOR* cassette \"*CASSETTENUMBER*\".";

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
            this.m_Text = "Consists of (*NUMBEROFFRAGMENTS*) tan-pink irregularly shaped, rough and ragged tissue fragments(s)" +
                "weighing in aggregate *WEIGHT*, and measuring/aggregating to *MEASURING/AGGREGATING*. " +
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
            this.m_Text = "*NUMBEROFFRAGMENTS* fragments of tan-pink tissue measuring in aggregate *MEASURING/AGGREGATING*. " +
                "The specimen is filtered through a fine mesh bag and entirely submitted in *COLOR* cassette \"*CASSETTENUMBER*\".";

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
            this.m_Text = "Received: *FORMALIN/FRESH*" + Environment.NewLine +
                          "Gross Description: *APPENDIX/MESOAPPENDIX* Measuring: *MEASUREMENTS*" + Environment.NewLine +
                          "Serosal Surface: *DESCRIPTION*" + Environment.NewLine +
                          "Mesoappendix: *DESCRIPTION/MEASUREMENTS*" + Environment.NewLine +
                          "Pertinent Abnormalities: *DESCRIPTION*" + Environment.NewLine +
                          "Luminal Contents: *DESCRIPTION*" + Environment.NewLine +
                          "Fecalith: *PRESENT/ABSENT/DESCRIPTION*" + Environment.NewLine +
                          "Submitted: *CASSETTE/COLOR*";

            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Recieved In", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Gross Description", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Measurements", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Serosal Surface", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Attached Mesoappendix: Measurements and Description", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Pertinent Abnormalities", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Luminal Contents", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Fecalith Present or Absent: Measurements and Description", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Color", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Number", ""));

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.AppendixExcision appendixExcision = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.AppendixExcision();
            this.m_SpecimenCollection.Add(appendixExcision);
        }
    }

    public class SkinShavePunchMiscTemplate : DictationTemplate
    {
        public SkinShavePunchMiscTemplate()
        {
            this.m_TemplateName = "Skin Shave Punch and Misc Biopsy";
            this.m_Text = "Received: *INFORMALIN/FRESH*" + Environment.NewLine +
                          "Gross Description: *SHAVE/PUNCH/MISC*" + Environment.NewLine +
                          "Measurements: *MEASUREMENTS*" + Environment.NewLine +
                          "Submitted: *ENTIRELY/BIS/TRI/SERIALLY* Cassette: *COLOR/NUMBER*";


            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Recieved In", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Number and Gross Description", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Measurements", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Specimen Sectioning", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Color", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Number", ""));

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.SkinShavePunchMiscBiopsy SkinShavePunchMiscBiopsy = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.SkinShavePunchMiscBiopsy();
            this.m_SpecimenCollection.Add(SkinShavePunchMiscBiopsy);

        }
    }

    public class SkinExcisionTemplate : DictationTemplate
    {
        public SkinExcisionTemplate()
        {
            this.m_TemplateName = "Skin Excision Biopsy";
            this.m_Text = "Received: *INFORMALIN/FRESH*" + Environment.NewLine +
                          "Gross Description: *OREINTED/UNORIENTED/INKED*" + Environment.NewLine +
                          "Measurements: *MEASUREMENTS*" + Environment.NewLine +
                          "Submitted: *ENTIRELY/BIS/TRI/SERIALLY* Cassette: *COLOR/NUMBER*";


            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Recieved In", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Gross Description, Number, Inking", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Measurements", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Specimen Sectioning", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Color", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Number", ""));

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.SkinExcisionBiopsy skinexcisionBiopsy = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.SkinExcisionBiopsy();
            this.m_SpecimenCollection.Add(skinexcisionBiopsy);

        }
    }

    public class GallbladderExcisionTemplate : DictationTemplate
    {
        public GallbladderExcisionTemplate()
        {
            this.m_TemplateName = "Gallbladder Excision";
            this.m_Text = "Received: *INFORMALIN/FRESH*" + Environment.NewLine +
                          "Loose Stones: *YES/NO/QUANTITY/DESCRIPTION*" + Environment.NewLine +
                          "Gross Description: *OPENED/UNOPENED*" + Environment.NewLine +
                          "Measurements: *MEASUREMENTS*" + Environment.NewLine +
                          "Serosal Surface: *SEROSALDESCRIPTION*" + Environment.NewLine +
                          "Adventitial Surface: *ADVENTITIALDESCRIPTION*" + Environment.NewLine +
                          "Mural Defects: *HOLES/PERFORATIONS/DIMENSIONS*" + Environment.NewLine +
                          "Luminal Contents: *BILE/BLOOD/STONES/ETC*" + Environment.NewLine +
                          "Mucosal Surface: *DESCRIPTION*, Wall Thickness: *MEASUREMENTS*" + Environment.NewLine +
                          "Submitted: *SUBMISSIONKEY* Cassette: *COLOR/NUMBER*";


            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Recieved In", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Loose Stones", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Gross Description", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Gallbladder Measurments", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Serosal Surface", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Adventitial Surface", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Mural Defects", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Luminal Contents", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Mucosal Surface", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Wall Thickness", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Submission Key", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Color", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Number", ""));

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.GallbladderExcision gallbladderExcision = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.GallbladderExcision();
            this.m_SpecimenCollection.Add(gallbladderExcision);

        }
    }

    public class TonsilExcisionTemplate : DictationTemplate
    {
        public TonsilExcisionTemplate()
        {
            this.m_TemplateName = "Tonsil Excision";
            this.m_Text = "Received: *INFORMALIN/FRESH*" + Environment.NewLine +
                          "Gross Description: *TONSILDESCRIPTION*" + Environment.NewLine +
                          "Adenoids: *PRESENT/ABSENT*" + Environment.NewLine +
                          "Weight: *AGGREGATEWEIGHT*" + Environment.NewLine +
                          "Measurement Tonsil 1: *MEASUREMENTS*" + Environment.NewLine +
                          "Measurement Tonsil 2: *MEASUREMENTS*" + Environment.NewLine +
                          "[Measurement of Adenoids: *MEASUREMENTS*]" + Environment.NewLine +
                          "Cut Surfaces: *SMOOTH/TAN/PINK/GLISTENING*" + Environment.NewLine +
                          "Submitted: *SUBMISSIONKEY* Cassette: *COLOR/NUMBER*";


            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Recieved In", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Gross Description", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Adenoids Present or Absent", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Aggregate Weight", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("First Tonsil Measurement", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Second Tonsil Measurement", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Adenoid Measurement", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cut Surfaces", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Submission Key", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Color", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Number", ""));

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.TonsilExcision TonsilExcision = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.TonsilExcision();
            this.m_SpecimenCollection.Add(TonsilExcision);

        }
    }

    public class POCTemplate : DictationTemplate
    {
        public POCTemplate()
        {
            this.m_TemplateName = "POC";
            this.m_Text = "Received: *INFORMALIN/FRESH*" + Environment.NewLine +
                          "Gross Description: *TISSUE/MUCUS/CLOTTEDBLOOD*" + Environment.NewLine +
                          "Measurement: *MEASUREMENTS*" + Environment.NewLine +
                          "Villi: *PRESENT/ABSENT/INDETERMINATE*" + Environment.NewLine +
                          "Fetal Parts: *PRESENT/ABSENT/INDETERMINATE*" + Environment.NewLine +
                          "Submitted: *SUBMISSIONKEY* Cassette: *COLOR/NUMBER*";


            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Recieved In", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Gross Description", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Measurements", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Villi", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Fetal Parts", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Submission Key", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Color", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Number", ""));

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.POC POC = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.POC();
            this.m_SpecimenCollection.Add(POC);

        }
    }

    public class BreastReductionTemplate : DictationTemplate
    {
        public BreastReductionTemplate()
        {
            this.m_TemplateName = "BreastReduction";
            this.m_Text = "Received: *INFORMALIN/FRESH*" + Environment.NewLine +
                          "Gross Description: *NUMBER/SKIN/ADIPOSE*" + Environment.NewLine +
                          "Measurement: *MEASUREMENTS*" + Environment.NewLine +
                          "Weight: *WEIGHT*" + Environment.NewLine +    
                          "Skin Surface: *DESCRIPTION*" + Environment.NewLine +
                          "Adipose: *DESCRIPTION*" + Environment.NewLine +
                          "Submitted: *SUBMISSIONKEY* Cassette: *COLOR/NUMBER*";


            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Recieved In", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Gross Description", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Measurements", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Weight", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Skin Description", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Adipose Description", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Submission Key", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Color", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Number", ""));

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.BreastReduction BreastReduction = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.BreastReduction();
            this.m_SpecimenCollection.Add(BreastReduction);

        }
    }

    public class ECCTemplate : DictationTemplate
    {
        public ECCTemplate()
        {
            this.m_TemplateName = "ECC";
            this.m_Text = "Received: *INFORMALIN/FRESH*" + Environment.NewLine +
                          "Gross Description: *#* *COLOR* *CONSISTENCY*" + Environment.NewLine +
                          "Measurement: *MEASUREMENTS*" + Environment.NewLine +
                          "Submitted: *SUBMISSIONKEY* Cassette: *COLOR/NUMBER*";


            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Recieved In", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Gross Description", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Measurements", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Submission Key", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Color", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Number", ""));

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.ECC ECC = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.ECC();
            this.m_SpecimenCollection.Add(ECC);
        }

    }

    public class EMBTemplate : DictationTemplate
    {
        public EMBTemplate()
        {
            this.m_TemplateName = "EMB";
            this.m_Text = "Received: *INFORMALIN/FRESH*" + Environment.NewLine +
                          "Gross Description: *#* *COLOR* *CONSISTENCY*" + Environment.NewLine +
                          "Measurement: *MEASUREMENTS*" + Environment.NewLine +
                          "Submitted: *SUBMISSIONKEY* Cassette: *COLOR/NUMBER*";


            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Recieved In", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Gross Description", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Measurements", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Submission Key", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Color", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Number", ""));

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.EMB EMB = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.EMB();
            this.m_SpecimenCollection.Add(EMB);
        }

    }

    public class CervicalBiopsyTemplate : DictationTemplate
    {
        public CervicalBiopsyTemplate()
        {
            this.m_TemplateName = "CervicalBiopsy";
            this.m_Text = "Received: *INFORMALIN/FRESH*" + Environment.NewLine +
                          "Gross Description: *#* *COLOR* *CONSISTENCY*" + Environment.NewLine +
                          "Inked: *COLOR*" + Environment.NewLine +
                          "Measurement: *MEASUREMENTS*" + Environment.NewLine +
                          "Submitted: *SUBMISSIONKEY* Cassette: *COLOR/NUMBER*";


            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Recieved In", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Gross Description", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Inked", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Measurements", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Submission Key", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Color", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Number", ""));

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.CervicalBiopsy CervicalBiopsy = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.CervicalBiopsy();
            this.m_SpecimenCollection.Add(CervicalBiopsy);
        }

    }

    public class LEEPConeTemplate : DictationTemplate
    {
        public LEEPConeTemplate()
        {
            this.m_TemplateName = "LEEPCone";
            this.m_Text = "Received: *INFORMALIN/FRESH*" + Environment.NewLine +
                          "Gross Description: *DISC* *COLOR/CHARACTERISTICS*" + Environment.NewLine +
                          "Measurement: *MEASUREMENTS*" + Environment.NewLine +
                          "OS: *DESCRIPTION* *MEASUREMENTS*" + Environment.NewLine +
                          "Endocervical Inked: *COLOR*" + Environment.NewLine +
                          "Cut Surface Inking: *COLOR*" + Environment.NewLine +
                          "Submitted: *SUBMISSIONKEY* Cassette: *COLOR/NUMBER*";


            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Recieved In", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Gross Description", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Measurements", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("OS Description", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Endocervix Inked", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cut Surface Inked", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Submission Key", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Color", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Number", ""));

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.LEEPCone LEEPCone = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.LEEPCone();
            this.m_SpecimenCollection.Add(LEEPCone);
        }

    }

    public class LEEPPiecesTemplate : DictationTemplate
    {
        public LEEPPiecesTemplate()
        {
            this.m_TemplateName = "LEEPPieces";
            this.m_Text = "Received: *INFORMALIN/FRESH*" + Environment.NewLine +
                          "Gross Description: *#* *COLOR* *MEASUERMENT*" + Environment.NewLine +
                          "OS Description: *SHAPE* *LOCATION* *MEASUREMENT*" + Environment.NewLine +
                          "Inked: *COLOR*" + Environment.NewLine +
                          "Measurement: *MEASUREMENTS*" + Environment.NewLine +
                          "Submitted: *SUBMISSIONKEY* Cassette: *COLOR/NUMBER*";


            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Recieved In", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Gross Description", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("OS Description/Location/Measurements", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Inked", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Measurements", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Submission Key", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Color", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Number", ""));

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.LEEPPieces LEEPPieces = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.LEEPPieces();
            this.m_SpecimenCollection.Add(LEEPPieces);
        }

    }

    public class SinglePlacentaTemplate : DictationTemplate
    {
        public SinglePlacentaTemplate()
        {
            this.m_TemplateName = "SinglePlacenta";
            this.m_Text = "Received: *INFORMALIN/FRESH*" + Environment.NewLine +
                          "Umbilical Cord:" + Environment.NewLine +
                                "Length: *LENGTH*, Insertion: *INSERTION*,Vessels: *VESSELS*" + Environment.NewLine +
                                "Knots: *KNOTS*, Coiling: *COILING*, Other: *OTHER*" + Environment.NewLine +
                          "Extraplacental Membranes:" + Environment.NewLine +
                                "Insertion: *INSERTION*, *COLOR*, *AMNION*" + Environment.NewLine +
                                "Cysts: *CYSTS*, *POINTOFRUPTURE*" + Environment.NewLine +
                          "Placental Disc:" + Environment.NewLine +
                                "Placental Disc: *WEIGHT*, *SHAPE*, *MEASUERMENT*" + Environment.NewLine +
                                "Fetal Surface:" + Environment.NewLine +                          
                                    "Gross Description: *SURFACE*, *AMNIONNODOSUM*" + Environment.NewLine +
                                "Maternal Surface:" + Environment.NewLine +
                                    "Hemorrhage: *PRESENT/ABSENT*" + Environment.NewLine +
                                        "[Adherent: *MEASUREMENTS*, *INDENTS/NO*]" +Environment.NewLine +
                                        "[Non-Adherent: *MEASUREMENT*]" + Environment.NewLine +
                                    "Gross Description: *SURFACE* *INFARCTS* *CALCIFICATION*" + Environment.NewLine +
                          "Submitted: *SUBMISSIONKEY* Cassette: *COLOR/NUMBER*";


            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Recieved In", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Umbilical Cord Length/Insertion/Vessels", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Umbilical Cord Knots/Coiling/Other", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Extraplacental Membrane Insertion/Color/Amnion", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Extraplacental Membrane Cysts/Rupture", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Placental Disc Weight/Shape/Measurements", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Placental Disc Fetal Surface Description/Amnion", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Placental Disc Maternal Surface Hemorrhage", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Placental Disc Maternal Surface Description/Infarcts/Calcifications", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Submission Key", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Color", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Number", ""));

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.SinglePlacenta SinglePlacenta = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.SinglePlacenta();
            this.m_SpecimenCollection.Add(SinglePlacenta);
        }

    }
}