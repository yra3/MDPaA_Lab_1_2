using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаб_1_1_СИАКОД
{
    class Program
    {
        public static int[] testArray;

        public static void fillArray()
        {
            Random rdm = new Random();
            int count = rdm.Next(10, 20);

            testArray = new int[count];

            for (int i = 0; i < testArray.Length; i++)
            {
                testArray[i] = rdm.Next(100);
            }
        }

        private static void swap(ref int a, ref int b)
        {
            int c = a;
            a = b;
            b = c;
        }

        public static void swapPairs(ref int[] arr)
        {
            for (int i = 0; i < arr.Length / 2; i++)
            {
                swap(ref arr[i * 2], ref arr[i * 2 + 1]);
            }
        }

        public static int[] shiftFirst(int[] arr, int sizeOfShift)
        {
            if (sizeOfShift< 0 || sizeOfShift> arr.Length)
            {
                throw new Exception("Incorrect value");
            }
            if (sizeOfShift==0)
            {
                return arr;
            }

            int[] answer = new int[arr.Length];
            for (int i = 0; i < sizeOfShift; i++)
            {
                answer[i] = 0;
            }
            for (int j = 0, i = sizeOfShift; i < arr.Length; j++, i++)
            {
                answer[i] = arr[j];
            }
            return answer;
        }

        public static int[] shiftSecond(int[] arr, int sizeOfShift)
        {
            if (sizeOfShift < 0 || sizeOfShift > arr.Length)
            {
                throw new Exception("Incorrect value");
            }
            if (sizeOfShift == 0)
            {
                return arr;
            }



            int[] answer = new int[arr.Length];
            for (int i = 0, j = arr.Length - sizeOfShift; i < sizeOfShift; j++, i++)
            {
                answer[i] = arr[j];
            }
            for (int j = 0, i = sizeOfShift; i < arr.Length; j++, i++)
            {
                answer[i] = arr[j];
            }
            return answer;
        }

        public static int[] erase(int[] arr, int number)
        {
            if (number < 0 || number > arr.Length)
            {
                throw new Exception("Incorrect value");
            }


            int[] answer = new int[arr.Length - 1];
            int j = 0;
            for (int i = 0; i < number ; i++)
            {
                answer[j++] = arr[i];
            }
            for (int i = number+1; i < arr.Length; i++)
            {
                answer[j++] = arr[i];
            }
            return answer;
        }

        public static int[] insert(int[] arr, int number, int value)
        {
            if (number < 0 || number > arr.Length+1)
            {
                throw new Exception("Incorrect value");
            }


            int[] answer = new int[arr.Length + 1];
            int j = 0;
            for (int i = 0; i < number ; i++)
            {
                answer[j++] = arr[i];
            }
            answer[j++] = value;
            for (int i = number ; i < arr.Length; i++)
            {
                answer[j++] = arr[i];
            }
            return answer;
        }
        public static int[] eraseValue(int[] arr, int value)
        {
            List<int> equals = new List<int>();

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i]!=value)
                {
                    equals.Add(arr[i]);
                }
            }

            return equals.ToArray();
        }



        static void Main(string[] args)
        {
            Console.WriteLine("меняем пары");
            fillAndWrite();
            swapPairs(ref testArray);
            WriteArr(testArray);
            Console.Write('\n');

            Console.WriteLine("сдвигаем с нулями");
            fillAndWrite();
            WriteArr(shiftFirst(testArray, 3));
            Console.Write('\n');

            Console.WriteLine("сдвигаем с замещением");
            fillAndWrite();
            WriteArr(shiftSecond(testArray, 3));
            Console.Write('\n');

            Console.WriteLine("удаляем пятый");
            fillAndWrite();
            WriteArr(erase(testArray, 5));
            Console.Write('\n');

            Console.WriteLine("добыаляем вторым 666");
            fillAndWrite();
            WriteArr(insert(testArray, 2, 666));
            Console.Write('\n');


            Console.WriteLine("удаляем числа 50");
            fillArray();
            testArray[0] = testArray[2] = 50;
            WriteArr(testArray);
            WriteArr(eraseValue(testArray, 50));
            Console.Write('\n');



            Console.Read();

        }

        public static void fillAndWrite()
        {
            fillArray();
            WriteArr(testArray);
        }

        public static void WriteArr(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i]);
                Console.Write(' ');
            }
            Console.Write('\n');
        }
    }
}
