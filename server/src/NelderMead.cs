// Импортируем типы из соседнего файла
using HELPERS;

namespace NELDER_MEAD
{
    // Пока что работаем с функцией f(x,y) = x^2+xy+y^2-6x-9y
    // потом рассмотрим варианты пользовательских функций


    // Ожидается, что в результате работы метода вернутся координаты финального решения,
    // а также список шагов. Один шаг представляется массивом из трёх точек
    public class NelderMead
    {
        public static List<Point[]> Steps { get; set; } = new List<Point[]>();
        public static Point Solution { get; set; }

        public static Result GetResult()
        {
            // Имитируем 2 шага работы алгоритма
            Steps.Add(new Point[] { new Point(1, 2), new Point(3, 4), new Point(5, 6) });
            Steps.Add(new Point[] { new Point(7, 8), new Point(9, 10), new Point(11, 12) });

            // Имитируем финальное решение
            Solution = new Point(1, 4);

            return new Result(Steps, Solution);
        }
    }
}