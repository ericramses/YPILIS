using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Flow
{
    public class CellPopulationsOfInterest : ObservableCollection<CellPopulationOfInterest>
    {
        public bool Exists(int cellPopulationId)
        {
            bool result = false;
            foreach(CellPopulationOfInterest item in this)
            {
                if(item.Id == cellPopulationId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
    }
}
