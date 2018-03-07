using System;
using System.Linq;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Json;

namespace Mazyaka.CommonModel
{
    /// <summary>
    /// От данного класса наследутся все классы, которые необходимо сериализовать для передачи между сервером и клиентом
    /// </summary>
    [DataContract(IsReference = true)]
    public class TransmittedClass<T>
    {
        /// <summary>
        /// Переводит json строку в объект
        /// </summary>
        /// <param name="xml">Строка</param>
        /// <returns></returns>
        public static T ToObject(String xml, Type[] types = null)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(xml);
                stream.Write(bytes, 0, bytes.Length);
                stream.Position = 0;

                DataContractSerializer ser = new DataContractSerializer(typeof(T), types);
                return (T) ser.ReadObject(stream);
            }
        }

        /// <summary>
        /// Переводит объект в строку JSON
        /// </summary>
        /// <param name="pack"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public static String ToXML(T pack, Type[] types = null)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                DataContractSerializer ser = new DataContractSerializer(typeof(T), types);
                ser.WriteObject(stream, pack);

                return System.Text.Encoding.UTF8.GetString(stream.ToArray());
            }
        }

        /// <summary>
        /// Переводит объект в набор байтов для передачи
        /// </summary>
        /// <param name="pack"></param>
        /// <returns></returns>
        public static byte[] ToBytes(T pack, Type[] types = null)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                var ser = new DataContractSerializer(typeof(T), types);
                ser.WriteObject(stream, pack);

                return stream.ToArray();
            }
        }

        /// <summary>
        /// Переводит набор байтов в объект
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static T ToPack(byte[] bytes, Type[] types = null)
        {
            bytes = bytes.Where(x => x != 0).ToArray(); // <=== Удаляем лишние данные

            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(bytes, 0, bytes.Length);
                stream.Position = 0;

                var ser = new DataContractSerializer(typeof(T), types);
                return (T)ser.ReadObject(stream);
            }
        }
    }
}
