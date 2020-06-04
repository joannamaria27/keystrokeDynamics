using System;
using System.Collections.Generic;
using System.Text;

namespace keystrokeDynamics {
	class Metrics {

        public static double MetricEuklides(long[] X, long[] Y) {
            double sum = 0;
            for (int i = 0; i < X.Length; i++) {
                sum += (X[i] - Y[i]) * (X[i] - Y[i]);
            }
            double distance = Math.Sqrt(sum);
            return distance;
        }
        public static int MetricManhattan(Dictionary<string, long> X, Dictionary<string, long> Y) {
            int distance = 0;

            //for (int i = 0; i < X.Length; i++) {
            //    distance += Math.Abs(X[i] - Y[i]);
            //}

            foreach (var key in X.Keys) {
                long xValue = X[key];
                long yValue = Y[key];
                distance += (int)Math.Abs(xValue - yValue);
            }
            return distance;
        }
        public static double MetricCzebyszew(long[] X, long[] Y) {
            double[] sum = new double[X.Length];
            for (int i = 0; i < X.Length; i++) {
                sum[i] = (X[i] - Y[i]);
            }
            List<double> sumList = new List<double>(sum);
            sumList.Sort();
            double distance = sumList[0];
            return distance;
        }
        public static double MetricMahalanobisa(long[] X, long[] Y) //todo
        {
            double sum = 0;

            double distance = Math.Sqrt(sum);

            return distance;
        }

    }
}
