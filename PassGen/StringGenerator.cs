using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace PassGen
{
    class StringGenerator
    {

        public static string ChooseCharactersSet(int passwordLength, params CharactersSet[] charactersOption)
        {
            const string lowerCaseSymbols =  "abcdefghijklmnopqrstuvwxyz",
                         biggerCaseSymbols = "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                         specialSymbols =    "!@#$%^&*()_-+=`~?';<>{}[]|\\",
                         numbersSymbols =    "1234567890";

            string resultCharacterSet = "";
            if (charactersOption.Contains(CharactersSet.lowerCaseSymbols))
                resultCharacterSet += lowerCaseSymbols;
            if (charactersOption.Contains(CharactersSet.biggerCaseSymbols))
                resultCharacterSet += biggerCaseSymbols;
            if (charactersOption.Contains(CharactersSet.specialSymbols))
                resultCharacterSet += specialSymbols;
            if (charactersOption.Contains(CharactersSet.numbersSymbols))
                resultCharacterSet += numbersSymbols;

            return GenerateString(passwordLength, resultCharacterSet);
        }

        public static string GenerateString(int length, string characterSet)
        {
            byte[] bytes = new byte[length * 8];
            char[] result = new char[length];
            var characterArray = characterSet.Distinct().ToArray();

            using (var cryptoProvider = new RNGCryptoServiceProvider())
            {
                cryptoProvider.GetBytes(bytes);
            }
            for (int i = 0; i < length; i++)
            {
                ulong value = BitConverter.ToUInt64(bytes, i * 8);
                result[i] = characterArray[value % (uint)characterArray.Length];
            }
            return new string(result);
        }
    }
}
