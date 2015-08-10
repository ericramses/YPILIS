using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.MaterialTracking.Model
{
	public class MaterialTrackingLogCollection : ObservableCollection<MaterialTrackingLog>
	{
		public MaterialTrackingLogCollection()
		{

		}		

		public bool MaterialExists(string materialId)
		{
			bool result = false;
			foreach (MaterialTrackingLog materialTrackingLog in this)
			{
				if (materialTrackingLog.MaterialId == materialId)
				{
					result = true;
					break;
				}
			}
			return result;
		}

        public MaterialTrackingLog Get(string materialTrackingLogId)
        {
            MaterialTrackingLog result = null;
            foreach (MaterialTrackingLog materialTrackingLog in this)
            {
                if (materialTrackingLog.MaterialTrackingLogId == materialTrackingLogId)
                {
                    result = materialTrackingLog;
                }
            }
            return result;
        }

		public void Remove(string materialId)
		{
			for (int idx = 0; idx < this.Count; idx++)
			{
				if (this[idx].MaterialId == materialId)
				{
					this.RemoveAt(idx);
					break;
				}
			}
		}
	}
}
