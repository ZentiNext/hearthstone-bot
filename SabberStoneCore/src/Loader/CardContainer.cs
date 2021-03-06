﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using Newtonsoft.Json;
using SabberStoneCore.Enchants;
using SabberStoneCore.Model;
//using SabberStoneCore.Properties;

namespace SabberStoneCore.Loader
{
	internal class CardContainer : IEnumerable<Card>
	{
		/// <summary>The cards container</summary>
		/// <autogeneratedoc />
		internal Dictionary<string, Card> Cards { get; private set; }

		/// <summary>Gets the <see cref="Card"/> with the specified card identifier.</summary>
		/// <value>The <see cref="Card"/>.</value>
		/// <param name="cardId">The card identifier.</param>
		/// <returns></returns>
		/// <autogeneratedoc />
		internal Card this[string cardId] => Cards[cardId];

		internal void Load(IEnumerable<Card> cards)
		{
			// Set cards (without behaviours)
			Cards = (from c in cards select new { Key = c.Id, Value = c }).ToDictionary(x => x.Key, x => x.Value);

			// Add Powers
			foreach (Card c in Cards.Values)
			{
				if (Powers.Instance.Get.TryGetValue(c.Id, out Power power))
				{
					c.Power = power;
					c.Implemented = power == null ||
					                power.PowerTask != null ||
					                power.DeathrattleTask != null ||
					                power.ComboTask != null ||
					                power.Aura != null ||
					                power.Trigger != null ||
					                power.Enchant != null;
				}
			}
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		/// <autogeneratedoc />
		public IEnumerator<Card> GetEnumerator()
		{
			return Cards.Values.GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>
		/// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
		/// </returns>
		/// <autogeneratedoc />
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

	}
}
