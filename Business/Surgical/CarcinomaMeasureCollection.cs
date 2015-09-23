using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Surgical
{
    public class CarcinomaMeasureCollection : ObservableCollection<CarcinomaMeasure>
    {
        public static CarcinomaMeasureCollection GetAll()
        {
            CarcinomaMeasureCollection result = new CarcinomaMeasureCollection();
            result.Add(new UterineCarcinomaMeasure());
            result.Add(new EndometrialCarcinomaMeasure());
            result.Add(new ColorectalCarcinomaMeasure());
            return result;
        }
    }
}
