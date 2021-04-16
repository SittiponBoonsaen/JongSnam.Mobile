using System;
using System.IO;
using System.Threading.Tasks;

namespace JongSnam.Mobile.Helpers
{
    /// <summary>
    /// The general helper class.
    /// </summary>
    public static class GeneralHelper
    {
        /// <summary>
        /// Gets the base64 string asynchronous.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns cref="Task{String}">The base 64 string of given input.</returns>
        public static async Task<string> GetBase64StringAsync(Stream input)
        {
            var buffer = new byte[16 * 1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    await ms.WriteAsync(buffer, 0, read);
                }

                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }
}
