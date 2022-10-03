namespace AngularUrlShortener.Helpers
{
    public static class RandomCodeGenerator
    {
        private static readonly Random _random = new Random();

        private static readonly char[] _lowerCase = new char[]
        {
            'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p',
            'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'z',
            'x', 'c', 'v', 'b', 'n', 'm'
        };

        private static readonly char[] _upperCase = new char[]
        {
            'Q', 'W', 'E', 'R', 'T', 'Y', 'U', 'I', 'O', 'P',
            'A', 'S', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'Z',
            'X', 'C', 'V', 'B', 'N', 'M'
        };

        private static readonly char[] _numerals = new char[]
        {
            '1', '2', '3', '4', '5', '6', '7', '8', '9', '0'
        };

        public static string GetRand(int length,
            bool useLowerCase = true,
            bool useUpperCase = false,
            bool useNumerals = true)
        {
            char[] randomString = new char[length];

            int characterCount = 0;
            char[] characters = null;

            if (useLowerCase) { characterCount += _lowerCase.Length; }
            if (useUpperCase) { characterCount += _upperCase.Length; }
            if (useNumerals) { characterCount += _numerals.Length; }

            characters = new char[characterCount];
            int lastIndex = 0;

            if (useLowerCase)
            {
                _lowerCase.CopyTo(characters, lastIndex);
                lastIndex += _lowerCase.Length;
            }

            if (useUpperCase)
            {
                _upperCase.CopyTo(characters, lastIndex);
                lastIndex += _upperCase.Length;
            }

            if (useNumerals)
            {
                _numerals.CopyTo(characters, lastIndex);
                lastIndex += _numerals.Length;
            }

            for (int i = 0; i < length; i++)
            {
                randomString[i] = characters[_random.Next(characterCount)];
            }

            return new String(randomString);
        }
    }
}
