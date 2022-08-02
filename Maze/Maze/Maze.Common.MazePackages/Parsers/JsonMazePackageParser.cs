using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Text;

namespace Maze.Common.MazePackages.Parsers
{
    /// <summary>
    /// Базовый парсер пакетов
    /// </summary>
    public class JsonMazePackageParser : IMazePackageParser
    {
        /// <summary>
        /// Имя поля, в котором лежит тип
        /// </summary>
        private const string TYPE_PROP_NAME = nameof(IMazePackage.TypeName);

        public virtual byte[] GetBytes(IMazePackage package)
        {
            string json = JsonConvert.SerializeObject(package);
            return Encoding.UTF8.GetBytes(json);
        }

        /// <summary>
        /// Десериализация пакета из набора байт
        /// </summary>
        /// <param name="bytes">Набор байт после передачи</param>
        /// <returns>Собранный пакет</returns>
        public virtual IMazePackage GetPackage(byte[] bytes)
        {
            var str = Encoding.UTF8.GetString(bytes);
            var obj = JObject.Parse(str);
            var type = Type.GetType($"{(string)obj.SelectToken(TYPE_PROP_NAME)}");
            var result = ParsePackage(str, type);
            return result;
        }

        private IMazePackage ParsePackage(string json, Type type)
        {
            var obj = JsonConvert.DeserializeObject(json, type);
            return (IMazePackage)obj;
        }
    }
}
