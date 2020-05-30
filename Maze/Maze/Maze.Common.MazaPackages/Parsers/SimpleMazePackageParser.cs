using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maze.Common.MazaPackages.Parsers
{
    /// <summary>
    /// Базовый парсер пакетов
    /// </summary>
    public class SimpleMazePackageParser : IMazePackageParser
    {
        /// <summary>
        /// Имя сборки в пакетах. Используется для восстановления типа.
        /// </summary>
        private const string ASSEMBLY_NAME = "Maze.Common.MazaPackages.Packages";

        /// <summary>
        /// Имя поля, в котором лежит тип
        /// </summary>
        private const string TYPE_PROP_NAME = "TypeName";

        public byte[] GetBytes(IMazePackage package)
        {
            string json = JsonConvert.SerializeObject(package);
            return System.Text.Encoding.UTF8.GetBytes(json);
        }

        /// <summary>
        /// Десериализация пакета из набора байт
        /// </summary>
        /// <param name="bytes">Набор байт после передачи</param>
        /// <returns>Собранный пакет</returns>
        public IMazePackage GetPackage(byte[] bytes)
        {
            var str = Encoding.UTF8.GetString(bytes);
            var obj = JObject.Parse(str);
            // TODO: Очень надеюсь, что сборку решим не менять
            var type = Type.GetType($"{ASSEMBLY_NAME}.{(string)obj.SelectToken(TYPE_PROP_NAME)}");
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
