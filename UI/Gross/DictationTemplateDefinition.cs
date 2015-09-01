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
            this.m_Text = "Consists of *NUMBEROFFRAGMENTS* tan-pink cylindrical tissue fragments *MEASURING/AGGREGATING*. " +
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
            this.m_Text = "Consists of *NUMBEROFFRAGMENTS* tan-pink irregularly shaped, rough and ragged tissue fragments(s)" +
                "weighing in aggregate *WEIGHT*, and measuring/aggregating to *MEASURING/AGGREGATING*. " +
                "The specimen is submitted *SUBMISSIONKEY* in *COLOR* cassette \"*CASSETTENUMBER*\"";

            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Specimen Number", "*SPECIMENNUMBER*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Patient Name", "*PATIENTNAME*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Weight Of Fragments", "*WEIGHING*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Number Of Fragments", "*NUMBEROFFRAGMENTS*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Measuring/Agregating", "*MEASURING/AGGREGATING*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Submission Key", "*SUBMISSIONKEY*"));
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
            this.m_Text = "Consists of *NUMBEROFFRAGMENTS* fragments of tan-pink tissue measuring in aggregate *MEASURING/AGGREGATING*. " +
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
                          "Gross Description: *APPENDIX/MESOAPPENDIX* Measuring: *APPENDIXMEASUREMENT*" + Environment.NewLine +
                          "Serosal Surface: *SEROSALSURFACE*" + Environment.NewLine +
                          "Mesoappendix: *MESOAPPENDIXMEASUREMENTS* *MESOAPPENDIXDESCRIPTION*" + Environment.NewLine +
                          "Pertinent Abnormalities: *ABNORMALITIES*" + Environment.NewLine +
                          "Luminal Contents: *LUMINALCONTENTS*" + Environment.NewLine +
                          "Fecalith: *PRESENT/ABSENT* *FECALITHMEASUREMENTS* *FECALITHDESCRIPTION*" + Environment.NewLine +
                          "Submitted: *SUBMISSIONKEY*, CASSETTE: (*COLOR*) \"*CASSETTENUMBER*\".";

            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Gross Description", "*APPENDIX/MESOAPPENDIX*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Appendix Measurements", "*APPENDIXMEASUREMENT*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Serosal Surface", "*SEROSALSURFACE*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Attached Mesoappendix: Measurements", "*MESOAPPENDIXMEASUREMENT*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Attached Mesoappendix: Description", "*MESOAPPENDIXDESCRIPTION*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Pertinent Abnormalities", "*ABNORMALITIES*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Luminal Contents", "*LUMINALCONTENTS*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Fecalith Present or Absent", "*PRESENT/ABSENT*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Fecalith Present: Measurements", "*FECALITHMEASUREMENTS*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Fecalith Present: Description", "*FECALITHDESCRIPTION*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Submission Key", "*SUBMISSIONKEY*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Color", "*COLOR*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Number", "*CASSETTENUMBER*"));

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
                          "Gross Description: *NUMBER* *SHAVE/PUNCH/MISC*" + Environment.NewLine +
                          "Measurements: *MEASUREMENT*" + Environment.NewLine +
                          "Submitted: *SUBMISSIONKEY*, Cassette: *COLOR* \"*CASSETTENUMBER*\".";


            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Number of Pieces", "*NUMBER*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Gross Description", "*SHAVE/PUNCH/MISC*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Measurements", "*MEASUREMENT*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Submission Key", "*SUBMISSIONKEY*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Color", "*COLOR*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Number", "*CASSETTENUMBER*"));

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
                          "Gross Description: *NUMBER* *OREINTED/UNORIENTED* *INKING*" + Environment.NewLine +
                          "Measurements: *MEASUREMENT*" + Environment.NewLine +
                          "Submitted: *SUBMISSIONKEY* Cassette: *COLOR* \"*CASSETTENUMBER*\".";


            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Number of Pieces", "*NUMBER"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Oriented or Unoriented", "*OREINTED/UNORIENTED*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Inking", "INKING"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Measurements", "*MEASUREMENT*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Submission Key", "*SUBMISSIONKEY*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Color", "*COLOR*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Number", "*CASSETTENUMBER*"));

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
                          "Loose Stones: *LOOSESTONES*" + Environment.NewLine +
                          "Gross Description: *DESCRIPTION*" + Environment.NewLine +
                          "Measurements: *MEASUREMENTS*" + Environment.NewLine +
                          "Serosal Surface: *SEROSALDESCRIPTION*" + Environment.NewLine +
                          "Adventitial Surface: *ADVENTITIALDESCRIPTION*" + Environment.NewLine +
                          "Mural Defects: *DEFECTS*" + Environment.NewLine +
                          "Luminal Contents: *LUMINALCONTENTS*" + Environment.NewLine +
                          "Mucosal Surface: *MUCOSALDESCRIPTION*, Wall Thickness: *WALLTHICKNESS*" + Environment.NewLine +
                          "Submitted: *SUBMISSIONKEY* Cassette: *COLOR* \"*CASSETTENUMBER*\".";


            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Loose Stones", "*LOOSESTONES*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Gross Description", "*DESCRIPTION*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Gallbladder Measurments", "*MEASUREMENTS*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Serosal Surface", "*SEROSALDESCRIPTION*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Adventitial Surface", "*ADVENTITIALDESCRIPTION*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Mural Defects", "*DEFECTS*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Luminal Contents", "*LUMINALCONTENTS*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Mucosal Surface", "*MUCOSALDESCRIPTION*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Wall Thickness", "*WALLTHICKNESS*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Submission Key", "*SUBMISSIONKEY*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Color", "*COLOR*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Number", "*CASSETTENUMBER*"));

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
                          "Measurement Tonsil 1: *FIRSTTONSILMEASUREMENTS*" + Environment.NewLine +
                          "Measurement Tonsil 2: *SECONDTONSILMEASUREMENTS*" + Environment.NewLine +
                          "[Measurement of Adenoids: *ADENOIDMEASUREMENT*]" + Environment.NewLine +
                          "Cut Surfaces: *CUTSURFACE*" + Environment.NewLine +
                          "Submitted: *SUBMISSIONKEY* Cassette: *COLOR* \"*CASSETTENUMBER*\".";


            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Gross Description", "TONSILDESCRIPTION"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Adenoids Present or Absent", "*PRESENT/ABSENT"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Aggregate Weight", "*AGGREGATEWEIGHT*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("First Tonsil Measurement", "*FIRSTTONSILMEASUREMENTS*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Second Tonsil Measurement", "*SECONDTONSILMEASUREMENTS*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Adenoid Measurement", "*ADENOIDMEASUREMENT*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cut Surfaces", "*CUTSURFACE*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Submission Key", "*SUBMISSIONKEY*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Color", "*COLOR*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Number", "*CASSETTENUMBER*"));

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
                          "Gross Description: *GROSSDESCRIPTION*" + Environment.NewLine +
                          "Measurement: *MEASUREMENT*" + Environment.NewLine +
                          "Villi: *VILLI*" + Environment.NewLine +
                          "Fetal Parts: *FETALPARTS*" + Environment.NewLine +
                          "Submitted: *SUBMISSIONKEY* Cassette: *COLOR* \"*CASSETTENUMBER*\".";


            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Gross Description", "*GROSSDESCRIPTION*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Measurements", "*MEASUREMENT*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Villi", "*VILLI*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Fetal Parts", "*FETALPARTS"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Submission Key", "*SUBMISSIONKEY*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Color", "*COLOR*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Number", "*CASSETTENUMBER*"));

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
                          "Submitted: *SUBMISSIONKEY* Cassette: *COLOR* \"*CASSETTENUMBER*\".";


            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Gross Description", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Gross Description", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Gross Description", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Measurements", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Weight", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Skin Description", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Adipose Description", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Submission Key", "*SUBMISSIONKEY*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Color", "*COLOR*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Number", "*CASSETTENUMBER*"));

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
                          "Gross Description: *NUMBER* *TISSUECOLOR* *TISSUECONSISTENCY*" + Environment.NewLine +
                          "Measurement: *MEASUREMENT*" + Environment.NewLine +
                          "Submitted: *SUBMISSIONKEY* Cassette: *COLOR* \"*CASSETTENUMBER*\".";


            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Number of Pieces", "*NUMBER*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Color of Tissue", "*TISSUECOLOR*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Consistency of Tissue", "*TISSUECONSISTENCY*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Measurements", "*MEASUREMENT*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Submission Key", "*SUBMISSIONKEY*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Color", "*COLOR*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Number", "*CASSETTENUMBER*"));

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
                          "Gross Description: *NUMBER* *TISSUECOLOR* *TISSUECONSISTENCY*" + Environment.NewLine +
                          "Measurement: *MEASUREMENT*" + Environment.NewLine +
                          "Submitted: *SUBMISSIONKEY* Cassette: *COLOR* \"*CASSETTENUMBER*\".";


            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Number of Pieces", "*NUMBER*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Color of Tissue", "*TISSUECOLOR*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Consistency of Tissue", "*TISSUECONSISTENCY*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Measurements", "*MEASUREMENT*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Submission Key", "*SUBMISSIONKEY*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Color", "*COLOR*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Number", "*CASSETTENUMBER*"));

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
                          "Inked: *INKCOLOR*" + Environment.NewLine +
                          "Measurement: *MEASUREMENTS*" + Environment.NewLine +
                          "Submitted: *SUBMISSIONKEY* Cassette: *COLOR* \"*CASSETTENUMBER*\".";


            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Number of Fragments", "*NUMBER*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Color of Tissue", "*TISSUECOLOR*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Consistency of Tissue", "*TISSUECONSISTENCY*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Inked", "*INKCOLOR*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Measurements", "*MEASUREMENTS*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Submission Key", "*SUBMISSIONKEY*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Color", "*COLOR*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Number", "*CASSETTENUMBER*"));

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
                          "Gross Description: *LEEPDESCRIPTION* *LEEPCOLOR/LEEPCHARACTERISTICS*" + Environment.NewLine +
                          "Measurement: *LEEPMEASUREMENTS*" + Environment.NewLine +
                          "OS: *OSDESCRIPTION* *OSMEASUREMENTS*" + Environment.NewLine +
                          "Endocervical Inked: *ENDOCERVICALINKEDCOLOR*" + Environment.NewLine +
                          "Cut Surface Inking: *CUTSURFACEINKEDCOLOR*" + Environment.NewLine +
                          "Submitted: *SUBMISSIONKEY* Cassette: *COLOR* \"*CASSETTENUMBER*\".";


            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Gross Description", "*LEEPDESCRIPTION*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Gross Color and Characteristics", "*LEEPCOLOR/LEEPCHARACTERISTICS*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Measurements", "*LEEPMEASUREMENTS*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("OS Description", "*OSDESCRIPTION*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("OS Measurement", "*OSMEASUREMENTS*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Endocervix Inked", "*ENDOCERVICALINKEDCOLOR*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cut Surface Inked", "*CUTSURFACEINKEDCOLOR*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Submission Key", "*SUBMISSIONKEY*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Color", "*COLOR*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Number", "*CASSETTENUMBER*"));

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
                          "Gross Description: *NUMBER* *COLOR* *MEASUERMENT*" + Environment.NewLine +
                          "OS Description: *OSSHAPE* *OSLOCATION* *OSMEASUREMENT*" + Environment.NewLine +
                          "Inked: *INKINGCOLOR*" + Environment.NewLine +
                          "Submitted: *SUBMISSIONKEY* Cassette: *COLOR* \"*CASSETTENUMBER*\".";


            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Number of Pieces", "*NUMBER*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Tissue Color", "*COLOR*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Tissue Measuremtents", "*MEASUREMET*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("OS Description", "*OSSHAPE*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("OS Location", "*OSLOCATION*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("OS Measurements", "*OSMEASUREMENT*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Inked", "*INKINGCOLOR*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Measurements", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Submission Key", "*SUBMISSIONKEY*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Color", "*COLOR*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Number", "*CASSETTENUMBER*"));

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
                                "Length: *LENGTH*" + Environment.NewLine +
                                "Insertion: *INSERTION*" + Environment.NewLine +
                                "Vessels: *VESSELS*" + Environment.NewLine +
                                "Knots: *KNOTS*" + Environment.NewLine +
                                "Coiling: *COILING*" + Environment.NewLine + 
                                "Other: *OTHER*" + Environment.NewLine +
                          "Extraplacental Membranes:" + Environment.NewLine +
                                "Insertion: *INSERTION*" + Environment.NewLine +
                                "Color: *COLOR*" + Environment.NewLine + 
                                "Amnion Nodosum: *PRESENT/ABSENT*" + Environment.NewLine +
                                "Cysts: *CYSTS*" + Environment.NewLine + 
                                "Point of Rupture: *POR*" + Environment.NewLine +
                          "Placental Disc:" + Environment.NewLine +
                                "Weight: *WEIGHT*" + Environment.NewLine +
                                "Shape: *SHAPE*" + Environment.NewLine + 
                                "Measurements: *MEASUREMENT*" + Environment.NewLine +
                                "Fetal Surface:" + Environment.NewLine +
                                    "Fetal Surface: *DESCRIPTION*" + Environment.NewLine + 
                                    "Amnion Nodosum: *PRESENT/ABSENT*" + Environment.NewLine +
                                "Maternal Surface:" + Environment.NewLine +
                                    "Hemorrhage: *PRESENT/ABSENT*" + Environment.NewLine +
                                        "[Adherent: *MEASUREMENTS*, *INDENTS/NO*]" +Environment.NewLine +
                                        "[Non-Adherent: *MEASUREMENT*]" + Environment.NewLine +
                                    "Maternal Surface: *SURFACE*" + Environment.NewLine +
                                    "Infacts: *INFARCTS*" + Environment.NewLine + 
                                    "Other: *CALCIFICATION/THROMBI*" + Environment.NewLine +
                          "Submitted: *SUBMISSIONKEY* Cassette: *COLOR* \"*CASSETTENUMBER*\".";


            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Umbilical Cord Length/Insertion/Vessels", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Umbilical Cord Knots/Coiling/Other", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Extraplacental Membrane Insertion/Color/Amnion", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Extraplacental Membrane Cysts/Rupture", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Placental Disc Weight/Shape/Measurements", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Placental Disc Fetal Surface Description/Amnion", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Placental Disc Maternal Surface Hemorrhage", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Placental Disc Maternal Surface Description/Infarcts/Calcifications", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Submission Key", "*SUBMISSIONKEY*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Color", "*COLOR*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Number", "*CASSETTENUMBER*"));

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.SinglePlacenta SinglePlacenta = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.SinglePlacenta();
            this.m_SpecimenCollection.Add(SinglePlacenta);
        }

    }

    public class UterusTemplate : DictationTemplate
    {
        public UterusTemplate()
        {
            this.m_TemplateName = "Uterus";
            this.m_Text = "Received: *INFORMALIN/FRESH*" + Environment.NewLine +
                          "Uterus and Cervix:" + Environment.NewLine +
                            "Uterine Corpus:" + Environment.NewLine +
                                "Weight: *UTERINEWEIGHT*" + Environment.NewLine +
                                "Length: *UTERINELENGTH*" + Environment.NewLine +
                                "Width: *UTERINEWIDTH*" + Environment.NewLine +
                                "Thickness: *TUTERINEHICKNESS*" + Environment.NewLine +
                            "Serosa:" + Environment.NewLine +
                                "Description: *SEROSADESCRIPTION*" + Environment.NewLine +
                                "Cul-de-sac: *SEROSACOLOR/SEROSADESCRIPTION*" + Environment.NewLine +
                            "Cervix:" + Environment.NewLine +
                                "Ectocervix: *ECTOCERVIXDESCRIPTION*" + Environment.NewLine +
                                "Os: *OSMEASUREMENT/OSDESCRIPTION*" + Environment.NewLine +
                            "Sectioning of Uterus and Cervix:" + Environment.NewLine +
                                "Endocervical Canal: *ENDOCERVICALCANALDESCRIPTION*" + Environment.NewLine +
                                "Endometrial Cavity:" + Environment.NewLine +
                                    "Dimensions: *ENDOMETRIALMEASUREMENTS*" + Environment.NewLine +
                                    "Description: *ENDOMETRIALDESCRIPTION*" + Environment.NewLine +
                                    "Average Endometrial Thickness: *ENDOMETRIALTHICKNESS*" + Environment.NewLine +
                                "Myometrium:" + Environment.NewLine +
                                    "Average Myometrial Thickness: *MYOMETRIUMTHICKNESS*" + Environment.NewLine +
                                    "Lesion in Uterine wall: *MYOMETRIUMLESIONS*" + Environment.NewLine +
                          "Submitted: *SUBMISSIONKEY* Cassette: *COLOR* \"*CASSETTENUMBER*\".";


            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Uterine Corpus Weight", "*UTERINEWEIGHT*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Uterine Corpus Length", "*UTERINELENGTH*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Uterine Corpus Width", "*UTERINEWIDTH*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Uterine Corpus Thickness", "*UTERINETHICKNESS*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Serosa Description/Cul-de-sac", "*SEROSADESCRIPTION*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Serosa Cul-de-sac", "*CULDESACCOLOR/CULDESACDESCRIPTION*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Ectocervix Description", "ECTOCERVIXDESCRIPTION"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Os Measurement and Description", "*OSMEASUREMENT/OSDESCRIPTION*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Endocervical Canal Description", "*ENDOCERVICALCANALDESCRIPTION*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Endometrial Cavity Dimensions", "*ENDOMETRIALDIMENSION*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Endometrial Cavity Desription", "*ENDOMETRIALDESCRIPTION*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Endometrial Cavity Thickness", "*ENDOMETRIALTHICKNESS*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Myometrium Thickness", "*MYOMETRIUMTHICKNESS*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Myometrium Lesions", "*MYOMETRIUMLESIONS*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Submission Key", "*SUBMISSIONKEY*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Color", "*COLOR*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Number", "*CASSETTENUMBER*"));

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.Uterus Uterus = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.Uterus();
            this.m_SpecimenCollection.Add(Uterus);
        }

    }

    public class UterusAdnexaTemplate : DictationTemplate
    {
        public UterusAdnexaTemplate()
        {
            this.m_TemplateName = "Uterus with Adnexa";
            this.m_Text = "Received: *INFORMALIN/FRESH*" + Environment.NewLine +
                          "Uterus and Cervix:" + Environment.NewLine +
                            "Uterine Corpus:" + Environment.NewLine +
                                "Weight: *UTERINEWEIGHT*" + Environment.NewLine +
                                "Length: *UTERINELENGTH*" + Environment.NewLine +
                                "Width: *UTERINEWIDTH*" + Environment.NewLine +
                                "Thickness: *TUTERINEHICKNESS*" + Environment.NewLine +
                            "Serosa:" + Environment.NewLine +
                                "Description: *SEROSADESCRIPTION*" + Environment.NewLine +
                                "Cul-de-sac: *SEROSACOLOR/SEROSADESCRIPTION*" + Environment.NewLine +
                            "Cervix:" + Environment.NewLine +
                                "Ectocervix: *ECTOCERVIXDESCRIPTION*" + Environment.NewLine +
                                "Os: *OSMEASUREMENT/OSDESCRIPTION*" + Environment.NewLine +
                            "Sectioning of Uterus and Cervix:" + Environment.NewLine +
                                "Endocervical Canal: *ENDOCERVICALCANALDESCRIPTION*" + Environment.NewLine +
                                "Endometrial Cavity:" + Environment.NewLine +
                                    "Dimensions: *ENDOMETRIALMEASUREMENTS*" + Environment.NewLine +
                                    "Description: *ENDOMETRIALDESCRIPTION*" + Environment.NewLine +
                                    "Average Endometrial Thickness: *ENDOMETRIALTHICKNESS*" + Environment.NewLine +
                                "Myometrium:" + Environment.NewLine +
                                    "Average Myometrial Thickness: *MYOMETRIUMTHICKNESS*" + Environment.NewLine +
                                    "Lesion in Uterine wall: *MYOMETRIUMLESIONS*" + Environment.NewLine +
                          "Right Adnexal Organs:" + Environment.NewLine +
                            "Fallopian Tube:" + Environment.NewLine +
                                "Dimensions: *RIGHTFALLOPIANLENGTH/RIGHTFALLOPIANDIAMETER*" + Environment.NewLine +
                                "Outer Surface: *RIGHTFALLOPIANOUTERSURFACE*" + Environment.NewLine +
                                "Cut Surface: *RIGHTFALLOPIANCUTSURFACE*" + Environment.NewLine +
                            "Ovary:" + Environment.NewLine +
                                "Dimensions: *RIGHTOVARYMEASUREMENT*" + Environment.NewLine +
                                "Outer Surface: *RIGHTOVARYOUTERSURFACE*" + Environment.NewLine +
                                "Cut Surface: *RIGHTOVACRYCUTSURFACE*" + Environment.NewLine +
                          "Left Adnexal Organs:" + Environment.NewLine +
                            "Fallopian Tube:" + Environment.NewLine +
                                "Dimensions: *LEFTFALLOPIANLENGTH/LEFTFALLOPIANDIAMETER*" + Environment.NewLine +
                                "Outer Surface: *LEFTFALLOPIANOUTERSURFACE*" + Environment.NewLine +
                                "Cut Surface: *LEFTFALLOPIANCUTSURFACE*" + Environment.NewLine +
                            "Ovary:" + Environment.NewLine +
                                "Dimensions: *LEFTOVARYMEASUREMENT*" + Environment.NewLine +
                                "Outer Surface: *LEFTOVARYOUTERSURFACE*" + Environment.NewLine +
                                "Cut Surface: *LEFTOVACRYCUTSURFACE*" + Environment.NewLine +
                          "Submitted: *SUBMISSIONKEY* Cassette: *COLOR* \"*CASSETTENUMBER*\".";


            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Uterine Corpus Weight", "*UTERINEWEIGHT*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Uterine Corpus Length", "*UTERINELENGTH*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Uterine Corpus Width", "*UTERINEWIDTH*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Uterine Corpus Thickness", "*UTERINETHICKNESS*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Serosa Description/Cul-de-sac", "*SEROSADESCRIPTION*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Serosa Cul-de-sac", "*CULDESACCOLOR/CULDESACDESCRIPTION*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Ectocervix Description", "ECTOCERVIXDESCRIPTION"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Os Measurement and Description", "*OSMEASUREMENT/OSDESCRIPTION*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Endocervical Canal Description", "*ENDOCERVICALCANALDESCRIPTION*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Endometrial Cavity Dimensions", "*ENDOMETRIALDIMENSION*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Endometrial Cavity Desription", "*ENDOMETRIALDESCRIPTION*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Endometrial Cavity Thickness", "*ENDOMETRIALTHICKNESS*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Myometrium Thickness", "*MYOMETRIUMTHICKNESS*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Myometrium Lesions", "*MYOMETRIUMLESIONS*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Right Fallopian Tube Measurement", "*RIGHTFALLOPIANLENGTH/RIGHTFALLOPIANDIAMETER*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Right Fallopian Tube OuterSurface", "*RIGHTFALLOPIANOUTERSURFACE*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Right Fallopian Tube CutSurface", "*RIGHTFALLOPIANCUTSURFACE*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Right Ovary Measurement", "*RIGHTOVARYMEASUREMENT*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Right Ovary OuterSurface", "*RIGHTOVARYOUTERSURFACE*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Right Ovary CutSurface", "*RIGHTOVACRYCUTSURFACE*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Left Fallopian Tube Measurement", "*LEFTFALLOPIANLENGTH/LEFTFALLOPIANDIAMETER*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Left Fallopian Tube OuterSurface", "*LEFTFALLOPIANOUTERSURFACE*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Left Fallopian Tube CutSurface", "*LEFTFALLOPIANCUTSURFACE*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Left Ovary Measurement", "*LEFTOVARYMEASUREMENT*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Left Ovary OuterSurface", "*LEFTOVARYOUTERSURFACE*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Left Ovary CutSurface", "*LEFTOVACRYCUTSURFACE*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Submission Key", "*SUBMISSIONKEY*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Color", "*COLOR*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Number", "*CASSETTENUMBER*"));

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.UterusAdnexa UterusAdnexa = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.UterusAdnexa();
            this.m_SpecimenCollection.Add(UterusAdnexa);
        }

    }
    public class NeedleCoreBiopsyTemplate : DictationTemplate
    {
        public NeedleCoreBiopsyTemplate()
        {
            this.m_TemplateName = "Needle Core Biopsy";
            this.m_Text = "Consists of *NUMBEROFFRAGMENTS* *COLOR* tissue fragments *MEASURING/AGGREGATING*. " +
                "The specimen is filtered through a fine mesh bag and entirely submitted to *COLOR* cassette \"*CASSETTENUMBER*\".";

            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Number Of Fragments", "*NUMBEROFFRAGMENTS*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Color of Tissue", "*Color*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Measuring/Agregating", "*MEASURING/AGGREGATING*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Color", "*COLOR*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Number", "*CASSETTENUMBER*"));

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.NeedleCoreBiopsy NeedleCoreBiopsy = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.NeedleCoreBiopsy();
            this.m_SpecimenCollection.Add(NeedleCoreBiopsy);
        }
    }
}