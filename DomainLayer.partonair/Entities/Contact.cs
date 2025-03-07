﻿using DomainLayer.partonair.Contracts;
using DomainLayer.partonair.Enums;


namespace DomainLayer.partonair.Entities
{
    public class Contact : IEntity
    {
        public Guid Id { get; set; }


        // Who send invitation
        public Guid Id_Sender { get; set; }
        public User Sender { get; set; }

        // Who recieve invitation
        public User Receiver { get; set; }
        public Guid Id_Receiver { get; set; }


        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public DateTime AddedAt { get; set; }
        public bool IsFriendly { get; set; }
        public bool IsBlocked { get; set; }
        public DateTime? BlockedAt { get; set; }
        public DateTime? AcceptedAt { get; set; }
        public StatusContact ContactStatus { get; set; }
    }
}