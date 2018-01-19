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
                if (bc.Date == date)
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
                if (bc.Date == date)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public void Load()
        {
            Business.Monitor.Model.BlockCountCollection ypiBlocks = Business.Gateway.AccessionOrderGateway.GetMonitorBlockCount();
            Business.Monitor.Model.BlockCountCollection bzBlocks = new BlockCountCollection();

            Store.RedisDB db = Store.AppDataStore.Instance.RedisStore.GetDB(Store.AppDBNameEnum.BozemanBlockCount);            
            string[] results = db.GetAllStrings();
            foreach (string result in results)
            {
                string[] spaceSplit = result.Split(' ');
                BlockCount block = new Model.BlockCount();
                var formatedDate = spaceSplit[0].Substring(4, 2) + "/" + spaceSplit[0].Substring(6, 2) + "/" + spaceSplit[0].Substring(0, 4);
                block.Date = DateTime.Parse(formatedDate);
                block.BozemanBlocks = Convert.ToInt32(spaceSplit[1]);
                bzBlocks.Add(block);
            }

            foreach(BlockCount ypi in ypiBlocks)
            {
                if(bzBlocks.ExistsByDate(ypi.Date) == true)
                {
                    BlockCount bz = bzBlocks.GetByDate(ypi.Date);
                    ypi.BozemanBlocks = bz.BozemanBlocks;
                }                
            }

            for(int i=ypiBlocks.Count - 1; i> ypiBlocks.Count - 7; i--)
            {
                this.Add(ypiBlocks[i]);
            }
        }        
    }
}
