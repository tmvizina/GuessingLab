
using System;
using System.Collections.Generic;
using System.Linq;

namespace GuessingGame
{
    class Program
    {//This program sets a random number from 1 to 100 and prompts the user to guess it while giving clues towards its directions, the other purpose of this program is to create 3 seperate methods
     // that will use different algorithms to attempt to guess the randomly generated method using a brute force guessing method, a straight random method, and third random elimination method.
        static void Main(string[] args)
        {
            Random r = new Random();
            int secret = r.Next(1, 101);

           //This was my cheater line I added to allow me to bypass guessing while debugging, it ended up saving a lot of time
           // Console.WriteLine(secret);
            int tries = 0;
            Console.WriteLine("This version uses user input");
            string response = "";

            while (response != "Match!")
            {
                int num = GetUserGuess();
                response = Guess(num, secret);
                Console.WriteLine(response);
                Console.WriteLine();
                tries++;
            }

            Console.WriteLine($"it took you {tries} to guess {secret}\n");

            string elimination = EliminationMethod(secret);
            string random = RandomMethod(secret);
            string bruteforce = BruteForce(secret);

            Console.WriteLine($"{elimination} \n {random} \n {bruteforce}");

        }

        public static int GetUserGuess()
        {
            while (true)
            {
                Console.WriteLine("Please guess a number between 1 and 100 and I will tell how close you are");
                try
                {
                    int num = int.Parse(Console.ReadLine());
                    if (num < 1)
                    {
                        throw new Exception("That number is too small, please input a number between 1 and 100");
                    }
                    else if (num > 100)
                    {
                        throw new Exception("That number is too large, please inptu a number between 1 and 100");
                    }

                    return num;

                }
                catch (FormatException)
                {
                    Console.WriteLine("That was not a valid number please try again");
                    continue;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
            }
        }

        public static string Guess(int guess, int secretNum)
        {
            if (guess == secretNum)
            {
                
                return "Match!";
            }
            int diff = guess - secretNum;
            diff = Math.Abs(diff);

            if (guess > secretNum)
            {
                if (diff > 10)
                {
                    return "Way too high!";
                }
                else
                {
                    return "too high!";
                }
            }
            else
            {
                if (diff > 10)
                {
                    return "Way too low!";
                }
                else
                {
                    return "too low!";
                }
            }
        }


        public static string EliminationMethod(int secretNum)
        {
         // This version uses random guesses while tracking past guesses and checking to see if newly generated guesses have already been guessed and does not add to the count
         // if they have already been guessed
            int elimtries = 1;
            Random r3 = new Random();
            string responseelim = "";
            List<int> previousguesses = new List<int>();

            while (responseelim != "Match!")
            {
                int elimguess = r3.Next(1, 101);

                if (previousguesses.Contains(elimguess) == false)
                {
                    responseelim = Guess(elimguess, secretNum);
                    elimtries++;
                }
                else
                {
                    continue;
                }

            }

            string output =$"The Elimination Guesser took {elimtries} times to guess the number {secretNum} ";
            return output;
        }


        public static string RandomMethod(int secretNum)
        {

            //   Console.WriteLine("This version uses random guesses to find the number");
            int randtries = 1;
            Random r2 = new Random();
            string responserandom = "";

            while (responserandom != "Match!")
            {

                int randguess = r2.Next(1, 101);

                responserandom = Guess(randguess, secretNum);

                if (responserandom != "Match!")
                {
                    randtries++;
                }
                else
                { break; }

            }
            string output = ($"The Random Guesser took {randtries} times to guess the number {secretNum} ");
            return output;

        }

        public static string BruteForce(int secretNum)
        {
            string response = "";
            //This version guesses starting at 100 and ticks down to 1 by 1;
            int current = 100;
            
            while (response != "Match!")
            {
                response = Guess(current, secretNum);
                if (response != "Match!")
                {
                    current--;

                }
                else
                    break;
            }

            string output = $"The brute force Guesser took {101 - current} times to guess the number {secretNum} ";
            return output;
        }
    }


    
}
