using Microsoft.AspNetCore.Cors.Infrastructure;
using System.Reflection.PortableExecutable;

namespace SmartBar
{
    public class AllowAnyCorsPolicy : CorsPolicy
    {
        public AllowAnyCorsPolicy()
        {
            Origins.Clear();
            IsOriginAllowed = origin => true;
            Headers.Clear();
            Headers.Add("*");

            Methods.Clear();
            Methods.Add("*");

            SupportsCredentials = true;
        }
    }
}
