using System;
using System.Text;

namespace Assignment1
{
    public class Base62Converter
    {
        const string _base62 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        static void Main(string[] args)
        {
            Console.WriteLine("Decode Test");
            ulong decode = ToBase10("LpuPe81bc2w");
            Console.WriteLine("Result:  " + decode.ToString());
            bool passed = false;
            if (decode == 18327995462734721974) passed = true;
            Console.WriteLine(passed.ToString());

            Console.WriteLine();

            Console.WriteLine("Encode Test");
            string encode = ToBase62(18327995462734721974);
            Console.WriteLine("Result: " + encode);
            passed = false;
            if (encode == "LpuPe81bc2w") passed = true;
            Console.WriteLine(passed.ToString());
        }

        /// <summary>
        /// Apply for loop to videoId
        /// Get characters from right to left
        /// Get the position of the character from Base62CharArray
        /// Then add the position of the character from Base62CharArray to the 62 power char position of the inputstring
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns>base 10 Value </returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static ulong ToBase10(string videoId)
        {
            ulong base10 = 0;
            char[] Base62CharArray = _base62.ToCharArray();
            if (string.IsNullOrEmpty(videoId))
                throw new ArgumentNullException("provided videoId string is null exception");

            for (int i = 0; i < videoId.Length; i++)
            {
                char c = videoId[videoId.Length - 1 - i];
                int indexValue = Array.IndexOf(Base62CharArray, c);
                ulong power = IntPow(62, i);
                base10 += (ulong)indexValue * power;
            }

            return base10;
        }

        //Calculates power value separately as Math.pow is double and has calculation issue at 62 power 8
        /// <summary>
        /// Calculates Power
        /// </summary>
        /// <param name="baseValue"></param>
        /// <param name="exponent"></param>
        /// <returns>Power Value as result</returns>
        private static ulong IntPow(int baseValue, int exponent)
        {
            ulong result = 1;
            for (int i = 0; i < exponent; i++)
            {
                result *= (ulong)baseValue;
            }
            return result;
        }

        /// <summary>
        /// create a While Loop untill the number reaches 0
        /// Get the reminder of the Inputted number
        /// use the reminder and find the value from Base62CharArray
        /// divide number Value by 62 and get the Quotenet in to the loop
        /// </summary>
        /// <param name="base10"></param>
        /// <returns>Base62 String</returns>
        public static string ToBase62(ulong number)
        {
            char[] Base62CharArray = _base62.ToCharArray();
            if (number == 0)
                return "0";
            StringBuilder base62 = new StringBuilder();
            while (number > 0)
            {
                ulong reminder = number % 62;
                base62.Insert(0, Base62CharArray[reminder]);
                number /= 62;
            }

            return base62.Length > 0 ? base62.ToString() : "0";
        }
    }
}
