using System.Text;

namespace GameOfWarByFilip
{
    internal class GameOfWar
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine(@"
================================================================================
||                     Welcome to the Game of War!                            ||
||                                                                            ||
|| HOW TO PLAY:                                                               ||
|| + Each of the two players are dealt one half of a shuffled deck of cards.  ||
|| + Each turn, each player draws one card from their deck.                   ||
|| + The player that drew the card with higher value gets both cards.         ||
|| + Both cards return to the winner's deck.                                  ||
|| + If there is a draw, both players place the next three cards face down    ||
||        and then another card face-up. The owner of the higher face-up      ||
||        card gets all the cards on the table.                               ||
||                                                                            ||
|| HOW TO WIN:                                                                ||
|| + The player who collects all the cards wins.                              ||
||                                                                            ||
|| CONTROLS:                                                                  ||
|| + Press [Enter] to draw a new card until we have a winner.                 ||
||                                                                            ||
||                              Have fun!                                     ||
================================================================================"
            );
            List<Card> deck = GenerateDeck();

            ShuffleDeck(deck);

            Queue<Card> firstPlayerDeck = new Queue<Card>();
            Queue<Card> secondPlayerDeck = new Queue<Card>();

            DealCardsToPlayers(deck, firstPlayerDeck, secondPlayerDeck);

            Card firstPlayerCard = new Card();
            Card secondPlayerCard = new Card();

            int totalMoves = 0;

            while (!GameHasWinner(firstPlayerDeck, secondPlayerDeck, totalMoves))
            {
                Console.ReadLine();
                DrawPlayersCards(firstPlayerCard, secondPlayerCard, firstPlayerDeck, secondPlayerDeck);
                Queue<Card> pool = new Queue<Card>();
                pool.Enqueue(firstPlayerCard);
                pool.Enqueue(secondPlayerCard);
                ProcessWar(pool);
                DetermineRoundWinner(pool);
                Console.WriteLine("===========================================================");
                Console.WriteLine($"First player currently has {firstPlayerDeck.Count} cards");
                Console.WriteLine($"Second player currently has {secondPlayerDeck.Count} cards");
                Console.WriteLine("===========================================================");
                totalMoves++;
            }

        }
        static List<Card> GenerateDeck()
        {
            List<Card> deck = new List<Card>();
            CardFace[] faces = (CardFace[])Enum.GetValues(typeof(CardFace));
            CardSuit[] suits = (CardSuit[])Enum.GetValues(typeof(CardSuit));
            for (int suite = 0; suite < suits.Length; suite++)
            {
                for (int face = 0; face < faces.Length; face++)
                {
                    CardFace currentFace = faces[face];
                    CardSuit currentSuit = suits[suite];
                    deck.Add(new Card
                    {
                        Face = currentFace,
                        Suite = currentSuit,
                    });
                }
            }
            return deck;
        }

        static void ShuffleDeck(List<Card> deck)
        {
            Random random = new Random();
            for (int i = 0; i < deck.Count; i++)
            {

                int firstCaardIndex = random.Next(deck.Count);
                //swap
                Card tempcard = deck[firstCaardIndex];
                deck[firstCaardIndex] = deck[i];
                deck[i] = tempcard;
            }
        }

        static void DealCardsToPlayers(List<Card> deck, Queue<Card> firstPlayerDeck, Queue<Card> secondPlayerDeck)
        {
            while (deck.Count > 0)
            {
                Card[] firstTwoDrawnCards = deck.Take(2).ToArray();
                deck.RemoveRange(0, 2);
                firstPlayerDeck.Enqueue(firstTwoDrawnCards[0]);
                secondPlayerDeck.Enqueue(firstTwoDrawnCards[1]);
            }
        }

        static bool GameHasWinner(Queue<Card> firstPlayerDeck, Queue<Card> secondPlayerDeck, int totalMoves)
        {
            if (!firstPlayerDeck.Any())
            {
                Console.WriteLine($"After a total of {totalMoves} moves, the second player has won!");
                return true;
            }
            if (!secondPlayerDeck.Any())
            {
                Console.WriteLine($"After a total of {totalMoves} moves, the first player has won!");
                return true;
            }
            return false;
        }

        static void DrawPlayersCards(Card firstPlayerCard, Card secondPlayerCard, Queue<Card> firstPlayerDeck, Queue<Card> secondPlayerDeck)
        {
            firstPlayerCard = firstPlayerDeck.Dequeue();
            Console.WriteLine($"first player has drawn: {firstPlayerCard}");

            secondPlayerCard = secondPlayerDeck.Dequeue();
            Console.WriteLine($"Second player has drawn: {secondPlayerCard}");
        }
        static void ProcessWar(Queue<Card> pool)
        {

        }
        static void AddCardsToWinnerDeck(Queue<Card> loserDeck, Queue<Card> winnerDeck)
        {

        }
        static void AddWarCardsToPool(Queue<Card> pool)
        {

        }
        static void DetermineRoundWinner(Queue<Card> pool)
        {

        }
    }
}
    
