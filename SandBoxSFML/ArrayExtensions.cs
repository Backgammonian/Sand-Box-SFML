using System;

namespace SandBoxSFML
{
    public static class ArrayExtensions
    {
        // Inspired from several Stack Overflow discussions and an implementation by David Walker at http://coding.grax.com/2011/11/initialize-array-to-value-in-c-very.html

        public static void Fill<T>(this T[] destinationArray, params T[] value)
        {
            if (destinationArray == null)
            {
                throw new ArgumentNullException(nameof(destinationArray));
            }

            if (value.Length > destinationArray.Length)
            {
                throw new ArgumentException("Length of value array must not be more than length of destination");
            }

            // set the initial array value
            Array.Copy(value, destinationArray, value.Length);

            int copyLength, nextCopyLength;

            for (copyLength = value.Length; (nextCopyLength = copyLength << 1) < destinationArray.Length; copyLength = nextCopyLength)
            {
                Array.Copy(destinationArray, 0, destinationArray, copyLength, copyLength);
            }

            Array.Copy(destinationArray, 0, destinationArray, copyLength, destinationArray.Length - copyLength);
        }

        public static void Fill<T>(this T[,] destinationMatrix, T[,] value)
        {
            if (destinationMatrix == null)
            {
                throw new ArgumentNullException(nameof(destinationMatrix));
            }

            if (value.Length > destinationMatrix.Length)
            {
                throw new ArgumentException("Length of value matrix must not be more than length of destination");
            }

            // set the initial matrix value
            Array.Copy(value, destinationMatrix, value.Length);

            int copyLength, nextCopyLength;

            for (copyLength = value.Length; (nextCopyLength = copyLength << 1) < destinationMatrix.Length; copyLength = nextCopyLength)
            {
                Array.Copy(destinationMatrix, 0, destinationMatrix, copyLength, copyLength);
            }

            Array.Copy(destinationMatrix, 0, destinationMatrix, copyLength, destinationMatrix.Length - copyLength);
        }
    }
}
