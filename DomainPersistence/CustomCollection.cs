using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Domain.Persistence
{
 
    public class CustomCollection<T> where T : ITrackable, INotifyDBPropertyChanged
    {        
        private List<T> innerCollection;

        public CustomCollection()
        {
            innerCollection = new List<T>();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return innerCollection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return innerCollection.GetEnumerator();
        }                
    
        public T this[int index]
        {
            get { return (T)innerCollection[index]; }
            set { innerCollection[index] = value; }
        }
    
        public bool Contains(T item)
        {
            bool found = false;
            foreach (T t in innerCollection)
            {                
                if (t.Equals(item))
                {
                    found = true;
                }
            }
            return found;
        }
    
        public bool Contains(T item, EqualityComparer<T> comp)
        {
            bool found = false;
            /*
            foreach (T t in innerCollection)
            {
                if (comp.Equals(T, item))
                {
                    found = true;
                }
            }
            */
            return found;
        }
    
        public void Add(T item)
        {

            if (!Contains(item))
            {
                innerCollection.Add(item);
            }
            else
            {
                //Console.WriteLine("A box with {0}x{1}x{2} dimensions was already added to the collection.",
                 //   item.Height.ToString(), item.Length.ToString(), item.Width.ToString());
            }
        }

        public void Clear()
        {
            innerCollection.Clear();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            for (int i = 0; i < innerCollection.Count; i++)
            {

                array[i] = (T)innerCollection[i];
            }
        }

        public int Count
        {
            get
            {
                return innerCollection.Count;
            }
        }

        public bool IsReadOnly
        {
            get { return IsReadOnly; }
        }

        public bool Remove(T item)
        {
            bool result = false;            
            for (int i = 0; i < innerCollection.Count; i++)
            {
                T cur = (T)innerCollection[i];
                //if (new BoxSameDimensions().Equals(curBox, item))
                //{
                //    innerCol.RemoveAt(i);
                //    result = true;
                //    break;
                //}
            }
            return result;
        }
    }  
 

}
