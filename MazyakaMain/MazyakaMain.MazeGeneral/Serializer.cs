using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MazyakaMain.MazeGeneral
{
    public class Serializer<T>
    {
        public static byte[] ToBytes(T obj)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                DataContractSerializer ser = new DataContractSerializer(typeof(T));
                ser.WriteObject(stream, obj); // Сериализуем и записываем в поток
                return stream.GetBuffer();
            }
        }

        public static String ToXmlString(T obj)
        {
            return Encoding.UTF8.GetString(ToBytes(obj));
        }

        public static T ToObject(byte[] bytes)
        {
            var temp = bytes.TakeWhile(x => x != 0).ToArray(); // Удаляем мусор

            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(temp, 0, temp.Length);
                stream.Position = 0;

                DataContractSerializer ser = new DataContractSerializer(typeof(T));
                return (T)ser.ReadObject(stream);
            }
        }

        public static T ToObject(String xml)
        {
            var temp = Encoding.UTF8.GetBytes(xml);
            return ToObject(temp);
        }
    }
}
