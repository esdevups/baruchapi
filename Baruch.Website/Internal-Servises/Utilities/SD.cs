namespace Mesfo.Internal_Servises.Utilities
{
    public static class SD
    {
        public const int Pagingcuont = 9;


        //Create a method that takes an input text and returns the first 100 characters
        public static string GetFirstCharacters(string text, int numberOfCharacters)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            return text.Length <= numberOfCharacters
                ? text
                : text.Substring(0, numberOfCharacters);
        }
    }


}
