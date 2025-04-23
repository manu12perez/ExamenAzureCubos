using Newtonsoft.Json;
using System.Security.Claims;

namespace ExamenAzureCubos.Helpers
{
    public class HelperUsuarioToken
    {
        private IHttpContextAccessor contextAccessor;

        public HelperUsuarioToken(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }

        
    }
}
