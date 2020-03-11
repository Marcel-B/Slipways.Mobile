﻿namespace Slipways.Mobile.Helpers
{
    public class Queries
    {
        public const string Slipways = "{ slipways { name id city street postalcode latitude longitude updated created water { id } port { id }} }";
        public const string Waters = "{ waters { id longname shortname updated } }";
        public const string Manufacturers = "{ manufacturers { id name updated } }";
        public const string Extras = "{ extras { id name updated } }";
        public const string Marinas = "{ ports { id name street postalcode city phone email url longitude latitude updated slipways { id } water { id } } }";
    }
}
