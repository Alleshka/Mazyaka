using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maze.Server.UdpServer
{
    public interface IPackageParser
    {
        byte[] GetBytes(IMazePackage package);
        IMazePackage GetPackage(byte[] bytes);
    }

    public class SimplePackageParser : IPackageParser
    {
        private const string REFERENCE_NAME = "Maze.Server.UdpServer.";
        private const string TYPE_PROP_NAME = "TypeName";

        public byte[] GetBytes(IMazePackage package)
        {
            string json = JsonConvert.SerializeObject(package);
            return System.Text.Encoding.UTF8.GetBytes(json);
        }

        public IMazePackage GetPackage(byte[] bytes)
        {
            var str = System.Text.Encoding.UTF8.GetString(bytes);
            var obj = JObject.Parse(str);
            var type = Type.GetType($"{REFERENCE_NAME}{(string)obj.SelectToken(TYPE_PROP_NAME)}");
            var result = ParsePackage(str, type);
            return result;
        }

        public IMazePackage ParsePackage(string json, Type type)
        {
            var obj = JsonConvert.DeserializeObject(json, type);
            return (IMazePackage)obj;
        }
    }
}
