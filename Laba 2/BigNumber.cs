using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace laba_2
{
    public class BigNumber : IComparable<BigNumber>
    {
    private static readonly int Base = 1000;
    private readonly int[] number; // number[0] — младший блок

    public int Length => number.Length;

    // Конструктор из строки
    public BigNumber(string numberStr)
    {
        if (string.IsNullOrEmpty(numberStr))
            throw new ArgumentException("Number string cannot be null or empty");

        numberStr = numberStr.TrimStart('0');
        if (string.IsNullOrEmpty(numberStr))
            numberStr = "0";

        if (numberStr.Any(c => !char.IsDigit(c)))
            throw new ArgumentException("Invalid character in number string");

        var blocks = new List<int>();
        // Разбиваем строку справа налево по 3 цифры
        for (int i = numberStr.Length; i > 0; i -= 3)
        {
            int start = Math.Max(0, i - 3);
            string chunk = numberStr.Substring(start, i - start);
            blocks.Add(int.Parse(chunk));
        }

        number = blocks.ToArray();
    }

    // Приватный конструктор для внутреннего использования
    private BigNumber(int[] digits)
    {
        number = TrimLeadingZeros(digits);
    }

    // Удаление ведущих нулей (со стороны старших разрядов → конец массива)
    private static int[] TrimLeadingZeros(int[] arr)
    {
        if (arr == null || arr.Length == 0) return new int[] { 0 };

        int lastNonZero = arr.Length - 1;
        while (lastNonZero > 0 && arr[lastNonZero] == 0)
            lastNonZero--;

        if (lastNonZero == 0 && arr[0] == 0)
            return new int[] { 0 };

        int[] result = new int[lastNonZero + 1];
        Array.Copy(arr, result, lastNonZero + 1);
        return result;
    }

    public BigNumber Clone()
    {
        return new BigNumber((int[])number.Clone());
    }

    public override string ToString()
    {
        if (number.Length == 0) return "0";

        var sb = new StringBuilder();
        // Старший блок (последний в массиве) — без ведущих нулей
        sb.Append(number[^1]);

        // Остальные блоки — с дополнением до 3 цифр
        for (int i = number.Length - 2; i >= 0; i--)
        {
            sb.Append(number[i].ToString("D3"));
        }

        string result = sb.ToString().TrimStart('0');
        return string.IsNullOrEmpty(result) ? "0" : result;
    }

    // Сравнение
    public int CompareTo(BigNumber other)
    {
        if (other == null) return 1;

        // Сначала по длине (число блоков)
        if (number.Length != other.number.Length)
            return number.Length.CompareTo(other.number.Length);

        // Поблочно, от старшего к младшему (с конца массива)
        for (int i = number.Length - 1; i >= 0; i--)
        {
            int cmp = number[i].CompareTo(other.number[i]);
            if (cmp != 0)
                return cmp;
        }
        return 0;
    }

    // Сложение
    public BigNumber Add(BigNumber other)
    {
        int maxLength = Math.Max(number.Length, other.number.Length);
        var result = new int[maxLength + 1];
        int carry = 0;

        for (int i = 0; i < maxLength || carry != 0; i++)
        {
            int sum = carry;
            if (i < number.Length) sum += number[i];
            if (i < other.number.Length) sum += other.number[i];

            if (i < result.Length)
            {
                result[i] = sum % Base;
                carry = sum / Base;
            }
        }

        return new BigNumber(result);
    }

    // Вычитание (только если this >= other)
    public BigNumber Subtract(BigNumber other)
    {
        if (CompareTo(other) < 0)
            throw new InvalidOperationException("Result would be negative");

        var result = new int[number.Length];
        int borrow = 0;

        for (int i = 0; i < number.Length; i++)
        {
            int diff = number[i] - borrow;
            if (i < other.number.Length)
                diff -= other.number[i];

            if (diff < 0)
            {
                diff += Base;
                borrow = 1;
            }
            else
            {
                borrow = 0;
            }

            result[i] = diff;
        }

        return new BigNumber(result);
    }

    // Умножение на целое число (long)
    public BigNumber Multiply(long multiplier)
    {
        if (multiplier < 0)
            throw new ArgumentException("Multiplier must be non-negative");
        if (multiplier == 0)
            return new BigNumber("0");

        var result = new int[number.Length + 20]; // запас под перенос
        long carry = 0;

        for (int i = 0; i < number.Length || carry != 0; i++)
        {
            long product = carry;
            if (i < number.Length)
                product += (long)number[i] * multiplier;

            if (i < result.Length)
            {
                result[i] = (int)(product % Base);
                carry = product / Base;
            }
        }

        return new BigNumber(result);
    }

    // Деление на целое число (long)
    public BigNumber Divide(long divisor)
    {
        if (divisor <= 0)
            throw new ArgumentException("Divisor must be positive");

        var result = new int[number.Length];
        long remainder = 0;

        // Идём от старшего блока к младшему (справа налево в массиве)
        for (int i = number.Length - 1; i >= 0; i--)
        {
            remainder = remainder * Base + number[i];
            int quotientDigit = (int)(remainder / divisor);
            remainder %= divisor;
            result[i] = quotientDigit;
        }

        return new BigNumber(result);
    }

    public static BigNumber operator +(BigNumber a, BigNumber b) => a.Add(b);
    public static BigNumber operator -(BigNumber a, BigNumber b) => a.Subtract(b);
    public static BigNumber operator *(BigNumber a, long b) => a.Multiply(b);
    public static BigNumber operator /(BigNumber a, long b) => a.Divide(b);

    public static bool operator >(BigNumber a, BigNumber b) => a.CompareTo(b) > 0;
    public static bool operator <(BigNumber a, BigNumber b) => a.CompareTo(b) < 0;
    public static bool operator >=(BigNumber a, BigNumber b) => a.CompareTo(b) >= 0;
    public static bool operator <=(BigNumber a, BigNumber b) => a.CompareTo(b) <= 0;
    public static bool operator ==(BigNumber a, BigNumber b)
    {
        if (ReferenceEquals(a, b)) return true;
        if (a is null || b is null) return false;
        return a.CompareTo(b) == 0;
    }
    public static bool operator !=(BigNumber a, BigNumber b) => !(a == b);

    public override bool Equals(object obj) => obj is BigNumber other && this == other;
    public override int GetHashCode() => ToString().GetHashCode();
    }

}