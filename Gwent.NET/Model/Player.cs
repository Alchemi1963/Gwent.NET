﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gwent.NET.Model
{
    public class Player
    {
        public Player()
        {
            HandCards = new HashSet<Card>();
            DeckCards = new HashSet<Card>();
            GraveyardCards = new HashSet<Card>();
            CardSlots = new List<PlayerCardSlot>();
        }

        [Key]
        public int Id { get; set; }

        public virtual User User { get; set; }

        public virtual Deck Deck { get; set; }

        public bool IsOwner { get; set; }

        public bool IsVictor { get; set; }

        public bool IsRoundStarter { get; set; }

        public bool IsTurn { get; set; }

        public bool CanUseBattleKingCard { get; set; }

        public int Lives { get; set; }

        public virtual ICollection<Card> HandCards { get; set; }

        public virtual ICollection<Card> DeckCards { get; set; }

        public virtual ICollection<Card> GraveyardCards { get; set; }


        [InverseProperty("Player")]
        public virtual ICollection<PlayerCardSlot> CardSlots  { get; set; }

    }
}