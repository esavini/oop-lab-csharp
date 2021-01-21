using System;

namespace Properties
{
    /// <summary>
    /// The class models a card.
    /// </summary>
    public class Card : IEquatable<Card>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Card"/> class.
        /// </summary>
        /// <param name="name">The name of the card.</param>
        /// <param name="seed">The seed of the card.</param>
        /// <param name="ordinal">The ordinal number of the card.</param>
        public Card(string name, string seed, int ordinal) => (Name, Seed, Ordinal) = (name, seed, ordinal);

        /// <summary>
        /// Initializes a new instance of the <see cref="Card"/> class.
        /// </summary>
        /// <param name="tuple">The informations about the card as a tuple.</param>
        internal Card(Tuple<string, string, int> tuple)
            : this(tuple.Item1, tuple.Item2, tuple.Item3)
        {
        }

        /// <summary>
        /// Card's seed.
        /// </summary>
        public readonly string Seed;

        /// <summary>
        /// Card's name.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Card's ordinal.
        /// </summary>
        public readonly int Ordinal;

        /// <inheritdoc cref="object.ToString"/>
        public override string ToString()
        {
            return $"{GetType().Name}(Name={Name}, Seed={Seed}, Ordinal={Ordinal})";
        }

        /// <inheritdoc cref="IEquatable{Card}.Equals(Card)"/>
        public bool Equals(Card card)
        {
            if (card is null)
            {
                return false;
            }

            return Seed == card.Seed && Name == card.Name && Ordinal == card.Ordinal;
        }

        /// <inheritdoc cref="IEquatable{T}.Equals(T)"/>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return Equals(obj as Card);
        }


        /// <inheritdoc cref="object.GetHashCode"/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Seed, Name, Ordinal);
        }
    }
}
