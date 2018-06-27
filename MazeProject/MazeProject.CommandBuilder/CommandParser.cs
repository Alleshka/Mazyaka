using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MazeProject.CommandBuilder
{
    public class CommandParser
    {
        public ICommand Parse (byte[] bytes)
        {
            String jsonString = Encoding.UTF8.GetString(bytes);
            String packageType = (String)(JObject.Parse(jsonString))["TypePackage"];
            ICommand command = null;

            switch (packageType)
            {
                default:
                    {
                        command = null;
                        break;
                    }
            }

            return command;
        }
    }
}
