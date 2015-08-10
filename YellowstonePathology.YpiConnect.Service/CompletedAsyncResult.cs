using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace YellowstonePathology.YpiConnect.Service
{
    class CompletedAsyncResult<T> : IAsyncResult
    {
        T data;

        public CompletedAsyncResult(T data)
        { 
            this.data = data; 
        }

        public T Data
        { 
            get { return data; } 
        }
        
        public object AsyncState
        { 
            get { return (object)data; } 
        }

        public WaitHandle AsyncWaitHandle
        { 
            get { throw new Exception("The method or operation is not implemented."); } 
        }

        public bool CompletedSynchronously
        { 
            get { return true; } 
        }

        public bool IsCompleted
        { 
            get { return true; } 
        }        
    }
}
