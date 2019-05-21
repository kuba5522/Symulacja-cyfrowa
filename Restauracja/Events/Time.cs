using System;

namespace Restauracja
{
    internal class Time
    {
        private readonly Random Rand = new Random();

        public int GaussianDistribution(int average, int variance)
        {

            var U1 = 1.0 - Rand.NextDouble();
            var U2 = 1.0 - Rand.NextDouble();
            var RandStdNormal = Math.Sqrt(-2.0 * Math.Log(U1)) * Math.Sin(2.0 * Math.PI * U2);
            var RandNormal = average + variance * RandStdNormal;
            return (int) RandNormal;
        }

        public int ExponentialDistribution(int mean)
        {
            return Convert.ToInt32(-mean * Math.Log((1-Rand.NextDouble()), Math.E));
        }

        public int UniformDistribution(int a, int b)
        {
            double x = Rand.NextDouble();
            return Convert.ToInt32(b * x + (1 - x) * a);
        }
    }
}
