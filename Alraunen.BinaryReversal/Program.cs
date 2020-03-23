using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Alraunen.BinaryReversal
{
    class MainClass
    {
        /// <summary>
        /// Returns any positive integer number as its binary inversion, padded to the next highest n*8 bits in length.
        /// </summary>
        /// <param name="str">The integer number to reverse.</param>
        /// <returns>The reversed number.</returns>
        /// <example>Input: 120 (0x0111 1000)
        /// Output: 30 (0x0001 1110)
        /// 
        /// Input: 6014 (0x0110 0000 0001 0100)
        /// Output: 10246 (0x0010 1000 0000 0110)</example>
        public static string BinaryReversal(string str)
        {
            var val = uint.Parse(str);
            uint result = 0;
            while (val != 0)
            {
                for (var i = 0; i < 8; i++)
                {
                    result = result << 1;
                    result = result | (val & 1);
                    val = val >> 1;
                }
            }
            return result.ToString();
        }

        static void Main()
        {
            Console.Write("Enter number to reverse: ");
            var input = Console.ReadLine();
            var output = BinaryReversal(input);
            var textLength = Math.Max(input.Length, output.Length);
            Console.WriteLine("Input:  {0,"+textLength+"}, 0x{1}\n" +
                              "Output: {2,"+textLength+"}, 0x{3}", input, formatBinaryNumber(input), output, formatBinaryNumber(output));
            Console.ReadKey();
        }

        private static string formatBinaryNumber(string input)
        {
            var result = Convert.ToString(Convert.ToUInt32(input), 2);
            result = result.PadLeft(result.Length + (8 - result.Length % 8) % 8, '0');
            result = Regex.Replace(result, ".{4}", "$0 ").TrimEnd();
            return result;
        }
    }
}
