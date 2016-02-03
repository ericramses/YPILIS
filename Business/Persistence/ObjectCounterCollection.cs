using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Persistence
{
    public class ObjectCounterCollection : ObservableCollection<ObjectCounter>
    {
        public ObjectCounterCollection()
        { }

        public void Update(object objectToCount)
        {
            foreach(ObjectCounter objectCounter in this)
            {
                if(objectCounter.ObjectType == objectToCount.GetType())
                {
                    objectCounter.Update(objectToCount);
                    break;
                }
            }
        }
    }
}
