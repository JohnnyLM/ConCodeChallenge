using System;
using System.Linq;
using System.Collections;
using System.Globalization;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;

namespace ConCodeChallenge
{
    class Program
    {

        #region "HARD"

        public static int SwitchSort1(int[] arr)
        {
            return CalculateSwaps(arr);
        }
        private static int CalculateSwaps(int[] array)
        {
            var counter = 0;
            var dictionary = new Dictionary<int, State1>();
            var count = 0;
            foreach (var item in array.ToList())
            {
                dictionary.Add(count, new State1() { Number = item, Visited = false });
                count++;
            }

            foreach (var item in dictionary)
            {
                if (item.Value.Visited || item.Value.Number == item.Key)
                    continue;

                count = 0;
                var value = item.Value.Number - 1;

                while (!dictionary[value].Visited)
                {
                    dictionary[value].Visited = true;
                    value = dictionary[value].Number - 1;
                    count++;
                }
                counter += count - 1;
            }

            return counter;
        }
        public class State1
        {
            public int Number { get; set; }
            public bool Visited { get; set; }
        }

        public class State
        {
            public int moves;
            public int[] numbers;

            public State(int moves, int[] numbers)
            {
                this.moves = moves;
                this.numbers = numbers;
            }
        }
        public static int trySwitch(int[] desiredOrder, Queue<State> statesToCheck)
        {
            State currentState = statesToCheck.Dequeue();
            int[] currentOrder = currentState.numbers;
            int moves = currentState.moves;

            for (int i = 0; i < desiredOrder.Length; i++)
            {
                if (desiredOrder[i] != currentOrder[i])
                    break;
                if (i == desiredOrder.Length - 1)
                    return moves;
            }

            // Try every switch
            for (int i = 0; i < desiredOrder.Length; i++)
            {
                int[] newOrder = new int[desiredOrder.Length];
                Array.Copy(currentOrder, 0, newOrder, 0, desiredOrder.Length);
                int index2 = i - currentOrder[i];
                if (index2 < 0)
                    index2 = desiredOrder.Length + index2;

                int temporary = newOrder[i];
                newOrder[i] = newOrder[index2];
                newOrder[index2] = temporary;

                State newState = new State(moves + 1, newOrder);
                statesToCheck.Enqueue(newState);

                int[] newOrder2 = new int[desiredOrder.Length];
                Array.Copy(currentOrder, 0, newOrder2, 0, desiredOrder.Length);
                index2 = i + currentOrder[i];
                if (index2 > desiredOrder.Length - 1)
                    index2 = index2 - desiredOrder.Length;

                temporary = newOrder2[i];
                newOrder2[i] = newOrder2[index2];
                newOrder2[index2] = temporary;

                State newState2 = new State(moves + 1, newOrder2);
                statesToCheck.Enqueue(newState2);
            }
            return -1;
        }
        public static int SwitchSortAnsw(int[] arr)
        {
            int[] sorted = new int[arr.Length];
            Array.Copy(arr, 0, sorted, 0, arr.Length);
            Array.Sort(sorted);

            Queue<State> statesToCheck = new Queue<State>();
            State initialState = new State(0, arr);
            statesToCheck.Enqueue(initialState);

            int result = -1;
            while (result == -1)
                result = trySwitch(sorted, statesToCheck);

            return result;
        }

        ///// <summary>
        ///// https://en.m.wikipedia.org/wiki/Circular_buffer
        ///// </summary>
        ///// <param name="arr"></param>
        ///// <returns></returns>
        //public static int SwitchSort(int[] arr)
        //{
        //    //2, 3, 1

        //    int result = 0;
        //    //List<int> lst = new List<int>();
        //    //List<int> lst = arr.ToList<int>();
        //    //int[] lst = new int[arr.Length];
        //    int[] lst = arr;
        //    int min = arr.Min();
        //    int max = arr.Max();


        //    for (int i = 0; i < lst.Length; i++)
        //    {
        //        for (int j = 1; j <= lst[i]; j++)
        //        {
        //            lst = shiftLeft(lst);

        //            result += 1;

        //            if (CheckSorting(lst))
        //            {
        //                goto EndOfLoop;
        //                //return result.ToString();
        //            }
        //        }
        //    }
        //EndOfLoop:;


        //    //var str = string.Empty;
        //    //foreach (int i in lst)
        //    //{
        //    //    str += i.ToString() + ",";
        //    //}


        //    //char c = ',';
        //    //str = str.TrimEnd(c);

        //    return result;
        //}
        //private static int GetIndexDesc(int[] arr)
        //{
        //    int result = 0;

        //    for (int i = 0; i < arr.Length; i++)
        //    {
        //        if (arr[i] > result)
        //        {
        //            result = arr[i];
        //        }
        //        else
        //        {
        //            return i;
        //        }
        //    }

        //    return result;
        //}
        //private static bool CheckSorting(int[] arr)
        //{
        //    int result = 0;

        //    if (arr.Length > 1)
        //    {
        //        foreach (int i in arr)
        //        {
        //            if (i > result)
        //            {
        //                result = i;
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        return true;
        //    }

        //    return true;
        //}
        //public static int[] shiftLeft(int[] arr)
        //{
        //    int[] demo = new int[arr.Length];

        //    for (int i = 0; i < arr.Length - 1; i++)
        //    {
        //        demo[i] = arr[i + 1];
        //    }

        //    demo[demo.Length - 1] = arr[0];

        //    return demo;
        //}
        //public static int[] shiftRight(int[] arr)
        //{
        //    int[] demo = new int[arr.Length];

        //    for (int i = 1; i < arr.Length; i++)
        //    {
        //        demo[i] = arr[i - 1];
        //    }

        //    demo[0] = arr[demo.Length - 1];

        //    return demo;
        //}


        public static string PatternChaser(string str)
        {
            int count = 0;
            string result = "no null";
            string start = string.Empty;
            string pattern = string.Empty;


            for (int i = 0; i < str.Length; i++)
            {
                start = string.Empty;

                for (int j = i; j < str.Length; j++)
                {
                    start += str[j].ToString();

                    for (int k = j; k < str.Length; k++)
                    {
                        pattern = string.Empty;

                        for (int m = k + 1; m < str.Length; m++)
                        {
                            pattern += str[m].ToString();
                            if (start.Equals(pattern) && start.Length > 1)
                            {
                                if (pattern.Length > count)
                                {
                                    count = pattern.Length;
                                    result = "yes " + start;
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }

        #endregion

        #region "MEDIUM"

        public static int CountingMinutes(string str)
        {
            DateTime dt1;
            DateTime dt2;
            string[] s = str.Split("-");

            string startTime = DateTime.Now.ToShortDateString() + " " + s[0];
            string endTime = DateTime.Now.ToShortDateString() + " " + s[1];

            if (DateTime.TryParse(startTime, out dt1) && DateTime.TryParse(endTime, out dt2))
            {
                if (dt1 > dt2)
                {
                    endTime = DateTime.Parse(endTime).AddHours(24).ToString();
                }

                TimeSpan duration = DateTime.Parse(endTime).Subtract(DateTime.Parse(startTime));

                return (int)(duration.TotalMinutes);
            }

            return 0;
        }

        public static string FibonacciChecker(long x)
        {
            if (IsPerfectSquare(5 * x * x + 4) || IsPerfectSquare(5 * x * x - 4))
                return "yes";
            return "no";
        }
        private static bool IsPerfectSquare(long x)
        {
            long s = (int)Math.Sqrt(x);
            return (s * s == x);
        }
        public static string FibonacciChecker1(int num)
        {

            if (num == 0 || num == 1) { return "yes"; }
            int sum1 = 0;
            int sum2 = 1;
            int sumAxlr = 0;

            while (sum2 <= num)
            {
                sumAxlr = sum1 + sum2;
                sum1 = sum2;
                sum2 = sumAxlr;
                if (sum2 == num) { return "yes"; }
            }
            return "no";

        }
        public static string FibonacciCheckerAnsw(int num)
        {
            return IsFibonacci(num) ? "yes" : "no";
        }
        static bool IsFibonacci(int numberToCheck, int firstNum = 0, int secondNum = 1)
        {
            var newNum = firstNum + secondNum;
            if (newNum < numberToCheck)
                return IsFibonacci(numberToCheck, secondNum, newNum);

            return numberToCheck == newNum;
        }

        public static int NumberSearch(string str)
        {
            int sum = 0;
            int count = 0;
            char[] arr = str.ToString().ToCharArray();

            foreach (char c in arr)
            {
                if (char.IsDigit(c))
                {
                    sum += int.Parse(c.ToString());
                }
                else if (Char.IsLetter(c))
                {
                    count++;
                }
            }

            return (int)Math.Round((double)sum / count, MidpointRounding.AwayFromZero);
        }
        public static string NumberSearchAnsw(string str)
        {
            var ints = str.Where(x => Char.IsDigit(x)).Select(x => Char.GetNumericValue(x)).Sum();
            var letters = str.Where(x => Char.IsLetter(x)).Count();

            return Math.Round((double)ints / letters, MidpointRounding.AwayFromZero).ToString();
        }

        public static bool ArrayAddition(int[] arr)
        {
            if (arr.Length > 0)
            {
                int sum = 0;

                Array.Sort(arr);

                for (int i = 0; i < arr.Length - 1; i++)
                {
                    sum += arr[i];
                }

                if (sum >= arr[arr.Length - 1])
                {
                    return true;
                }
            }

            return false;
        }

        public static string CharacterRemoval(string[] arr)
        {
            string[] arr1 = arr[0].Split(",");
            string[] arr2 = arr[1].Split(",");
            int result = arr1[0].Length;

            List<string> lst = new List<string>();

            foreach (string word in arr1)
            {
                for (int i = 0; i < word.Length; i++)
                {
                    if (i == 0)
                    {
                        lst.Add(word);
                    }
                    else
                    {
                        foreach (var permutation in Permutations(word, i))
                        {
                            lst.Add(permutation);
                        }
                    }
                }
            }

            lst = lst.Distinct().ToList();

            foreach (string s in lst)
            {
                if (arr2.Contains(s))
                {
                    if ((arr[0].Trim().Length - s.Length) < result)
                        result = (arr[0].Length - s.Length);
                }
            }

            if (result == arr1[0].Length)
                return "-1";
            return result.ToString();
        }
        static IEnumerable<string> Permutations(string word, int length)
        {
            if (length == 1)
            {
                return word.Select(c => c.ToString());
            }
            else
            {
                return Permutations(word, length - 1)
                    .SelectMany(c => word.Where(d => !c.Contains(d)),
                        (c1, c2) => String.Concat(c1, c2));
            }
        }
        public static int CharacterRemovalAnsw(string[] arr)
        {
            string letterJumble = arr[0];
            string[] words = arr[1].Split(',');
            int minLettersRemoved = -1;

            foreach (string word in words)
            {
                if (word.Length > letterJumble.Length) continue;
                if (word.Any(c => letterJumble.Contains(c) == false)) continue;

                string newJumble = letterJumble;

                for (int i = 0; i < newJumble.Length; i++)
                {
                    if (i > word.Length - 1 || newJumble[i] != word[i])
                    {
                        newJumble = newJumble.Remove(i, 1);
                        i--;
                    }

                    if (newJumble == word)
                    {
                        int lettersRemoved = letterJumble.Length - word.Length;
                        if (minLettersRemoved < 0 || lettersRemoved < minLettersRemoved) minLettersRemoved = lettersRemoved;
                        break;
                    }
                }
            }

            return minLettersRemoved;
        }

        public static int Division(int num1, int num2)
        {
            int result = 0;
            int nrLarge;
            int nrSmall;

            if (num1 > num2)
            {
                nrLarge = num1;
                nrSmall = num2;
            }
            else
            {
                nrLarge = num2;
                nrSmall = num1;
            }

            if (nrLarge % nrSmall == 0)
                return nrSmall;

            while (nrLarge % nrSmall != 0)
            {
                int remainder = nrLarge % nrSmall;
                nrLarge = nrSmall;
                nrSmall = remainder;
                result = nrSmall;
            }

            return result;
        }
        public static int DivisionAnsw(int num1, int num2)
        {
            int lowerNumber = Math.Min(num1, num2);

            for (int i = lowerNumber; i > 0; i--)
            {

                if (num1 % i == 0 && num2 % i == 0)
                {
                    return i;
                }
            }

            return 0;
        }

        public static int PrimeMover(int nr)
        {
            //2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53

            int counter = 10000;

            List<int> lst = new List<int>();

            for (int i = 1; i < counter; i++)
            {
                int a = 0;
                for (int j = 1; j <= i; j++)
                {
                    if (i % j == 0)
                    {
                        a++;
                    }
                }
                if (a == 2)
                {
                    lst.Add(i);
                }
            }

            return lst[nr - 1];
        }
        public static int PrimeMoverAnsw(int num)
        {
            List<int> primeNumbers = new List<int>();

            for (int i = 2; primeNumbers.Count < num; i++)
            {
                if (IsPrimeNumber(i))
                {
                    primeNumbers.Add(i);
                }
            }

            return primeNumbers[primeNumbers.Count - 1];
        }
        static bool IsPrimeNumber(int number)
        {
            for (int i = 2; i < number; i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        public static string RunLength(string str)
        {

            char[] ch = str.ToCharArray();
            string result = string.Empty;
            int count = 1;

            for (int i = 0; i <= ch.Length; i++)
            {
                if (i > 0)
                {
                    if (i < ch.Length)
                    {
                        if (ch[i - 1] == ch[i])
                        {
                            count += 1;
                        }
                        else if (i < ch.Length)
                        {
                            result += count.ToString() + ch[i - 1];
                            count = 1;
                        }
                    }
                    else if (ch[i - 2] != ch[i - 1])
                    {
                        result += "1" + ch[i - 1];
                    }
                    else
                    {
                        result += count.ToString() + ch[i - 1];
                    }
                }
                else if (ch.Length == 1)
                {
                    return count.ToString() + ch[i];
                }
            }

            return result;
        }
        public static string RunLengthAnsw(string str)
        {
            var output = string.Empty;
            var chars = str.ToArray();
            char lastChar = ' ';
            var charCount = 1;

            for (var i = 0; i < chars.Length; i++)
            {
                if (i == 0)
                {
                    lastChar = chars[i];
                }
                else
                {
                    if (lastChar == chars[i])
                    {
                        charCount++;
                    }
                    else
                    {
                        output += charCount.ToString() + lastChar;
                        charCount = 1;
                    }
                    lastChar = chars[i];
                }
            }
            output += charCount.ToString() + lastChar;

            return output;
        }

        public static bool StringScramble(string str1, string str2)
        {
            List<char> lst2 = new List<char>(str2);
            List<char> lst1 = new List<char>(str1);

            foreach (char s in lst2)
            {
                if (lst1.Contains(s))
                {
                    lst1.Remove((from a in lst1 where a == s select a).First());
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public static string PalindromeTwo(string str)
        {

            //str = new string(str.Where(c => !char.IsPunctuation(c)).ToArray());
            str = new string(str.Where(c => char.IsLetter(c)).ToArray());
            str = str.ToLower();

            char[] chars = (str.Replace(" ", "")).ToCharArray();
            char[] reversed = (str.Replace(" ", "")).ToCharArray();
            Array.Reverse(reversed);

            return new string(chars) == new string(reversed) ? "true" : "false";
        }

        public static bool PrimeTime(int nr)
        {
            if (nr <= 1) return false;
            if (nr == 2) return true;
            if (nr % 2 == 0) return false;

            return true;
        }

        /// <summary>
        /// MEDIUM
        /// </summary>
        /// <param name="strArr"></param>
        /// <returns></returns>
        public static bool FormattedNumber(string[] strArr)
        {
            int intDot = 0;
            int intComma = 0;
            int intCount = 0;

            string str = strArr[0];
            char[] ch = strArr[0].ToCharArray();

            for (int i = ch.Length - 1; i >= 0; i--)
            {
                if (char.IsLetter(ch[i]))
                    return false;
                else if (char.IsDigit(ch[i]))
                    intCount++;

                if (ch[i] == '.')
                {
                    intDot++;
                    intCount = 0;
                }

                if (ch[i] == ',')
                {
                    if (intCount == 3)
                    {
                        intComma++;
                        intCount = 0;
                    }
                    else
                        return false;
                }
            }

            if ((intComma == 0 & str.Length > 3) & intDot == 0)
                return false;
            if ((intComma == 0 & str.Length > 3 & intCount > 3) & intDot > 0)
                return false;
            else if (intDot > 1)
                return false;

            return true;
        }
        public static bool FormattedNumberAnsw(string[] strArr)
        {

            // code goes here  
            bool isNumber = false;
            if (Regex.IsMatch(strArr[0].ToString(), @"^\d{0,3}((,\d{3})*)?(\.\d+)?$"))
            // if (Regex.IsMatch(strArr[0].ToString(), @"^(\d+)?\.?(\d{0,})?"))
            // if (Regex.IsMatch(strArr[0].ToString(), @"^\d+(\.\d+)?$"))
            // if (Regex.IsMatch(strArr[0].ToString(), @"^\d\d+\.\d*$"))
            {
                isNumber = true;
            }
            return isNumber;

        }
        #endregion

        #region "EASY"

        public static string BitwiseTwo(string[] strArr)
        {
            string result = string.Empty;
            for (int i = 0; i < strArr[0].Length; i++)
            {
                result += (strArr[0][i] == '1' && strArr[1][i] == '1') ? '1' : '0';
            }

            return result;
        }

        public static int BinaryReversal(string str)
        {
            string result = string.Empty;
            int num = int.Parse(str);

            string binary = Convert.ToString(num, 2);

            while(binary.Length % 8 > 0)
            {
                binary = "0" + binary;
            }

            char[] ch = binary.ToCharArray();
            var reversed = ch.Reverse();

            result = string.Join("", reversed);

            return Convert.ToInt32(result, 2);
        }

        public static string BitwiseOne(string[] arr)
        {
            var result = string.Empty;

            string[] arr1 = arr[0].Split(",");
            string[] arr2 = arr[1].Split(",");
            char[] ch1 = string.Join(string.Empty, arr1).ToCharArray();
            char[] ch2 = string.Join(string.Empty, arr2).ToCharArray();

            for (int i = 0; i < ch1.Length; i++)
            {
                if (ch1[i] == '1' || ch2[i] == '1')
                    result += "1";
                else
                    result += "0";
            }

            return result;
        }
        public static string BitwiseOne1(string[] strArr)
        {
            var result = new StringBuilder();
            for (int i = 0; i < strArr[0].Length; i++)
            {
                if (strArr[0][i] == '0' && strArr[1][i] == '0')
                {
                    result.Append("0");
                }
                else
                {
                    result.Append("1");
                }
            }

            return result.ToString();
        }
        public static string BitwiseOneAnsw(string[] strArr)
        {
            string result = string.Empty;
            for (int i = 0; i < strArr[0].Length; i++)
            {
                result += (strArr[0][i] == '1' || strArr[1][i] == '1') ? '1' : '0';
            }

            return result;
        }

        /// <summary>
        /// Not passed
        /// For input "vhhgghhgghhk" the output was incorrect. The correct output is vk
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string PalindromeCreator(string str)
        {
            string result = string.Empty;
            str = "mmop";

            char[] ch = str.ToCharArray();
            var lst = ch.Select(c => c.ToString()).ToList();
            var temp = ch.Select(c => c.ToString()).ToList();

            if (CheckPalindrome(temp) && temp.Count > 2)
                return "palindrome";

            for(int i = 0; i < 2; i++)
            {
                if (i == 0)
                    for (int j = 0; j <  lst.Count; j++)
                    {
                        temp = ch.Select(c => c.ToString()).ToList(); ;

                        temp.RemoveAt(j);
                        result = lst[j];

                        if (CheckPalindrome(temp) && temp.Count > 2)
                            return result;
                    }
                else 
                {
                    for (int j = 0; j < lst.Count - 1; j++)
                    {
                        temp = ch.Select(c => c.ToString()).ToList(); ;

                        temp.RemoveAt(j);
                        temp.RemoveAt(j);

                        result = lst[j] + lst[j +1];

                        if (CheckPalindrome(temp) && temp.Count > 2)
                            return result;
                    }
                }
            }

            return "not possible";
        }
        private static bool CheckPalindrome(List<string> str)
        {
            var count = 0; 

            for(int i = 0; i < str.Count; i++)
            {
                if ((str.Where<string>(a => (a == str[i])).Count() % 2) != 0)
                {
                    count += 1;
                }
                else
                {
                    if(str[i] != str[str.Count - 1 - i])
                        return false;
                }

                if (count > 1)
                    return false;
            }

            return true;
        }

        public static string NextPalindrome(int num)
        {
            var output = (num + 1).ToString();

            do
            {
                char[] charArray = output.ToString().ToCharArray();
                Array.Reverse(charArray);
                var reverse = new string(charArray);
                if (output == reverse)
                    return output;
                else
                    output = (int.Parse(output.ToString()) + 1).ToString();
            }
            while (num.ToString() != output.ToString());

            return output;
        }

        public static string OtherProducts(int[] arr)
        {
            arr = new int[] { 1, 4, 3 };

            string result = string.Empty;
            List<int> list = arr.ToList<int>();

            for(int i = 0; i < list.Count; i++)
            {
                var lst = list.Where((v, x) => x != i).ToList();
                var product = 1;

                foreach(var j in lst)
                {
                    product *= j;
                }

                result += product.ToString() + "-";
            }

            return result.Remove(result.Length - 1, 1);
        }
        public static string OtherProductsAnsw(int[] arr)
        {
            string result = string.Empty;

            for (int i = 0; i < arr.Length; i++)
            {
                var lst = arr.Where((v, x) => x != i).ToList();
                var product = 1;

                foreach (var j in lst)
                {
                    product *= j;
                }

                result += product.ToString() + "-";
            }

            return result.Remove(result.Length - 1, 1);
        }

        public static bool WaveSorting(int[] arr)
        {
            //a1 > a2 < a3 > a4 < a5 >
            //Array.Sort(arr);
            List<int> list = arr.ToList<int>();

            //var ascendingOrder = list.OrderBy(i => i).ToList();
            //var descendingOrder = list.OrderByDescending(i => i).ToList();
            list.Sort((a, b) => a.CompareTo(b)); // ascending sort
            //list.Sort((a, b) => b.CompareTo(a)); // descending sort

            var indexH = arr.Length - 1;
            var indexM = (arr.Length - 1) / 2;
            for (int i = 0; i <= indexM; i++)
            {
                if (list[indexH - i] <= list[indexM - i])
                {
                    return false;
                }
            }

            return true;
        }
        public static string WaveSortingAnsw(int[] arr)
        {
            Array.Sort(arr);
            var highIndex = arr.Length - 1;
            var midIndex = (arr.Length - 1) / 2;
            for (int i = 0; i <= midIndex; i++)
            {
                if (arr[highIndex - i] <= arr[midIndex - i])
                {
                    return "false";
                }
            }
            if (arr.Length % 2 == 1)
            {
                if (arr[midIndex + 1] <= arr[0])
                {
                    return "false";
                }
            }

            return "true";
        }

        public static string ScaleBalancing(string[] strArr)
        {
            strArr[0] = strArr[0].Replace("[", "").Replace("]", "");
            strArr[1] = strArr[1].Replace("[", "").Replace("]", "");

            string[] arrStr1 = strArr[0].ToString().Split(",");
            string[] arrStr2 = strArr[1].ToString().Split(",");

            int[] balance = Array.ConvertAll(arrStr1, s => int.Parse(s));
            int[] weight = Array.ConvertAll(arrStr2, s => int.Parse(s));

            List<int> list = weight.ToList<int>();

            list.Add(0);
            list.Sort();

            var combinations = (from item1 in list
                                from item2 in list
                                select Tuple.Create(item1, item2)).Distinct().ToList();

            foreach (var comb in combinations)
            {
                if (comb.Item1 != comb.Item2)
                {
                    if ((balance[0] + comb.Item1) - (balance[1] + comb.Item2) == 0 ||
                        (balance[0] + comb.Item2) - (balance[1] + comb.Item1) == 0 ||
                        (balance[1] + comb.Item1) - (balance[0] + comb.Item2) == 0 ||
                        (balance[1] + comb.Item2) - (balance[0] + comb.Item1) == 0 ||
                        (balance[0] + comb.Item1 + comb.Item2) - (balance[1]) == 0 ||
                        (balance[0]) - (balance[1] + comb.Item1 + comb.Item2) == 0)
                    {
                        if (comb.Item1 == 0)
                        {
                            return comb.Item2.ToString();
                        }
                        else if (comb.Item2 == 0)
                        {
                            return comb.Item1.ToString();
                        }
                        else if (comb.Item1 < comb.Item2)
                            return comb.Item1 + "," + comb.Item2;
                        else
                            return comb.Item2 + "," + comb.Item1;
                    }
                }
            }

            return "not possible";
        }
        public static string ScaleBalancing1(string[] strArr)
        {
            //{"[3, 4]", "[1, 2, 7, 7]"}
            //{"[13, 4]", "[1, 2, 3, 6, 14]"}
            //"{[5, 9]", "[1, 2, 6, 7]"}

            strArr = new string[] { "[5, 9]", "[1, 2, 6, 7]" };

            strArr[0] = strArr[0].Replace("[", "").Replace("]", "");
            strArr[1] = strArr[1].Replace("[", "").Replace("]", "");

            string[] arrStr1 = strArr[0].ToString().Split(",");
            string[] arrStr2 = strArr[1].ToString().Split(",");

            int[] balance = Array.ConvertAll(arrStr1, s => int.Parse(s));
            int[] weight = Array.ConvertAll(arrStr2, s => int.Parse(s));

            //weight = weight.Concat(new int[] { 0 }).ToArray();

            //var newList = CreateSubsets<int>(weight.ToArray());
            //var newlist = GetPowerSet(weight.ToList());

            List<int> list = weight.ToList<int>();

            list.Add(0);
            list.Sort();

           var combinations = (from item1 in list
                              from item2 in list
                              select Tuple.Create(item1, item2)).Distinct().ToList();

            //var lst = combinations.Distinct().ToList();

            foreach(var comb in combinations)
            {
                //string s = (balance[0] + comb.Item1).ToString() + " - " + (balance[1] + comb.Item2).ToString();

                //return s;

                if (comb.Item1 != comb.Item2)
                {
                    if ((balance[0] + comb.Item1) - (balance[1] + comb.Item2) == 0 ||
                        (balance[0] + comb.Item2) - (balance[1] + comb.Item1) == 0 ||
                        (balance[1] + comb.Item1) - (balance[0] + comb.Item2) == 0 ||
                        (balance[1] + comb.Item2) - (balance[0] + comb.Item1) == 0 ||
                        (balance[0] + comb.Item1 + comb.Item2) - (balance[1]) == 0 ||
                        (balance[0]) - (balance[1] + comb.Item1 + comb.Item2) == 0)
                    {
                        if (comb.Item1 == 0)
                        {
                            return comb.Item2.ToString();
                        }    
                        else if (comb.Item2 == 0)
                        {
                            return comb.Item1.ToString();
                        }
                        else if(comb.Item1 < comb.Item2)
                            return comb.Item1 + "," + comb.Item2;
                        else
                            return comb.Item2 + "," + comb.Item1;
                    }
                    //else if( (balance[0] + comb.Item1) - (balance[1]) == 0 ||
                    //         (balance[0]) - (balance[1] + comb.Item1) == 0)
                    //{
                    //    return comb.Item1.ToString();
                    //}
                }
            }

            return "not possible";
        }

        public static string TimeDifference(string[] strArr)
        {
            DateTime[] dt = new DateTime[strArr.Length];

            for (int i = 0; i < strArr.Length; i++)
            {
                dt[i] = DateTime.Parse(strArr[i]);
            }

            Array.Sort(dt);
            Array.Reverse(dt);

            double minuteDiff = 24 * 60 - (dt[0] - dt[dt.Length - 1]).TotalMinutes;

            for (int i = 0; i < dt.Length - 1; i++)
            {
                if (minuteDiff > (dt[i] - dt[i + 1]).TotalMinutes)
                {
                    minuteDiff = (dt[i] - dt[i + 1]).TotalMinutes;
                }
            }

            return minuteDiff.ToString();
        }
        public static int TimeDifferenceAnsw(string[] strArr)
        { 
            DateTime[] dateTimes = new DateTime[strArr.Length];
            for (int i = 0; i<strArr.Length; i++)
            {
              dateTimes[i] = DateTime.Parse(strArr[i]);
            }

            Array.Sort(dateTimes);
            Array.Reverse(dateTimes);

            double totalMinutes = 24 * 60 - (dateTimes[0] - dateTimes[dateTimes.Length - 1]).TotalMinutes;
            for (int i = 0; i<dateTimes.Length - 1; i++)
            {
              if ((dateTimes[i] - dateTimes[i + 1]).TotalMinutes < totalMinutes) 
                { 
                    totalMinutes = (dateTimes[i] - dateTimes[i + 1]).TotalMinutes; 
                }
            }

            return (int)totalMinutes;
          }

        public static string StringMerge(string str)
        {
            str = str.Replace(" ", "");
            var result = string.Empty;

            string[] arr = str.Split("*");

            for (int i = 0; i < arr[0].Length; i++)
            {
                result += arr[0][i].ToString() + arr[1][i].ToString();
            }

            return result;
        }

        public static bool HappyNumbers(int nr)
        {
            //int[] intArr = nr.ToString().Select(o => Convert.ToInt32(o) - 48).ToArray();

            char[] ch = nr.ToString().ToCharArray();
            int[] intArr = Array.ConvertAll(ch, s => int.Parse(s.ToString()));
            int result = 0;

            foreach (int i in intArr)
            {
                result += (int)Math.Pow(Convert.ToDouble(i), 2);
            }

            if (result == 1)
                return true;
            else if (result.ToString().Length == 1)
                return false;
            else
                return HappyNumbers(result);
        }

        public static string ASCIIConversion(string str)
        {
            char[] ch = str.ToCharArray();
            var result = string.Empty;

            foreach(char c in ch)
            {
                if (c == ' ')
                {
                    result += " ";
                }
                else
                {
                    int val = c;
                    result += val.ToString();
                }
            }

            //int i = 65;
            //char c = Convert.ToChar(i);

            return result;
        }

        public static bool DistinctCharacters(string str)
        {
            return (str.Distinct().Count() >= 10);
        }

        public static string CorrectPath(string str)
        {
            //rdrdr??rddd?dr

            IList<string> lst = new List<string> {""};

            foreach (char c in str)
            {
                if ("udrl".IndexOf(c) >= 0)
                {
                    //String without ?
                    lst = lst.Select(s => s + c.ToString()).ToList();
                }
                else
                {
                    //All combinations with udrl (??? ==> (udrl): 4*4*4)
                    lst = lst.SelectMany(s => "udrl".Select(d => s + d.ToString())).ToList(); 
                }
            }

            return lst.Where(s => PathCombination(s)).First();
        }
        private static bool PathCombination(string str)
        {
            //Every str from lst is tested here

            int y= 0;
            int x = 0;
            bool[] array = new bool[25];                    //Bool with 25 places for storing combination checked Y/N

            foreach (char c in str)
            {
                if (c == 'd')
                {
                    y += 1;
                }
                if (c == 'u')
                {
                    y += -1;
                }
                if (c == 'r')
                {
                    x += 1;
                }
                if (c == 'l')
                {
                    x += -1;
                }

                if (x < 0 || y < 0 || x > 4 || y > 4)
                {
                    return false;
                }

                if (array[x * 5 + y] == true)           //Already checked this combination
                    return false;                       //Go to next str in lst for testing

                array[x * 5 + y] = true;
            } 

            return (x == 4 && y == 4) ? true : false;
        }
        ///// <summary>
        ///// Not working!!!
        ///// </summary>
        ///// <param name="str"></param>
        ///// <returns></returns>
        //public static string CorrectPath(string str)
        //{
        //    char[] ch = str.ToString().ToCharArray();
        //    char[] res = str.ToString().ToCharArray();

        //    char[] dir = new char[4] { 'r', 'l', 'u', 'd' };
        //    int intQ = str.Where(a => (a == '?')).Count();

        //    List<string> lst = new List<string>();

        //    //for(int i = 0; i < intQ; i++)
        //    //{
        //    //    return GetResult(ch, res, dir, i);
        //    //}

        //    foreach (char c1 in dir)
        //    {
        //        for (int i = 0; i < ch.Length; i++)
        //        {
        //            if (ch[i] == '?')
        //            {
        //                res[i] = c1;

        //                lst.Add(new string(res));

        //                foreach (char c2 in dir)
        //                {
        //                    for (int j = i + 1; j < ch.Length; j++)
        //                    {
        //                        if (ch[j] == '?')
        //                        {
        //                            res[j] = c2;


        //                            lst.Add(new string(res));

        //                            foreach (char c3 in dir)
        //                            {
        //                                for (int k = j + 1; k < ch.Length; k++)
        //                                {
        //                                    if (ch[k] == '?')
        //                                    {
        //                                        res[k] = c3;

        //                                        lst.Add(new string(res));
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }


        //    lst.RemoveAll(u => u.Contains('?'));
        //    List<string> noDupes = lst.Distinct().ToList();

        //    foreach(string result in noDupes)
        //    if (PathCombination(result))
        //        return result;


        //    return string.Empty;
        //}
        public static string CorrectPath1(string str)
        {
            string[] letters = new string[] { "r", "l", "u", "d" };

            if (str.Contains("?"))
            {
                var regex = new Regex(Regex.Escape("?"));
                foreach (var letter in letters)
                {
                    string current = CorrectPathAnsw(regex.Replace(str, letter, 1));     //Recursive function
                    if (current != null)
                    {
                        return current;
                    }
                }
            }

            int x = 0;
            int y = 0;
            bool[,] grid = new bool[5, 5];
            foreach (char direction in str)
            {
                switch (direction)
                {
                    case 'r':
                        x++;
                        break;
                    case 'l':
                        x--;
                        break;
                    case 'u':
                        y--;
                        break;
                    case 'd':
                        y++;
                        break;
                }

                if (x < 0 || y < 0 || x > 4 || y > 4 || grid[x, y])
                {
                    return null;
                }
                else
                {
                    grid[x, y] = true;
                }
            }

            if (x == 4 && y == 4)
            {
                return str;
            }
            else
            {
                return null;
            }

        }
        public static string CorrectPathAnsw(string str)
        {
            IList<string> possibilities = new List<string>();

            possibilities.Add("");
            
            foreach (char c in str)
            {
                if ("udrl".IndexOf(c) >= 0)
                {
                    possibilities = possibilities.Select(s => s + c.ToString()).ToList();
                }
                else
                {
                    possibilities = possibilities.SelectMany(s => "udrl".Select(d => s + d.ToString())).ToList();
                }
            }

            return possibilities.Where(s => EvalSolution(s)).First();
        }
        static bool EvalSolution(string str)
        {
            int row = 0;
            int col = 0;
            
            bool[] array = new bool[25];
            array[0] = true;

            foreach (char c in str)
            {
                switch (c)
                {
                    case 'u':
                        row--;
                        break;
                    case 'd':
                        row++;
                        break;
                    case 'l':
                        col--;
                        break;
                    case 'r':
                        col++;
                        break;
                    default:
                        return false;
                }

                if (row < 0 || row > 4 || col < 0 || col > 4)
                    return false;

                if (array[row * 5 + col] == true)
                    return false;
                
                array[row * 5 + col] = true;
            }

            return (row == 4 && col == 4);
        }


        public static bool SerialNumber(string str)
        {
            string[] arr= str.Split(".");

            if (arr.Length != 3)
                return false;

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].Length != 3)
                    return false;


            //int[] intArr = Array.ConvertAll(arr[i].ToArray(), a => int.Parse(a.ToString()));
            int[] intArr = arr[i].Select(c => Int32.Parse(c.ToString())).ToArray();

            if (! (intArr[2] > intArr[1] || intArr[2] > intArr[0]))
                    return false;

                if (i == 0)
                {
                    var num1 = Array.ConvertAll(arr[0].ToArray(), a => int.Parse(a.ToString())).Sum();
                    if (num1 % 2 != 0)
                    {
                        return false;
                    }
                }

                if (i == 3)
                {
                    var num2 = Array.ConvertAll(arr[1].ToArray(), a => int.Parse(a.ToString())).Sum();
                    if (num2 % 2 == 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static int ClosestEnemy(int[] arr)
        {
            int result = arr.Length;

            var index = Array.FindIndex(arr, row => row == 1);

            if (Array.FindIndex(arr, row => row == 2) == -1)
                return 0;

            for (int i = 0; i < arr.Length; i++)
            {
                if(arr[i] == 2)
                {
                    if(Math.Abs(index - i) < result)
                    {
                        result = Math.Abs(index - i);
                    }
                }
            }

            return result;
        }

        public static int BasicRomanNumerals(string str)
        {
            int result = 0;

            char[] ch = str.ToCharArray();

            for (int i = 0; i < ch.Length; i++)
            {
                if (i < ch.Length -1)
                {
                    if (ch[i] == 'I' && (ch[i + 1] == 'V' || ch[i + 1] == 'X'))
                    {
                        result -= 1;
                    }
                    else if(ch[i] == 'X' && (ch[i + 1] == 'L' || ch[i + 1] == 'C'))
                    {
                        result -= 10;
                    }

                    else
                    {
                        result += ConvertRomanNum(ch[i].ToString());
                    }
                }
                else
                {
                    result += ConvertRomanNum(ch[i].ToString());
                }               
            }

            return result;
        }
        private static int ConvertRomanNum(string numeral)
        {
            switch (numeral)
            {
                case "I":
                    return 1;
                case "V":
                    return 5;
                case "X":
                    return 10;
                case "L":
                    return 50;
                case "C":
                    return 100;
                case "D":
                    return 500;
                case "M":
                    return 1000;
                default:
                    return 0;
            }
        }

        public static string VowelSquare(string[] strArr)
        {
            char[] arr1 = strArr[0].ToString().ToCharArray();
            char[] arr2 = strArr[1].ToString().ToCharArray();
            char[] arr3 = new char[arr1.Length];
            char[] arr4 = new char[arr1.Length];
            string result = string.Empty;

            if (strArr.Length >= 3)
            {
                arr3 = strArr[2].ToString().ToCharArray();
            }
            if (strArr.Length == 4)
            {
                arr4 = strArr[3].ToString().ToCharArray();
            }
        

            //First row + second row
                if(IsVowelSquare(arr1, arr2, out result))
                {
                    return "0" + "-" + result;
                }

            //Second row + third row
            if (strArr.Length >= 3)
            {
                    if (IsVowelSquare(arr2, arr3, out result))
                    {
                        return "1" + "-" + result;
                    }
            }

            //Third row + fourth row
            if (strArr.Length > 3)
            {
                    if (IsVowelSquare(arr3, arr4, out result))
                    {
                        return "2" + "-" + result;
                    }
            }

            return "not found";
        }
        private static bool IsVowelSquare(char[] arr1, char[] arr2, out string result)
        {
            var regex = new Regex("a|e|i|o|u");
            result = string.Empty;

            for (int i = 0; i < arr1.Length - 1; i++)
            {
                if (regex.IsMatch(arr1[i].ToString()) && regex.IsMatch(arr1[i + 1].ToString()) &&
                    regex.IsMatch(arr2[i].ToString()) && regex.IsMatch(arr2[i + 1].ToString()))
                {
                    result = i.ToString();
                    return true;
                }
            }

            return false;
        }
        public static string VowelSquareAnsw(string[] strArr)
        {
            //Rows
            for (int i = 0; i < strArr.Length - 1; i++)
            {
                //Columns
                for (int j = 0; j < strArr[0].Length - 1; j++)
                {
                    if (IsVowel(strArr[i][j]) && 
                        IsVowel(strArr[i + 1][j]) &&
                        IsVowel(strArr[i][j + 1]) && 
                        IsVowel(strArr[i + 1][j + 1]))

                        return $"{i}-{j}";
                }
            }

            return "not found";
        }
        static bool IsVowel(char c)
        {
            return c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u';
        }

        public static string NumberStream(string str)
            {
                char[] charArr = str.ToCharArray();
                int[] intArr = Array.ConvertAll(charArr, c => (int)Char.GetNumericValue(c));

                foreach (int i in intArr)
                {
                    var consecutive = string.Empty;
                    for (int j = 0; j < i; j++)
                    {
                        consecutive += i.ToString();
                    }

                    var regex = new Regex(consecutive);
                    int nr = regex.Matches(str).Count;

                    if (nr > 0)
                        return "true";
                }

                return "false";
            }

        /// <summary>
        /// Wrong as counting number of Nrs ==> OK!
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        //public static string NumberStream(string str)
        //{
        //    char[] charArr = str.ToCharArray();
        //    int[] intArr = Array.ConvertAll(charArr, c => (int)Char.GetNumericValue(c));

        //    foreach(int i in intArr)
        //    {
        //        var regex = new Regex(i.ToString());
        //        int nr = regex.Matches(str).Count;

        //        if (nr > i)
        //            return "true";
        //    }

        //    return "false";
        //}

        public static bool ThreeNumbers(string str)
            {
            string[] words = str.Split(' ');

            foreach (var word in words)
            {
                int countDigit = 0;

                char[] ch = word.ToCharArray();
                var similar = string.Empty;

                for (int i = 0; i < ch.Length; i++)
                {
                    if (char.IsDigit(ch[i]))
                    {
                        if((i < ch.Length - 2) && char.IsDigit(ch[(i + 1)]) && char.IsDigit(ch[(i + 2)]))
                        {
                            return false;
                        }
                        else
                        {
                            if (ch[i].ToString() != similar)
                            {
                                similar = ch[i].ToString();
                                countDigit++;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }

                if (countDigit != 3)
                {
                    return false;
                }
            }

                return true;
            }

        public static string NonrepeatingCharacter(string str)
    {
        char[] ch = str.ToCharArray();

        foreach(char c in ch)
        {
            int count = str.Count(s => s == c);
            if (count == 1)
            {
                return c.ToString();
            }
        }

        return "";
    }

        public static bool ThreeSum(int[] intArr)
        {
            string result = string.Empty;

            int target = intArr[0];

            for (int i = 1; i < intArr.Length; i++)
            {
                int el1 = intArr[i];

                for (int j = i + 1; j < intArr.Length; j++)
                {
                    int el2 = intArr[j];

                    for (int k = j + 1; k < intArr.Length; k++)
                    {
                        if (el1 + el2 + intArr[k] == target)
                        {
                            result += el1.ToString() + "," + el2.ToString() + "," + intArr[k].ToString() + " ";
                            break;
                        }
                    }
                }
            }

            return result == string.Empty ? false : true;
        }

        public static bool AlphabetSearching(string str)
        {
            var alphabet = "abcdefghijklmnopqrstuvwxyz";

            char[] ch = alphabet.ToCharArray();

            foreach (char c in ch)
            {
                if (!str.Contains(c))
                {
                    return false;
                }
            }

            return true;
        }
        public static bool AlphabetSearchingAnsw(string str)
        {
            Regex rgx = new Regex(@"[^a-zA-Y]");
            str = rgx.Replace(str, "");
            str = str.ToLower();
            int cnt = str.Distinct().Count();

            if (cnt == 26) { return true; }

            return false;
        }

        public static string LargestPair(long num)
        {
            char[] ch = num.ToString().ToCharArray();
            long[] lngArr = Array.ConvertAll(ch, c => (long)Char.GetNumericValue(c));

            string result = lngArr.Max().ToString();
            long largest = lngArr.Max();

            if (lngArr.Length == 2)
                return lngArr[0].ToString() + lngArr[1].ToString();

            for (int i = 0; i < lngArr.Length - 1; i++)
            {
                if (lngArr[i] == largest && i < lngArr.Length)
                {
                    result = largest.ToString() + lngArr[i + 1].ToString();
                }
            }

            return result;
        }

        public static int HammingDistance(string[] strArr)
        {
            int result = 0;
            char[] arr1 = strArr[0].ToString().ToCharArray();
            char[] arr2 = strArr[1].ToString().ToCharArray();

            for (int i = 0; i < arr1.Length; i++)
            {
                if (arr1[i] != arr2[i])
                {
                    result++;
                }
            }

            return result;
        }
        public static int HammingDistanceAnsw(string[] strArr)
        {
            int i = 0;
            int HammingDistance = 0;

            while (i < strArr[0].Length)
            {
                if (strArr[0][i] != strArr[1][i])
                {
                    HammingDistance++;

                }

                i++;
            }

            return HammingDistance;
        }

        public static bool Superincreasing(int[] arr)
        {
            //1, 2, 4, 8, 16

            bool result = true;
            int sum = 0;

            foreach (int i in arr)
            {
                if (sum < i)
                    sum += i;
                else
                    return false;
            }

            return result;
        }

        public static int ChangingSequence(int[] arr)
        {
            //{-4, -2, 9, 10}

            int result = -1;
            bool plus = false;

            if (arr.Length == 1)
                return result;
            else
                plus = (arr[1] - arr[0] > 0);

            for (int i = 1; i < arr.Length; i++)
            {
                if (plus != (arr[i] - arr[i - 1] > 0))
                {
                    return i - 1;
                }
            }

            return result;
        }
        public static int ChangingSequenceAnsw(int[] arr)
        {
            int diffPoint = -1;

            for (int i = 0; i < arr.Length - 2; i++)
            {
                int diffOne = arr[i + 1] - arr[i];
                int diffTwo = arr[i + 2] - arr[i + 1];
                if (diffTwo > 0 != diffOne > 0)
                    diffPoint = i + 1;
            }

            return diffPoint;
        }

        public static int MultiplicativePersistence(int num)
        {

            int nr = num;
            int count = 0;

            while (nr.ToString().Length > 1)
            {
                nr = GetNumber(nr);
                count++;
            }

            return count;
        }
        private static int GetNumber(int num)
        {
            char[] ch = num.ToString().ToCharArray();
            int nr = 1;

            foreach (char c in ch)
            {
                if (char.IsDigit(c))
                {
                    nr *= int.Parse(c.ToString());
                }
            }
            return nr;
        }
        public static int MultiplicativePersistenceAnsw(int num)
        {
            int step = 0;

            while (num > 9)
            {
                var digits = Array.ConvertAll(num.ToString().ToArray(), a => int.Parse(a.ToString()));

                num = 1;

                for (int i = 0; i < digits.Length; i++)
                {
                    num *= digits[i];
                }

                step++;
            }

            return step;

        }

        public static string OffLineMinimum(string[] strArr)
        {
            string result = string.Empty;
            int smallest = int.MaxValue;
            List<int> lst = new List<int>();

            foreach (string s in strArr)
            {
                if (s != "E")
                {
                    int nr = 0;
                    if (int.TryParse(s, out nr))
                    {
                        if (nr < smallest)
                        {
                            smallest = nr;
                        }
                        lst.Add(nr);
                    }
                }
                else
                {
                    //result += lst.AsQueryable().Min() + ",";
                    result += lst.Min() + ",";
                    lst.Remove(lst.Min());
                }
            }

            if (result.Length > 0)
            {
                result = result.Trim();
                result = result.Remove(result.Length - 1);
            }

            return result;

        }
        public static string OffLineMinimumAnsw(string[] strArr)
        {
            var nums = new List<int>();
            var mins = new List<int>();

            foreach (var s in strArr)
            {
                if (s == "E")
                {
                    var min = nums.Min();
                    mins.Add(min);
                    nums.Remove(min);
                }
                else
                {
                    nums.Add(Int32.Parse(s));
                }
            }

            return String.Join(",", mins.ToArray());
        }

        public static bool OverlappingRanges(int[] arr)
        {
            int x = arr[arr.Length - 1];
            int count = 0;

            List<int> intArr1 = new List<int>();
            List<int> intArr2 = new List<int>();

            for (int i = arr[0]; i <= arr[1]; i++)
            {
                intArr1.Add(i);
            }

            for (int i = arr[2]; i <= arr[3]; i++)
            {
                intArr2.Add(i);
            }

            var intersect = intArr1.Intersect(intArr2).ToList();

            foreach (var item in intersect)
            {
                count++;
            }

            return count >= x ? true : false;
        }
        public static bool OverlappingRanges1(int[] arr)
        {
            if (arr[0] > arr[3] || arr[1] < arr[2])
                return false;

            int min = arr[0] < arr[2] ? arr[2] : arr[0];
            int max = arr[1] < arr[3] ? arr[1] : arr[3];

            return max - min + 1 >= arr[4] ? true : false;
        }
        public static bool OverlappingRangesAnsw(int[] arr)
        {
            if (arr.Length < 5) return false;

            IEnumerable<int> range1 = Enumerable.Range(arr[0], arr[1] - arr[0] + 1);
            IEnumerable<int> range2 = Enumerable.Range(arr[2], arr[3] - arr[2] + 1);

            int count = range1.Count(x => range2.Contains(x));

            if (count >= arr[4]) return true;
            return false;
        }

        public static string FindIntersection(string[] arr)
        {
            string result = string.Empty;

            string[] arr1 = arr[0].Split(",");
            string[] arr2 = arr[1].Split(",");

            int[] intArr1 = Array.ConvertAll(arr1, s => int.Parse(s));
            int[] intArr2 = Array.ConvertAll(arr2, s => int.Parse(s));

            var intersect = intArr1.Intersect(intArr2).ToList();

            foreach (var item in intersect)
            {
                result += item + ",";
            }

            return String.IsNullOrEmpty(result) ? "false" : result.Remove(result.Length - 1);
        }
        public static string FindIntersectionAnsw(string[] strArr)
        {
            string resultString = string.Join(",", strArr[0].Split(',')
                                        .Select(x => x.Trim())
                                        .Intersect(strArr[1].Split(',')
                                        .Select(x => x.Trim())));

            return string.IsNullOrEmpty(resultString) ? "false" : resultString;
        }

        public static string ArithGeo(int[] arr)
        {
            if (IsArithmetic(arr))
            {
                return "Arithmetic";
            }
            else if (IsGeometric(arr))
            {
                return "Geometric";
            }

            return "-1";
        }
        static bool IsGeometric(int[] n)
        {
            var factor = 0;

            if (n.Length > 1)
                factor = n[1] / n[0];
            else
                return false;

            for (int i = 1; i < n.Length; i++)
            {
                if (i < n.Length - 1)
                {
                    if (factor != n[i + 1] / n[i])
                        return false;
                }
            }

            return true;
        }
        static bool IsArithmetic(int[] n)
        {
            int factor;

            if (n.Length > 1)
                factor = n[1] - n[0];
            else
                return false;

            for (int i = 1; i < n.Length; i++)
            {
                if (i < n.Length - 1)
                {
                    if (factor != n[i + 1] - n[i])
                        return false;
                }
            }

            return true;
        }
        public static string ArithGeoAnsw(int[] arr)
        {
            if (arr.Length <= 2) return "-1";

            bool arith = true;
            bool geo = true;

            for (int i = 2; i < arr.Length; i++)
            {
                arith = arith &&
                        (arr[i] - arr[i - 1] == arr[i - 1] - arr[i - 2]);

                geo = geo &&
                        (arr[i] == (arr[i - 1] * (arr[i - 1]) / arr[i - 2]));
            }

            return arith ? "Arithmetic" :
                        geo ? "Geometric" : "-1";

            //if (arith)
            //    return "Arithmetic";
            //else if (geo)
            //    return "Geometric";
            //else
            //    return "-1";

        }

        public static int AdditivePersistence(int num)
        {
            int nrOfTimes = 0;

            while (num.ToString().Length > 1)
            {
                num = GetResult(num);
                nrOfTimes++;
            }

            return nrOfTimes;
        }
        private static int GetResult(int num)
        {
            int result = 0;

            foreach (var n in num.ToString())
            {
                result += Convert.ToInt32(n.ToString());
            }

            return result;
        }
        //private static int GetResult(int num)
        //{
        //    int result = 0;

        //    char[] arr = num.ToString().ToCharArray();

        //    List<int> intArr = new List<int>();

        //    foreach (char c in arr)
        //    {
        //        if (Char.IsDigit(c))
        //        {
        //            intArr.Add(int.Parse(c.ToString()));
        //        }
        //    }

        //    for (int i = 0; i < intArr.Count; i++)
        //    {
        //        result += intArr[i];
        //    }

        //    return result;
        //}
        public static int AdditivePersistence1(int num)
        {
            var cnt = 0;
            var sum = 0;
            var numString = num.ToString();

            while (sum > 9)
            {
                sum = 0;
                foreach (var n in numString)
                {
                    sum += Convert.ToInt32(n.ToString());
                }
                numString = sum.ToString();
                cnt++;
            }

            return cnt;
        }
        public static int AdditivePersistenceAnsw(int num)
        {
            int step = 0;

            while (num > 9)
            {
                num = Array.ConvertAll(num.ToString().ToArray(), a => int.Parse(a.ToString())).Sum();

                step++;
            }

            return step;
        }

        public static bool PowersofTwo(int num)
        {
            //double nr = Math.Sqrt(n);

            //return ((nr * nr) == n);

            int n = 2;
            while (n <= num)
            {
                if (n == num)
                    return true;
                n = n * 2;

            }

            return false;
        }
        /// <summary>
        ///  if a number n is a power of 2 then bitwise & of n and n-1 will be zero. 
        ///  We can say n is a power of 2 or not based on value of n&(n-1). 
        ///  The expression n&(n-1) will not work when n is 0. To handle this case. 
        ///  Our expression will become n& (!n&(n-1))  
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static bool PowersofTwo1(int num)
        {
            return ((num & (num - 1)) == 0 ? true : false);
        }
        public static bool PowersofTwoAnsw(int n)
        {
            if (n == 0)
                return false;

            while (n != 1)
            {
                if (n % 2 != 0)
                    return false;

                n = n / 2;
            }

            return true;
        }

        public static int MeanMode(int[] arr)
        {
            double mean = arr.Sum() / arr.Length;
            double result = arr.Where(x => x.Equals(mean)).Count();

            return (result > 1 ? 1 : 0);

            //if (result > 1)
            //    return 1;
            //return 0;
        }
        public static int MeanModeAnsw(int[] arr)
        {
            double mean = arr.Average();
            var mode = arr.GroupBy(v => v)
                            .OrderByDescending(g => g.Count())
                            .First()
                            .Key;

            return (mean == mode ? 1 : 0);
        }

        public static string SwapCase(string str)
        {
            string result = string.Empty;

            for (int i = 0; i < str.Length; i++)
            {
                if (char.IsLetter(str[i]))
                {
                    if (char.IsUpper(str[i]))
                        result += str[i].ToString().ToLower();
                    else
                        result += str[i].ToString().ToUpper();
                }
                else
                    result += str[i].ToString();
            }

            return result;
        }
        public static string SwapCase1(string str)
        {
            Regex lower = new Regex(@"[a-z]");
            Regex upper = new Regex(@"[A-Z]");

            string res = "";

            for (int i = 0; i < str.Length; i++)
            {
                string s = str[i].ToString();
                if (lower.IsMatch(s))
                {
                    res += s.ToUpper();
                }
                else if (upper.IsMatch(s))
                {
                    res += s.ToLower();
                }
                else
                {
                    res += s;
                }
            }

            return res;
        }
        public static string SwapCaseAnsw(string str)
        {
            char[] result = new char[str.Length];

            for (int i = 0; i < str.Length; i++)
            {
                char c = Char.IsUpper(str[i]) ? Char.ToLower(str[i]) : Char.ToUpper(str[i]);
                result[i] = c;
            }

            return (new string(result));
        }

        public static string SecondGreatLow(int[] arr)
        {
            int intSecLow = 0;
            int intSecHigh = 0;

            Array.Sort(arr);
            int[] dist = arr.Distinct().ToArray();

            if (dist.Length >= 2)
            {
                intSecLow = dist[1];
                intSecHigh = dist[dist.Length - 2];
            }
            else
            {
                intSecLow = arr[1];
                intSecHigh = arr[arr.Length - 2];
            }

            return intSecLow.ToString() + " " + intSecHigh.ToString();
        }
        public static string SecondGreatLowAnsw(int[] arr)
        {
            var max = arr.Max();
            var min = arr.Min();
            var result = " ";
            var length = arr.Length;

            if (length == 2)
            {
                return max.ToString() + " " + min.ToString();
            }

            var secondBiggest = arr.Where(number => number != max).Max();
            var secondSmallest = arr.Where(number => number != min).Min();

            result = secondSmallest.ToString() + " " + secondBiggest.ToString();

            return result;
        }

        public static string ArrayAdditionI(int[] arr)
        {

            int sum = arr.Max();

            arr = arr.Where(val => val != sum).ToArray();


            Helper(arr, sum);

            int[] exept = new int[arr.Length];

            for (int i = 0; i < arr.Length; i++)
            {

                exept[i] = arr[i];

                for (int j = 0; j < arr.Length; j++)
                {
                    exept[i] = arr[j];

                    var arr1 = arr.Except(exept).ToArray();

                    if (Helper(arr1, sum))
                    {
                        return "true";
                    }
                }
            }

            return "false";
        }
        public static bool Helper(int[] arr, int sum)
        {
            int temp = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != sum)
                {
                    temp += arr[i];
                }

                if (temp == sum)
                    return true;
            }

            return false;
        }
        public static string ArrayAdditionI2(int[] arr)
        {
            Array.Sort(arr);
            int length = arr.Length - 1;
            int sum = arr[length];

            if (IsSum(arr, length, sum))
            {
                return "true";
            }

            return "false";

        }
        public static bool IsSum(int[] arr, int length, int sum)
        {
            if (sum == 0)
            {
                return true;
            }

            if (length == 0 && sum != 0)
            {
                return false;
            }

            return IsSum(arr, length - 1, sum) || IsSum(arr, length - 1, sum - arr[length - 1]);
        }
        public static string ArrayAdditionIAnsw(int[] arr)
        {
            if (arr.Length > 0)
            {
                int sum = 0;

                Array.Sort(arr);

                for (int i = 0; i < arr.Length - 1; i++)
                {
                    sum += arr[i];
                }

                if (sum >= arr[arr.Length - 1])
                {
                    return "true";
                }
            }

            return "false";
        }

        public static string ArrayMatching(string[] strArr)
        {
            //My first attempt :-(

            strArr[0] = strArr[0].Replace(@"""", "").Replace("{", "").Replace("}", "").Replace("[", "").Replace("]", "");
            strArr[1] = strArr[1].Replace(@"""", "").Replace("{", "").Replace("}", "").Replace("[", "").Replace("]", "");

            string result = string.Empty;

            string[] arrStr1 = strArr[0].ToString().Split(",");
            string[] arrStr2 = strArr[1].ToString().Split(",");


            List<int> intLst1 = new List<int>();
            List<int> intLst2 = new List<int>();

            int nr = 0;

            foreach (string s in arrStr1)
            {
                if (int.TryParse(s, out nr))
                {
                    intLst1.Add(nr);
                }
            }

            foreach (string s in arrStr2)
            {
                if (int.TryParse(s, out nr))
                {
                    intLst2.Add(nr);
                }
            }


            if (intLst1.Count > intLst2.Count)
            {
                for (int i = 0; i < intLst2.Count; i++)
                {
                    intLst1[i] += int.Parse(intLst2[i].ToString());
                }

                foreach (int i in intLst1)
                {
                    result += i.ToString() + "-";
                }
            }
            else
            {
                for (int i = 0; i < intLst1.Count; i++)
                {
                    intLst2[i] += int.Parse(intLst1[i].ToString());
                }

                foreach (int i in intLst2)
                {
                    result += i.ToString() + "-";
                }
            }

            result = result.Remove(result.Length - 1, 1);


            return result;
        }
        public static string ArrayMatching1(string[] strArr)
        {

            strArr[0] = strArr[0].Replace("[", "").Replace("]", "");
            strArr[1] = strArr[1].Replace("[", "").Replace("]", "");

            string[] arrStr1 = strArr[0].ToString().Split(",");
            string[] arrStr2 = strArr[1].ToString().Split(",");

            List<int> intLst1 = new List<int>();
            List<int> intLst2 = new List<int>();

            int nr = 0;

            foreach (string s in arrStr1)
            {
                if (int.TryParse(s, out nr))
                {
                    intLst1.Add(nr);
                }
            }

            foreach (string s in arrStr2)
            {
                if (int.TryParse(s, out nr))
                {
                    intLst2.Add(nr);
                }
            }


            int lenMax = 0;
            if (intLst1.Count > intLst2.Count)
                lenMax = intLst1.Count;
            else
                lenMax = intLst2.Count;


            int[] arrResult = new int[lenMax];

            for (int i = 0; i < lenMax; i++)
            {
                arrResult[i] = (i < intLst1.Count ? intLst1[i] : 0) + (i < intLst2.Count ? intLst2[i] : 0);
            }

            return string.Join("-", arrResult.Select(i => i.ToString()).ToArray());
        }
        public static string ArrayMatchingAnsw(string[] strArr)
        {
            int[] a = IntArr(strArr[0]);                //Function intArr to get integer Array
            int[] b = IntArr(strArr[1]);                //Function intArr to get integer Array

            int len = a.Length > b.Length ? a.Length : b.Length;  //Ternary operator

            int[] result = new int[len];

            for (int i = len - 1; i >= 0; i--)
            {
                result[i] = (i < a.Length ? a[i] : 0) + (i < b.Length ? b[i] : 0);
            }

            return string.Join("-", result.Select(i => i.ToString()).ToArray());

        }
        static int[] IntArr(string str)
        {
            string[] arr = str.Substring(1, str.Length - 2).Split(',', '[', ']');
            return arr.Select(i => Int32.Parse(i)).ToArray();
        }

        public static string DivisionStringified(int num1, int num2)
        {
            if (num2 > 0)
            {
                double nr1 = Convert.ToDouble(num1);
                double nr2 = Convert.ToDouble(num2);
                double result = nr1 / nr2;

                return (Math.Round(result, MidpointRounding.AwayFromZero)).ToString("N0", new CultureInfo("en-US"));
            }

            return "";
        }
        public static string DivisionStringifiedAnsw(int num1, int num2)
        {
            decimal result = (decimal)num1 / (decimal)num2;
            return String.Format("{0:#,##0}", result);
        }

        public static string ThirdGreatest(string str)
        {
            //Regex rgx = new Regex(@"[^\w\s]");
            //str = rgx.Replace(str, "");
            ////List<string> words = str.Split(' ').ToList<string>();
            string[] words = str.Split(' ');

            words = words.OrderByDescending(s => s.Length).ToArray();
            return words[2].ToString();

            //string result = words.OrderByDescending(s => s.Length).Skip(2).First();
            //return result;

            //return words.OrderByDescending(s => s.Length).Skip(2).First();
        }

        /// <summary>
        /// https://stackoverflow.blog/2022/01/31/the-complete-beginners-guide-to-dynamic-programming/
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string PairOfNumbers(string str)
        {
            //8, 10, 2, 9, 7, 5

            List<string> list = new List<string>();

            foreach (char c in str)
            {
                if (Char.IsDigit(c))
                    list.Add(c.ToString());
            }

            String[] strArr = list.ToArray();
            int[] intArr = Array.ConvertAll(strArr, int.Parse);
            int sum = 11;
            string result = string.Empty;

            ArrayList arrResult = new ArrayList();

            foreach (int i in intArr)
            {
                int diff = sum - i;

                foreach (int j in intArr)
                {
                    if (i != j && j == diff)
                    {
                        result += i.ToString() + "," + j.ToString() + "|";
                    }
                }
            }



            return result;
        }

        public static int CountingMinutes1(string str)
        {
            DateTime dt1;
            DateTime dt2;
            string[] s = str.Split("-");

            string startTime = DateTime.Now.ToShortDateString() + " " + s[0];
            string endTime = DateTime.Now.ToShortDateString() + " " + s[1];

            if (DateTime.TryParse(startTime, out dt1) && DateTime.TryParse(endTime, out dt2))
            {
                if (dt1 > dt2)
                {
                    endTime = DateTime.Parse(endTime).AddHours(24).ToString();
                }

                TimeSpan duration = DateTime.Parse(endTime).Subtract(DateTime.Parse(startTime));

                return (int)(duration.TotalMinutes);
            }

            return 0;
        }
        public static string CountingMinutes1Answ(string str)
        {
            string[] dt = str.Split('-');
            DateTime dt1 = DateTime.Parse(dt[0]), dt2 = DateTime.Parse(dt[1]);

            if (dt2 < dt1)
                dt2 = dt2.AddDays(1);

            return (dt2 - dt1).TotalMinutes.ToString();
        }

        public static bool ExOh(string str)
        {
            int countX = 0;
            int countO = 0;

            char[] ch = str.ToLower().ToCharArray();


            foreach (char c in ch)
            {
                if (c == 'x')
                    countX += 1;
                if (c == 'o')
                    countO += 1;
            }

            if (countX == countO)
                return true;
            else
                return false;
        }
        public static string ExOh1(string str)
        {
            var count = 0;
            foreach (var c in str)
            {
                if (char.ToLower(c) == 'x')
                    count++;

                if (char.ToLower(c) == 'o')
                    count--;
            }
            return count == 0 ? "true" : "false";
        }
        public static string ExOhAnsw(string str)
        {
            return str.Count(x => x == 'x') == str.Count(x => x == 'o') ? "true" : "false";
            //return str.Where(c => c == 'o').Count() == str.Where(c => c == 'x').Count() ? "true" : "false";
        }

        public static bool QuestionsMarks(string str)
        {
            int nr1 = 0;
            int nr2 = 0;
            bool isOK = false;
            int countQM = 0;


            str = Regex.Replace(str, "[^0-9.?]", "");

            for (int i = 0; i < str.Length; i++)
            {
                if (int.TryParse(str[i].ToString(), out nr1))
                {
                    for (int j = i + 1; j < str.Length; j++)
                    {
                        if (str[j] == '?')
                        {
                            countQM += 1;
                        }
                        else if (int.TryParse(str[j].ToString(), out nr2))
                        {
                            if (nr1 + nr2 == 10 && countQM == 3)
                            {
                                isOK = true;
                            }
                            else if (nr1 + nr2 == 10)
                            {
                                return false;
                            }
                            else
                            {
                            }

                            countQM = 0;
                            i = j - 1;
                            break;
                        }
                    }
                }
            }
            return isOK;
        }
        public static string QuestionsMarksAnsw(string str)
        {

            int nQCount = 0;
            int nFirstNum = 0;
            bool bSumTen = false;

            foreach (char ch in str)
            {
                if (Char.IsDigit(ch))
                {
                    if (nFirstNum + Int32.Parse(ch.ToString()) == 10 && nQCount != 3)
                    {
                        return "false";
                    }

                    if (nFirstNum + Int32.Parse(ch.ToString()) == 10)
                    {
                        bSumTen = true;
                    }

                    nFirstNum = Int32.Parse(ch.ToString());
                    nQCount = 0;
                }
                else if (ch == Convert.ToChar("?"))
                {
                    nQCount++;
                }
            }

            if (bSumTen)
                return "true";
            else
                return "false";

        }

        public static string TwoSum(int[] intArr)
        {
            string result = string.Empty;

            int target = intArr[0];

            for (int i = 1; i < intArr.Length; i++)
            {
                int el1 = intArr[i];

                for (int j = i + 1; j < intArr.Length; j++)
                {
                    if (el1 + intArr[j] == target)
                    {
                        result += el1.ToString() + "," + intArr[j].ToString() + " ";
                        break;
                    }
                }
            }

            return result == string.Empty ? "-1" : result;
        }

        public static string ABCheck(string str)
        {

            str = str.ToLower();

            for (int i = 0; i < str.Length; i++)
            {
                if (i + 4 < str.Length)
                {
                    if (str[i] == 'a' && str[i + 4] == 'b')
                    {
                        return "true";
                    }
                    else if (str[i] == 'b' && str[i + 4] == 'a')
                    {
                        return "true";
                    }
                }
            }

            return "false";
        }

        public static Boolean SimpleSymbols(string s)
        {
            bool isOK = true;

            char[] ch = s.ToCharArray();

            int length = ch.Length;

            for (int i = 0; i < length; i++)
            {
                int index = Convert.ToInt32(ch[i]);
                if ((index >= 65 & index <= 90) | (index >= 97 & index <= 122))
                {
                    if (i == 0)
                    {
                        isOK = false;
                        break;
                    }
                    else if (ch[i - 1] == '+' & ch[i + 1] == '+')
                    {
                        i += 2;
                        if (i == length)
                        {
                            break;
                        }
                    }
                    else
                    {
                        isOK = false;
                        break;
                    }
                }
            }

            return isOK;
        }
        public static string SimpleSymbolsAnsw(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (char.IsLetter(str[i]))
                {
                    if (i == 0 || str[i - 1] != '+' || str[i + 1] != '+')
                        return "false";
                }
            }

            return "true";
        }

        public static string TimeConvert(int num)
        {
            int hours = num / 60;
            int minutes = num % 60;

            //return hours.ToString() + ":" + minutes.ToString();
            //return string.Format("{0}:{1}", hours, minutes);
            return $"{hours}:{minutes}";

        }

        public static string LetterCount1(string s)
        {
            string[] wordArr = s.Split(" ");
            string result = string.Empty;
            int maxCountTotal = 0;

            foreach (string word in wordArr)
            {
                int[] charCount = new int[256];

                int length = word.Length;

                for (int i = 0; i < length; i++)
                {
                    charCount[word[i]]++;
                }

                int maxCount = 0;
                for (int i = 0; i < length; i++)
                {
                    if (maxCount < charCount[word[i]])
                    {
                        maxCount = charCount[word[i]];
                    }
                }

                if (maxCount > maxCountTotal)
                {
                    maxCountTotal = maxCount;
                    result = word;
                }
                else if (maxCount == maxCountTotal)
                {
                    if (maxCount < 2)
                    {
                        result = "-1";
                    }
                }
            }

            return result;
        }

        public static string CheckNums(int num1, int num2)
        {

            if (num2 > num1)
            {
                return "true";
            }
            else if (num1 == num2)
            {
                return "-1";
            }
            else
            {
                return "false";
            }
        }
        public static string CheckNumsAnsw(int num1, int num2)
        {
            if (num2 == num1)
                return "-1";
            else
                return num2 > num1 ? "true" : "false";  //Ternary Operator
        }

        public static int VowelCount(string str)
        {
            str = str.ToLower();

            int countVowel = 0;
            Char[] arrChar = str.ToCharArray();

            foreach (var c in arrChar)
            {
                //int index = Convert.ToInt32(c);

                switch (c)
                {
                    case 'a':
                    case 'e':
                    case 'i':
                    case 'o':
                    case 'u':
                        countVowel += 1;
                        break;
                }
            }

            return countVowel;
        }
        public static int VowelCount1(string str)
        {
            str = str.ToLower();

            var regex = new Regex("a|e|i|o|u");
            return regex.Matches(str).Count;
        }
        public static int VowelCountAnsw(string str)
        {
            int counter = 0;

            for (int i = 0; i < str.Length; i++)
            {
                if ("aeiouAEIOU".Contains(str[i]))
                    counter += 1;

            }

            return counter;

        }

        public static string LetterChanges(string str)
        {

            string strAlpha = string.Empty;
            Char[] arrChar = str.ToCharArray();

            foreach (var c in arrChar)
            {
                int index = Convert.ToInt32(c);

                if ((index >= 65 & index <= 90) | (index >= 97 & index <= 122))
                {
                    switch (index)
                    {
                        case 90:
                        case 122:
                            index -= 25;
                            break;
                        default:
                            index += 1;
                            break;
                    }

                    switch (((char)index))
                    {
                        case 'a':
                        case 'e':
                        case 'i':
                        case 'o':
                        case 'u':
                            strAlpha += ((char)index).ToString().ToUpper();
                            break;
                        default:
                            strAlpha += ((char)index).ToString();
                            break;
                    }


                }
                else
                {
                    strAlpha += c;
                }
            }

            return strAlpha;
        }
        public static string LetterChanges1(string str)
        {
            char[] charArray = str.ToCharArray();

            for (var i = 0; i < charArray.Length; i++)
            {
                if (charArray[i] >= 'a' && charArray[i] <= 'z')
                    charArray[i]++;

                if (charArray[i] > 'z')
                    charArray[i] = 'a';

                if ("aeiou".IndexOf(charArray[i]) >= 0)
                    charArray[i] = char.ToUpper(charArray[i]);

            }
            return new string(charArray);
        }
        public static string LetterChangesAnsw(string str)
        {
            char c;
            string result = string.Empty;

            foreach (var letter in str)
            {
                c = letter;
                if (Char.IsLetter(letter))
                {
                    if (letter == 'z' | letter == 'Z')
                        c = 'a';
                    else
                        c = (char)(((int)letter) + 1);

                    if ("aeiou".IndexOf(c) >= 0)
                        c = char.ToUpper(c);
                }

                //Implicit casting: converting a smaller type to a larger type size: char to string
                //char -> int -> long -> float -> double
                //Explicit casting: converting a larger type to a smaller type size
                //double -> float -> long -> int -> char
                result += c;
                // result += c.ToString();
            }

            return result;
        }

        public static Boolean Palindrome(string s)
        {
            bool isPalindroom = true;

            if (s.Length == 0)
                return false;

            char[] c = s.ToLower().ToCharArray();

            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] != c[c.Length - 1 - i])
                {
                    isPalindroom = false;
                    break;
                }
            }

            return isPalindroom;
        }
        public static string Palindrome1(string str)
        {
            string str2 = str.Replace(" ", String.Empty), str3 = "";

            for (int i = 0; i < str2.Length; ++i)
                str3 += str2[str2.Length - 1 - i];

            //Tenary Operator ?
            return (str2 == str3) ? "true" : "false";

        }
        public static string Palindrome2(string str)
        {
            char[] chars = (str.Replace(" ", "")).ToCharArray();
            char[] reversed = (str.Replace(" ", "")).ToCharArray();
            Array.Reverse(reversed);

            return new string(chars) == new string(reversed) ? "true" : "false";
        }
        public static Boolean PalindromeAnsw(string str)
        {
            //return new String(str.ToLower().Replace(" ", "").Reverse().ToArray()) == str.ToLower().Replace(" ", "").ToString().ToLower();
            return new String(str.ToLower().Replace(" ", "").Reverse().ToArray()) == str.ToLower().Replace(" ", "").ToLower();
        }

        public static string AlphabetSoup(string s)
        {
            char[] c = s.ToCharArray();

            Array.Sort(c);

            //new string (s.OrderBy(c => c).ToArray())

            return new string(c);
        }
        public static string AlphabetSoup1(string str)
        {
            StringBuilder sb = new StringBuilder(str);
            for (int i = 0; i < sb.Length; i++)
            {
                for (int j = i + 1; j < sb.Length; j++)
                {
                    if (sb[i] > sb[j])
                    {
                        var temp = sb[i];
                        sb[i] = sb[j];
                        sb[j] = temp;
                    }
                }
            }

            return sb.ToString();
        }
        public static string AlphabetSoupAnsw(string s)
        {
            char[] c = s.ToCharArray();

            Array.Sort(c);

            //return new string (s.OrderBy(c => c).ToArray())
            //return String.Concat(str.OrderBy(c => c));

            return new string(c);
        }

        public static string WordCount(string s)
        {
            string[] arrStr = s.Split(" ");
            ArrayList lst = new ArrayList(arrStr);

            return lst.Count.ToString();
        }
        public static int WordCountAnsw(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return 0;

            string[] arrStr = s.Split(" ");
            //return s.Count(x => x == ' ') + 1;

            return s.Split().Length;
        }

        public static int FirstFactorial(int num)
        {
            int total = 1;

            for (int i = 1; i <= num; i++)
            {
                total *= i;
            }

            return total;
        }
        public static int FirstFactorialAnsw(int num)
        {
            if (num > 0)
            {
                return num * FirstFactorialAnsw(num - 1);
            }
            else
            {
                return 1;
            }
        }

        public static string FirstReverse(string s)
        {
            char[] c = s.ToCharArray();
            string reversed = string.Empty;

            for (int i = c.Length - 1; i >= 0; i--)
            {
                reversed += c[i];
            }

            return reversed;

        }
        public static string FirstReverseAnsw(string s)
        {
            string rev = string.Empty;
            foreach (char c in s)
            {
                rev = c + rev;
            }

            return rev;
        }

        public static string LongestWord(string sen)
        {
            sen = new string(sen.Where(c => !char.IsPunctuation(c)).ToArray());

            int max = 0;
            var words = sen.Split(" ");

            foreach (string word in words)
            {
                if (word.Length > max)
                {
                    max = word.Length;
                    sen = word;
                }

                //Another solution
                //var text = word.Where(c => char.IsLetter(c) || char.IsNumber(c)).ToArray();
                //if (text.Length > max)
                //{
                //    max = text.Length;
                //    sen = word;
                //}
            }

            //sen = words.OrderByDescending(str => str.Length).First();

            return sen;

        }
        public static string LongestWordAnsw(string sen)
        {
            Regex rgx = new Regex(@"[^\w\s]");
            //Regex rgx = new Regex("[^a-zA-Z0-9]");
            //Regex r = new Regex("(?:[^a-z0-9 ]|(?<=['\"])s)", 
            //RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            sen = rgx.Replace(sen, "");
            string[] words = sen.Split(' ');

            return words.OrderByDescending(s => s.Length).First();

            ////-----------------Another solution
            ////Regex to strip our punctuation
            ////Split on spaces
            ////Linq for the sorting by length
            ////Linq to get the first item
            //return Regex.Replace(sen, @"(\p{P})", "").Split(' ').OrderByDescending(i => i.Length).First();
        }

        public static int SimpleAdding(int num)
        {
            var total = 0;

            for (int x = 1; x <= num; x++)
                total += x;

            return total;
        }
        public static int SimpleAddingAnsw(int num)
        {
            if (num < 1)
                return 0;

            int sum = num + SimpleAdding(num - 1);
            return sum;
        }

        public static string LetterCapitalize(string s)
        {
            s = s.ToLower();

            var strUpper = string.Empty;
            string[] arrStr = s.Split(" ");

            foreach (var str in arrStr)
            {
                strUpper += str[0].ToString().ToUpper() + str.Substring(1) + " ";
            }

            return strUpper;
        }
        public static string LetterCapitalizeAnsw(string s)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
        }

        #endregion



        static void Main(string[] args)
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-EN", false);
            CultureInfo.CurrentUICulture = new CultureInfo("en-EN", false);


            string[] arr = new string[] { "100", "000" };
            Console.WriteLine(BitwiseTwo(arr));


            //string str = "47";
            //Console.WriteLine(BinaryReversal(str));

            //string[] arr = new string[] { "100", "000" };
            //Console.WriteLine(BitwiseOne(arr));
            //Console.WriteLine(BitwiseOneAnsw(arr));

            //var str = "kjjjhjjj";
            //Console.WriteLine(PalindromeCreator(str));

            //var num = 180;
            //Console.WriteLine(NextPalindrome(num));

            //int[] arr = new int[] { 0, 4, 22, 4, 14, 4, 2 };
            //Console.WriteLine(OtherProducts(arr));

            //int[] arr = new int[] { 4, 2, 1, 1, 1, 1, 0 };
            //int[] arr = new int[] { 0, 1, 2, 4, 1, 1, 1 };
            //int[] arr = new int[] { 0, 4, 22, 4, 14, 4, 2 };
            //Console.WriteLine(WaveSorting(arr));
            //Console.WriteLine(WaveSortingAnsw(arr));

            //string[] arr = new string[] { "[3, 4]", "[1, 2, 7, 7]" };
            //Console.WriteLine(ScaleBalancing((arr)));

            //string str = "1:23am-1:08am";
            //string str = "12:30pm-12:00am";
            //Console.WriteLine(CountingMinutes((str)));

            //string[] arr = new string[] { "1:10pm", "4:40am", "5:00pm" };
            //string[] arr = new string[] { "2:10pm", "1:30pm", "10:30am", "4:42pm" };
            //string[] arr = new string[] { "10:00am", "11:45pm", "5:00am", "12:01am" };
            //Console.WriteLine(TimeDifference((arr)));
            //Console.WriteLine(TimeDifferenceAnsw((arr)));

            //string str = "123hg* aaabb";
            //Console.WriteLine(StringMerge((str)));

            //var i = 101;
            //Console.WriteLine(HappyNumbers(i));

            //string str = "abc **";
            //Console.WriteLine(ASCIIConversion(str));

            //string str = "12345789mansjvenf";
            //Console.WriteLine(DistinctCharacters(str));

            //string str = "rd?u??dld?ddrr";
            //Console.WriteLine(CorrectPath(str));
            //Console.WriteLine(CorrectPathAnsw(str));

            //string str = "114.568.112";    
            //Console.WriteLine(SerialNumber(str));

            //string str = "75025";    
            //Console.WriteLine(FibonacciChecker(long.Parse(str)));

            //string str = "3ko6";      
            //Console.WriteLine(NumberSearch(str));
            //Console.WriteLine(NumberSearchAnsw(str));

            //int[] arr = new int[] { 1, 0, 0, 0, 2, 2, 2 };      
            //Console.WriteLine(ClosestEnemy(arr));

            //Console.WriteLine(BasicRomanNumerals(Console.ReadLine()));

            //string[] arr = new string[] { "abcd", "eikr", "oufj" };
            //string[] arr = new string[] { "aqrst", "ukaei", "ffooo" };
            //string[] arr = new string[] { "gg", "ff" };
            //string[] arr = new string[] { "aeooo", "ffffg" };
            //string[] arr = new string[] { "fghik", "mnors", "tuqae", "ffgei" };
            //string[] arr = new string[] { "fghik", "mnoos", "tueae", "ffgei" };
            //string[] arr = new string[] { "eeei", "ffgi", "kkmo" };
            //Console.WriteLine(VowelSquare((arr)));
            //Console.WriteLine(VowelSquareAnsw((arr)));

            //Console.WriteLine(NumberStream((Console.ReadLine())));

            //Console.WriteLine(ThreeNumbers((Console.ReadLine())));

            //Console.WriteLine(NonrepeatingCharacter((Console.ReadLine())));

            //int[] arr = new int[] { 3, 2, 1 };
            //Console.WriteLine(SwitchSortAnsw(arr));

            //int[] arr = new int[] { 3, 5, -1, 8, 12 };      
            //Console.WriteLine(ArrayAddition(arr));

            //var arr = new int[] { 12, 3, 1, -5, -4, 7 };
            //Console.WriteLine(ThreeSum(arr));

            //Console.WriteLine(AlphabetSearching(Console.ReadLine()));
            //Console.WriteLine(AlphabetSearchingAnsw(Console.ReadLine()));

            //string[] arr = new string[] { "apbpleeeef", "a,abc,abcg,b,c,dog,e,efd,zzzz" };
            //Console.WriteLine(CharacterRemoval(arr));
            //Console.WriteLine(CharacterRemovalAnsw(arr));

            //Console.WriteLine(LargestPair(long.Parse(Console.ReadLine())));

            //string[] arr = new string[] { "helloworld", "worldhello" };
            //Console.WriteLine(HammingDistance(arr));
            //Console.WriteLine(HammingDistanceAnsw(arr));

            //Console.WriteLine(PatternChaser(Console.ReadLine()));

            //Console.WriteLine(Division(int.Parse(Console.ReadLine()), int.Parse(Console.ReadLine())));
            //Console.WriteLine(DivisionAnsw(int.Parse(Console.ReadLine()), int.Parse(Console.ReadLine())));

            //var arr = new int[] { 1, 2, 4, 8, 16, 17 };
            //Console.WriteLine(Superincreasing(arr));

            //var arr = new int[] { 5, 4, 3, 2, 10, 11 };
            //Console.WriteLine(ChangingSequence(arr));
            //Console.WriteLine(ChangingSequenceAnsw(arr));

            //var arr = new int[] { 5, 4, 3, 2, 10, 11 };
            //Console.WriteLine(TwoSum(arr));

            //Console.WriteLine(PrimeMover(int.Parse(Console.ReadLine())));
            //Console.WriteLine(PrimeMoverAnsw(int.Parse(Console.ReadLine())));

            //Console.WriteLine(MultiplicativePersistence(int.Parse(Console.ReadLine())));
            //Console.WriteLine(MultiplicativePersistenceAnsw(int.Parse(Console.ReadLine())));

            //Console.WriteLine(RunLengthAnsw(Console.ReadLine()));

            //string[] arr = new string[] { "1", "2", "E", "E", "3" };
            //string[] arr = new string[] { "4", "E", "1", "E", "2", "E", "3", "E" };
            //Console.WriteLine(OffLineMinimum(arr));
            //Console.WriteLine(OffLineMinimumAnsw(arr));

            //Console.WriteLine(StringScramble(Console.ReadLine(), Console.ReadLine()));

            //Console.WriteLine(PalindromeTwo(Console.ReadLine()));

            //var arr = new int[] { 4, 10, 5, 8, 2 };
            //Console.WriteLine(OverlappingRanges(arr));
            //Console.WriteLine(OverlappingRangesAnsw(arr));


            //int[] arr = new int[] { 1, 2, 3 };
            //Console.WriteLine(PrimeTime(int.Parse(Console.ReadLine())));

            //string[] arr = new string[] { "1, 3, 4, 7, 13", "1, 2, 4, 13, 15" };
            //Console.WriteLine(FindIntersection(arr));
            //Console.WriteLine(FindIntersectionAnsw(arr));

            //string[] arr = new string[] { "100" };
            //string[] arr = new string[] { "0.232567" };

            //Console.WriteLine(FormattedNumber(arr));
            //Console.WriteLine(FormattedNumberAnsw(arr));

            //Console.WriteLine(ArithGeo(arr));
            //Console.WriteLine(ArithGeoAnsw(arr));

            //Console.WriteLine(AdditivePersistenceAnsw(int.Parse(Console.ReadLine())));
            //Console.WriteLine(AdditivePersistence(int.Parse(Console.ReadLine())));

            //Console.WriteLine(PowersofTwo(num) ? "Yes" : "No");
            //Console.WriteLine(PowersofTwoAnsw(int.Parse(Console.ReadLine())));

            //int[] arr = new int[] { 4,4,4,6,2 };
            //int[] arr = new int[] { 1, 2, 3 };
            //Console.WriteLine(MeanMode(arr));
            //Console.WriteLine(MeanModeAnsw(arr));

            //Console.WriteLine(SwapCase(Console.ReadLine()));
            //Console.WriteLine(SwapCaseAnsw(Console.ReadLine()));

            //int[] arr = new int[] { 80,80 };
            //int[] arr = new int[] { 4,6,23,10,1,3 };                  //True
            //int[] arr = new int[] {3,5,-1,8,12};                      //True
            //int[] arr = new int[] { 5,7,16,1,2 };                     //False
            //int[] arr = new int[] { 20, 10, 20, 30, 50 };             //True
            //int[] arr = new int[] { 1, 2, 3, 5, 8, 13, 21, 34 };      //True
            //int[] arr = new int[] { 10, 12, 11, 14 };                 //True

            //Console.WriteLine(SecondGreatLow(arr));

            //Console.WriteLine(ArrayAdditionIAnsw(arr));

            //string[] arr = new string[] { "[7,7,7,7]", "[2]" };
            //Console.WriteLine(ArrayMatching(arr));
            //Console.WriteLine(ArrayMatchingAnsw(arr));
            //Console.ReadKey();

            //Console.WriteLine(DivisionStringifiedAnsw(int.Parse(Console.ReadLine()), int.Parse(Console.ReadLine())));

            //Console.WriteLine(ThirdGreatest(Console.ReadLine()));

            //Console.WriteLine(PairOfNumbers(Console.ReadLine()));

            //Console.WriteLine(CountingMinutes1(Console.ReadLine()));
            //Console.WriteLine(CountingMinutes1Answ(Console.ReadLine()));

            //Console.WriteLine(ExOh(Console.ReadLine()));
            //Console.WriteLine(ExOhAnsw(Console.ReadLine()));

            //Console.WriteLine(QuestionsMarks(Console.ReadLine()));
            //Console.WriteLine(QuestionsMarksAnsw(Console.ReadLine()));

            //Console.WriteLine(ABCheck(Console.ReadLine()));

            //Console.WriteLine(SimpleSymbols(Console.ReadLine()));
            //Console.WriteLine(SimpleSymbolsAnsw(Console.ReadLine()));

            //Console.WriteLine(TimeConvert(int.Parse(Console.ReadLine())));

            //Console.WriteLine(LetterCount1(Console.ReadLine()));

            //Console.WriteLine(CheckNums(int.Parse(Console.ReadLine()), int.Parse(Console.ReadLine())));
            //Console.WriteLine(CheckNumsAnsw(int.Parse(Console.ReadLine()), int.Parse(Console.ReadLine())));

            //Console.WriteLine(VowelCount(Console.ReadLine()));
            //Console.WriteLine(VowelCount1(Console.ReadLine()));
            //Console.WriteLine(VowelCountAnsw(Console.ReadLine()));

            //Console.WriteLine(LetterChanges(Console.ReadLine()));
            //Console.WriteLine(LetterChangesAnsw(Console.ReadLine()));

            //Console.WriteLine(Palindrome(Console.ReadLine()));
            //Console.WriteLine(PalindromeAnsw(Console.ReadLine()));

            //Console.WriteLine(AlphabetSoup(Console.ReadLine()));
            //Console.WriteLine(AlphabetSoupAnsw(Console.ReadLine()));

            //Console.WriteLine(WordCount(Console.ReadLine()));
            //Console.WriteLine(WordCountAnsw(Console.ReadLine()));

            //Console.WriteLine(FirstFactorial(Int32.Parse(Console.ReadLine())));
            //Console.WriteLine(FirstFactorialAnsw(int.Parse(Console.ReadLine())));

            //Console.WriteLine(FirstReverse(Console.ReadLine()));
            //Console.WriteLine(FirstReverseAnsw(Console.ReadLine()));

            //Console.WriteLine(LongestWord(Console.ReadLine()));
            //Console.WriteLine(LongestWordAnsw(Console.ReadLine()));

            //Console.WriteLine(SimpleAdding(int.Parse(Console.ReadLine())));
            //Console.WriteLine(SimpleAddingAnsw(int.Parse(Console.ReadLine())));

            //Console.WriteLine(LetterCapitalize(Console.ReadLine()));
            //Console.WriteLine(LetterCapitalizeAnsw(Console.ReadLine()));

            Console.ReadKey();
        }

    }

}
