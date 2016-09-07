using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Persistence
{
    public class DocumentTestBuilders : Document
    {
        public DocumentTestBuilders(object v, object c)
        {
            this.m_Value = v;
            this.m_Clone = c;
        }
        public override bool IsDirty()
        {
            bool result = false;
            YellowstonePathology.Business.Persistence.SqlCommandSubmitter sqlCommandSubmitter = this.GetSqlCommands(this.m_Value);
            if (sqlCommandSubmitter.HasChanges() == true)
            {
                sqlCommandSubmitter.LogCommands();
                result = true;
            }
            return result;
        }
    }
}
