﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Gwent.NET.DTOs;
using Gwent.NET.Model;

namespace Gwent.NET
{
    public static class ExtensionMethods
    {
        public static GwintType GetGwintTypes(this Card card)
        {
            return card.TypeFlags.Aggregate(GwintType.None, (current, typeFlag) => current | typeFlag.Name);
        }

        public static GwintEffect GetGwintEffects(this Card card)
        {
            return card.EffectFlags.Aggregate(GwintEffect.None, (current, effectFlag) => current | effectFlag.Name);
        }

        public static UserDto ToDto(this User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Picture = user.Picture
            };
        }

        public static CardDto ToDto(this Card card)
        {
            return new CardDto
            {
                Index = card.Id,
                Title = card.Title,
                Description = card.Description,
                Power = card.Power,
                Picture = card.Picture,
                Faction = card.FactionIndex,
                Type = card.GetGwintTypes(),
                Effect = card.GetGwintEffects(),
                SummonFlags = card.SummonFlags.Select(s => s.SummonCardId).ToList(),
                IsBattleKing = card.IsBattleKing
            };
        }

        public static DeckDto ToDto(this Deck deck)
        {
            return new DeckDto
            {
                Id = deck.Id,
                Cards = deck.Cards.Select(c => c.Id).ToList(),
                Faction = deck.Faction,
                BattleKingCard = deck.BattleKingCard.Id,
                IsPrimaryDeck = deck.IsPrimaryDeck
            };
        }

        public static GameDto ToDto(this Game game)
        {
            return new GameDto
            {
                Id = game.Id,
                State = game.State.Name,
                Players = game.Players.Select(p => p.ToDto()).ToList()
            };
        }

        public static PlayerDto ToDto(this Player player)
        {
            return new PlayerDto
            {
                User = player.User.Id,
                IsLobbyOwner = player.IsOwner,
                Lives = player.Lives,
                HandCardCount = player.HandCards.Count,
                DeckCardCount = player.DeckCards.Count,
                GraveyardCards = player.GraveyardCards.Select(c => c.Id).ToList(),
                MeleeCards = player.CardSlots.Where(s => s.Slot == GwintSlot.Melee).Select(s => s.Card.Id).ToList(),
                RangeCards = player.CardSlots.Where(s => s.Slot == GwintSlot.Ranged).Select(s => s.Card.Id).ToList(),
                SiegeCards = player.CardSlots.Where(s => s.Slot == GwintSlot.Siege).Select(s => s.Card.Id).ToList(),
                WeatherCards = player.CardSlots.Where(s => s.Slot == GwintSlot.Weather).Select(s => s.Card.Id).ToList(),
                MeleeModifiers = player.CardSlots.Where(s => s.Slot == GwintSlot.MeleeModifier).Select(s => s.Card.Id).ToList(),
                RangedModifiers = player.CardSlots.Where(s => s.Slot == GwintSlot.RangedModifier).Select(s => s.Card.Id).ToList(),
                SiegeModifiers = player.CardSlots.Where(s => s.Slot == GwintSlot.SiegeModifier).Select(s => s.Card.Id).ToList()
            };
        }

        public static void Shuffle<T>(this IList<T> list, Random random)
        {
            for (var i = 0; i < list.Count; i++)
                list.Swap(i, random.Next(i, list.Count));
        }

        private static void Swap<T>(this IList<T> list, int i, int j)
        {
            var temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }

        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                collection.Add(item);
            }
        }
        public static void AddRange<TEntity>(this IDbSet<TEntity> set, IEnumerable<TEntity> items) where TEntity : class
        {
            foreach (var item in items)
            {
                set.Add(item);
            }
        }


    }
}