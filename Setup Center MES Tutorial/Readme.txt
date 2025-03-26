** Security Notice **

If you run the Setup Center MES Tutorial as normal user under Vista / Windows 7 you have to 
grant the current user to open the HTTP Port.

The following command has to be run as an administrator:

"netsh http add urlacl url=http://+:33335/SiplaceOibTestMes user=[mylocaluser]"
