using System;
using System.Collections.Generic;

namespace CountryFriend.Domain.Country
{
    public class Country
    {
        public Guid Id { get; set; }
        public string URL { get; set; }
        public string Filename { get; set; }
        public string Name { get; set; }
        public IList<Domain.State.State> States { get; set; }
    }
}
