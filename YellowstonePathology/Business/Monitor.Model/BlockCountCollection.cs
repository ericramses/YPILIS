using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Monitor.Model
{
    public class BlockCountCollection : ObservableCollection<BlockCount>
    {
        public BlockCountCollection() { }

        public bool Exists(DateTime blockCountDate)
        {
            bool result = false;
            foreach(BlockCount blockCount in this)
            {
                if(blockCount.BlockCountDate == blockCountDate)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public BlockCount Get(DateTime blockCountDate)
        {
            BlockCount result = null;
            foreach (BlockCount blockCount in this)
            {
                if (blockCount.BlockCountDate == blockCountDate)
                {
                    result = blockCount;
                    break;
                }
            }
            return result;
        }
    }
}
