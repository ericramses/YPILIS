using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.ReportDistribution.Model
{
    public class IncompatibleDistributionTypeCollection : ObservableCollection<IncompatibleDistributionType>
    {
        public IncompatibleDistributionTypeCollection()
        {
            this.Add(new Model.IncompatibleDistributionType(DistributionType.EPIC, DistributionType.ATHENA));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.EPIC, DistributionType.ECW));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.EPIC, DistributionType.MEDITECH));

            this.Add(new Model.IncompatibleDistributionType(DistributionType.ATHENA, DistributionType.EPIC));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.ATHENA, DistributionType.ECW));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.ATHENA, DistributionType.MEDITECH));

            this.Add(new Model.IncompatibleDistributionType(DistributionType.MEDITECH, DistributionType.EPIC));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.MEDITECH, DistributionType.ECW));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.MEDITECH, DistributionType.ATHENA));

            this.Add(new Model.IncompatibleDistributionType(DistributionType.FAX, DistributionType.EPIC));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.FAX, DistributionType.ATHENA));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.FAX, DistributionType.ECW));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.FAX, DistributionType.MEDITECH));

            this.Add(new Model.IncompatibleDistributionType(DistributionType.MAIL, DistributionType.EPIC));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.MAIL, DistributionType.ATHENA));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.MAIL, DistributionType.ECW));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.MAIL, DistributionType.MEDITECH));

            this.Add(new Model.IncompatibleDistributionType(DistributionType.PRINT, DistributionType.EPIC));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.PRINT, DistributionType.ATHENA));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.PRINT, DistributionType.ECW));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.PRINT, DistributionType.MEDITECH));

            this.Add(new Model.IncompatibleDistributionType(DistributionType.WEBSERVICE, DistributionType.EPIC));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.WEBSERVICE, DistributionType.ATHENA));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.WEBSERVICE, DistributionType.EPIC));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.WEBSERVICE, DistributionType.MEDITECH));

            this.Add(new Model.IncompatibleDistributionType(DistributionType.WEBSERVICEANDFAX, DistributionType.EPIC));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.WEBSERVICEANDFAX, DistributionType.ATHENA));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.WEBSERVICEANDFAX, DistributionType.ECW));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.WEBSERVICEANDFAX, DistributionType.MEDITECH));
        }

        public bool TypesAreIncompatible(string primaryDistributionType, string checkingingDistributionType)
        {
            bool result = false;
            foreach(IncompatibleDistributionType incompatibleDistributionType in this)
            {
                if(incompatibleDistributionType.PrimaryDistributionType == primaryDistributionType && 
                    incompatibleDistributionType.SecondaryDistributionType == checkingingDistributionType)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
    }
}
