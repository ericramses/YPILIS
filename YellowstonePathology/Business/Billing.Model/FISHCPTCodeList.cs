using System;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Billing.Model
{
    public class FISHCPTCodeList : ObservableCollection<CptCode>
    {
        public FISHCPTCodeList()
        {
            this.Add(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88374", null));
            this.Add(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88374", "26"));
            this.Add(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88377", null));
        }
    }
}
