// An array that contains all the words that can be used in the game
// NOTE: Respect the order word|definition
string[] words = { "apple|fruit", "cupcake|food", "minecraft|game", "smartphone|device",
                   "keyboard|device", "mouse|device", "youtube|social network", "facebook|social network",
                   "thinking|act", "workout|act", "house|object", "painting|object"};
Random rnd = new Random();
// The variable that controls the game status
char status = 'y';

while (status == 'y')
{
    // Get a random word (in word|definition format)
    string chosenWord = words[rnd.Next(words.Length)];
    // Separate the word from word|definition
    string word = chosenWord.Substring(0, chosenWord.IndexOf('|'));
    // Separate the definition from word|definition
    string tip = chosenWord.Substring(chosenWord.IndexOf('|') + 1);
    // A char array that will store just the correct letters from user input
    char[] finalWord = new char[word.Length];
    // All the wrong letters will be concatenated in a single string
    string wrongLetters = "";

    Console.Write("TIP: " + tip);
    Console.WriteLine();

    string[] hangman = { "   ____",
                     "   |  |",
                     "   |",
                     "   | ",
                     "   |  ",
                     "   | ",
                     "___|___" };
    // Draw the gallows
    foreach (string s in hangman)
    {
        Console.WriteLine(s);
    }

    // Draw the fields for each letter
    Console.WriteLine();
    foreach (char s in word)
    {
        Console.Write("_ ");
    }
    Console.WriteLine("\n");

    // The name speaks for itself
    int failedAttempts = 0, successfulAttempts = 0;

    Console.Write("Enter a letter: ");
    // The loop keeps running until the user misses 6 times or gets the word right
    while (failedAttempts < 6 && !(successfulAttempts == word.Length))
    {
        // Read user input
        char input = Console.ReadKey(true).KeyChar;
        // if the user got the letter right ...
        if (word.Contains(input))
        {
            for (int i = 0; i < word.Length; i++)
            {
                //... and if the letter he typed hasn't already been pressed...
                //...the program draws the letters that he got right in their respective fields
                if (word[i] == input && finalWord.Count(c => c == input) != word.Count(c => c == input))
                {
                    Console.SetCursorPosition(i * 2, 9);
                    Console.Write(input);
                    finalWord[i] = input;
                    successfulAttempts++;
                }
            }
        }
        // If he got the words wrong..
        else
        {
            //... and if the letter hasn't already been pressed...
            if (wrongLetters.IndexOf(input) == -1)
            {
                // The program will draw a body part
                switch (failedAttempts)
                {
                    case 0:
                        Console.SetCursorPosition(6, 3);
                        Console.Write('O');
                        break;
                    case 1:
                        Console.SetCursorPosition(6, 4);
                        Console.Write('|');
                        Console.SetCursorPosition(6, 5);
                        Console.Write('|');
                        break;
                    case 2:
                        Console.SetCursorPosition(5, 4);
                        Console.Write('/');
                        break;
                    case 3:
                        Console.SetCursorPosition(7, 4);
                        Console.Write('\\');
                        break;
                    case 4:
                        Console.SetCursorPosition(5, 6);
                        Console.Write('/');
                        break;
                    case 5:
                        Console.SetCursorPosition(7, 6);
                        Console.Write('\\');
                        break;
                }

                // And the program will also show the letter that you got wrong
                Console.SetCursorPosition((failedAttempts * 4) + 9, 3);
                Console.Write(input + " - ");
                failedAttempts++;
            }
            wrongLetters += input;
        }
        // Send the cursor to the correct place
        Console.SetCursorPosition(16, 11);
    }
    // Clear the console when the game ends and print the final message
    Console.Clear();
    Console.SetCursorPosition(8, 5);
    if (successfulAttempts == word.Length)
    {
        Console.WriteLine("You Win!");
    }
    else
    {
        Console.WriteLine("You Lose");
        Console.SetCursorPosition(2, 6);
        Console.WriteLine("The word was: \"" + word + "\"");
    }
    // Asks if the user wants to keep playing
    Console.SetCursorPosition(0, 11);
    Console.Write("Do you want to play agin? [y/n]: ");
    status = Console.ReadKey().KeyChar;
    Console.Clear();
}
