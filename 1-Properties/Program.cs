using System;

namespace Properties
{
    /// <summary>
    /// The runnable entrypoint of the exercise.
    /// </summary>
    public static class Program
    {
        /// <inheritdoc cref="Program" />
        public static void Main()
        {
            DeckFactory deckFactory = new DeckFactory
            {
                Names = Enum.GetNames(typeof(ItalianNames)),
                Seeds = Enum.GetNames(typeof(ItalianSeeds))
            };

            Console.WriteLine("The {1} deck has {0} cards: ", deckFactory.Size, "italian");

            foreach (Card card in deckFactory.Deck)
            {
                Console.WriteLine(card);
            }
        }
    }
}
