using System;

namespace PR1
{
    internal class Program
    {
        //Я думал, что дело в том, что с типом данных double что-то не так, поэтому я сделал всё в decimal (16 байт который),
        //даже нашёл аналог Math, но в типе данных decimal: https://github.com/raminrahimzada/CSharp-Helper-Classes/tree/master/Math/DecimalMath
        //Но дело в было не в double, а переправлять уже лень стало. Да и так даже точнее так-то.
        static void Main(string[] args)
        {
            decimal eps = 0.00001M; //Допустимая погрешность
            decimal a = 2;
            decimal b = 5;

            Console.WriteLine("Метод дихотомии:\nМинимальное значение: " + dihot(a, b, eps, function(1)).Item1 +"\nИтераций: "+ dihot(a, b, eps, function(1)).Item2);
        }
        static decimal function(decimal x)
        {
            return DMath.Log(2 * x) * x + 2 * DMath.Sin(x) - 1;
            //return DMath.Power((x + 1), (x / 2)) + DMath.Power((10 - x), 2); //Функция из примера (для проверки)
        }

        static (decimal,int) dihot(decimal a, decimal b, decimal eps, decimal f)
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
                else if(f2 > f1)
                    b = c;             
                else
                {
                    a = c - eps;
                    b = c + eps;
                }           
            }
            return ((a+b)/2, k);
        }
    }
}
