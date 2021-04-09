using Microsoft.AspNetCore.Http;

namespace TitsAPI
{
    public static class HttpContextExtensions
    {
        public static bool TryGetAuthToken(this HttpContext context, out string authToken)
        {
            var headers = context.Request.Headers;
            if (headers.ContainsKey("auth-token"))
            {
                authToken = headers["auth-token"];

                return true;
            }

            authToken = "";

            return false;
        }
    }
}