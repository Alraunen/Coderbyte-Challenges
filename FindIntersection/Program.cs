using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindIntersection
{
    class Program
    {
        public static string FindIntersection(string[] strArr) // using LINQ
        {
            try
            {
                return converter(strArr[0]).Intersect(converter(strArr[1]))
                    .Select(item => item.ToString())
                    .Aggregate((agg, item) => agg + ',' + item);
            }
            catch
            {
                return "false";
            }
        }

        public static string findIntersection(string[] strArr) // using enumerators
        {
            var arr1Ptr = converter(strArr[0]).GetEnumerator();
            var arr2Ptr = converter(strArr[1]).GetEnumerator();
            if (!arr1Ptr.MoveNext() || !arr2Ptr.MoveNext())
                return "false";
            
            IEnumerator<int> tmp;
            var result = "";
            Action swap = () => { tmp = arr2Ptr; arr2Ptr = arr1Ptr; arr1Ptr = tmp; };

            if (arr1Ptr.Current > arr2Ptr.Current)
                swap();

            do
            {
                if (arr1Ptr.Current == arr2Ptr.Current)
                {
                    result += arr1Ptr.Current + ",";
                    arr2Ptr.MoveNext();
                }
                else if (arr1Ptr.Current > arr2Ptr.Current)
                    swap();
            } while (arr1Ptr.MoveNext());

            if (result == "")
                return "false";
            result = result.Remove(result.Length - 1);
            return result;
        }

        static void Main(string[] args)
        {
            Console.WriteLine(findIntersection(new string[] { "1, 3, 4, 7, 13", "1, 2, 4, 13, 15" }));
            Console.ReadKey();
        }

        private static IEnumerable<int> converter(string str) => str.Split(',').Select(item => int.Parse(item.Trim()));
    }
}
