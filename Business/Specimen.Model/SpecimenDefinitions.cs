using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Specimen.Model.SpecimenDefinition
{
    public class NullSpecimen : Specimen
    {
        public NullSpecimen()
        {
            this.m_SpecimenId = null;
            this.m_SpecimenName = null;
            this.m_Description = null;
        }
    }    

    public class ThinPrepFluid : Specimen
    {
        public ThinPrepFluid()
        {
            this.m_SpecimenId = "SPCMNTHNPRPFLD";
            this.m_SpecimenName = "Thin Prep Fluid";
            this.m_Description = "Thin Prep Fluid";
            this.m_LabFixation = "PreservCyt";
            this.m_ClientFixation = "PreservCyt";
            this.m_RequiresGrossExamination = false;
        }        
    }

    public class GIBiopsy : Specimen
    {
        public GIBiopsy()
        {
            this.m_SpecimenId = "GISPCMN";
            this.m_SpecimenName = "GI Biopsy";
            this.m_Description = null;
            this.m_LabFixation = "Formalin";
            this.m_ClientFixation = "Formalin";
            this.m_RequiresGrossExamination = true;
        }
    }
 
    public class SkinShavePunchMiscBiopsy : Specimen
    {
        public SkinShavePunchMiscBiopsy()
        {
            this.m_SpecimenId = "SKSHPHMSSPCMN";
            this.m_SpecimenName = "Skin Shave Punch and Misc Biopsy";
            this.m_Description = null;
            this.m_LabFixation = "Formalin";
            this.m_ClientFixation = "Formalin";
            this.m_RequiresGrossExamination = true;
        }
    }
    
    public class GallbladderExcision : Specimen
    {
        public GallbladderExcision()
        {
            this.m_SpecimenId = "GBSPCMN";
            this.m_SpecimenName = "Gallbladder Excision";
            this.m_Description = "Gallbladder Excision";
            this.m_LabFixation = "Formalin";
            this.m_ClientFixation = null;
            this.m_RequiresGrossExamination = true;
        }
    }
    public class SkinExcisionBiopsy : Specimen
    {
        public SkinExcisionBiopsy()
        {
            this.m_SpecimenId = "SKEXSPCMN";
            this.m_SpecimenName = "Skin Excision Biopsy";
            this.m_Description = null;
            this.m_LabFixation = "Formalin";
            this.m_ClientFixation = "Formalin";
            this.m_RequiresGrossExamination = true;
        }
    }

    public class ProstateTUR : Specimen
    {
        public ProstateTUR()
        {
            this.m_SpecimenId = "SPCMNPRSTTTUR";
            this.m_SpecimenName = "Prostate TUR";
            this.m_Description = "Prostate, transurethral resection";
            this.m_CPTCode = new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88309();            
        }
    }

    public class ProstateRadicalResection : Specimen
    {
        public ProstateRadicalResection()
        {
            this.m_SpecimenId = "SPCMNPRSTTRDCLRSCTN";
            this.m_SpecimenName = "Prostate Radical Resection";
            this.m_Description = "Prostate, radical resection";
            this.m_CPTCode = new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88309();
        }
    }

    public class ProstateExceptRadicalResection : Specimen
    {
        public ProstateExceptRadicalResection()
        {
            this.m_SpecimenId = "SPCMNPRSTTRDCLRSCTN";
            this.m_SpecimenName = "Prostate Except Radical Resection";
            this.m_Description = "Prostate, except radical resection";
            this.m_CPTCode = new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88307();
        }
    }

    public class ProstateNeedleBiopsy : Specimen
    {
        public ProstateNeedleBiopsy()
        {
            this.m_SpecimenId = "SPCMNPRSTTNDLBPSY";
            this.m_SpecimenName = "Prostate Needle Biopsy";
            this.m_Description = "Prostate, [right/left/base], needle biopsy";
            this.m_LabFixation = "Formalin";
            this.m_ClientFixation = "B+ Fixative";
            this.m_CPTCode = new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88305();
        }
    }

    public class AppendixExcision : Specimen
    {
        public AppendixExcision()
        {
            this.m_SpecimenId = "APPNDXXCSN";
            this.m_SpecimenName = "Appendix Excision";
            this.m_Description = "Appendix, excision";
            this.m_LabFixation = "Formalin";
            this.m_ClientFixation = null;
            this.m_CPTCode = new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88304();
        }
    }
}
