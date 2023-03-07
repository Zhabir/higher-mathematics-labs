using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Drawing;
using System.Runtime.ConstrainedExecution;
using static methodGauss.Class1;
using System.Timers;
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
        static public void showMatrix(float[,] matrix)
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N + 1; j++)
                    Console.Write($"{matrix[i, j]} ");
                Console.Write("\n");
            }
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

                Console.WriteLine("\nMatrix after —tmp:");
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

        static public float getSum(float[,] matrix, int indexLine, int indexColumn)
        {
            float sum = 0;
            for (int i = indexLine + 1; i < N; i++)
                sum += matrix[i,indexColumn];
            return sum;
        }
        static public float[,] swapLines(float[,] matrix, int index)
        {
            for (int i = 0; i <= N; i++)
            {
                float c = matrix[index, i];
                matrix[index, i] = matrix[index + 1, i];
                matrix[index+1, i] = c; 
            }
            return matrix;
        }
        static public float[,] changeMatrix(float[,] matrix)
        {
            int index = 0;
            for (int i = 0; i < N; i++)
            {
                if (matrix[i, index] < getSum(matrix, i, index))
                    matrix = swapLines(matrix, i);
                index++;
            }

            return matrix;
        }
        public static float norm_matrix(float[,] matrix, int N)
        {
            float max_sum = 0;
            float sum = 0;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    sum += Math.Abs(matrix[i, j]);
                }
                if (max_sum < sum) max_sum = sum;
                sum = 0;
            }
            return max_sum;
        }
        public static float norm_vec(float[] vec, int N)
        {
            float max = Math.Abs(vec[0]);
            for (int i = 0; i < N; i++)
            {
                if (max < Math.Abs(vec[i])) max = vec[i];
            }
            return max;
        }
        static public void Iter_Method(float[,] matrix, int N)
        {
            float[,] alpha = new float[N, N];
            float[] beta = new float[N];
            float buff = 0;
            float norm_B = 0;
            float sum_of_alpha = 0;
            matrix = changeMatrix(matrix);
            Console.WriteLine("\nMatrix after changing:");
            showMatrix(matrix);
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

            Console.WriteLine("\nALPHA Matrix:");
            for(int i=0; i<N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    Console.Write($"{alpha[i,j]} ");
                }
                Console.Write($"\n");
            }
            Console.WriteLine("\nBeta Matrix:");
            for(int i=0; i<N;i++)
                Console.WriteLine($"{beta[i]} ");
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
            int itera = 0;
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
                        sum_of_alpha += alpha[i, j] * (t[j]);
                    Console.WriteLine($"\nsum of alphs = {sum_of_alpha}");
                    x[i] = beta[i] - sum_of_alpha;

                    Console.WriteLine($"\nx[{i}] = {beta[i]}+{sum_of_alpha} = {x[i]}");
                }
                //проверка условия окончания

                float[] v = new float[N];
                for(int i = 0; i < N; i++)
                {
                    v[i] = t[i] - x[i];
                }
                if (norm_vec(v, N) <= ((1 - norm_matrix(alpha, N)) / norm_matrix(alpha, N) * 0.0001)) return;
            }

        }
        
        public static void Main(string[] args)
        {
            Console.WriteLine("\nInput size:");
            N = Convert.ToInt32(Console.ReadLine());
            float[,] matrix = new float[3, 4] { { (float)1, (float)1, (float)15, (float)17 }, 
                                                { (float)15, (float)0, (float)1, (float)16 }, 
                                                { (float)4, (float)15, (float)1, (float)20 } };
            x = new float[N];
            //Console.WriteLine("\nInput matrix:");
            //for (int i = 0; i < N; i++)
            //{
            //    for (int j = 0; j < N + 1; j++)
            //        matrix[i, j] = (float)Convert.ToDouble(Console.ReadLine());

            //}
            Console.WriteLine("\nMatrix:");
            showMatrix(matrix);

            //Console.WriteLine("\nMethod Gauss:");
            //Gauss(matrix, N);
            Iter_Method(matrix, N);;
            Console.WriteLine("\nAnswers:");
            for (int i = 0; i < N; i++)
            {
                Console.Write($"{x[i]} ");
            }

            int a = Convert.ToInt32(Console.ReadLine());
        }
    }
}
