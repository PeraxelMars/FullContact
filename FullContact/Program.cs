using System;
using System.Net.Http;
using System.Threading.Tasks;
using FullContact.Enteties;
using FullContact.Web;
using FullContact.Web.Interfaces;

namespace FullContact
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.Write("Enter an email address: ");
            var email = Console.ReadLine();

            GetContactDataAsync(email).Wait();

            Console.WriteLine("\nHit 'Enter' to exit.");
        }

        private static async Task GetContactDataAsync(string email)
        {

            IFullContactApi api = new FullContactApi(); // Dependency injection here, in a full implementation.

            try
            {
                var fullContactPerson = await api.LookupPersonByEmailAsync(email);

                if (fullContactPerson == null)
                {
                    Console.WriteLine("No contact info found.");
                }
                else
                {
                    Output(fullContactPerson);
                }
            }
            catch (HttpRequestException)
            {
                //Handel exception
                Console.WriteLine("Friendly error message.");
            }

        }

        private static void Output(FullContactPerson fullContactPerson)
        {
            Console.WriteLine("\nLikelihood: {0}", fullContactPerson.Likelihood);
            //if (result.ContactInfo != null)
            {
                Console.WriteLine("\nContact info:");
                Console.WriteLine("\t" + fullContactPerson.ContactInfo.GivenName);
                Console.WriteLine("\t" + fullContactPerson.ContactInfo.FamilyName);
                Console.WriteLine("\t" + fullContactPerson.ContactInfo.FullName);

                if (fullContactPerson.ContactInfo.Websites.Count != 0)
                {
                    Console.WriteLine("\n\tWebsites:");
                    foreach (Websites site in fullContactPerson.ContactInfo.Websites)
                    {
                        Console.WriteLine("\t" + site.Url);
                    }
                }

            }
            if (fullContactPerson.SocialProfiles.Count != 0)
            {
                Console.WriteLine("\nSocial Profiles:");

                foreach (SocialProfiles socialProfile in fullContactPerson.SocialProfiles)
                {
                    Console.WriteLine($"\t{socialProfile.Type}, {socialProfile.TypeId}, {socialProfile.TypeName}");
                    Console.WriteLine("\t" + socialProfile.Url);
                    Console.WriteLine();
                }
            }
        }
    }
}