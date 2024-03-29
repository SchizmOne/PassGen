﻿using PassGen.Modules;


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

    static class Program
    {
        static void Main(string[] args)
        {
            // Если программа была запущена с аргументами.
            if (args.Length != 0)
            {
                // Парсим все аргументы, получаем выбранные ключи, а так же длину
                // паролей и их число.
                List<ArgumentKeys> options = new List<ArgumentKeys>();
                ArgParser.KeysParser(args, options, out int passwordLength,
                                                out int numberOfPasswords);

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
