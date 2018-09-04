using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Monitor.Model
{
    public class BlockCountCollection : ObservableCollection<BlockCount>
    {
        public BlockCountCollection()
        {

        } 
        
        public BlockCount GetByDate(DateTime date)
        {
            BlockCount result = null;
            foreach(BlockCount bc in this)
            {
                if (bc.BlockCountDate == date)
                {
                    result = bc;
                    break;
                }
            }
            return result;                
        }

        public bool ExistsByDate(DateTime date)
        {
            bool result = false;
            foreach (BlockCount bc in this)
            {
                if (bc.BlockCountDate == date)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
    }
}
