using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Specimen.Model
{
    public class SpecimenCollection : ObservableCollection<Specimen>
    {
        public SpecimenCollection()
        {

        }

        public bool Exists(string specimenId)
        {
            bool result = false;
            foreach (Specimen specimen in this)
            {
                if (specimen.SpecimenId == specimenId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public Specimen GetSpecimen(string specimenId)
        {
            Specimen result = null;
            foreach (Specimen specimen in this)
            {
                if (specimen.SpecimenId == specimenId)
                {
                    result = specimen;
                    break;
                }
            }
            return result;
        }

        private static SpecimenCollection Sort(SpecimenCollection specimenCollection)
        {
            SpecimenCollection result = new SpecimenCollection();
            IOrderedEnumerable<Specimen> orderedResult = specimenCollection.OrderBy(i => i.SpecimenName);
            foreach (Specimen specimen in orderedResult)
            {
                result.Add(specimen);
            }
            return result;
        }

        public static SpecimenCollection GetAll()
        {
            SpecimenCollection result = new SpecimenCollection();
            result.Add(new SpecimenDefinition.NullSpecimen());
            result.Add(new SpecimenDefinition.AdenoidExcision());
            result.Add(new SpecimenDefinition.AorticValve());
            result.Add(new SpecimenDefinition.AppendixExcision());
            result.Add(new SpecimenDefinition.BreastReduction());
            result.Add(new SpecimenDefinition.CervicalBiopsy());
            result.Add(new SpecimenDefinition.ECC());
            result.Add(new SpecimenDefinition.EMB());
            result.Add(new SpecimenDefinition.FemoralHead());
            result.Add(new SpecimenDefinition.FallopianTube());
            result.Add(new SpecimenDefinition.GallbladderExcision());
            result.Add(new SpecimenDefinition.GIBiopsy());
            result.Add(new SpecimenDefinition.KneeTissue());
            result.Add(new SpecimenDefinition.LEEPCone());
            result.Add(new SpecimenDefinition.LEEPPieces());
            result.Add(new SpecimenDefinition.MitralValve());
            result.Add(new SpecimenDefinition.NeedleCoreBiopsy());
            result.Add(new SpecimenDefinition.POC());
            result.Add(new SpecimenDefinition.ProstateExceptRadicalResection());            
            result.Add(new SpecimenDefinition.ProstateNeedleBiopsy());
            result.Add(new SpecimenDefinition.ProstateRadicalResection());
            result.Add(new SpecimenDefinition.ProstateTUR());
            result.Add(new SpecimenDefinition.SinusContent());
            result.Add(new SpecimenDefinition.SinglePlacenta());
            result.Add(new SpecimenDefinition.SkinExcisionOrientedBiopsy());
            result.Add(new SpecimenDefinition.SkinExcisionUnorientedBiopsy());
            result.Add(new SpecimenDefinition.SkinExcisionOrientedwithCurettingsBiopsy());
            result.Add(new SpecimenDefinition.SkinExcisionUnorientedwithCurettingsBiopsy());            
            result.Add(new SpecimenDefinition.SkinShavePunchMiscBiopsy());
            result.Add(new SpecimenDefinition.ThinPrepFluid());
            result.Add(new SpecimenDefinition.TonsilAdenoidExcision());
            result.Add(new SpecimenDefinition.TonsilExcision());
            result.Add(new SpecimenDefinition.Uterus());            
            result.Add(new SpecimenDefinition.UterusAdnexa());
            return Sort(result);
        }
    }
}
