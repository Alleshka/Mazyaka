using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.IO.Compression;
namespace MazeProject.MazeGeneral.Serializier
{
    public class CompressNewtosoftSerializer : ISerializer
    {
        public byte[] ToBytes<T>(T obj, Type[] types = null)
        {
            String json = Newtonsoft.Json.JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            });
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(json);

            using (var stream = new MemoryStream())
            {
                using (var gstream = new System.IO.Compression.GZipStream(stream, CompressionMode.Compress))
                {
                    gstream.Write(bytes, 0, bytes.Length);
                }
                return stream.GetBuffer();
            }
        }

        public T ToObject<T>(byte[] bytes, Type[] types = null)
        {
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                using (MemoryStream str = new MemoryStream())
                {
                    using (System.IO.Compression.GZipStream gzip = new System.IO.Compression.GZipStream(stream, System.IO.Compression.CompressionMode.Decompress))
                    {
                        gzip.CopyTo(str);
                    }
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(System.Text.Encoding.UTF8.GetString(str.GetBuffer()), new JsonSerializerSettings()
                    {
                        TypeNameHandling = TypeNameHandling.All
                    });
                }
            }
        }

        public T ToObject<T>(string strFormat, Type[] types = null)
        {
            return ToObject<T>(Encoding.UTF8.GetBytes(strFormat), types);
        }

        public string ToStringFormat<T>(T obj, Type[] types = null)
        {
            return Encoding.UTF8.GetString(ToBytes<T>(obj, types));
        }
    }
}
