using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace YellowstonePathology.MySQLMigration
{
    public class NonpersistentTableDefCollection : ObservableCollection<NonpersistentTableDef>
    {
        public NonpersistentTableDefCollection()
        { }

        public static NonpersistentTableDefCollection GetAll()
        {
            NonpersistentTableDefCollection result = new NonpersistentTableDefCollection();
            result.Add(new NonpersistentTableDefCytologyDiagnosis());
            result.Add(new NonpersistentTableDefCytologyScreeningImpression());
            result.Add(new NonpersistentTableDefFlowCommentV2());
            result.Add(new NonpersistentTableDefMarkers()); 
            result.Add(new NonpersistentTableDefPanelSet());
            result.Add(new NonpersistentTableDefStVincent());
            result.Add(new NonpersistentTableDefTest());
            result.Add(new NonpersistentTableDefWebServiceAccount());
            result.Add(new NonpersistentTableDefWebServiceAccountClient());

            return result;
        }
    }
}
