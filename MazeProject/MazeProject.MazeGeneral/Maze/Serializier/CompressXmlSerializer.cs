using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;

namespace MazeProject.MazeGeneral.Maze.Serializier
{
    public class CompressXmlSerializer : ISerializer
    {
        public byte[] ToBytes<T>(T obj, Type[] types = null)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (System.IO.Compression.GZipStream gstream = new System.IO.Compression.GZipStream(stream, System.IO.Compression.CompressionMode.Compress))
                {
                    var ser = new DataContractSerializer(typeof(T), types);
                    ser.WriteObject(gstream, obj);
                }

                return stream.GetBuffer().ToArray();
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

                    str.Position = 0;
                    var ser = new DataContractSerializer(typeof(T), types);
                    return (T)ser.ReadObject(str);
                }
            }
        }

        public T ToObject<T>(string strFormat, Type[] types = null)
        {
            return ToObject(Encoding.UTF8.GetBytes(xml), types);
        }

        public string ToStringFormat<T>(T obj, Type[] types = null)
        {
            return Encoding.UTF8.GetString(ToBytes(obj, types));
        }
    }
}
