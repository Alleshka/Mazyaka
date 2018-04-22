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

    public class Serializer<T>
    {
        public static byte[] ToBytes(T obj, Type[] types = null)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                var ser = new DataContractSerializer(typeof(T), types);
                ser.WriteObject(stream, obj);

                return stream.GetBuffer().TakeWhile(x=>x!=0).ToArray();
            }
        }

        public static String ToXml(T obj, Type[] types = null)
        {
            return Encoding.UTF8.GetString(ToBytes(obj, types));
        }

        public static T ToObject(byte[] bytes, Type[] types = null)
        {
            bytes = bytes.TakeWhile(x => x != 0).ToArray();
            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(bytes, 0, bytes.Length);
                stream.Position = 0;
                String temp = Encoding.UTF8.GetString(stream.GetBuffer());

                var ser = new DataContractSerializer(typeof(T), types);
                return (T)ser.ReadObject(stream);
            }
        }

        public static T ToObject(String xml, Type[] types = null)
        {
            return ToObject(Encoding.UTF8.GetBytes(xml), types);
        }
    }
}
