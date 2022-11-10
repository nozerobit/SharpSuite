
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunAs.Args
{
    internal class Help
    {
        public static void ShowLogo()
        {
            string logo = @"

  _____                          
 |  __ \               /\        
 | |__) |   _ _ __    /  \   ___ 
 |  _  / | | | '_ \  / /\ \ / __|
 | | \ \ |_| | | | |/ ____ \\__ \
 |_|  \_\__,_|_| |_/_/    \_\___/
                                 
                                 
        Author: @nozerobit
";
            Console.WriteLine(logo);
        }
        public static void ShowUsage()
        {
            string usage = @"
The arguments listed below are required

    /user:  username
    /pass:  password
    /cmd:   whoami

Note: If the password contains special characters use single quotes
Commands: It is better to use single quotes in commands as well

Example:
    RunAs.exe /user:'LazyUser' /pass:'@$^ULKIr4nd0m##!3' /cmd:'whoami'
";
            Console.WriteLine(usage);
        }
    }
}
