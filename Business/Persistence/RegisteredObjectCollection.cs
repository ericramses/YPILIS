using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Persistence
{
    public class RegisteredObjectCollection : ObservableCollection<RegisteredObject>
    {
        public RegisteredObjectCollection()
        { }

        public RegisteredObject Get(object key)
        {
            RegisteredObject result = null;
            foreach (RegisteredObject registeredObject in this)
            {
                if(registeredObject.Key == key)
                {
                    result = registeredObject;
                    break;
                }
            }

            return result;
        }

        public bool Exists(object key)
        {
            bool result = false;
            foreach (RegisteredObject registeredObject in this)
            {
                if (registeredObject.Key == key)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }
    }
}
