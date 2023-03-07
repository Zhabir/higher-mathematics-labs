using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Drawing;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Intrinsics;
using static methodGauss.Class1;
using System.Timers;
using System.Text.Json;
using System.IO;
using System.Security;
using System.Xml;

namespace methodGauss
{
    public class Class1
    {
        static public int N;
        static public float[,] findMainElement(float[,] matrix, int size, int column)
        {
            float max = -99999;
            int index = -1;
            for (int i = 0; i < size; i++)
                if (Math.Abs(matrix[i, column]) > max)
                {
                    max = matrix[i, column];
                    index = i;
                }

            for (int i = 0; i < size + 1; i++)
            {
                float a = matrix[index, i];
                matrix[index, i] = matrix[column, i];
                matrix[column, i] = a;
            }
            return matrix;
        }

        static public void Gauss(float[,] matrix, int N)
        {
            float tmp;
            float[] xx = new float[N];
            int i, j, k;
            for (i = 0; i < N; i++)
            {
                matrix = findMainElement(matrix, N, i);
                Console.WriteLine("\nMatrix after Main El:");
                for (int i1 = 0; i1 < 3; i1++)
                {
                    for (int j1 = 0; j1 < 4; j1++)
                        Console.Write($"{matrix[i1, j1]} ");
                    Console.Write("\n");
                }
                tmp = matrix[i, i];
                for (j = N; j >= i; j--)
                    matrix[i, j] /= tmp;

                for (j = i + 1; j < N; j++)
                {
                    tmp = matrix[j, i];
                    for (k = N; k >= i; k--)
                        matrix[j, k] -= tmp * matrix[i, k];
                }

                Console.WriteLine("\nMatrix after Чtmp:");
                for (int i1 = 0; i1 < 3; i1++)
                {
                    for (int j1 = 0; j1 < 4; j1++)
                        Console.Write($"{matrix[i1, j1]} ");
                    Console.Write("\n");
                }
            }

            xx[N - 1] = matrix[N - 1, N];
            for (i = N - 2; i >= 0; i--)
            {
                xx[i] = matrix[i, N];
                for (j = i + 1; j < N; j++) xx[i] -= matrix[i, j] * xx[j];
            }

            Console.WriteLine("\nMetod Gaussa:\n");
            for (i = 0; i < N; i++)
                Console.WriteLine($"x{i + 1}={xx[i]}\n");
        }
        public static float[] x;

        static public changeMatrix(float[,] matrix, int size )
        {
            int index = 0;
            for (int i=0; i<N; i++)
            {
                if(matrix[i,index]>)
            }
        }

        static public void Iter_Method(float[,] matrix, int N)
        {
            float[,] alpha = new float[N, N];
            float[] beta = new float[N];
            float buff = 0;
            float norm_B = 0;
            float sum_of_alpha = 0;
            for (int i = 0; i < N; i++)
            {
                beta[i] = matrix[i, N] / matrix[i, i];
                for (int j = 0; j < N; j++)
                {
                    if (i == j)
                    {

                        alpha[i, j] = 0;
                    }
                    else
                    {
                        alpha[i, j] = -1 * (matrix[i, j] / matrix[i, i]);

                    }
                }
            }
            for (int j = 0; j < N; j++) //ищем максимальную сумму строк(норму)
            {
                for (int i = 0; i < N; i++)
                {
                    alpha[i, j] = alpha[i, j] / (float)20.0;
                    buff += Math.Abs(alpha[i, j]);
                }
                if (buff > norm_B)
                    norm_B = buff;
                buff = (float)0.0;
            }
            int iter = 0;
            float[] t = new float[N];
            float norm = 0;
            for (int i = 0; i < N; i++)
            {
                x[i] = beta[i];
            }
            while (true)
            {
                iter++;
                for (int i = 0; i < N; i++)
                {
                    t[i] = x[i];
                }
                for (int i = 0; i < N; i++)
                {
                    sum_of_alpha = (float)0.0;
                    for (int j = 0; j < N; j++)
                        sum_of_alpha += alpha[i, j] * x[j];

                    x[i] = beta[i] + sum_of_alpha;
                }
                //проверка услови€ окончани€
                for (int i = 0; i < N; i++)
                    norm += Math.Abs(t[i] - x[i]);

                if (norm <= (1 - norm_B) * 0.0000001 / norm_B)
                    return;
                norm = (float)0.0;
            }

        }
        public static void Main(string[] args)
        {
            N = Convert.ToInt32(Console.ReadLine());
            float[,] matrix = new float[N, N + 1];
            x = new float[N];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N + 1; j++)
                    matrix[i, j] = (float)Convert.ToDouble(Console.ReadLine());

            }

            Console.WriteLine("Matrix");
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N + 1; j++)
                    Console.Write($"{matrix[i, j]} ");
                Console.Write("\n");
            }
            Iter_Method(matrix, N);
            for (int i = 0; i < N; i++)
            {
                Console.WriteLine(x[i]);
            }
        }
    }
}