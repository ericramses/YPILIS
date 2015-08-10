using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.YpiConnect.Contract
{
    public class FileHelper
    {       
		public static void CopyStream(System.IO.Stream fromStream, System.IO.Stream toStream)
		{
			int Length = 65536;
			Byte[] buffer = new Byte[Length];
			int bytesRead = fromStream.Read(buffer, 0, Length);
			while (bytesRead > 0)
			{
				toStream.Write(buffer, 0, bytesRead);
				bytesRead = fromStream.Read(buffer, 0, Length);
			}
		}
	}
}
