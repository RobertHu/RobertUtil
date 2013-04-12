using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;
namespace CommonUtil
{
    public static class GzipDemo
    {
        public static byte[] Compress(byte[] input)
        {
            using (var ms2 = new MemoryStream())
            {
                using (GZipStream zs = new GZipStream(ms2, CompressionMode.Compress))
                {
                    zs.Write(input, 0, input.Length);
                }
                return ms2.ToArray();
            }
        }

        public static byte[] Decompress(byte[] input)
        {
            List<byte> result = new List<byte>();
            using (var ms2 = new MemoryStream(input))
            {
                using (GZipStream zs = new GZipStream(ms2, CompressionMode.Decompress))
                {
                    int read = zs.ReadByte();
                    while (read != -1)
                    {
                        result.Add((byte)read);
                        read = zs.ReadByte();
                    }
                }
                return result.ToArray();
            }
        }


    }
}
