using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http.Headers;

namespace SOILSTREAMAPI.Models
{
    public class User:IdentityUser
    {
        public string Id { get; set; }= Guid.NewGuid().ToString();
        public string FullName { get; set; }
        public string Location { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }       

    }
}
