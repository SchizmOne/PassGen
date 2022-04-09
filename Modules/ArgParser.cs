namespace PassGen.Modules
{
    static class ArgParser
    {
        public static void KeysParser(string[] args,
                                      List<ArgumentKeys> options,
                                      out int passwordLength,
                                      out int numberOfPasswords)
        {

            // Для начала проверим аргументы на ключи, сразу завершающие работу программы.
            if (args.Contains("-h") || args.Contains("--help")) ShowHelpMessage();
            if (args.Contains("-v") || args.Contains("--version")) ShowVersion(exitFlag: true);

            // Теперь проверим его на остальные возможные ключи.
            // По умолчанию в пароле должны быть строчные буквы.
            options.Add(ArgumentKeys.lowerCaseSymbols);
            if (args.Contains("-c") || args.Contains("--capitalize")) options.Add(ArgumentKeys.biggerCaseSymbols);
            if (args.Contains("-n") || args.Contains("--numerals")) options.Add(ArgumentKeys.numbersSymbols);
            if (args.Contains("-s") || args.Contains("--special")) options.Add(ArgumentKeys.specialSymbols);


            // После этого получим значения длинны пароля и количества паролей.
            passwordLength = 0;
            numberOfPasswords = 0;
            foreach (string arg in args)
            {
                // Просто по очереди проверяем каждый аргумент, который записали при запуске программы,
                // пробуя преобразовать его из строки в целое число. Первым по формату должна идти
                // длина пароля.
                if (int.TryParse(arg, out int argIntPasswordLength) && passwordLength == 0)
                {
                    // Если удалось успешно преобразовать, то сразу скипаем вторую проверку
                    // и переходим ко следующему аргументу.
                    passwordLength = argIntPasswordLength;
                    continue;

                }
                // Вторым должно идти количество паролей.
                if (int.TryParse(arg, out int argIntNumberOfPasswords) && numberOfPasswords == 0)
                {
                    numberOfPasswords = argIntNumberOfPasswords;
                    continue;
                }
                    
            }

            // Если ни одно из значений не удалось получить - выводим спаравочное сообщение.
            if ((passwordLength == 0) || (numberOfPasswords == 0))
                ShowHelpMessage();
        }

        public static void FormatOutput(List<ArgumentKeys> options, int passwordLength, int numberOfPasswords)
        {
            ShowVersion(exitFlag: false);
            Console.WriteLine("Число ключей: {0}\nДлина пароля: {1}\nЧисло паролей: {2}",
                                                                            options.Count,
                                                                            passwordLength,
                                                                            numberOfPasswords);

            string[] optionsStringOutput = new string [options.Count];

            ForegroundColorStringOutput("Выбранные символы для генерации паролей:\n", ConsoleColor.Yellow);
            for (int positionInOptions = 0; positionInOptions < options.Count; positionInOptions++)
            {
                if (options[positionInOptions] == ArgumentKeys.lowerCaseSymbols)
                {
                    optionsStringOutput[positionInOptions] = "Латиница в нижнем регистре";
                    ForegroundColorStringOutput($"[{positionInOptions}]\t", ConsoleColor.DarkYellow);
                    Console.WriteLine(optionsStringOutput[positionInOptions]);
                }
                    
                if (options[positionInOptions] == ArgumentKeys.biggerCaseSymbols)
                {
                    optionsStringOutput[positionInOptions] = "Латиница в верхнем регистре";
                    ForegroundColorStringOutput($"[{positionInOptions}]\t", ConsoleColor.DarkYellow);
                    Console.WriteLine(optionsStringOutput[positionInOptions]);
                }
                    
                if (options[positionInOptions] == ArgumentKeys.numbersSymbols)
                {
                    optionsStringOutput[positionInOptions] = "Арабские цифры";
                    ForegroundColorStringOutput($"[{positionInOptions}]\t", ConsoleColor.DarkYellow);
                    Console.WriteLine(optionsStringOutput[positionInOptions]);
                }
                    
                if (options[positionInOptions] == ArgumentKeys.specialSymbols)
                {
                    optionsStringOutput[positionInOptions] = "Специальные символы";
                    ForegroundColorStringOutput($"[{positionInOptions}]\t", ConsoleColor.DarkYellow);
                    Console.WriteLine(optionsStringOutput[positionInOptions]);
                }   
            }

            ForegroundColorStringOutput("Сгенерированные пароли:\n", ConsoleColor.Yellow);
        }

        public static void ForegroundColorStringOutput(string line, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(line);
            Console.ResetColor();
        }

        public static void ShowHelpMessage()
        {
            string programName = "PassGen";
            ForegroundColorStringOutput("======================== СПРАВОЧНАЯ СТРАНИЦА ========================\n", ConsoleColor.Yellow);
            Console.WriteLine("Данная программа представляет из себя простейший консольный генератор паролей.\n" +
                              "Для того чтобы выдать определенное количество паролей заданной длины, вам нужно\n"+
                              "запустить программу в консоли, указав ей необходимые аргументы через пробел.\n\n" +
                              "Предполагается следующий формат запуска:\n" +
                              "\t{0} [ключи] [длина пароля] [количество паролей]\n", programName);
            Console.WriteLine("Возможные ключи программы:\n" +
                              "-h или --help          : Печатать эту подсказку\n" +
                              "-v или --version       : Печатать версию программы\n" +
                              "-c или --capitalize    : Включить в пароль хотя бы одну прописную букву\n" +
                              "-n или --numerals      : Включить в пароль хотя бы одну цифру\n" +
                              "-s или --special       : Включить в пароль хотя бы один специальный символ\n");

            System.Environment.Exit(0);
        }

        public static void ShowVersion(bool exitFlag)
        {
            string programName = "Password Generator";
            string version = "v0.1";
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"======================== {programName.ToUpper()} {version} ========================");
            Console.ResetColor();

            if (exitFlag)
                System.Environment.Exit(0);
        }
    }
}
