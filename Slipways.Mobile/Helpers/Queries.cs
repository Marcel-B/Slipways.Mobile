namespace Slipways.Mobile.Helpers
{
    public class Queries
    {
        public const string Slipways = "{ slipways { name id city street postalcode latitude longitude created water { id longname shortname } } }";
        public const string Waters = "{ waters { id longname shortname } }";
        public const string Manufacturers = "{ manufacturers { id name } }";
        public const string Extras = "{ extras { id name } }";
    }
}
