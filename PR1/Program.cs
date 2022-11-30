using System;

namespace PR1
{
    internal class Program
    {
        //ПР 1: Методы оптимизации https://github.com/Hawinar

        //С decimal, конечно, я переборчил, даже нашёл аналог Math, но на decimal https://github.com/raminrahimzada/CSharp-Helper-Classes/tree/master/Math/DecimalMath.
        //Однако, было намного проще тестировать, зная, что числа будут 1 в 1 с калькулятором и даже не придётся задаваться вопросом,
        //типа "Это формула неверная или double не смог удержать точное число?". Не думаю, что в этой программе возникнут проблемы только из-за типа данных decimal.
        //Обычно, я стараюсь использовать более лёгкие типы данных, но раз программа только для холодного расчёта, эту задачу она должна выполнять как можно точнее.
        //К тому же, тема довольно сложная и я не стал гадать, где какой тип данных понадобится.
        //По поводу наименования переменных, их названия я с примеров тех взял, чтобы удобнее было переписывать.

        static void Main(string[] args)
        {
            decimal eps = 0.00001M; //Допустимая погрешность
            decimal a = 2;
            decimal b = 6; //Кол-во также итераций повысится, если вы значение этой переменной повысите

            Console.WriteLine("Метод дихотомии:\nМинимальное значение: " + dihot(a, b, eps, function(1)).Item1 + "\nИтераций: " + dihot(a, b, eps, function(1)).Item2 + "\n");

            
            Console.WriteLine("Метод Фибоначчи:\nМинимальное значение: " + OptFib(a, b, eps, function(1)).Item1 + "\nИтераций: " + OptFib(a, b, eps, function(1)).Item2 + 
                "\nfmin: " + function(OptFib(a, b, eps, function(1)).Item1));
        }
        static decimal function(decimal x)
        {
            return DMath.Log(2 * x) * x + 2 * DMath.Sin(x) - 1;
            //return DMath.Power((x + 1), (x / 2)) + DMath.Power((10 - x), 2); //Функция из примера (для проверки)
        }

        static (decimal, int) dihot(decimal a, decimal b, decimal eps, decimal f)
        {
            int k = 0;
            decimal c, f1, f2 = 0;

            while (DMath.Abs(a - b) > eps)
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
        static (decimal, int) OptFib(decimal a, decimal b, decimal eps, decimal f)
        {
            int k, i, s, j;
            k = i = s = j = 0;
            decimal x, fx, x1, fx1, h;
            x = fx = x1 = fx1 = h = 0;
            
            decimal N = (b - a) / eps;
            decimal[] R = new decimal[(int)N];
            decimal Fib(decimal N)
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
            R = new decimal[(int)N];
            i = 0;
            decimal Num(decimal k)
            {
                R[0] = 1;
                R[1] = 1;
                i = 2;
                while (i<=k)
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
                    fx1=function(x1);
                }
                j++;
            }
            return ((x+x1)/2, j);
        }
    }
}
