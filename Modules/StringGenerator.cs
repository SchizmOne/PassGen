using System.Security.Cryptography;


namespace PassGen.Modules
{
    class StringGenerator
    {
        private static RandomNumberGenerator cryptoProvider = RandomNumberGenerator.Create();

        public static string ChooseCharactersSet(int passwordLength, List<ArgumentKeys> charactersOption)
        {
            const string lowerCaseSymbols =  "abcdefghijklmnopqrstuvwxyz",
                         biggerCaseSymbols = "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                         specialSymbols =    "!@#$%^&*()_-+=`~?';<>{}[]|\\",
                         numbersSymbols =    "1234567890";

            string resultCharacterSet = "";
            if (charactersOption.Contains(ArgumentKeys.lowerCaseSymbols))
                resultCharacterSet += lowerCaseSymbols;
            if (charactersOption.Contains(ArgumentKeys.biggerCaseSymbols))
                resultCharacterSet += biggerCaseSymbols;
            if (charactersOption.Contains(ArgumentKeys.specialSymbols))
                resultCharacterSet += specialSymbols;
            if (charactersOption.Contains(ArgumentKeys.numbersSymbols))
                resultCharacterSet += numbersSymbols;

            return GenerateString(passwordLength, resultCharacterSet);
        }

        public static string GenerateString(int length, string characterSet)
        {
            byte[] bytes = new byte[length * 8];
            char[] result = new char[length];
            var characterArray = characterSet.Distinct().ToArray();

            cryptoProvider.GetBytes(bytes);
            for (int i = 0; i < length; i++)
            {
                ulong value = BitConverter.ToUInt64(bytes, i * 8);
                result[i] = characterArray[value % (uint)characterArray.Length];
            }
            return new string(result);
        }
    }
}
