using System;
using System.Collections.Generic;

namespace CountryFriend.Domain.Friend
{
    public class Friend
    {
        public Guid Id { get; set; }
        public string URL { get; set; }
        public string Filename { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime Birthday { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public virtual IList<Domain.Friend.FriendFriend> Friends { get; set; }
    }
}
