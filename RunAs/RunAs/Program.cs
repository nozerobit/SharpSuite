using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RunAs.Command;
using RunAs.Args;

namespace RunAs
{
    class ProgramOptions
    {
        public string user;
        public string pass;
        public string cmd;

        public ProgramOptions(string uUser = "", string uPass = "", string uCmd = "")
        {
            user = uUser;
            pass = uPass;
            cmd = uCmd;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            /// <summary>
            /// Program Options
            /// </summary>
            ProgramOptions options = new ProgramOptions();

            foreach (var arg in args)
            {
                if (arg.StartsWith("/user:"))
                {
                    string[] components = arg.Split(new string[] { "/user:" }, StringSplitOptions.None);
                    options.user = SanitizeInput(components[1]);
                }
                else if (arg.StartsWith("/pass:"))
                {
                    string[] components = arg.Split(new string[] { "/pass:" }, StringSplitOptions.None);
                    options.pass = SanitizeInput(components[1]);
                }
                else if (arg.StartsWith("/cmd:"))
                {
                    string[] components = arg.Split(new string[] { "/cmd:" }, StringSplitOptions.None);
                    options.cmd = SanitizeInput(components[1]);
                }
                else
                {
                    Console.WriteLine($"[!] Invalid flag: {arg}");
                    return;
                }
            }

            if (options.user == "" || options.pass == "" || options.cmd == "")
            {
                Help.ShowLogo();
                Help.ShowUsage();
                return;
            }
            else
            {
                Command.ExecCMD.ExecuteCommandSync(options.user, options.pass, options.cmd);
            }      
        }
        // This code will remove quotes if exists. 
        public static string SanitizeInput(string input)
        {
            if (input == null)
                return "";

            string lastChar = input.Substring(input.Length - 1);
            string firstChar = input.Substring(0, 1);
            if (firstChar == lastChar)
            {
                if (lastChar == "'" || lastChar == '"'.ToString())
                    input = input.Trim(lastChar.ToCharArray());
            }
            return input;
        }
    }
}
