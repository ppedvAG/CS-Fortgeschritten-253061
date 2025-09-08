namespace LinqSamples.Extensions
{
    // Klassen mit Extensions Methods muessen static sein
    internal static class MathExtensions
    {
        // Quersumme berechnen
        // Der erste Parameter von Extensions Methoden ist immer this gekennzeichnet
        public static int DigitSum(this int number)
        {
            return number.ToString()
                .Sum(c => (int)char.GetNumericValue(c));
        }
    }
}
