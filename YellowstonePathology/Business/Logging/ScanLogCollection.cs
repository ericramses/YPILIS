using System;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace YellowstonePathology.Business.Logging
{
    public class ScanLogCollection : ObservableCollection<ScanLog>
    {
        public ScanLogCollection()
        { }
    }
}
