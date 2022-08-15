using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;

namespace YuyukoRecord.utils
{
    internal class GzipUtils
    {
        public static byte[] Compress(byte[] bytes)
        {
            using (MemoryStream compressStream = new MemoryStream())
            {
                using (var zipStream = new GZipStream(compressStream, CompressionMode.Compress))
                    zipStream.Write(bytes, 0, bytes.Length);
                return compressStream.ToArray();
            }
        }

        public static string Decompress(HttpWebResponse response)
        {
            StringBuilder s = new StringBuilder(102400);
            GZipStream g = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress);
            byte[] d = new byte[20480];
            int l = g.Read(d, 0, 20480);
            while (l > 0)
            {
                s.Append(Encoding.Default.GetString(d, 0, l));
                l = g.Read(d, 0, 20480);
            }

            return s.ToString();
        }


        public static byte[] Decompress(byte[] bytes)
        {
            using (var compressStream = new MemoryStream(bytes))
            {
                using (var zipStream = new GZipStream(compressStream, CompressionMode.Decompress))
                {
                    using (var resultStream = new MemoryStream())
                    {
                        zipStream.CopyTo(resultStream);
                        return resultStream.ToArray();
                    }
                }
            }
        }
    }
}
