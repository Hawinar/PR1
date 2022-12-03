using System;

namespace PR1
{
    internal class Program
    {
        //ПР 1: Методы одномерной оптимизации https://github.com/Hawinar Вариант: 8

        static void Main(string[] args)
        {
            double eps = 0.00001;
            double a = 2;
            double b = 5;

            Console.WriteLine("Метод дихотомии:\nМинимальное значение: " + dihot(a, b, eps, function(1)).Item1 + "\nИтераций: " + dihot(a, b, eps, function(1)).Item2 + "\n");

            Console.WriteLine($"Метод золотого сечения:\nМинимальное значение: {GoldenRation(a, b, eps).Item1} \nИтераций: {GoldenRation(a, b, eps).Item2}\n");
            a = 2;
            b = 5;
            Console.WriteLine("Метод Фибоначчи:\nМинимальное значение: " + OptFib(a, b, eps, function(1)).Item1 + "\nИтераций: " + OptFib(a, b, eps, function(1)).Item2 +
                "\nfmin: " + function(OptFib(a, b, eps, function(1)).Item1));
            Console.ReadKey();
        }
        static double function(double x)
        {
            return Math.Log(2 * x) * x + 2 * Math.Sin(x) - 1;
            //return Math.Pow((x + 1), (x / 2)) + Math.Pow((10 - x), 2); //Функция из примера (для проверки)
        }
        static (double,int) GoldenRation(double a, double b, double eps)
        {
            int k = 0;
            while (Math.Abs(b - a) > eps)
            {
                double x1 = b - (b - a) / 1.618;
                double x2 = a + (b - a) / 1.618;
                double y1 = function(x1);
                double y2 = function(x2);
                if (y1 >= y2)
                    a = x1;
                else
                    b = x2;
                k++;
            }
            return ((a + b) / 2,k);
        }
        static (double, int) dihot(double a, double b, double eps, double f)
        {
            int k = 0;
            double c, f1, f2 = 0;

            while (Math.Abs(a - b) > eps)
            {
                k++;
                c = (a + b) / 2;
                f1 = function(c - eps);
                f2 = function(c + eps);
                if (f1 > f2)
                    a = c;
                else if (f2 > f1)
                    b = c;
                else
                {
                    a = c - eps;
                    b = c + eps;
                }
            }
            return ((a + b) / 2, k);
        }
        static (double, int) OptFib(double a, double b, double eps, double f)
        {
            int k, i, s, j;
            k = i = s = j = 0;
            double x, fx, x1, fx1, h;
            x = fx = x1 = fx1 = h = 0;

            double N = (b - a) / eps;
            double[] R = new double[(int)N];
            double Fib(double N)
            {
                R[0] = 1;
                R[1] = 1;
                i = 2;
                while (R[i - 1] < N)
                {
                    R[i] = R[i - 1] + R[i - 2];
                    i++;
                }
                return i - 1;
            }
            s = (int)Fib(N);
            R = new double[(int)N];
            i = 0;
            double Num(double k)
            {
                R[0] = 1;
                R[1] = 1;
                i = 2;
                while (i <= k)
                {
                    R[i] = R[i - 1] + R[i - 2];
                    i++;
                }
                return R[(int)k];
            }
            R[s] = Num(s);

            h = ((b - a) / Num(s));
            x = a + h * Num(s - 2);
            fx = function(x);
            x1 = b - h * Num(s - 2);
            fx1 = function(x1);
            j = 1;
            while (j <= s - 2)
            {
                if (fx < fx1)
                {
                    b = x1;
                    x1 = x;
                    fx1 = fx;
                    x = a + h * Num(s - j - 2);
                    fx = function(x);
                }
                else
                {
                    a = x;
                    x = x1;
                    fx = fx1;
                    x1 = b - h * Num(s - j - 2);
                    fx1 = function(x1);
                }
                j++;
            }
            return ((x + x1) / 2, j);
        }
    }
}
