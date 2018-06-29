using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Alraunen.KaprekarsConstant
{
    public class Program
    {
        #region Fields
        private static Random _rnd = new Random();
        private static string[] _invalidStrings =
        {
            "0",
            "00",
            "000",
            "0000",
            "1111",
            "2222",
            "3333",
            "4444",
            "5555",
            "6666",
            "7777",
            "8888",
            "9999"
        };
        private static string[] _rndStrings =
        {
            "r",
            "'r'",
            "rnd",
            "random"
        };
        #endregion

        private static void Main(string[] args)
        {
            _routine();
        }

        #region Berechnung
        public static int KaprekarsConstant(int num)
        {
            var result = 0;
            while (num != 6174)
            {
                result++;
                var arranged = _arrangeHighLowNumbers(num);
                num = arranged.Item1 - arranged.Item2;
            }
            return result;
        }

        private static Tuple<int, int> _arrangeHighLowNumbers(int num)
        {
            var numString = num.ToString();

            var leadingZeroes = String.Empty;
            for (int i = numString.Length; i < 4; i++)
                leadingZeroes += '0';
            numString = leadingZeroes + numString;

            var high = Int32.Parse(new String(numString.OrderByDescending(x => x).ToArray()));
            var low = Int32.Parse(new String(numString.OrderBy(x => x).ToArray()));
            return Tuple.Create(high, low);
        }
        #endregion

        #region Datenverifizierung
        private static bool _checkNumberValidity(int num)
        {
            return num > 0 && _checkNumberValidity(num.ToString());
        }

        private static bool _checkNumberValidity(string num)
        {
            return Regex.IsMatch(num, @"^\d{1,4}$") && !_invalidStrings.Contains(num);
        }
        #endregion

        #region Konsole
        private static void _dumpExposition()
        {
            Console.WriteLine("Die vierstellige Kaprekar-Konstante");
            Console.WriteLine();
            Console.WriteLine("  \"Um die Kaprekar-Konstante einer drei-, vier-, sechs-, acht-, neun- oder \n"
                + "zehnstelligen Dezimalzahl, bei der nicht alle Ziffern gleich sind, zu erhalten,\n"
                + "ordne man die Ziffern der betreffenden Zahl (ggf. mit führenden Nullen) einmal\n"
                + "so, dass die größtmögliche Zahl entsteht, und dann so, dass die kleinstmögliche\n"
                + "Zahl entsteht. Dann bildet man durch Subtraktion die Differenz und wendet das\n"
                + "Verfahren auf das Resultat erneut an. Nach endlich vielen Schritten erhält man -\n"
                + "unabhängig von der Ausgangszahl - eine bestimmte Zahl. Diese Zahl heißt\n"
                + "„Kaprekar-Konstante“, die nach dem indischen Mathematiker D. R. Kaprekar (1905–\n"
                + "1986) benannt wurde, der diese Eigenschaft im Jahr 1949 zuerst für vierstellige\n"
                + "Zahlen fand.\"");
            Console.WriteLine("--Auszug Wikipedia, Kaprekar-Konstante");
            Console.WriteLine();
            Console.WriteLine("  Dieses Programm berechnet die Anzahl Schritte, die nötig sind, um von einer\n"
                + "beliebigen vierstelligen Zahl zur vierstelligen Kaprekar-Konstante zu gelangen.");
            Console.WriteLine();
            Console.Write("  Bitte geben Sie eine beliebige, vierstellige Zahl mit mindestens zwei\n"
                + "verschiedenen Ziffern an (führende Nullen sind gestattet), oder 'r' für eine\n"
                + "zufällige Zahl: ");
        }

        private static int _readInput()
        {
            var input = Console.ReadLine();
            var editedInput = input.Trim().ToLower();

            if (_rndStrings.Contains(editedInput))
            {
                return _generateRandom();
            }

            if (!_checkNumberValidity(editedInput))
            {
                Console.Write("Ihre Angabe {0} ist leider nicht gültig; versuchen Sie es noch einmal: ", input);
                return _readInput();
            }

            return Int32.Parse(input);
        }
        #endregion

        private static void _routine()
        {
            _dumpExposition();
            var input = _readInput();
            var result = KaprekarsConstant(input);
            Console.WriteLine();
            Console.WriteLine("  Es wurden {0} Durchläufe gebraucht, bis mit der Zahl {1} die Kaprekar-\n"
                + "Konstante erreicht wurde.", result, input);
            Console.Write("Beliebige Taste drücken...");
            Console.ReadKey();

            Console.Clear();
            _routine();
        }

        private static int _generateRandom()
        {
            int result;
            do
            {
                result = _rnd.Next(9999);
            } while (!_checkNumberValidity(result));
            return result;
        }
    }
}
