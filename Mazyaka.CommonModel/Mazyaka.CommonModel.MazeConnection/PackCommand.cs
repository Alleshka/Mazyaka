using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Mazyaka.CommonModel.MazeConnection
{
    /// <summary>
    /// Возможные команды
    /// login - получить ID
    /// creategame - Создать игру
    /// </summary>
    public enum TypeCommand { login, creategame, joingame }

    /// <summary>
    /// Передаваемый пакет данных
    /// </summary>
    [DataContract(IsReference = true)]
    public class PackCommand : TransmittedClass<PackCommand>
    {
        [DataMember]
        public TypeCommand type { get; private set; } // Тип команды
        [DataMember]
        public List<String> args { get; private set; } // список аргументов


        public PackCommand(TypeCommand type)
        {
            this.type = type;
            args = new List<string>();
        }

        public void AddArgument(String arg)
        {
            args.Add(arg);
        }

        //public static PackCommand ToPack(String json)
        //{
        //    using (MemoryStream stream = new MemoryStream())
        //    {
        //        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(json);
        //        stream.Write(bytes, 0, bytes.Length);
        //        stream.Position = 0;

        //        DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(PackCommand));
        //        return ser.ReadObject(stream) as PackCommand;
        //    }
        //}

        //public static String ToJson(PackCommand pack)
        //{
        //    using (MemoryStream stream = new MemoryStream())
        //    {
        //        DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(PackCommand));
        //        ser.WriteObject(stream, pack);

        //        return System.Text.Encoding.UTF8.GetString(stream.ToArray());
        //    }
        //}

        //public static byte[] ToBytes(PackCommand pack)
        //{
        //    using (MemoryStream stream = new MemoryStream())
        //    {
        //        DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(PackCommand));
        //        ser.WriteObject(stream, pack);

        //        return stream.ToArray();
        //    }
        //}

        //public static PackCommand ToPack(byte[] bytes)
        //{
        //    bytes = bytes.Where(x => x != 0).ToArray(); // <=== Удаляем лишние данные

        //    using (MemoryStream stream = new MemoryStream())
        //    {
        //        stream.Write(bytes, 0, bytes.Length);
        //        stream.Position = 0;

        //        DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(PackCommand));
        //        return ser.ReadObject(stream) as PackCommand;
        //    }
        //}
    }
}
