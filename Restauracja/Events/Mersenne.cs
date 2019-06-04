using System;

namespace Restauracja
{
    public class Mersenne
    {
        private const int MERS_N = 624;
        private const int MERS_M = 397;
        private const int MERS_R = 31;
        private const int MERS_U = 11;
        private const int MERS_S = 7;
        private const int MERS_T = 15;
        private const int MERS_L = 18;
        private const uint MERS_A = 0x9908B0DF;
        private const uint MERS_B = 0x9D2C5680;
        private const uint MERS_C = 0xEFC60000;

        private readonly uint[] Mt = new uint[MERS_N];          // state vector
        private uint Mti;                            // index into mt

        
        public Mersenne(uint seed)
        {
            RandomInit(seed);
        }
        public void RandomInit(uint seed)
        {
            Mt[0] = seed;
            for (Mti = 1; Mti < MERS_N; Mti++)
            {
                Mt[Mti] = (1812433253U * (Mt[Mti - 1] ^ (Mt[Mti - 1] >> 30)) + Mti);
            }
        }
        public double Random()
        {
            // output random float number in the interval 0 <= x < 1
            uint r = BRandom(); // get 32 random bits
            if (BitConverter.IsLittleEndian)
            {
                byte[] i0 = BitConverter.GetBytes((r << 20));
                byte[] i1 = BitConverter.GetBytes(((r >> 12) | 0x3FF00000));
                byte[] bytes = { i0[0], i0[1], i0[2], i0[3], i1[0], i1[1], i1[2], i1[3] };
                double f = BitConverter.ToDouble(bytes, 0);
                return f - 1.0;
            }
            else
            {
                return r * (1.0 / (0xFFFFFFFF + 1.0));
            }
        }
        private uint BRandom()
        {
            // generate 32 random bits
            uint y;

            if (Mti >= MERS_N)
            {
                const uint LOWER_MASK = 2147483647;
                const uint UPPER_MASK = 0x80000000;
                uint[] mag01 = { 0, MERS_A };

                int kk;
                for (kk = 0; kk < MERS_N - MERS_M; kk++)
                {
                    y = (Mt[kk] & UPPER_MASK) | (Mt[kk + 1] & LOWER_MASK);
                    Mt[kk] = Mt[kk + MERS_M] ^ (y >> 1) ^ mag01[y & 1];
                }

                for (; kk < MERS_N - 1; kk++)
                {
                    y = (Mt[kk] & UPPER_MASK) | (Mt[kk + 1] & LOWER_MASK);
                    Mt[kk] = Mt[kk + (MERS_M - MERS_N)] ^ (y >> 1) ^ mag01[y & 1];
                }

                y = (Mt[MERS_N - 1] & UPPER_MASK) | (Mt[0] & LOWER_MASK);
                Mt[MERS_N - 1] = Mt[MERS_M - 1] ^ (y >> 1) ^ mag01[y & 1];
                Mti = 0;
            }

            y = Mt[Mti++];

            // Tempering (May be omitted):
            y ^= y >> MERS_U;
            y ^= (y << MERS_S) & MERS_B;
            y ^= (y << MERS_T) & MERS_C;
            y ^= y >> MERS_L;
            return y;
        }
    }
}
