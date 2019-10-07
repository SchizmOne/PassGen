using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassGen
{
    enum ArgumentKeys
    {
        showHelpMessage,
        showVersion,
        biggerCaseSymbols,
        lowerCaseSymbols,
        numbersSymbols,
        specialSymbols
    }

    static class MainProgram
    {
        static void Main(string[] args)
        {
            // Если программа была запущена с аргументами.
            if (args.Length != 0)
            {
                // Парсим все аргументы, получаем выбранные ключи, а так же длину
                // паролей и их число.
                ArgParser.KeysParser(args, out ArgumentKeys[] options,
                                     out int passwordLength, out int numberOfPasswords);

                // Выводим информацию о запуске программы в форматированном виде.
                ArgParser.FormatOutput(options, passwordLength, numberOfPasswords);
                for (int i = 0; i < numberOfPasswords; i++)
                {
                    ArgParser.ForegroundColorStringOutput($"[{i}]\t", ConsoleColor.DarkGray);
                    Console.WriteLine(StringGenerator.ChooseCharactersSet(passwordLength, options));
                }

            }
            // Если аргументов нет вообще, выводим справочное сообщение.
            else ArgParser.ShowHelpMessage();
        }
    }
}
