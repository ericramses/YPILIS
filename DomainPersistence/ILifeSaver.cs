using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Domain.Persistence
{
    public interface ILifeSaver
    {
        void SubmitChanges<T>(Collection<T> collection) where T : ITrackable, INotifyDBPropertyChanged, IPersistable;
    }
}
