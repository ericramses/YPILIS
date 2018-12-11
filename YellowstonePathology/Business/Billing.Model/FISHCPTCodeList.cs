using System;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Billing.Model
{
    public class FISHCPTCodeList : ObservableCollection<TypingCptCodeListItem>
    {
        public FISHCPTCodeList()
        {
            this.Add(new TypingCptCodeListItem(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88374", null)));
            this.Add(new TypingCptCodeListItem(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88377", null)));

            foreach (TypingCptCodeListItem item in this) item.Quantity = 0;
        }
    }
}
