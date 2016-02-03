using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Specimen.Model;
using YellowstonePathology.Business.Test;

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
            this.m_TemplateName = "Prostate Needle Core";
            this.m_Text = "[identifier] and consists of [number] tan-pink cylindrical tissue fragments measuring [measurement].  " +
                "The specimen is entirely submitted in cassette [cassettelabel].  ";            

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.ProstateNeedleBiopsy nb = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.ProstateNeedleBiopsy();
            this.m_SpecimenCollection.Add(nb);
        }

        public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceCassetteLabel(result, specimenOrder);
            return result;
        }
    }

    public class ProstateTURTemplate : DictationTemplate
    {
        public ProstateTURTemplate()
        {
            this.m_TemplateName = "Prostate TUR";
            this.m_Text = "[identifier] and consists of [number] irregular ragged tan-pink tissue fragments " +
                "weighing [weight] and measuring [measurement] in aggregate.  [representativesections]. ";                

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.ProstateTUR tur = new Business.Specimen.Model.SpecimenDefinition.ProstateTUR();
            this.m_SpecimenCollection.Add(tur);
        }

        public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceRepresentativeSections(result, specimenOrder);
            return result;
        }
    }

    public class GITemplate : DictationTemplate
    {
        public GITemplate()
        {
            this.m_TemplateName = "GI Specimen";
            this.m_Text = "[identifier] and consists of [number] tan-pink tissue fragments measuring [measurement] in aggregate.  " +
                "The specimen is filtered through a fine mesh bag and entirely submitted in cassette [cassettelabel].  ";            

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.GIBiopsy gi = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.GIBiopsy();
            this.m_SpecimenCollection.Add(gi);
        }

        public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceCassetteLabel(result, specimenOrder);
            return result;
        }
    }

    public class FallopianTubeTemplate : DictationTemplate
    {
        public FallopianTubeTemplate()
        {
            this.m_TemplateName = "Fallopian Tube Specimen";
            this.m_Text = "[identifier]." + Environment.NewLine +
            	"Gross description:  [description]" + Environment.NewLine +
                "Fimbriated Ends:  [description]" + Environment.NewLine +
            	"Fallopian Tube 1:  [description], [measurement]" + Environment.NewLine +                          
				"Fallopian Tube 2:  [description], [measurement]" + Environment.NewLine +
                "Submitted:  Serially sectioned with representative sections submitted into cassette [cassettelabel].  ";

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.FallopianTube fallopianTube = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.FallopianTube();
            this.m_SpecimenCollection.Add(fallopianTube);
        }

        public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceCassetteLabel(result, specimenOrder);
            return result;
        }
    }

    public class SinusContentTemplate : DictationTemplate
    {
        public SinusContentTemplate()
        {
            this.m_TemplateName = "Sinus Content Specimen";
            this.m_Text = "[identifier] and consists of multiple fragments of tan-pink tissue and bone aggregating to [measurement].  " +
                "They are filtered through a fine mesh bag and entirely submitted in cassette [cassettelabel] for decalcification prior to processing.  ";

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.SinusContent sinusContent = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.SinusContent();
            this.m_SpecimenCollection.Add(sinusContent);
        }

        public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceCassetteLabel(result, specimenOrder);
            return result;
        }
    }

    public class AppendixExcisionTemplate : DictationTemplate
    {
        public AppendixExcisionTemplate()
        {
            this.m_TemplateName = "Appendix Excision";
            this.m_Text = "[identifier]." + Environment.NewLine +
                "Specimen Integrity:  [description]" + Environment.NewLine +
                "Measurements:  [measurement]" + Environment.NewLine +
                "Serosal Surface:  [description]" + Environment.NewLine +
                "Mesoappendix:  [measurement], [description]" + Environment.NewLine +
                "Pertinent Abnormalities:  [description]" + Environment.NewLine +
                "Luminal Contents:  [description]" + Environment.NewLine +
                "Fecaliths:  [description] [measurement], [description]" + Environment.NewLine +
                "Submitted:  [representativesections].  ";

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.AppendixExcision appendixExcision = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.AppendixExcision();
            this.m_SpecimenCollection.Add(appendixExcision);
        }

        public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceRepresentativeSections(result, specimenOrder);
            return result;
        }
    }

    public class SkinShavePunchMiscTemplate : DictationTemplate
    {
        public SkinShavePunchMiscTemplate()
        {
            this.m_TemplateName = "Skin Shave Punch and Misc Biopsy";
            this.m_Text = "[identifier]." + Environment.NewLine +
                "Gross Description:  [description]" + Environment.NewLine +
                "Measurements:  [measurements]" + Environment.NewLine +
                "Submitted:  [submitted].  ";            

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.SkinShavePunchMiscBiopsy skinShavePunchMiscBiopsy = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.SkinShavePunchMiscBiopsy();
            this.m_SpecimenCollection.Add(skinShavePunchMiscBiopsy);
        }

        public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceSubmitted(result, specimenOrder);
            return result;
        }
    }

    public class SkinShavewithCurettingsTemplate : DictationTemplate
    {
        public SkinShavewithCurettingsTemplate()
        {
            this.m_TemplateName = "Skin Shave with Curettings Biopsy";
            this.m_Text = "[identifier]." + Environment.NewLine +
                "Gross Description:  [description]; Curettings:  [description], [measurement]" + Environment.NewLine +
                "Measurements:  [measurements]" + Environment.NewLine +
                "Submitted:  [submitted].  ";

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.SkinShavewithCurettingsBiopsy skinShavewithCurettingsBiopsy = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.SkinShavewithCurettingsBiopsy();
            this.m_SpecimenCollection.Add(skinShavewithCurettingsBiopsy);
        }

        public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceSubmitted(result, specimenOrder);
            return result;
        }
    }

    public class SkinExcisionUnorientedTemplate : DictationTemplate
    {
        public SkinExcisionUnorientedTemplate()
        {
            this.m_TemplateName = "Skin Excision Biopsy, Unoriented";
            this.m_Text = "[identifier]." + Environment.NewLine +                
                "Gross Description:  Unoriented excision." + Environment.NewLine +
                "Measurements:  [measurements]" + Environment.NewLine +
                "Inking:  [color]" + Environment.NewLine +
                "Sectioning:  [description]" + Environment.NewLine +
                "Submitted:  [tipssubmitted].  ";

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.SkinExcisionUnorientedBiopsy skinExcisionUnorientedBiopsy = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.SkinExcisionUnorientedBiopsy();
            this.m_SpecimenCollection.Add(skinExcisionUnorientedBiopsy);
        }

        public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceTipsSubmitted(result, specimenOrder);
            return result;
        }
    }

    public class SkinExcisionOrientedTemplate : DictationTemplate
    {
        public SkinExcisionOrientedTemplate()
        {
            this.m_TemplateName = "Skin Excision Biopsy, Oriented";
            this.m_Text = "[identifier]." + Environment.NewLine +                
                "Gross Description:  Oriented excision." + Environment.NewLine +
                "Measurements:  [measurements]" + Environment.NewLine +
                "Orientation:  [designation]" + Environment.NewLine +
                "Inking:  12 to 3 o'clock = blue; 3 to 6 o'clock = red; 6 to 9 o'clock = green; 9 to 12 o'clock = orange; deep = black.  " + Environment.NewLine +
                "Sectioning:  [description]" + Environment.NewLine +
                "Submitted:  [tipssubmitted].  ";

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.SkinExcisionOrientedBiopsy skinExcisionOrientedBiopsy = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.SkinExcisionOrientedBiopsy();
            this.m_SpecimenCollection.Add(skinExcisionOrientedBiopsy);
        }

        public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceTipsSubmitted(result, specimenOrder);
            return result;
        }
    }
    
        public class SkinExcisionUnorientedwithCurettingsTemplate : DictationTemplate
    {
        public SkinExcisionUnorientedwithCurettingsTemplate()
        {
            this.m_TemplateName = "Skin Excision Biopsy with Curettings, Unoriented";
            this.m_Text = "[identifier]." + Environment.NewLine +                
                "Gross Description:  Unoriented excision with curettings." + Environment.NewLine +
                "Measurements:  [measurements]; Curettings: [measurements]" + Environment.NewLine +
                "Inking:  [color]" + Environment.NewLine +
                "Sectioning:  [description]" + Environment.NewLine +
                "Submitted:  [tipssubmittedwithcurettings].  ";

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.SkinExcisionUnorientedwithCurettingsBiopsy skinExcisionUnorientedwithCurettingsBiopsy = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.SkinExcisionUnorientedwithCurettingsBiopsy();
            this.m_SpecimenCollection.Add(skinExcisionUnorientedwithCurettingsBiopsy);
        }

        public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceTipsSubmittedWithCurettings(result, specimenOrder);
            return result;
        }
    }

    public class SkinExcisionOrientedwithCurettingsTemplate : DictationTemplate
    {
        public SkinExcisionOrientedwithCurettingsTemplate()
        {
            this.m_TemplateName = "Skin Excision Biopsy with Curettings, Oriented";
            this.m_Text = "[identifier]." + Environment.NewLine +                
                "Gross Description:  Oriented excision with curettings." + Environment.NewLine +
                "Measurements:  [measurements]; Curettings: [measurements]" + Environment.NewLine +
                "Orientation:  [designation]" + Environment.NewLine +
                "Inking:  12 to 3 o'clock = blue; 3 to 6 o'clock = red; 6 to 9 o'clock = green; 9 to 12 o'clock = orange; deep = black.  " + Environment.NewLine +
                "Sectioning:  [description]" + Environment.NewLine +
                "Submitted:  [tipssubmittedwithcurettings].  ";

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.SkinExcisionOrientedwithCurettingsBiopsy skinExcisionOrientedwithCurettingsBiopsy = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.SkinExcisionOrientedwithCurettingsBiopsy();
            this.m_SpecimenCollection.Add(skinExcisionOrientedwithCurettingsBiopsy);
        }

        public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceTipsSubmittedWithCurettings(result, specimenOrder);
            return result;
        }
    }

    public class GallbladderExcisionTemplate : DictationTemplate
    {
        public GallbladderExcisionTemplate()
        {
            this.m_TemplateName = "Gallbladder Excision";
            this.m_Text = "[identifier]." + Environment.NewLine +
                "Specimen Integrity:  [description]" + Environment.NewLine +
                "Measurements:  [measurement]" + Environment.NewLine +
                "Serosal Surface:  [description]" + Environment.NewLine +
                "Adventitial Surface:  [description]" + Environment.NewLine +
                "Mural Defects:  [description]" + Environment.NewLine +                          
                "Luminal Contents:  [description]" + Environment.NewLine +
                "Loose Stones:  [description]" + Environment.NewLine +
                "Mucosal Surface:  [description]" + Environment.NewLine +
                "Wall Thickness:  [thickness]" + Environment.NewLine +
                "Submitted:  [representativesections].  ";            

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.GallbladderExcision gallbladderExcision = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.GallbladderExcision();
            this.m_SpecimenCollection.Add(gallbladderExcision);
        }

        public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceRepresentativeSections(result, specimenOrder);
            return result;
        }
    }

    public class TonsilAdenoidExcisionTemplate : DictationTemplate
    {
        public TonsilAdenoidExcisionTemplate()
        {
            this.m_TemplateName = "Tonsil Adenoid Excision";
            this.m_Text = "[identifier]." + Environment.NewLine +
                "Tonsils:  Two tan-pink, lobular, palatine tonsils." + Environment.NewLine +
                "Adenoids:  [description]" + Environment.NewLine +
                "Weight:  [weight]" + Environment.NewLine +
                "Measurement Tonsil 1:  [measurement]" + Environment.NewLine +
                "Measurement Tonsil 2:  [measurement]" + Environment.NewLine +
                "Measurement Adenoids:  [measurement]" + Environment.NewLine +
                "Cut Surface:  [description]" + Environment.NewLine +
                "Submitted:  [representativesectionsagerestricted].  ";            

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.TonsilAdenoidExcision tonsilAdenoidExcision = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.TonsilAdenoidExcision();
            this.m_SpecimenCollection.Add(tonsilAdenoidExcision);
        }

        public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);            
            result = this.ReplaceRepresentativeSectionsAgeRestricted(result, specimenOrder, accessionOrder);
            return result;
        }
    }

    public class TonsilExcisionTemplate : DictationTemplate
    {
        public TonsilExcisionTemplate()
        {
            this.m_TemplateName = "Tonsil Excision";
            this.m_Text = "[identifier]." + Environment.NewLine +
                "Gross Description:  [quantity] tan-pink, lobular, palatine [tonsils]." + Environment.NewLine +
                "Weight:  [weight]" + Environment.NewLine +
                "[measurementstring]" + Environment.NewLine +                
                "Cut Surface:  [description]" + Environment.NewLine +
                "Submitted:  [representativesectionsagerestricted].  ";

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.TonsilExcision tonsilExcision = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.TonsilExcision();
            this.m_SpecimenCollection.Add(tonsilExcision);
        }

        public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            
            if (accessionOrder.SpecimenOrderCollection.Count == 2)
            {
                result = result.Replace("[quantity]", "One");
                result = result.Replace("[tonsils]", "tonsil");
                result = result.Replace("[measurementstring]", "Measurement:  [measurement]");
            }
            else
            {
                string measurementString = "Measurement Tonsil 1:  [measurement]" + Environment.NewLine +
                    "Measurement Tonsil 2:  [measurement]";
                result = result.Replace("[tonsils]", "tonsils");
                result = result.Replace("[quantity]", "Two");
                result = result.Replace("[measurementstring]", measurementString);
            }
            
            result = this.ReplaceRepresentativeSectionsAgeRestricted(result, specimenOrder, accessionOrder);
            return result;
        }
    }    

    public class AdenoidExcisionTemplate : DictationTemplate
    {
        public AdenoidExcisionTemplate()
        {
            this.m_TemplateName = "Adenoid Excision";
            this.m_Text = "[identifier]." + Environment.NewLine +
                "Gross Description:  [description]" + Environment.NewLine +
                "Weight:  [weight]" + Environment.NewLine +
                "Measurement Adenoids:  [measurement]" + Environment.NewLine +
                "Cut Surface:  [description]" + Environment.NewLine +
                "Submitted:  [representativesectionsagerestricted].  ";

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.AdenoidExcision adenoidExcision = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.AdenoidExcision();
            this.m_SpecimenCollection.Add(adenoidExcision);
        }

        public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceRepresentativeSectionsAgeRestricted(result, specimenOrder, accessionOrder);
            return result;
        }
    }

    public class POCTemplate : DictationTemplate
    {
        public POCTemplate()
        {
            this.m_TemplateName = "POC";
            this.m_Text = "[identifier]." + Environment.NewLine +
                "Gross Description:  [description]" + Environment.NewLine +
                "Measurement:  [measurement]" + Environment.NewLine +
                "Villi:  [description]" + Environment.NewLine +
                "Fetal Parts:  [description]" + Environment.NewLine +
                "Submitted:  [submitted].  ";            

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.POC poc = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.POC();
            this.m_SpecimenCollection.Add(poc);
        }

        public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceSubmitted(result, specimenOrder);
            return result;
        }
    }

    public class BreastReductionTemplate : DictationTemplate
    {
        public BreastReductionTemplate()
        {
            this.m_TemplateName = "BreastReduction";
            this.m_Text = "[identifier]." + Environment.NewLine +
                "Gross Description:  [description]" + Environment.NewLine +
                "Weight:  [weight]" + Environment.NewLine +
                "Measurement:  [measurement]" + Environment.NewLine +
                "Cut Surface:  [description]" + Environment.NewLine +
                "Submitted:  [submitted].  ";            

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.BreastReduction breastReduction = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.BreastReduction();
            this.m_SpecimenCollection.Add(breastReduction);
        }

        public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceSubmitted(result, specimenOrder);
            return result;
        }
    }

    public class ECCTemplate : DictationTemplate
    {
        public ECCTemplate()
        {
            this.m_TemplateName = "ECC";
            this.m_Text = "[identifier]." + Environment.NewLine +
                "Gross Description:   Multiple tan-pink fragments of tissue and mucus." + Environment.NewLine +
                "Measurement:  [measurement]" + Environment.NewLine +
                "Submitted:  Filtered through a fine mesh bag and entirely submitted in cassette [cassettelabel].  ";            

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.ECC ecc = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.ECC();
            this.m_SpecimenCollection.Add(ecc);
        }

        public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceCassetteLabel(result, specimenOrder);
            return result;
        }
    }

    public class EMBTemplate : DictationTemplate
    {
        public EMBTemplate()
        {
            this.m_TemplateName = "EMB";
            this.m_Text = "[identifier]." + Environment.NewLine +
                "Gross Description:  Multiple tan-pink fragments of tissue and mucus." + Environment.NewLine +
                "Measurement:  [measurement]" + Environment.NewLine +
                "Submitted:  Filtered through a fine mesh bag and entirely submitted in cassette [cassettelabel].  ";            

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.EMB emb = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.EMB();
            this.m_SpecimenCollection.Add(emb);
        }

        public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceCassetteLabel(result, specimenOrder);
            return result;
        }
    }

    public class CervicalBiopsyTemplate : DictationTemplate
    {
        public CervicalBiopsyTemplate()
        {
            this.m_TemplateName = "CervicalBiopsy";
            this.m_Text = "[identifier]." + Environment.NewLine +
                "Gross Description:  [number], tan-pink, rubbery fragment[s]." + Environment.NewLine +
                "Measurement:  [measurement]" + Environment.NewLine +
                "Submitted:  Filtered through a fine mesh bag and entirely submitted in cassette [cassettelabel].  ";

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.CervicalBiopsy cervicalBiopsy = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.CervicalBiopsy();
            this.m_SpecimenCollection.Add(cervicalBiopsy);
        }

        public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceCassetteLabel(result, specimenOrder);
            return result;
        }
    }

    public class LEEPConeTemplate : DictationTemplate
    {
        public LEEPConeTemplate()
        {
            this.m_TemplateName = "LEEPCone";
            this.m_Text = "[identifier]." + Environment.NewLine +
                "Gross Description:  [color], [characteristics]" + Environment.NewLine +
                "Measurement:  [measurement]" + Environment.NewLine +
                "Os:  [description], [measurement]" + Environment.NewLine +
                "Inking:  [description]" + Environment.NewLine +
                "Submitted:  [submitted].  ";            

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.LEEPCone leepCone = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.LEEPCone();
            this.m_SpecimenCollection.Add(leepCone);
        }

        public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceSubmitted(result, specimenOrder);
            return result;
        }
    }

    public class LEEPPiecesTemplate : DictationTemplate
    {
        public LEEPPiecesTemplate()
        {
            this.m_TemplateName = "LEEPPieces";
            this.m_Text = "[identifier]." + Environment.NewLine +
                "Gross Description:  [color], [characteristics]" + Environment.NewLine +
                "Measurement:  [measurement]" + Environment.NewLine +
                "Os:  [description], [measurement]" + Environment.NewLine +
                "Inking:  [description]" + Environment.NewLine +
                "Submitted:  [submitted].  ";            

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.LEEPPieces leepPieces = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.LEEPPieces();
            this.m_SpecimenCollection.Add(leepPieces);
        }

        public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceSubmitted(result, specimenOrder);
            return result;
        }
    }

    public class SinglePlacentaTemplate : DictationTemplate
    {
        public SinglePlacentaTemplate()
        {
            this.m_TemplateName = "SinglePlacenta";
            this.m_Text = "[identifier]." + Environment.NewLine +
                Environment.NewLine +
                "Umbilical Cord:" + Environment.NewLine +
                "   Length:  [length]" + Environment.NewLine +
                "   Insertion:  [insertion]" + Environment.NewLine +
                "   Vessels:  [number]" + Environment.NewLine +
                "   Knots:  [description]" + Environment.NewLine +
                "   Coiling:  [description]" + Environment.NewLine +
                "   Other:  [description]" + Environment.NewLine +
                Environment.NewLine +
                "Extraplacental Membranes:" + Environment.NewLine +
                "   Insertion:  [insertion]" + Environment.NewLine +
                "   Color:  [color]" + Environment.NewLine +
                "   Amnion Nodosum:  [description]" + Environment.NewLine +
                "   Cysts:  [description]" + Environment.NewLine +
                "   Point of Rupture:  [description]" + Environment.NewLine +
                Environment.NewLine +
                "Placental Disc:" + Environment.NewLine +
                "   Weight:  [weight]" + Environment.NewLine +
                "   Shape:  [shape]" + Environment.NewLine +
                "   Measurements:  [measurement]" + Environment.NewLine +
                "   Fetal Surface:" + Environment.NewLine +
                "      Fetal Surface:  [description]" + Environment.NewLine +
                "      Amnion Nodosum:  [description]" + Environment.NewLine +
                "      Cysts:  [description]" + Environment.NewLine +
                "   Maternal Surface:" + Environment.NewLine +
                "      Hemorrhage:  [description]" + Environment.NewLine +
                "         Adherent:  [measurement], [description]" + Environment.NewLine +
                "         Non-Adherent:  [measurement]" + Environment.NewLine +
                "      Maternal Surface:  [description]" + Environment.NewLine +
                "      Infarcts:  [description]" + Environment.NewLine +
                "      Other:  [description]" + Environment.NewLine +
                "   Sectioned at 1 cm intervals:" + Environment.NewLine +
                "       Surface Infarcts:  [description]" + Environment.NewLine +
                "       Cysts:  [description]" + Environment.NewLine + 
            	Environment.NewLine +
                "Cassette Summary: " + Environment.NewLine +
                "\"1A\" - Cord from both ends, " + Environment.NewLine +
                "\"1B\" - Membranes, " + Environment.NewLine +
                "\"1C\" - \"1D\" - Central placenta.  ";            

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.SinglePlacenta singlePlacenta = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.SinglePlacenta();
            this.m_SpecimenCollection.Add(singlePlacenta);
        }

        public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            return base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
        }
    }

    public class UterusTemplate : DictationTemplate
    {
        public UterusTemplate()
        {
            this.m_TemplateName = "Uterus";
            this.m_Text = "[identifier]." + Environment.NewLine +                
                Environment.NewLine +
                "Uterine Corpus:" + Environment.NewLine +
                "   Weight:  [weight]" + Environment.NewLine +
                "   Dimensions:  [measurements]" + Environment.NewLine +
                "Serosa:" + Environment.NewLine +
                "   Uterine Serosa:  [color], [description]" + Environment.NewLine +
                "   Cul-de-sac Serosa:  [color], [description]" + Environment.NewLine +
                "Cervix:" + Environment.NewLine +
                "   Measurement:  [measurement] " + Environment.NewLine +
                "   Os:  [description], [measurement]" + Environment.NewLine +
                "   Ectocervical Mucosa:  [description]" + Environment.NewLine +
                "Sectioning of Uterus and Cervix:" + Environment.NewLine +
                "   Endocervical Canal:  [description]" + Environment.NewLine +                
                "   Endometrial Cavity:" + Environment.NewLine +
                "      Dimensions:  [measurement]" + Environment.NewLine +
                "      Description:  [description]" + Environment.NewLine +
                "      Endometrial Thickness:  [thickness]" + Environment.NewLine +
                "   Myometrium:" + Environment.NewLine +
                "      Myometrial Thickness:  [thickness]" + Environment.NewLine +                
                "      Lesions in Myometrium:  [description]" + Environment.NewLine +
                Environment.NewLine +
                "[summarysubmission].  ";            

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.Uterus uterus = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.Uterus();
            this.m_SpecimenCollection.Add(uterus);
        }

        public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceSummarySubmission(result, specimenOrder);
            return result;
        }
    }

    public class UterusAdnexaTemplate : DictationTemplate
    {
        public UterusAdnexaTemplate()
        {
            this.m_TemplateName = "Uterus with Adnexa";
            this.m_FontSize = 16;
            this.m_Text = "[identifier]." + Environment.NewLine +
                Environment.NewLine +
                "Right Adnexal Organs:" + Environment.NewLine +
                "   Fallopian Tube: " + Environment.NewLine +
                "      Dimensions:  [measurements]([w/wo] fimbriated ends)" + Environment.NewLine +
                "      Surface:  [description/paratubal cysts absent/present]" + Environment.NewLine +
                "      Cut Surface:  [patent/stenotic lumen]" + Environment.NewLine +
                "   Ovary:" + Environment.NewLine +
                "      Dimensions:  [measurement]" + Environment.NewLine +
                "      Surface:  [description]" + Environment.NewLine +
                "      Cut Surface:  [description]" + Environment.NewLine +
                "Left Adnexal Organs:" + Environment.NewLine +
                "   Fallopian Tube: " + Environment.NewLine +
                "      Dimensions:  [measurements]([w/wo] fimbriated ends)" + Environment.NewLine +
                "      Surface:  [description/paratubal cysts absent/present]" + Environment.NewLine +
                "      Cut Surface:  [patent/stenotic lumen]" + Environment.NewLine +
                "   Ovary:" + Environment.NewLine +
                "      Dimensions:  [measurement]" + Environment.NewLine +
                "      Surface:  [description]" + Environment.NewLine +
                "      Cut Surface:  [description]" + Environment.NewLine +
                "Uterine Corpus:" + Environment.NewLine +
                "   Weight:  [weight]" + Environment.NewLine +
                "   Dimensions: [measurements]" + Environment.NewLine +
                "   Serosa:" + Environment.NewLine +
                "      Uterine Serosa Description:  [description]" + Environment.NewLine +
                "      Cul-de-sac Description:  [description]" + Environment.NewLine +
                "Cervix:" + Environment.NewLine +
                "   Measurement:  [measurement] " + Environment.NewLine +
                "   Os:  [description], [measurement]" + Environment.NewLine +
                "   Ectocervical Mucosa:  [description]" + Environment.NewLine +
                "Sectioning of Uterus and Cervix:" + Environment.NewLine +
                "   Endocervical Canal:  [description]" + Environment.NewLine +
                "   Endocervical Mucosa:  [description]" + Environment.NewLine +
                "   Endometrial Cavity:" + Environment.NewLine +
                "      Dimensions:  [measurement]" + Environment.NewLine +
                "      Description:  [description]" + Environment.NewLine +
                "      Endometrial Thickness:  [thickness]" + Environment.NewLine +
                "   Myometrium:" + Environment.NewLine +
                "      Myometrial Thickness:  [thickness]" + Environment.NewLine +                
                "      Lesions in Myometrium:  [description]" + Environment.NewLine +
                Environment.NewLine +
                "[summarysubmission].  ";
            

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.UterusAdnexa uterusAdnexa = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.UterusAdnexa();
            this.m_SpecimenCollection.Add(uterusAdnexa);
        }

        public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceSummarySubmission(result, specimenOrder);
            return result;
        }
    }

    public class NeedleCoreBiopsyTemplate : DictationTemplate
    {
        public NeedleCoreBiopsyTemplate()
        {
            this.m_TemplateName = "Needle Core Biopsy";
            this.m_Text = "[identifier] and consists of [number] [color] threadlike tissue fragments measuring [measurement] in aggregate.  " +
                "They are filtered through a fine mesh bag and entirely submitted in cassette [cassettelabel].  ";            

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.NeedleCoreBiopsy needleCoreBiopsy = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.NeedleCoreBiopsy();
            this.m_SpecimenCollection.Add(needleCoreBiopsy);
        }

        public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceCassetteLabel(result, specimenOrder);
            return result;
        }
    }

    public class AorticValveTemplate : DictationTemplate
    {
        public AorticValveTemplate()
        {
            this.m_TemplateName = "Aortic Valve";            

            this.m_Text = "[identifier]." + Environment.NewLine +
                "Gross Description:  [description]" + Environment.NewLine +
                "Measurements:  [measurements]" + Environment.NewLine +
                "Calcification:  [present/not present]" + Environment.NewLine +
                "Vegetation:  [present/not present]" + Environment.NewLine +
                "Submitted:  [representativesections] for decalcification prior to processing.  ";

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.AorticValve aorticValve = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.AorticValve();
            this.m_SpecimenCollection.Add(aorticValve);
        }

        public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceRepresentativeSections(result, specimenOrder);
            return result;
        }
    }

    public class MitralValveTemplate : DictationTemplate
    {
        public MitralValveTemplate()
        {
            this.m_TemplateName = "Mitral Valve";            

            this.m_Text = "[identifier]." + Environment.NewLine +
               "Leaflets Description:  [description]" + Environment.NewLine +
               "Measurements:  [measurements]" + Environment.NewLine +
               "Chordae:  [description]" + Environment.NewLine +               
               "Submitted:  [representativesections] for decalcification prior to processing.  ";

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.MitralValve mitralValve = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.MitralValve();
            this.m_SpecimenCollection.Add(mitralValve);
        }

        public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceRepresentativeSections(result, specimenOrder);
            return result;
        }
    }

    public class KneeTissueTemplate : DictationTemplate
    {
        public KneeTissueTemplate()
        {
            this.m_TemplateName = "Knee Tissue";
            this.m_Text = "[identifier] and consists of [number] fragments of soft tissue and bone aggregating to [measurement].  " +
                "Recognizable tibial plateau and femoral condyles [are/not] identified. The articular surface shows [description] eburnation" +
                "and [description] osteophyte formation. Sectioning reveals tan-yellow trabecular bone.  " + 
                "Sections are not submitted for microscopic evaluation.  ";            

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.KneeTissue kneeTissue = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.KneeTissue();
            this.m_SpecimenCollection.Add(kneeTissue);
        }

        public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            return base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
        }
    }

    public class FemoralHeadTemplate : DictationTemplate
    {
        public FemoralHeadTemplate()
        {
            this.m_TemplateName = "Femoral Head";
            this.m_Text = "[identifier] and consists of a [description] femoral head with attached femoral neck measuring [measurement].  " +
                "The femoral neck margin is [description]. The articular surface shows [percent] eburnation and [percent] osteophyte formation.  " +
                "Sectioning reveals tan-yellow trabecular bone. [representativesections] following overnight decalcification.  ";            

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.FemoralHead femoralHead = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.FemoralHead();
            this.m_SpecimenCollection.Add(femoralHead);
        }

        public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceRepresentativeSections(this.m_Text, specimenOrder);
            return result;
        }
    }
}