using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace CommonUtil
{
  public static  class StreamExtension
    {
      public static void CopyTo(this Stream source, Stream target)
      {
          byte[] buffer = new byte[4096];
          int n;
          while ((n = source.Read(buffer, 0, buffer.Length))!= 0)
          {
              target.Write(buffer, 0, n);
          }
      }
    }
}
