namespace Slipways.Mobile.Helpers
{
    public class Queries
    {
        public const string Slipways = "{ slipways { name id city street postalcode latitude longitude created water { id longname } } }";
        public const string Waters = "{ waters { longname id shortname } }";
    }
}
