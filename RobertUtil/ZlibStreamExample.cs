// ZlibDeflateInflate.cs
// ------------------------------------------------------------------
//
// Copyright (c) 2009 by Dino Chiesa
// All rights reserved!
//
// This code module is part of DotNetZip, a zipfile class library.
//
// ------------------------------------------------------------------
//
// This code is licensed under the Microsoft Public License. 
// See the file License.txt for the license details.
// More info on: http://dotnetzip.codeplex.com
//
// ------------------------------------------------------------------
//
// Purpose: Demonstrate compression and decompression with the ZlibStream
// class, which is part of the Ionic.Zlib namespace.
// 
// ------------------------------------------------------------------
//

using System;
using System.Reflection;
using Ionic.Zlib;
using System.IO;
using CommonUtil;
using System.Collections.Generic;

namespace Ionic.ToolsAndTests
{

    public class ZlibStreamExample
    {

        /// <summary>
        /// Converts a string to a MemoryStream.
        /// </summary>
        static System.IO.MemoryStream StringToMemoryStream(string s)
        {
            byte[] a = System.Text.Encoding.ASCII.GetBytes(s);
            return new System.IO.MemoryStream(a);
        }

        /// <summary>
        /// Converts a MemoryStream to a string. Makes some assumptions about the content of the stream. 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        static String MemoryStreamToString(System.IO.MemoryStream ms)
        {
            byte[] ByteArray = ms.ToArray();
            return System.Text.Encoding.UTF8.GetString(ByteArray);
        }



        static void CopyStream(System.IO.Stream src, System.IO.Stream dest)
        {
            byte[] buffer = new byte[1024];
            int len = src.Read(buffer, 0, buffer.Length);
            while (len > 0)
            {
                dest.Write(buffer, 0, len);
                len = src.Read(buffer, 0, buffer.Length);
            }
            dest.Flush();
        }


        private static void WriteToFile(byte[] data)
        {
            string fileName = "test.bin";
            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                fs.Write(data, 0, data.Length);
            }
        }


        private static byte[] ZibCompress(byte[] data)
        {
            //using (var cms = new MemoryStream(data))
            using (var msSinkCompressed = new System.IO.MemoryStream())
            {
                using (var zOut = new ZlibStream(msSinkCompressed, CompressionMode.Compress, CompressionLevel.BestCompression, true))
                {
                    //CopyStream(cms, zOut);
                    zOut.Write(data, 0, data.Length);
                }
                return msSinkCompressed.ToArray();
            }
        }

        private static byte[] ZibDecompress(byte[] data)
        {
            var ms = new MemoryStream(data);
            using (var msSinkDecompressed = new System.IO.MemoryStream())
            {
                using (var zOut = new ZlibStream(msSinkDecompressed, CompressionMode.Decompress, true))
                {
                    int readByte = ms.ReadByte();
                    while (readByte != -1)
                    {
                        zOut.WriteByte((byte)readByte);
                        readByte = ms.ReadByte();
                    }
                    zOut.Flush();
                }
                return msSinkDecompressed.ToArray();

            }
        }


        [STAThread]
        public static void Main(System.String[] args)
        {
            try
            {
               
                String originalText = " <Commands FirstSequence=\"1443\" LastSequence=\"1443\"><Quotation Overrided=\"2603e653-910a-47ea-b2e9-e7ac78721b6a	2013-03-27 17:29:14	28.47	28.45	28.95	28.38\" /></Commands>";

                byte[] orginalBytes = System.Text.Encoding.UTF8.GetBytes(originalText);

                Console.WriteLine("orignal bytes length: {0}", orginalBytes.Length);

                System.Console.Out.WriteLine("original:     {0}", originalText);

                // first, compress:
                var compressBytes= ZibCompress(orginalBytes);
                WriteToFile(compressBytes);

                Console.WriteLine("compress bytes length: {0}", compressBytes.Length);

                // at this point, msSinkCompressed contains the compressed bytes

                // now, decompress:
               // msSinkCompressed.Seek(0, System.IO.SeekOrigin.Begin);
                var decompressedBytes =ZibDecompress(compressBytes);
                Console.WriteLine("decompress bytes length: {0}", decompressedBytes.Length);
                // at this point, msSinkDecompressed contains the decompressed bytes
                string decompressed = MemoryStreamToString(new MemoryStream(decompressedBytes));
                System.Console.Out.WriteLine("decompressed: {0}", decompressed);
                System.Console.WriteLine();

                if (originalText == decompressed)
                    System.Console.WriteLine("A-OK. Compression followed by decompression gets the original text.");
                else
                    System.Console.WriteLine("The compression/decompression cycle failed.");
            }
            catch (System.Exception e1)
            {
                Console.WriteLine("Exception: " + e1);
            }
        }
    }
}