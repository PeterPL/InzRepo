using System;

namespace MainApp
{
    /// <summary>
    /// Cooley-Tukey FFT algorithm.
    /// </summary>
    public static class CooleyTukey
    {
        /// <summary>
        /// Calculates FFT using Cooley-Tukey FFT algorithm.
        /// </summary>
        /// <param name="data">input data</param>
        /// <returns>spectrogram of the data</returns>
        /// <remarks>
        /// If amount of data items not equal a power of 2, then algorithm
        /// automatically pad with 0s to the lowest amount of power of 2.
        /// </remarks>
        public static double[] Perform(double[] data)
        {
            int lengthOfData;
            int bitsInLength;
            if (IsPowerOfTwo(data.Length))
            {
                lengthOfData = data.Length;
                bitsInLength = Log2(lengthOfData) - 1;
            }
            else
            {
                bitsInLength = Log2(data.Length);
                lengthOfData = 1 << bitsInLength;
                // the items will be pad with zeros
            }

            // bit reversal
            ComplexNumber[] dataForFft = new ComplexNumber[lengthOfData];
            for (int i = 0; i < data.Length; i++)
            {
                int j = ReverseBits(i, bitsInLength);
                dataForFft[j] = new ComplexNumber(data[i]);
            }

            // Cooley-Tukey 
            for (int i = 0; i < bitsInLength; i++)
            {
                int m = 1 << i;
                int n = m * 2;
                double alpha = -(2 * Math.PI / n);

                for (int k = 0; k < m; k++)
                {
                    // e^(-2*pi/N*k)
                    ComplexNumber oddPartMultiplier = new ComplexNumber(0, alpha * k).PoweredE();

                    for (int j = k; j < lengthOfData; j += n)
                    {
                        ComplexNumber evenPart = dataForFft[j];
                        ComplexNumber oddPart = oddPartMultiplier * dataForFft[j + m];
                        dataForFft[j] = evenPart + oddPart;
                        dataForFft[j + m] = evenPart - oddPart;
                    }
                }
            }

            // calculate spectrogram
            double[] spectrogramOfData = new double[lengthOfData];
            for (int i = 0; i < spectrogramOfData.Length; i++)
            {
                spectrogramOfData[i] = dataForFft[i].AbsPower2();
            }
            return spectrogramOfData;
        }

        /// <summary>
        /// Gets number of significat bytes.
        /// </summary>
        /// <param name="n">Number</param>
        /// <returns>Amount of minimal bits to store the number.</returns>
        private static int Log2(int n)
        {
            int i = 0;
            while (n > 0)
            {
                ++i; n >>= 1;
            }
            return i;
        }

        /// <summary>
        /// Reverses bits in the number.
        /// </summary>
        /// <param name="number">Number</param>
        /// <param name="countOfBits">Significant bits in the number.</param>
        /// <returns>Reversed binary number.</returns>
        private static int ReverseBits(int number, int countOfBits)
        {
            int reversedNumber = 0;
            for (int i = 0; i < countOfBits; i++)
            {
                int nextBit = number & 1;
                number >>= 1;

                reversedNumber <<= 1;
                reversedNumber |= nextBit;
            }
            return reversedNumber;
        }

        /// <summary>
        /// Checks if number is power of 2.
        /// </summary>
        /// <param name="number">number</param>
        /// <returns>true if n=2^k and k is positive integer</returns>
        private static bool IsPowerOfTwo(int number)
        {
            return number > 1 && (number & (number - 1)) == 0;
        }
    }
}