using System;
using System.Collections.Generic;
using System.Linq;

namespace Properties
{
    /// <summary>
    /// A factory class for building <see cref="ISet{T}">decks</see> of <see cref="Card"/>s.
    /// </summary>
    public class DeckFactory
    {
        private string[] _seeds;

        private string[] _names;

        /// <summary>
        /// Cars seeds.
        /// </summary>
        public IList<string> Seeds
        {
            get => _seeds.ToList();
            set => _seeds = value.ToArray();
        }

        /// <summary>
        /// Cards names.
        /// </summary>
        public IList<string> Names
        {
            get => _names.ToList();
            set => _names = value.ToArray();
        }

        /// <summary>
        /// Gets deck's size.
        /// </summary>
        public int Size => _names.Length * _seeds.Length;

        /// <summary>
        /// Gets card deck.
        /// </summary>
        /// <exception cref="InvalidOperationException">When <see cref="Names"/> or <see cref="Seeds"/> are null.</exception>
        public ISet<Card> Deck
        {
            get
            {
                if (_names is null || _seeds is null)
                {
                    throw new InvalidOperationException();
                }

                return new HashSet<Card>(Enumerable
                    .Range(0, _names.Length)
                    .SelectMany(i => Enumerable
                        .Repeat(i, _seeds.Length)
                        .Zip(
                            Enumerable.Range(0, _seeds.Length),
                            (n, s) => Tuple.Create(_names[n], _seeds[s], n)))
                    .Select(tuple => new Card(tuple))
                    .ToList());
            }
        }
    }
}
