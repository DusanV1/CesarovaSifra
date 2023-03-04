using System;
namespace Sifra_posun
{
	public class Alphabet
	{
        public static List<char> lowerChars = new List<char>() { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        public static List<char> upperChars = new List<char>() { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

		public Alphabet()
		{
		}

        internal static List<int> FindAlphabetIndex(char letter)
        {

            List<int> result = new List<int>();
            int lowerIndex = lowerChars.FindIndex(a => a == letter);
            int upperIndex = upperChars.FindIndex(a => a == letter);

            if (lowerIndex >= 0)
            {
                result.Add(lowerIndex);
                result.Add(0);  //0=lower char

            }
            else if (upperIndex >= 0)
            {
                result.Add(upperIndex);
                result.Add(1);  //1=upper char

            }
            else
            {
                result.Add(-1);
                result.Add(2);  //2 = char not in the list
            }

            return result;


        }

        public static char ShiftedLetter(char letter, int shift)
        {


            if (FindAlphabetIndex(letter)[1] == 0)
            {
                //index = lowerChars.FindIndex(a => a == letter);
                return lowerChars[(FindAlphabetIndex(letter)[0] + shift) % 26]; // pouzit modulo %

            }
            else if (FindAlphabetIndex(letter)[1] == 1)
            {
                //index = upperChars.FindIndex(a => a == letter);
                return upperChars[(FindAlphabetIndex(letter)[0] + shift) % 26];
            }
            else
            {
                return letter;
            }

        }



        public static int ShiftedLetter(string encriptedString, string decriptedString)
        {

            int i = 0;
            while (!(FindAlphabetIndex(encriptedString[i])[0] >= 0) && !(FindAlphabetIndex(decriptedString[i])[0] >= 0) && i < encriptedString.Length)
            {
                i++;
            }

            if (i < encriptedString.Length)
            {
                return FindAlphabetIndex(encriptedString[i])[0] - FindAlphabetIndex(decriptedString[i])[0];
            }
            else
            {
                return 999;  //no character in the string;
            }

        }
    }
}

