using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace keystrokeDynamics
{
    class Metrics
    {

        public static int MetricEuklides(Dictionary<string, long> X, Dictionary<string, long> Y)
        {
            long sum = 0;
            foreach (var key in X.Keys)
            {
                sum += (X[key] - Y[key]) * (X[key] - Y[key]);
            }
            int distance = (int)Math.Sqrt(sum);
            return distance;
        }
        public static int MetricManhattan(Dictionary<string, long> X, Dictionary<string, long> Y)
        {
            int distance = 0;
            foreach (var key in X.Keys)
            {
                long xValue = X[key];
                long yValue = Y[key];
                distance += (int)Math.Abs(xValue - yValue);
            }
            return distance;
        }
        public static int MetricCzebyszew(Dictionary<string, long> X, Dictionary<string, long> Y)
        {
            double[] sum = new double[X.Count];
            int i = 0;
            foreach (var key in X.Keys)
            {
                sum[i++] = (X[key] - Y[key]);
            }
            List<double> sumList = new List<double>(sum);
            sumList.Sort();
            int distance = (int)sumList.Max();
            return distance;
        }
        public static int MetricMahalanobisa(Dictionary<string, long> X, Dictionary<string, long> Y) //todo
        {
            double sum = 0;

            int distance = (int)Math.Sqrt(sum);

            return distance;
        }

    }
}
