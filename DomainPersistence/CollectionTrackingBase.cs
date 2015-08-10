using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Domain.Persistence
{	
	public class CollectionTrackingBase<T> : ObservableCollection<T> where T : ITrackable, INotifyDBPropertyChanged
    {       
        Collection<T> m_DeletedItems = new Collection<T>();

        public CollectionTrackingBase()
        {
            
        }

		public void Reset(CollectionTrackingResetModeEnum collectionTrackingResetModeEnum)
        {
            this.m_DeletedItems.Clear();
			if (collectionTrackingResetModeEnum == CollectionTrackingResetModeEnum.KeyPropertyDataPresentAfterInsert)
			{
				foreach (T item in this)
				{
					item.TrackingState = TrackingStateEnum.Unchanged;
				}
			}
			else
			{
				foreach (T item in this)
				{
					if (item.TrackingState == TrackingStateEnum.Created)
					{
						item.TrackingState = TrackingStateEnum.Inserted;
					}
					else
					{
						if (item.TrackingState != TrackingStateEnum.Inserted)
						{
							item.TrackingState = TrackingStateEnum.Unchanged;
						}
					}
				}
			}
        }        

        public Collection<T> DeletedItems
        {
            get { return this.m_DeletedItems; }
            set { this.m_DeletedItems = value; }
        }

		public void Load(T item)
		{
			this.Add(item);
			item.TrackingState = TrackingStateEnum.Unchanged;
		}

        private void OnItemChanged(object sender, PropertyChangedEventArgs e)
        {
            ITrackable item = (ITrackable)sender;                
            if (e.PropertyName != "TrackingState") // This if is not necessary because of dbPropertyChanged.
            {
                if (item.TrackingState == TrackingStateEnum.Unchanged)
                {
                    item.TrackingState = TrackingStateEnum.Updated;						
                }
            }
        }

        protected override void ClearItems()
        {
            for (int idx = this.Count - 1; idx > -1; idx--)
            {
                this.RemoveAt(0);
            }
        }
        
        protected override void InsertItem(int index, T item)
        {
            item.DBPropertyChanged += OnItemChanged;
            item.TrackingState = TrackingStateEnum.Created;
            base.InsertItem(index, item);
		}
        
        protected override void RemoveItem(int index)
        {            
            T item = this.Items[index];

            if (item.TrackingState == TrackingStateEnum.Inserted)
            {
                throw new Exception("This item is in the Inserted State and cannot be Deleted. A refresh operation should preceed.");
            }
            else
            {
                if (item.TrackingState != TrackingStateEnum.Created)
                {
                    item.TrackingState = TrackingStateEnum.Deleted;
                    item.DBPropertyChanged -= OnItemChanged;
					this.m_DeletedItems.Add(item);						
                }                    
            }                
            base.RemoveItem(index);
        }

        public Collection<T> GetInserted()
        {
            Collection<T> changes = new Collection<T>();
            foreach (T existing in this)
            {
                if (existing.TrackingState == TrackingStateEnum.Created)
                {
                    changes.Add(existing);
                }
            }            
            return changes;
        }
        
        public void AddDeleted(Collection<T> collection)
        {
            Collection<T> changed = this.GetDeleted();
            foreach (T t in changed)
            {
                collection.Add(t);
            }
        }

        public Collection<T> GetDeleted()
        {
            Collection<T> changes = new Collection<T>();
            foreach (T deleted in this.m_DeletedItems)
            {                
                changes.Add(deleted);
            }
            return changes;
        }

        public Collection<T> GetUpdated()
        {
            Collection<T> changes = new Collection<T>();
            foreach (T existing in this)
            {
                if (existing.TrackingState == TrackingStateEnum.Updated)
                {
                    changes.Add(existing);
                }
            }              
            return changes;
        }

        public void AddChanged(Collection<T> collection)
        {
            Collection<T> changed = this.GetChanged();
            foreach (T t in changed)
            {
                collection.Add(t);
            }
            this.AddDeleted(changed);
        }

        public Collection<T> GetChanged()
		{
            Collection<T> changes = new Collection<T>();
			foreach (T existing in this)
			{
				if (existing.TrackingState != TrackingStateEnum.Unchanged)
				{
					changes.Add(existing);
				}
			}
			foreach (T deleted in this.m_DeletedItems)
			{
				changes.Add(deleted);
			}
			return changes;
		}

		public void AddNewAndModified(Collection<T> collection)
		{
			Collection<T> added = this.GetInserted();
			Collection<T> modified = this.GetUpdated();
			foreach(T item in added)
			{
				collection.Add(item);
			}
			foreach (T mod in modified)
			{
				collection.Add(mod);
			}
		}

		public bool HasChanges()
		{
			bool result = false;
			foreach (T existing in this)
			{
				if (existing.TrackingState == TrackingStateEnum.Created || existing.TrackingState == TrackingStateEnum.Updated)
				{
					result = true;
                    break;
				}
			}
            if (this.DeletedItems.Count > 0)
            {
                result = true;
            }

			return result;
		}
    }
}
