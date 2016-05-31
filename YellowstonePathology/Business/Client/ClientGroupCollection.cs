using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Data;

namespace YellowstonePathology.Business.Client
{
    public class ClientGroupCollection : ObservableCollection<ClientGroup>
    {        
        public ClientGroupCollection()
        {
            
        }        
    }    
}
