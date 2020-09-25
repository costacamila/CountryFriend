using System;
using System.Text.Json.Serialization;

namespace CountryFriend.Domain.State
{
    public class State
    {
        public Guid Id { get; set; }
        public string URL { get; set; }
        public string Filename { get; set; }
        public string Name { get; set; }
        public string CountryName { get; set; }
        [JsonIgnore]
        public virtual Domain.Country.Country Country { get; set; }
    }
}
