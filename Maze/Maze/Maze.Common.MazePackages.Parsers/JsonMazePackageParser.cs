using Maze.Common.MazePackages.MazePackages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection;
using System.Text;

namespace Maze.Common.MazePackages.Parsers
{
    /// <summary>
    /// Базовый парсер пакетов
    /// </summary>
    public class JsonMazePackageParser : IMazePackageParser
    {
        /// <summary>
        /// Имя сборки в пакетах. Используется для восстановления типа.
        /// </summary>
        private const string ASSEMBLY_NAME = "Maze.Common.MazePackages.MazePackages";

        /// <summary>
        /// Имя поля, в котором лежит тип
        /// </summary>
        private const string TYPE_PROP_NAME = "TypeName";

        public virtual byte[] GetBytes(IMazePackage package)
        {
            string json = JsonConvert.SerializeObject(package);
            return System.Text.Encoding.UTF8.GetBytes(json);
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
            var type = Assembly.Load(ASSEMBLY_NAME).GetType($"{ASSEMBLY_NAME}.{(string)obj.SelectToken(TYPE_PROP_NAME)}");
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
