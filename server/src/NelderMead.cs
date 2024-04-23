// Импортируем типы из соседнего файла
using HELPERS;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace NELDER_MEAD {
    // Пока что работаем с функцией f(x,y) = x^2+xy+y^2-6x-9y
    // потом рассмотрим варианты пользовательских функций

    // Ожидается, что в результате работы метода вернутся координаты финального решения,
    // а также список шагов. Один шаг представляется массивом из трёх точек
    public class NelderMead {
        public List<Simplex> Steps { get; set; } = new List<Simplex>();
        public Point? Solution { get; set; }
        public Simplex InitialPoints { get; set; }

        public NelderMead(Simplex initialPoints) {
            InitialPoints = initialPoints;
            Steps.Add((Simplex)InitialPoints.Clone());
        }

        public Result GetResult() {
            int uselessSteps = 0;
            Simplex step = InitialPoints;

            while (uselessSteps < 3) {
                Point xReflection = step.Reflection();
                if (xReflection.f() < step.Good.f()) {
                    Point xExpansion = step.Expansion();
                    if (xExpansion.f() < step.Best.f()) step.Worst = xExpansion;
                    else step.Worst = xReflection;
                }
                else {
                    Point xContract = step.Contract();
                    if (xContract.f() < step.Good.f()) step.Worst = xContract;
                    else {
                        step.Shrink();
                        uselessSteps++;
                    }
                }
                step.Sort();
                Steps.Add((Simplex)step.Clone());
            }
            Solution = Steps.Last().Best;
            return new Result(Steps, Solution);
        }
    }
}
