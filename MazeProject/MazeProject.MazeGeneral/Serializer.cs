using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeProject.MazeGeneral.Command;
using System.Runtime.Serialization;
using System.IO;

namespace MazeProject.MazeGeneral
{
    // TODO: Сделать свой сериализатор, используя TOString и парсить на клиентах

    //public class Serializer<T>
    //{
    //    public static byte[] ToBytes(T obj, Type[] types = null)
    //    {
    //        using (MemoryStream stream = new MemoryStream())
    //        {
    //            using (System.IO.Compression.GZipStream gstream = new System.IO.Compression.GZipStream(stream, System.IO.Compression.CompressionMode.Compress))
    //            {
    //                var ser = new DataContractSerializer(typeof(T), types);
    //                ser.WriteObject(gstream, obj);
    //            }

    //            return stream.GetBuffer().ToArray();
    //        }
    //    }

    //    public static String ToXml(T obj, Type[] types = null)
    //    {
    //        return Encoding.UTF8.GetString(ToBytes(obj, types));
    //    }

    //    public static T ToObject(byte[] bytes, Type[] types = null)
    //    {
    //        using (MemoryStream stream = new MemoryStream(bytes))
    //        {
    //            using (MemoryStream str = new MemoryStream())
    //            {
    //                using (System.IO.Compression.GZipStream gzip = new System.IO.Compression.GZipStream(stream, System.IO.Compression.CompressionMode.Decompress))
    //                {
    //                    gzip.CopyTo(str);
    //                }

    //                str.Position = 0;
    //                var ser = new DataContractSerializer(typeof(T), types);
    //                return (T)ser.ReadObject(str);
    //            }
    //        }
    //    }

    //    public static T ToObject(String xml, Type[] types = null)
    //    {
    //        return ToObject(Encoding.UTF8.GetBytes(xml), types);
    //    }
    //}
}
