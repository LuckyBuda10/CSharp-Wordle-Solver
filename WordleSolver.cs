using System;
using System.Collections.Generic;

namespace WordleSolver
{
    class Program
    {
        private static Random random = new Random();

        static void Main(string[] args)
        {
            //Every possible word for Wordle (as of December 2022)
            List<string> wordList = new List<string> { "Words Here" };
            
            string[] starterWords = { "audio", "adieu", "crane", "salet" }; //What I think are the best starting words
            List<string> wordsToRemove = new List<string>(); //Words that get removed from the list after each round

            int guesses = 6;
            string computerGuess;
            bool wonGame = false;

            string[] progress = { "_", "_", "_", "_", "_" };

            for (int i = 0; i < guesses; i++)
            {
                //"_", "?", "!" feedback
                string? userFeedback;

                //Generating a random guess
                if (i == 0) //If its the first guess use a good starter word
                {
                    computerGuess = starterWords[random.Next(0, starterWords.Length)];
                }
                else
                {
                    computerGuess = wordList[random.Next(0, wordList.Count)];
                }

                Console.WriteLine($"{computerGuess}");
                Console.Write("(_/?/!) ");
                userFeedback = Console.ReadLine();

                //Only runs if feedback is in proper format
                if (userFeedback != null && userFeedback.Length == 5)
                {
                    //Breaks if the guess was correct
                    if (userFeedback == "!!!!!")
                    {
                        wonGame = true;
                        break;
                    }

                    //For every item in the user feedback
                    for (int j = 0; j < 5; j++)
                    {
                        if (userFeedback[j] == '_')
                        {
                            foreach (string word in wordList)
                            {
                                //Removes word if it contains the "wrong" letter
                                if (word.Contains(computerGuess[j]))
                                {
                                    wordsToRemove.Add(word);
                                }
                            }
                        }
                        else if (userFeedback[j] == '?')
                        {
                            foreach (string word in wordList)
                            {
                                if (word.Contains(computerGuess[j]))
                                {
                                    //Removes word if letter is in same position
                                    if (word[j] == computerGuess[j])
                                    {
                                        wordsToRemove.Add(word);
                                    }
                                    else
                                    {
                                        progress[j] = "?";
                                    }
                                }
                                else //Remove word if it doesn't have letter at all
                                {
                                    wordsToRemove.Add(word);
                                }
                            }
                        }
                        else if (userFeedback[j] == '!')
                        {
                            foreach (string word in wordList)
                            {
                                //Remove word if it doesn't have letter in exact spot
                                if (word.IndexOf(computerGuess[j]) != j)
                                {
                                    wordsToRemove.Add(word);
                                }
                                else
                                {
                                    progress[j] = "!";
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Input!");
                        }

                        //Remove all the words from the wordList
                        int wordRemoveCount = wordsToRemove.Count;
                        for (int k = 0; k < wordRemoveCount; k++)
                        {
                            try
                            {
                                wordList.Remove(wordsToRemove[k]);
                            }
                            catch
                            {
                                break;
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Guess!");
                }

                //Clears the remove words list for the next iteration
                wordsToRemove.Clear();
            }

            //End text depending on if program correctly guessed word
            if (wonGame)
            {
                Console.WriteLine("I guessed it!");
            }
            else
            {
                Console.WriteLine("I didn't guess it...");
            }
        }
    }
}
