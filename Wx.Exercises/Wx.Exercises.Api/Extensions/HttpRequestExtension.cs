using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Wx.Exercises.Api.Middleware
{
    public static class HttpRequestExtension
    {
        public static async Task<string> GetRequestBodyAsync(this HttpRequest request)
        {
            string strRequestBody = string.Empty;
            // IMPORTANT: Ensure the requestBody can be read multiple times.
            HttpRequestRewindExtensions.EnableBuffering(request);

            // IMPORTANT: Leave the body open so the next middleware can read it.
            using (StreamReader reader = new StreamReader(
                request.Body,
                Encoding.UTF8,
                detectEncodingFromByteOrderMarks: false,
                leaveOpen: true))
            {
                strRequestBody = await reader.ReadToEndAsync();


                // IMPORTANT: Reset the request body stream position so the next middleware can read it
                request.Body.Position = 0;
            }

            return strRequestBody;
        }
    }
}
