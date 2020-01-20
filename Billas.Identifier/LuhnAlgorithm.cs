using System.Linq;

namespace Billas.Identifier
{
    public static class LuhnAlgorithm
    {
        public static bool Validate(string sequence, bool backwards = false)
        {
            var alt = backwards;
            var sum = 0;
            for (var i = sequence.Length - 1; i >= 0; i--)
            {
                var pos = sequence[i];
                var num = char.IsNumber(pos) ? sequence[i] - '0' : pos;
                if (alt)
                {
                    num *= 2;
                }
                
                sum += num.ToString().Select(x => x - '0').Sum();
                alt = !alt;
            }

            return sum % 10 == 0;
        }
        public static int Generate(string sequence)
        {
            var alt = true;
            var sum = 0;
            for (var i = sequence.Length - 1; i >= 0; i--)
            {
                var pos = sequence[i];
                var num = char.IsNumber(pos) ? sequence[i] - '0' : pos;
                if (alt)
                {
                    num *= 2;
                    if (num > 9) num -= 9;
                }
                sum += num.ToString().Select(x => x - '0').Sum();
                alt = !alt;
            }

            var value = 10 - (sum % 10);
            return value == 10 ? 0 : value;
        }
    }
}
