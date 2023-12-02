using System;
using System.Collections.Generic;
using System.Text;

namespace LethalFirstPlugin
{
    public static class CommandService
    {
        private static Dictionary<string, Command> commands;
        public static Dictionary<string, Command> Commands
        {
            get { return commands; }
        }

        static CommandService() 
        {
            commands = new Dictionary<string, Command>();

            commands.Add("spawncube", new SpawnCube());
        
        }
    }
}
