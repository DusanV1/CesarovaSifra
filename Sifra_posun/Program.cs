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
        Console.WriteLine(ShiftedLetter(textShift[0], Decription(textShift)[0]));

        
    }

    public static string Decription(string text)
    {
        List<string> decriptedTexts = new List<string>();
        List<double> charFreqNum = new List<double>();
        //string helpText = string.Empty;

        for (int i=1; i<=26; i++)
        {
            if (!Encription(text, i).ToLower().Contains('w') && !Encription(text, i).ToLower().Contains('q'))
            {
                decriptedTexts.Add(Encription(text, i));

                //helpText = Encription(text, i);
                int o= Encription(text, i).Count(x => x == 'o');
                int e = Encription(text, i).Count(x => x == 'e');
                int a = Encription(text, i).Count(x => x == 'a');
                charFreqNum.Add(o * 0.83 + e * 0.78 + a * 0.67);  //based on the character frequency on czech language
            };
            
        }

        int index = charFreqNum.FindIndex(x => x == charFreqNum.Max()); //najde tu vetu, kde jsou nejvic zastoupeny pismena o,e,a
        
        return decriptedTexts[index]; ;
    }

    //dve metody jedna posune pismeno o int shift a druha ze dvou pismen urci shift, tak abych nemusel opakovat
    //ty dva listy s abecedou
    

    public static char ShiftedLetter(char letter, int shift) 
    {
        List<char> chars = new List<char>() { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        char resultedLetter;
        int index;
        bool lowered=false;

        
        if(char.IsUpper(letter))
        {
            index = chars.FindIndex(a => a == char.ToLower(letter));
            lowered = true;
        }
        else
        {
            index = chars.FindIndex(a => a == letter);
        }
        

        if (index+shift<26)
        {
            resultedLetter = chars[index + shift];
        }else
        {
            resultedLetter = chars[index + shift - ((index + shift) / 26) * 26];
        }

        if(lowered)
        {
            resultedLetter = char.ToUpper(resultedLetter);
        }

        return resultedLetter;
    }

    public static int ShiftedLetter(char encriptedLetter, char decriptedLetter)
    {
        List<char> chars = new List<char>() { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

        if (char.IsUpper(encriptedLetter))
        {
            encriptedLetter = char.ToLower(encriptedLetter);
        }

        if (char.IsUpper(decriptedLetter))
        {
            decriptedLetter = char.ToLower(decriptedLetter);
        }

        int shift = chars.FindIndex(a => a == encriptedLetter) -chars.FindIndex(a => a == decriptedLetter);
        return shift;
    }

    

    public static string Encription(string text,int shift)
    {
        string textShift = string.Empty;
        
        foreach (char item in text)
        {
            
            if (!char.IsLetter(item) )
            {
                textShift += item;
            }
            else
            {
                textShift += ShiftedLetter(item, shift);
            }


        }
        return textShift;
    }
}

