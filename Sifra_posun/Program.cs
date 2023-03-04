using System.Diagnostics.Metrics;

namespace Sifra_posun;
class Program
{
    
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
        
        int letterShift = Alphabet.ShiftedLetter(textShift, Decription(textShift));
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
        
    }

    public static string Encription(string text, int shift)
    {
        string textShift = string.Empty;

        foreach (char item in text)
        {
            textShift += Alphabet.ShiftedLetter(item, shift);
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

}
