using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ALKForNSCLCByFISH
{
    public class ALKForNSCLCByFISHResultCollection : ObservableCollection<ALKForNSCLCByFISHResult>
    {
        public ALKForNSCLCByFISHResultCollection()
        {
            this.Add(new ALKForNSCLCByFISHNoResult());
            this.Add(new ALKForNSCLCByFISHPositiveResult());
            this.Add(new ALKForNSCLCByFISHNegativeResult());
            this.Add(new ALKForNSCLCByFISHNegativeWithGeneAmplificationResult());
        }
    }
}
