using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassGen
{
    enum CharactersSet
    {
        nonSymbols,
        lowerCaseSymbols,
        biggerCaseSymbols,
        specialSymbols,
        numbersSymbols
    }

    class Program
    {
        static void Main(string[] args)
        {

            if (args.Length != 0)
            {

                CharactersSet[] characterOption = new CharactersSet[4];
                if (args.Contains("-A")) characterOption[0] = CharactersSet.lowerCaseSymbols;
                if (args.Contains("-c")) characterOption[1] = CharactersSet.biggerCaseSymbols;
                if (args.Contains("-n")) characterOption[2] = CharactersSet.numbersSymbols;
                if (args.Contains("-l")) characterOption[3] = CharactersSet.specialSymbols;

                int passwordLength = 0;
                int numberOfPasswords = 0;
                foreach (string arg in args)
                {
                    if (int.TryParse(arg, out int argIntPasswordLength) && passwordLength == 0)
                        passwordLength = argIntPasswordLength;
                    if (int.TryParse(arg, out int argIntNumberOfPasswords) && numberOfPasswords == 0)
                        numberOfPasswords = argIntNumberOfPasswords;
                }

                Console.WriteLine("Passwords:");
                Console.WriteLine(characterOption[0].ToString() + characterOption[1].ToString() + characterOption[2].ToString() + characterOption[3].ToString());

                for (int i = 0; i < numberOfPasswords; i++)
                {
                    Console.WriteLine(StringGenerator.ChooseCharactersSet(passwordLength, characterOption));
                }

            }

        }
    }
}
