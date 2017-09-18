using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Monitor.Model
{
    public class BlockCountCollection : ObservableCollection<BlockCount>
    {

        public void SetState()
        {
            foreach (BlockCount blockCount in this)
            {
                blockCount.SetState();
            }
        }
    }
}
