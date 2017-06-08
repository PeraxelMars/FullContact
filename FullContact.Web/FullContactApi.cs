using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FullContact.Enteties;
using FullContact.Web.Interfaces;
using Newtonsoft.Json;

namespace FullContact.Web
{
    public class FullContactApi : IFullContactApi
    {
        public async Task<FullContactPerson> LookupPersonByEmailAsync(string email)
        {
            string apiKey;
            try
            {
                apiKey = GetFullContactKey();
            }
            catch (ArgumentException ae)
            {
                //Handle exception
                return null;
            }

            string contactData = String.Empty;

            using (HttpClient client = new HttpClient())
            {
                client.Timeout.Add(TimeSpan.FromSeconds(30));
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("X-FullContact-APIKey", apiKey);

                UriBuilder uriBuilder =
                    new UriBuilder(new Uri("https://api.fullcontact.com/v2/person.json/"));
                uriBuilder.Query = $"email={email}";

                var httpResponseMessage = await client.GetAsync(uriBuilder.ToString());

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    contactData = await httpResponseMessage.Content.ReadAsStringAsync();
                }
            }
            if (!string.IsNullOrEmpty(contactData))
            {
                return ParseContactData(contactData);
            }

            return null;
        }

        private FullContactPerson ParseContactData(string contactData)
        {
            var fullContactPerson = JsonConvert.DeserializeObject<FullContactPerson>(contactData);
            return fullContactPerson;
        }

        private string GetFullContactKey()
        {
            string env = Environment.GetEnvironmentVariable("FullContactKey");

            if (string.IsNullOrEmpty(env))
            {
                // Log!
                throw new ArgumentException("No API key found. Are you missing an environment varable?");
            }

            return env;
        }
    }
}
