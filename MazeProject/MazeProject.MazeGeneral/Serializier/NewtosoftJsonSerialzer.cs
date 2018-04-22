using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MazeProject.MazeGeneral.Serializier
{
    public class NewtosoftJsonSerialzer : ISerializer
    {
        public byte[] ToBytes<T>(T obj, Type[] types = null)
        {
            String json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            return System.Text.Encoding.UTF8.GetBytes(json);
        }

        public T ToObject<T>(byte[] bytes, Type[] types = null)
        {
            String json = System.Text.Encoding.UTF8.GetString(bytes);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public T ToObject<T>(string strFormat, Type[] types = null)
        {
            return JsonConvert.DeserializeObject<T>(strFormat);
        }

        public string ToStringFormat<T>(T obj, Type[] types = null)
        {
           return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
    }
}
