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
            this.m_Text = "Consists of [number] tan-pink cylindrical tissue fragments [measurement]. " +
                "The specimen is filtered through a fine mesh bag and entirely submitted in cassette \"label\".";            

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.ProstateNeedleBiopsy nb = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.ProstateNeedleBiopsy();
            this.m_SpecimenCollection.Add(nb);
        }
    }

    public class ProstateTURTemplate : DictationTemplate
    {
        public ProstateTURTemplate()
        {
            this.m_TemplateName = "Prostate Specimen";
            this.m_Text = "Consists of [number] tan-pink irregularly shaped, rough and ragged tissue fragments(s)" +
                "weighing in aggregate [weight], and aggregating to [measurement]. " +
                "The specimen is submitted [submission] in cassette \"[label]\"";
                        
            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.ProstateTUR tur = new Business.Specimen.Model.SpecimenDefinition.ProstateTUR();
            this.m_SpecimenCollection.Add(tur);
        }
    }

    public class GITemplate : DictationTemplate
    {
        public GITemplate()
        {
            this.m_TemplateName = "GI Specimen";
            this.m_Text = "Consists of [number] fragments of tan-pink tissue measuring in aggregate [measurement]. " +
                "The specimen is filtered through a fine mesh bag and entirely submitted in cassette \"[label]\".";            

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.GIBiopsy gi = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.GIBiopsy();
            this.m_SpecimenCollection.Add(gi);
        }
    }

    public class FallopianTubeTemplate : DictationTemplate
    {
        public FallopianTubeTemplate()
        {
            this.m_TemplateName = "Fallopian Tube Specimen";
            this.m_Text = "Consists of a tan tan-pink segment of fallopian tube [verb] fimbriated ends measuring [length] in length, and up to [diameter] in diameter. " +
                "The specimen is serially sectioned revealing a small empty lumen lined by homogeneous tan mucosa without lesions. Representative sections are submitted in cassette \"[label]\".";            

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.FallopianTube ft = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.FallopianTube();
            this.m_SpecimenCollection.Add(ft);
        }
    }

    public class SinusContentTemplate : DictationTemplate
    {
        public SinusContentTemplate()
        {
            this.m_TemplateName = "Sinus Content Specimen";
            this.m_Text = "Consists of multiple tan-pink tissue and bone fragments aggregating to [measurement]. " +
                "The tissue is filtered through a fine mesh bag and submitted in cassette \"[label]\".";            

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.SinusContent sc = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.SinusContent();
            this.m_SpecimenCollection.Add(sc);
        }
    }

    public class AppendixExcisionTemplate : DictationTemplate
    {
        public AppendixExcisionTemplate()
        {
            this.m_TemplateName = "Appendix Excision";
            this.m_Text = "Received: [fixative]" + Environment.NewLine +
                          "Gross Description: [description] Measuring: [measurement]" + Environment.NewLine +
                          "Serosal Surface: [description]" + Environment.NewLine +
                          "Mesoappendix: [measurement], [description]" + Environment.NewLine +
                          "Pertinent Abnormalities: [???]" + Environment.NewLine +
                          "Luminal Contents: [???]" + Environment.NewLine +
                          "Fecalith: [???] [measurement], [description]" + Environment.NewLine +
                          "Submitted: *\"[label]\".";            

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.AppendixExcision appendixExcision = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.AppendixExcision();
            this.m_SpecimenCollection.Add(appendixExcision);
        }
    }

    public class SkinShavePunchMiscTemplate : DictationTemplate
    {
        public SkinShavePunchMiscTemplate()
        {
            this.m_TemplateName = "Skin Shave Punch and Misc Biopsy";
            this.m_Text = "Received: [fixative]" + Environment.NewLine +
                          "Gross Description: [number], [???]" + Environment.NewLine +
                          "Measurements: [measurement]" + Environment.NewLine +
                          "Submitted: \"[label]\".";            

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.SkinShavePunchMiscBiopsy SkinShavePunchMiscBiopsy = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.SkinShavePunchMiscBiopsy();
            this.m_SpecimenCollection.Add(SkinShavePunchMiscBiopsy);
        }
    }

    public class SkinExcisionTemplate : DictationTemplate
    {
        public SkinExcisionTemplate()
        {
            this.m_TemplateName = "Skin Excision Biopsy";
            this.m_Text = "Received: [fixative]" + Environment.NewLine +
                          "Gross Description: [number], [???], [???]" + Environment.NewLine +
                          "Measurements: [measurement]" + Environment.NewLine +
                          "Submitted: \"[label]\".";            

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.SkinExcisionBiopsy skinexcisionBiopsy = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.SkinExcisionBiopsy();
            this.m_SpecimenCollection.Add(skinexcisionBiopsy);
        }
    }

    public class GallbladderExcisionTemplate : DictationTemplate
    {
        public GallbladderExcisionTemplate()
        {
            this.m_TemplateName = "Gallbladder Excision";
            this.m_Text = "Received: [fixative]" + Environment.NewLine +
                          "Loose Stones: [???]" + Environment.NewLine +
                          "Gross Description: [description]" + Environment.NewLine +
                          "Measurements: [measurement]" + Environment.NewLine +
                          "Serosal Surface: [description]" + Environment.NewLine +
                          "Adventitial Surface: [description]" + Environment.NewLine +
                          "Mural Defects: [???]" + Environment.NewLine +
                          "Luminal Contents: [???]" + Environment.NewLine +
                          "Mucosal Surface: [description], Wall Thickness: [thickness]" + Environment.NewLine +
                          "Submitted: \"[label]\".";            

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.GallbladderExcision gallbladderExcision = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.GallbladderExcision();
            this.m_SpecimenCollection.Add(gallbladderExcision);
        }
    }

    public class TonsilExcisionTemplate : DictationTemplate
    {
        public TonsilExcisionTemplate()
        {
            this.m_TemplateName = "Tonsil Excision";
            this.m_Text = "Received: [fixative]" + Environment.NewLine +
                          "Gross Description: [description]" + Environment.NewLine +
                          "Adenoids: [???]" + Environment.NewLine +
                          "Weight: [weight]" + Environment.NewLine +
                          "Measurement Tonsil 1: [measurement]" + Environment.NewLine +
                          "Measurement Tonsil 2: [measurement]" + Environment.NewLine +
                          "[Measurement of Adenoids: [meaasurement]]" + Environment.NewLine +
                          "Cut Surfaces: [???]" + Environment.NewLine +
                          "Submitted: \"[label]\".";            

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.TonsilExcision TonsilExcision = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.TonsilExcision();
            this.m_SpecimenCollection.Add(TonsilExcision);
        }
    }

    public class POCTemplate : DictationTemplate
    {
        public POCTemplate()
        {
            this.m_TemplateName = "POC";
            this.m_Text = "Received: [fixative]" + Environment.NewLine +
                          "Gross Description: [description]" + Environment.NewLine +
                          "Measurement: [measurement]" + Environment.NewLine +
                          "Villi: [???]" + Environment.NewLine +
                          "Fetal Parts: []" + Environment.NewLine +
                          "Submitted: \"[label]\".";            

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.POC POC = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.POC();
            this.m_SpecimenCollection.Add(POC);
        }
    }

    public class BreastReductionTemplate : DictationTemplate
    {
        public BreastReductionTemplate()
        {
            this.m_TemplateName = "BreastReduction";
            this.m_Text = "Received: [fixative]" + Environment.NewLine +
                          "Gross Description: [description]" + Environment.NewLine +
                          "Weight: [weight]" + Environment.NewLine +
                          "Measurement: [measurement]" + Environment.NewLine +
                          "Submitted: \"[label]\".";            

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.BreastReduction BreastReduction = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.BreastReduction();
            this.m_SpecimenCollection.Add(BreastReduction);
        }
    }

    public class ECCTemplate : DictationTemplate
    {
        public ECCTemplate()
        {
            this.m_TemplateName = "ECC";
            this.m_Text = "Received: [fixative]" + Environment.NewLine +
                          "Gross Description: [number], [color], [consistency]" + Environment.NewLine +
                          "Measurement: [measurement]" + Environment.NewLine +
                          "Submitted: \"[label]\".";            

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.ECC ECC = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.ECC();
            this.m_SpecimenCollection.Add(ECC);
        }
    }

    public class EMBTemplate : DictationTemplate
    {
        public EMBTemplate()
        {
            this.m_TemplateName = "EMB";
            this.m_Text = "Received: [fixative]" + Environment.NewLine +
                          "Gross Description: [number], [color], [consistency]" + Environment.NewLine +
                          "Measurement: [measurement]" + Environment.NewLine +
                          "Submitted: \"[label]\".";            

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.EMB EMB = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.EMB();
            this.m_SpecimenCollection.Add(EMB);
        }
    }

    public class CervicalBiopsyTemplate : DictationTemplate
    {
        public CervicalBiopsyTemplate()
        {
            this.m_TemplateName = "CervicalBiopsy";
            this.m_Text = "Received: [fixative]" + Environment.NewLine +
                          "Gross Description: [number], [color], [consistency]" + Environment.NewLine +
                          "Measurement: [measurement]" + Environment.NewLine +
                          "Inked: [???]" + Environment.NewLine +
                          "Submitted: \"[label]\".";            

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.CervicalBiopsy CervicalBiopsy = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.CervicalBiopsy();
            this.m_SpecimenCollection.Add(CervicalBiopsy);
        }
    }

    public class LEEPConeTemplate : DictationTemplate
    {
        public LEEPConeTemplate()
        {
            this.m_TemplateName = "LEEPCone";
            this.m_Text = "Received: [fixative]" + Environment.NewLine +
                          "Gross Description: [description], [color], [characteristics]" + Environment.NewLine +
                          "Measurement: [measurement]" + Environment.NewLine +
                          "OS: *OSDESCRIPTION* [measurement]" + Environment.NewLine +
                          "Endocervical Inked: [???]" + Environment.NewLine +
                          "Cut Surface Inking: [???]" + Environment.NewLine +
                          "Submitted: \"[label]\".";            

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.LEEPCone LEEPCone = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.LEEPCone();
            this.m_SpecimenCollection.Add(LEEPCone);
        }
    }

    public class LEEPPiecesTemplate : DictationTemplate
    {
        public LEEPPiecesTemplate()
        {
            this.m_TemplateName = "LEEPPieces";
            this.m_Text = "Received: [fixative]" + Environment.NewLine +
                          "Gross Description: [number], [color], [measurement]" + Environment.NewLine +
                          "OS Description: [shape], [location], [measurement]" + Environment.NewLine +
                          "Inked: [???]" + Environment.NewLine +
                          "Submitted: \"[label]\".";            

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.LEEPPieces LEEPPieces = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.LEEPPieces();
            this.m_SpecimenCollection.Add(LEEPPieces);
        }
    }

    public class SinglePlacentaTemplate : DictationTemplate
    {
        public SinglePlacentaTemplate()
        {
            this.m_TemplateName = "SinglePlacenta";
            this.m_Text = "Received: [fixative]" + Environment.NewLine +
                          "Umbilical Cord:" + Environment.NewLine +
                                "Length: [length]" + Environment.NewLine +
                                "Insertion: [insertion]" + Environment.NewLine +
                                "Vessels: [number]" + Environment.NewLine +
                                "Knots: [???]" + Environment.NewLine +
                                "Coiling: [???]" + Environment.NewLine +
                                "Other: [???]" + Environment.NewLine +
                          "Extraplacental Membranes:" + Environment.NewLine +
                                "Insertion: [insertion]" + Environment.NewLine +
                                "Color: [color]" + Environment.NewLine +
                                "Amnion Nodosum: [???]" + Environment.NewLine +
                                "Cysts: [???]" + Environment.NewLine +
                                "Point of Rupture: [???]" + Environment.NewLine +
                          "Placental Disc:" + Environment.NewLine +
                                "Weight: [weight]" + Environment.NewLine +
                                "Shape: [shape]" + Environment.NewLine + 
                                "Measurements: [measurement]" + Environment.NewLine +
                                "Fetal Surface:" + Environment.NewLine +
                                    "Fetal Surface: [description]" + Environment.NewLine +
                                    "Amnion Nodosum: [???]" + Environment.NewLine +
                                "Maternal Surface:" + Environment.NewLine +
                                    "Hemorrhage: [???]" + Environment.NewLine +
                                        "[Adherent: [measurement], [???]]" + Environment.NewLine +
                                        "[Non-Adherent: [measurement]]" + Environment.NewLine +
                                    "Maternal Surface: *SURFACE*" + Environment.NewLine +
                                    "Infacts: [???]" + Environment.NewLine +
                                    "Other: [???]" + Environment.NewLine +
                          "Submitted: \"[label]\".";            

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.SinglePlacenta SinglePlacenta = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.SinglePlacenta();
            this.m_SpecimenCollection.Add(SinglePlacenta);
        }
    }

    public class UterusTemplate : DictationTemplate
    {
        public UterusTemplate()
        {
            this.m_TemplateName = "Uterus";
            this.m_Text = "Received: [fixative]" + Environment.NewLine +
                          "Uterus and Cervix:" + Environment.NewLine +
                            "Uterine Corpus:" + Environment.NewLine +
                                "Weight: [weight]" + Environment.NewLine +
                                "Length: [length]" + Environment.NewLine +
                                "Width: [width]" + Environment.NewLine +
                                "Thickness: [thickness]" + Environment.NewLine +
                            "Serosa:" + Environment.NewLine +
                                "Description: [description]" + Environment.NewLine +
                                "Cul-de-sac: [color], [description]" + Environment.NewLine +
                            "Cervix:" + Environment.NewLine +
                                "Ectocervix: [description]" + Environment.NewLine +
                                "Os: [description], [measurement]" + Environment.NewLine +
                            "Sectioning of Uterus and Cervix:" + Environment.NewLine +
                                "Endocervical Canal: [description], [measurement]" + Environment.NewLine +
                                "Endometrial Cavity:" + Environment.NewLine +
                                    "Dimensions: [measurement]" + Environment.NewLine +
                                    "Description: [description]" + Environment.NewLine +
                                    "Average Endometrial Thickness: [thickness]" + Environment.NewLine +
                                "Myometrium:" + Environment.NewLine +
                                    "Average Myometrial Thickness: [thickness]" + Environment.NewLine +
                                    "Lesion in Uterine wall: [???]" + Environment.NewLine +
                          "Submitted: \"[label]\".";            

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.Uterus Uterus = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.Uterus();
            this.m_SpecimenCollection.Add(Uterus);
        }

    }

    public class UterusAdnexaTemplate : DictationTemplate
    {
        public UterusAdnexaTemplate()
        {
            this.m_TemplateName = "Uterus with Adnexa";
            this.m_Text = "Received: [fixative]" + Environment.NewLine +
                          "Uterus and Cervix:" + Environment.NewLine +
                            "Uterine Corpus:" + Environment.NewLine +
                                "Weight: [weight]" + Environment.NewLine +
                                "Length: [length]" + Environment.NewLine +
                                "Width: [width]" + Environment.NewLine +
                                "Thickness: [thickness]" + Environment.NewLine +
                            "Serosa:" + Environment.NewLine +
                                "Description: [description]" + Environment.NewLine +
                                "Cul-de-sac: [color], [description]" + Environment.NewLine +
                            "Cervix:" + Environment.NewLine +
                                "Ectocervix: [description]" + Environment.NewLine +
                                "Os: [description], [measurement]" + Environment.NewLine +
                            "Sectioning of Uterus and Cervix:" + Environment.NewLine +
                                "Endocervical Canal: [description], [measurement]" + Environment.NewLine +
                                "Endometrial Cavity:" + Environment.NewLine +
                                    "Dimensions: [measurement]" + Environment.NewLine +
                                    "Description: [description]" + Environment.NewLine +
                                    "Average Endometrial Thickness: [thickness]" + Environment.NewLine +
                                "Myometrium:" + Environment.NewLine +
                                    "Average Myometrial Thickness: [thickness]" + Environment.NewLine +
                                    "Lesion in Uterine wall: [???]" + Environment.NewLine +
                          "Right Adnexal Organs:" + Environment.NewLine +
                            "Fallopian Tube:" + Environment.NewLine +
                                "Dimensions: [???], [length], [diameter]" + Environment.NewLine +
                                "Outer Surface: [description]" + Environment.NewLine +
                                "Cut Surface: [description]" + Environment.NewLine +
                            "Ovary:" + Environment.NewLine +
                                "Dimensions: [measurement]" + Environment.NewLine +
                                "Outer Surface: [decription]" + Environment.NewLine +
                                "Cut Surface: [description]" + Environment.NewLine +
                          "Left Adnexal Organs:" + Environment.NewLine +
                            "Fallopian Tube:" + Environment.NewLine +
                                "Dimensions: [???], [length], [diameter]" + Environment.NewLine +
                                "Outer Surface: [description]" + Environment.NewLine +
                                "Cut Surface: [description]" + Environment.NewLine +
                            "Ovary:" + Environment.NewLine +
                                "Dimensions: [measurement]" + Environment.NewLine +
                                "Outer Surface: [description]" + Environment.NewLine +
                                "Cut Surface: [description]" + Environment.NewLine +
                          "Submitted: \"[label]\".";
            

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.UterusAdnexa UterusAdnexa = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.UterusAdnexa();
            this.m_SpecimenCollection.Add(UterusAdnexa);
        }

    }

    public class NeedleCoreBiopsyTemplate : DictationTemplate
    {
        public NeedleCoreBiopsyTemplate()
        {
            this.m_TemplateName = "Needle Core Biopsy";
            this.m_Text = "Consists of [number] [color] tissue fragments measuring [measurement] in aggregate. " +
                "The specimen is filtered through a fine mesh bag and entirely submitted in cassette \"[label]\".";            

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.NeedleCoreBiopsy NeedleCoreBiopsy = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.NeedleCoreBiopsy();
            this.m_SpecimenCollection.Add(NeedleCoreBiopsy);
        }
    }

    public class AorticValveTemplate : DictationTemplate
    {
        public AorticValveTemplate()
        {
            this.m_TemplateName = "Aortic Valve";
            this.m_Text = "The specimen consists of [number] valve cusps, aggregating to [measurement]." +
                "Vegetations [???] identified. The valve cusps are [description]." +
                "Representative section are submitted in cassette \"[label]\". ";            

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.AorticValve AorticValve = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.AorticValve();
            this.m_SpecimenCollection.Add(AorticValve);
        }
    }

    public class MitralValveTemplate : DictationTemplate
    {
        public MitralValveTemplate()
        {
            this.m_TemplateName = "Mitral Valve";
            this.m_Text = "The specimen consists of [number] of valve leaflets with attached chordae tendina aggregating to [measurement]." +
                "Vegetations [???] identified. The valve leaflets are [description]." +
                "Representative section are submitted in cassette \"[label]\". ";            

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.MitralValve MitralValve = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.MitralValve();
            this.m_SpecimenCollection.Add(MitralValve);
        }
    }

    public class KneeTissueTemplate : DictationTemplate
    {
        public KneeTissueTemplate()
        {
            this.m_TemplateName = "Knee Tissue";
            this.m_Text = "The specimen consists of [number] of bone and soft tissue aggregating to [measurement]." +
                "Recognizable tibial plateau and femoral condyles [are/not] identified. The articular surface shows [description] eburation" +
                "and [description] osteophyte formation. Sectioning reveals tan-yellow trabecular bone." + 
                "Sections are not submitted for microscopic evaluation. ";            

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.KneeTissue KneeTissue = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.KneeTissue();
            this.m_SpecimenCollection.Add(KneeTissue);
        }
    }

    public class FemoralHeadTemplate : DictationTemplate
    {
        public FemoralHeadTemplate()
        {
            this.m_TemplateName = "Femoral Head";
            this.m_Text = "The specimen consists of a [description] femoral head with attached femoral neck measuring [measurement]." +
                "The femoral neck has a [description]. There is [percent] eburation and [percent] osteophyte formation." +
                "Sectioning reveals tan-yellow trabecual bone. [???]. A representative section is submitted in cassette \"[label]\"" +
                "following overnight decalcification. ";            

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.FemoralHead FemoralHead = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.FemoralHead();
            this.m_SpecimenCollection.Add(FemoralHead);
        }
    }
}