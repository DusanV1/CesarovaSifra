using System.Diagnostics.Metrics;

namespace Sifra_posun;
class Program
{
    public static List<char> lowerChars = new List<char>() { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
    public static List<char> upperChars = new List<char>() { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };


    static void Main(string[] args)
    {
        Console.WriteLine("Napis ceskou vetu bez diakritiky, ktera bude zasifrovana.");
        
        string text = Console.ReadLine();
        while (string.IsNullOrEmpty(text))
        {
            Console.WriteLine("Napis vetu k zasifrovani.");
            text = Console.ReadLine();
        }
        
        Console.WriteLine("Zadej posunuti.");
        int shift;
        while(!Int32.TryParse(Console.ReadLine(), out shift))
        {
            Console.WriteLine("Vstup musi byt cely cislo.");
        };

        string textShift = Encription(text, shift);

        Console.WriteLine("-------------------------");
        Console.WriteLine("Zasifrovany text:");
        Console.WriteLine(textShift);
        Console.WriteLine();

        Console.WriteLine("Odsifrovany text:");
        Console.WriteLine(Decription(textShift));
        Console.WriteLine();
        
        Console.WriteLine("Posunuti sifry:");
        //Console.WriteLine(ShiftedLetter(textShift[0], Decription(textShift)[0]));
        int letterShift = ShiftedLetter(textShift, Decription(textShift));
        if(letterShift<0)
        {
            Console.WriteLine($"Posunuti je {letterShift}, eventualne {26 + letterShift}");
        }else
        {
            if(letterShift==999)
            {
                Console.WriteLine("Veta neobsahuje pismeno na kterym by se dalo zjistit posunuti.");
            }else
            {
                Console.WriteLine($"Posunuti je {letterShift}, eventualne {letterShift - 26}");
            }
            
        }
        //Console.WriteLine(ShiftedLetter2(textShift, Decription(textShift)));


    }

    public static string Encription(string text, int shift)
    {
        string textShift = string.Empty;

        foreach (char item in text)
        {
            textShift += ShiftedLetter(item, shift);
        }
        return textShift;
    }

    public static string Decription(string text)
    {
        List<string> decriptedTexts = new List<string>();
        List<double> charFreqNum = new List<double>();
        

        for (int i=1; i<=26; i++)
        {
            if (!Encription(text, i).ToLower().Contains('w') && !Encription(text, i).ToLower().Contains('q'))
            {
                decriptedTexts.Add(Encription(text, i));

                
                int o= Encription(text, i).Count(x => x == 'o');
                int e = Encription(text, i).Count(x => x == 'e');
                int a = Encription(text, i).Count(x => x == 'a');
                charFreqNum.Add(o * 0.83 + e * 0.78 + a * 0.67);  //based on the character frequency on czech language
            };
            
        }

        int index = charFreqNum.FindIndex(x => x == charFreqNum.Max()); //najde tu vetu, kde jsou nejvic zastoupeny pismena o,e,a
        
        return decriptedTexts[index]; ;
    }

    

    public static char ShiftedLetter(char letter, int shift)
    {
        

        if (FindIndex(letter)[1]==0)
        {
            //index = lowerChars.FindIndex(a => a == letter);
            return lowerChars[(FindIndex(letter)[0] + shift) % 26]; // pouzit modulo %

        }
        else if(FindIndex(letter)[1] == 1)
        {
            //index = upperChars.FindIndex(a => a == letter);
            return upperChars[(FindIndex(letter)[0] + shift) % 26];
        }
        else
        {
            return letter;
        }

    }

    

    public static int ShiftedLetter(string encriptedString, string decriptedString)
    {
   
        int i = 0;
        while (!(FindIndex(encriptedString[i])[0] >=0) && !(FindIndex(decriptedString[i])[0] >= 0 )&& i<encriptedString.Length)
        {
            i++;
        }

        if(i<encriptedString.Length)
        {
            return FindIndex(encriptedString[i])[0] - FindIndex(decriptedString[i])[0];
        }else
        {
            return 999;  //no character in the string;
        }
        
    }

    public static List<int> FindIndex(char letter)
    {

        List<int> result = new List<int>();
        int lowerIndex= lowerChars.FindIndex(a => a == letter);
        int upperIndex= upperChars.FindIndex(a => a == letter);

        if(lowerIndex>=0)
        {
            result.Add(lowerIndex);
            result.Add(0);  //0=lower char
            
        }else if(upperIndex>=0)
        {
            result.Add(upperIndex);
            result.Add(1);  //1=upper char
            
        }else
        {
            result.Add(-1);
            result.Add(2);  //2 = char not in the list
        }

        return result;

        
    }

}

//volani x krat funkce misto aby se ulozila do promene