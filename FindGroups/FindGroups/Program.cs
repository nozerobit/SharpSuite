using FindGroups.Commands;
using System;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.ActiveDirectory;
using System.Net.NetworkInformation;
using System.Net;

namespace FindGroups
{
    class ProgramOptions
    {
        public string domain;
        public string group;

        public ProgramOptions(string uDomain = "", string uGroup = "")
        {
            domain = uDomain;
            group = uGroup;
        }
    }
    internal class Program
    {
        public static void FindMembers(string groupName, string domainName)
        {
            try
            {
                PrincipalContext pc = new PrincipalContext(ContextType.Domain, domainName);
                GroupPrincipal gp = GroupPrincipal.FindByIdentity(pc, groupName);
                
                foreach (Principal group in gp.GetMembers())
                {
                    if (group.StructuralObjectClass == "user")
                    {
                        if (group.Name.Equals(groupName, StringComparison.InvariantCultureIgnoreCase))
                        {
                            Console.WriteLine("[i] Possible misconfiguration found:");
                            Console.WriteLine($"\tUser: {group.Name} is a member of {groupName}");
                            Console.WriteLine("[!] Stopping the search");
                            break;
                        }
                        Console.WriteLine($"[+] User: {group.Name} is a member of {groupName}");
                    }
                    if (group.StructuralObjectClass == "group")
                    {
                        if (group.Name.Equals(groupName, StringComparison.InvariantCultureIgnoreCase))
                        {
                            Console.WriteLine("[i] Possible misconfiguration found:");
                            Console.WriteLine($"\tGroup: {group.Name} is a member of {groupName}");
                            Console.WriteLine("[!] Stopping the search");
                            break;
                        }
                        Console.WriteLine($"[+] Group: {group.Name} is a member of {groupName}");
                        FindMembers(group.Name, domainName);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"[-] Error: {ex.Message}");
            }
        }
        static void Main(string[] args)
        {
            /// <summary>
            /// Program Options
            /// </summary>
            ProgramOptions options = new ProgramOptions();

            try {
                foreach (var arg in args)
                {
                    if (arg.StartsWith("/domain:"))
                    {
                        string[] components = arg.Split(new string[] { "/domain:" }, StringSplitOptions.None);
                        options.domain = SanitizeInput(components[1]);
                    }
                    else if (arg.StartsWith("/group:"))
                    {
                        string[] components = arg.Split(new string[] { "/group:" }, StringSplitOptions.None);
                        options.group = SanitizeInput(components[1]);
                    }
                    else
                    {
                        Console.WriteLine($"[!] Invalid flag: {arg}");
                        return;
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"[-] Error: {ex.Message}");
                System.Environment.Exit(0);
            }
 

            if (options.domain == "" || options.group == "")
            {
                Help.ShowLogo();
                Help.ShowUsage();
                return;
            }
            else
            {
                // Reference: https://stackoverflow.com/questions/4666334/activedirectory-how-to-find-if-a-domain-is-available
                try
                {
                    var myDomain = Domain.GetCurrentDomain();
                    var joinedDomain = Domain.GetComputerDomain();
                    var forestDomains = myDomain.Forest.Domains;
                    var fqdnName = GetFQDN(options.domain);
                    var context = new PrincipalContext(ContextType.Domain); // .Computer (local)
                    var groupPrincipal = GroupPrincipal.FindByIdentity(context, options.group);

                    // Compare domain argument with domain
                    if (myDomain.ToString() == options.domain || joinedDomain.ToString() == options.domain) {
                        Console.WriteLine("[+] Success: Domain name is valid!");
                    }
                    else if (options.domain == "localhost" || options.domain == "127.0.0.1")
                    {
                        Console.WriteLine("[+] Trying with localhost!");
                        //Console.WriteLine("[i] Please don't specify localhost in a workstation, this may only work on servers");
                    }
                    else if (fqdnName.Equals(options.domain, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("[+] Success: Domain name is valid!");
                    }
                    else if (fqdnName != options.domain || myDomain.ToString() != options.domain && joinedDomain.ToString() != options.domain)
                    {
                        Console.WriteLine("[-] Error: The domain name doesn't seem to be valid or the domain is down!");
                        Console.WriteLine("[i] Please verify your domain string (i.e /domain:lab.local)");
                        Console.WriteLine("[i] Please make sure you're on the same network");
                    }

                    // Compare group
                    if (groupPrincipal == null)
                    {
                        Console.WriteLine("[-] Error: It seems this group doesn't exists, please read the message below this one.");
                    }
                    else if (groupPrincipal != null)
                    {
                        Console.WriteLine("[+] Success: The group is valid!");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[-] Error: {ex.Message}");
                }
                Console.WriteLine("[+] Attempting to gather information!");
                FindMembers(options.group, options.domain);
            }
            // This code will remove quotes if exists. 
            static string SanitizeInput(string input)
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
            static string GetFQDN(string domain)
            {
                string domainName = IPGlobalProperties.GetIPGlobalProperties().DomainName;
                IPHostEntry hostName = Dns.GetHostEntry(domain);
                // FQDN
                return hostName.HostName;
            }
        }
    }
}
