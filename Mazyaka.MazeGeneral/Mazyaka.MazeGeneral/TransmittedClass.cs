﻿using System;
using System.Linq;
using System.Runtime.Serialization;
using System.IO;

namespace Mazyaka.MazeGeneral
{
    /// <summary>
    /// От данного класса наследутся все классы, которые необходимо сериализовать для передачи между сервером и клиентом
    /// </summary>
    [DataContract(IsReference = true)]
    public class TransmittedClass<T>
    {
        /// <summary>
        /// Переводит xml строку в объект
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
                return (T)ser.ReadObject(stream);
            }
        }

        /// <summary>
        /// Переводит объект в строку XML
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

                return stream.GetBuffer();
            }
        }

        /// <summary>
        /// Переводит набор байтов в объект
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static T ToPack(byte[] data, Type[] types = null)
        {
            var bytes = data.TakeWhile(x => x != 0).ToArray(); // <=== Удаляем лишние данные

            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(bytes, 0, bytes.Length);
                stream.Position = 0;

                var ser = new DataContractSerializer(typeof(T), types);
                var pack = (T)ser.ReadObject(stream);
                return pack;
            }
        }
    }
}
