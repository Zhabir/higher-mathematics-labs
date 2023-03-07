using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ВычМат_Лаба1
{
    internal class FindSolutions
    {
        static public float e = 0.001f;
        static float f(float x) {return x*x - (20* (float)Math.Sin(x)) - 5;}
        static float df(float x){return 2 * x - 20 * (float)Math.Cos(x);}
        
        static public float half_div_method(float a, float b)
        {
            float x=0;
            float result = x;

            while (Math.Abs(b - a) >= 2 * e)
            {
                result = x;
                x = (a + b) / 2;

                if(f(a)<=0)
                {
                    if (f(x) < 0) a = x;
                    else b = x;
                }
            }
            return result;
        }

        static public float newton_method(float a, float b)
        {
            float Dx, x;
            float x0 = b;
            if ((float)Math.Abs(f(a)) < (float)Math.Abs(f(b))) x0 = a;
            do
            {
                Dx = f(x0) / df(x0);
                x = x0 - Dx;

                if (f(x0) == 0)
                    break;
                x0 = x;
            }
            while (Math.Abs(f(x0)) > e);
            return x;

        }

        static public float simple_iter_method(float a,float b)
        {
            float result;
            float x = a;
            float c;
            float max = Math.Abs(df(a));
            //float min = df(a);
            float e = 0.001f;
            for (float i = a; i < b; i += e)
            {
                if (max < Math.Abs(df(i))) max = Math.Abs(df(i));
               // if (min > df(i)) min = df(i);
            }

            c = -1 / max;
            max = 2 * c * a - 20 * c * (float)Math.Cos(a);
            for (float i = a; i < b; i+=e)
            {
                if (max < 2 * c * i - 20 * c * Math.Cos(i)) max = 2 * c * i - 20 * c * (float)Math.Cos(i);
            }
            float q = max;
            bool flag;
            do
            {
                result = x;
                x = f(x) * c + x;
                if (q > 1 / 2)
                {
                    if (Math.Abs(result - x) < (q - 1) * e / q) flag = true;
                    else flag = false;
                }
                else
                {
                    if (Math.Abs(result - x) <= e) flag = true;
                    else flag = false;
                }
            }
            while (flag);
            return x;
        }
    }
}
