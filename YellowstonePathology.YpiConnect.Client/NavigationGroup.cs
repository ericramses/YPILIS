using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.YpiConnect.Client
{
    public class NavigationGroup : List<Type>
    {
        public NavigationGroup()
        {

        }

        public bool IsInGroup(Type type)
        {
            bool result = false;
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].Name == type.Name)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }   
    }
}
