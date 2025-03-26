** Security Notice **

If you run the EBRExample as normal user under Vista / Windows 7 / Windows 10 you have to 
grant the current user to open the HTTP Port.

The following command has to be run as an administrator:

	"netsh http add urlacl url=http://+:9881/BarcodeAdapterService user=mylocaluser"

or

	"netsh http add urlacl url=http://+:9881/BarcodeAdapterService user=DOMAIN\user"


