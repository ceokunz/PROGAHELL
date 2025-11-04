using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba_2
{
    public class BigNumber
    {
        private int[] number;
        private const int Base = 1000;
        public int ArrayLength => number.Length;

        public BigNumber(string numberStr)
        {
            if (string.IsNullOrEmpty(numberStr))
                throw new ArgumentException("Number string cannot be null or empty");
            numberStr = numberStr.TrimStart('0');
            if (string.IsNullOrEmpty(numberStr))
                numberStr = "0";

            number = new int[numberStr.Length];
            for (int i = 0; i < numberStr.Length; i++)
            {
                if (!char.IsDigit(numberStr[i]))
                    throw new ArgumentException("Invalid character in number string");

                number[i] = numberStr[i] - '0';
            }
        }

        private BigNumber(int[] digits)
        {
            number = TrimLeadingZeros(digits);
        }

        public BigNumber Clone()
        {
            int[] clonedArray = new int[number.Length];
            Array.Copy(number, clonedArray, number.Length);
            return new BigNumber(clonedArray);
        }

        public override string ToString()
        {
            return string.Join("", number);
        }

        public BigNumber Add(BigNumber bnum)
        {
            int maxLength = Math.Max(ArrayLength, bnum.ArrayLength);
            int[] result = new int[maxLength + 1];
            int carry = 0;

            for (int i = 0; i < maxLength; i++)
            {
                int digit1 = i < ArrayLength ? number[ArrayLength - 1 - i] : 0;
                int digit2 = i < bnum.ArrayLength ? bnum.number[bnum.ArrayLength - 1 - i] : 0;

                int sum = digit1 + digit2 + carry;
                result[result.Length - 1 - i] = sum % Base;
                carry = sum / Base;
            }

            result[0] = carry;

            return new BigNumber(result);
        }

        public BigNumber Subtract(BigNumber bnum)
        {
            if (CompareTo(bnum) < 0)
                throw new InvalidOperationException("Result would be negative");

            int[] result = new int[ArrayLength];
            int borrow = 0;

            for (int i = 0; i < ArrayLength; i++)
            {
                int digit1 = number[ArrayLength - 1 - i];
                int digit2 = i < bnum.ArrayLength ? bnum.number[bnum.ArrayLength - 1 - i] : 0;

                int diff = digit1 - digit2 - borrow;
                if (diff < 0)
                {
                    diff += Base;
                    borrow = 1;
                }
                else
                {
                    borrow = 0;
                }

                result[result.Length - 1 - i] = diff;
            }

            return new BigNumber(result);
        }

        public BigNumber Multiply(double multiplier)
        {
            if (multiplier % 1 != 0)
                throw new ArgumentException("Multiplier must be integer for BigNumber operations");

            long multiplierLong = (long)multiplier;

            int[] result = new int[ArrayLength + 20];
            long carry = 0;

            for (int i = 0; i < ArrayLength; i++)
            {
                long product = number[ArrayLength - 1 - i] * multiplierLong + carry;
                result[result.Length - 1 - i] = (int)(product % Base);
                carry = product / Base;
            }

            int carryIndex = ArrayLength;
            while (carry > 0)
            {
                result[result.Length - 1 - carryIndex] = (int)(carry % Base);
                carry /= Base;
                carryIndex++;
            }

            return new BigNumber(result);
        }

        public BigNumber Divide(double divisor)
        {
            if (divisor == 0)
                throw new DivideByZeroException("Division by zero");

            if (divisor % 1 != 0)
                throw new ArgumentException("Divisor must be integer for BigNumber operations");

            long divisorLong = (long)divisor;
            int[] result = new int[ArrayLength];
            long remainder = 0;

            for (int i = 0; i < ArrayLength; i++)
            {
                long current = remainder * Base + number[i];
                result[i] = (int)(current / divisorLong);
                remainder = current % divisorLong;
            }

            return new BigNumber(result);
        }

        private int[] TrimLeadingZeros(int[] arr)
        {
            int startIndex = 0;
            while (startIndex < arr.Length - 1 && arr[startIndex] == 0)
            {
                startIndex++;
            }

            if (startIndex == 0)
                return arr;

            int[] result = new int[arr.Length - startIndex];
            Array.Copy(arr, startIndex, result, 0, result.Length);
            return result;
        }

        public int CompareTo(BigNumber bnum)
        {
            if (ArrayLength != bnum.ArrayLength)
                return ArrayLength.CompareTo(bnum.ArrayLength);

            for (int i = 0; i < ArrayLength; i++)
            {
                if (number[i] != bnum.number[i])
                    return number[i].CompareTo(bnum.number[i]);
            }

            return 0;
        }
        public static BigNumber operator +(BigNumber a, BigNumber b)
        {
            return a.Add(b);
        }
        public static BigNumber operator -(BigNumber a, BigNumber b)
        {
            return a.Subtract(b);
        }
        public static BigNumber operator *(BigNumber a, double b)
        {
            return a.Multiply(b);
        }
        public static BigNumber operator /(BigNumber a, double b)
        {
            return a.Divide(b);
        }
        public static bool operator >(BigNumber a, BigNumber b)
        {
            return a.CompareTo(b) > 0;
        }
        public static bool operator <(BigNumber a, BigNumber b)
        {
            return a.CompareTo(b) < 0;
        }


    }
}
