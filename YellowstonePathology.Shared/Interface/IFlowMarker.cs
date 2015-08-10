using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Shared.Interface
{
    public interface IFlowMarker
    {
		string Name { get; }
		int Expresses { get; }
    }
}
