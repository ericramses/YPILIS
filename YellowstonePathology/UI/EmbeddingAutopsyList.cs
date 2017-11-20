using System;
using System.Collections.Generic;

namespace YellowstonePathology.UI
{
    public class EmbeddingAutopsyList: List<UI.EmbeddingAutopsyItem>
    {
        public EmbeddingAutopsyList()
        {
        }

        public void FillList()
        {
            this.Clear();
            List<string> ids = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetEmbeddingAutopsyAliquotIdList();
            DateTime date = DateTime.Today;
            DateTime earliest = date.AddDays(-31);
            while (date > earliest)
            {
                YellowstonePathology.Business.BarcodeScanning.EmbeddingScanCollection scans = YellowstonePathology.Business.BarcodeScanning.EmbeddingScanCollection.GetByScanDate(date);
                foreach (string id in ids)
                {
                    foreach (YellowstonePathology.Business.BarcodeScanning.EmbeddingScan scan in scans)
                    {
                        if (scan.AliquotOrderId == id)
                        {
                            EmbeddingAutopsyItem item = new EmbeddingAutopsyItem(scan.AliquotOrderId, scan.ProcessorStartTime,
                                scan.ProcessorFixationDuration, scan.DateScanned, scan.ScannedBy, scan.ScannedById, scan.Updated);
                            this.Add(item);
                            break;
                        }
                    }
                }
                date = date.AddDays(-1);
            }

            foreach(string id in ids)
            {
                bool found = false;
                foreach (EmbeddingAutopsyItem item in this)
                {
                    if (item.AliquotOrderId == id)
                    {
                        found = true;
                        break;
                    }
                }
                if(found == false)
                {
                    EmbeddingAutopsyItem addition = new EmbeddingAutopsyItem(id);
                    this.Add(addition);
                }
            }
        }
    }
}
