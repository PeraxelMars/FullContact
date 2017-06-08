using System.Collections.Generic;

namespace FullContact.Enteties
{
    public class FullContactPerson
    {
        public FullContactPerson()
        {
            SocialProfiles = new List<SocialProfiles>();
        }
        public float Likelihood { get; set; }

        public ContactInfo ContactInfo { get; set; }

        public List<SocialProfiles> SocialProfiles { get; set; }
    }

    public class SocialProfiles
    {
        public string Type { get; set; }
        public string TypeId { get; set; }
        public string TypeName { get; set; }
        public string Url { get; set; }
    }

    public class ContactInfo
    {
        public ContactInfo()
        {
            Websites = new List<Websites>();
        }
        public List<Websites> Websites { get; set; }
        public string FamilyName { get; set; }
        public string FullName { get; set; }
        public string GivenName { get; set; }
    }

    public class Websites
    {
        public string Url { get; set; }
    }
}
