using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindGroups.Commands
{
    internal class Help
    {
        public static void ShowLogo()
        {
            string logo = @"

  ______ _           _  _____                           
 |  ____(_)         | |/ ____|                          
 | |__   _ _ __   __| | |  __ _ __ ___  _   _ _ __  ___ 
 |  __| | | '_ \ / _` | | |_ | '__/ _ \| | | | '_ \/ __|
 | |    | | | | | (_| | |__| | | | (_) | |_| | |_) \__ \
 |_|    |_|_| |_|\__,_|\_____|_|  \___/ \__,_| .__/|___/
                                             | |        
                                             |_|        
                                 
        Author: @nozerobit
";
            Console.WriteLine(logo);
        }
        public static void ShowUsage()
        {
            string usage = @"
The arguments listed below are required

    /domain:  domain name
    /group:   group name

Note: If the string contains special characters use single quotes. Use single/double quotes if there's a space.

Example:
";
            var example = "\tFindGroups.exe /domain:'contoso.local' /group:\"Domain Admins\"";
            Console.WriteLine(usage + example);
        }
    }
}
