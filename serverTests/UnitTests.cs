// Импортируем пространство имён из файла NelderMead.cs
// Тесты запускаются сочетанием клавиш Ctrl+R, затем a (вероятно Run All)
// Проваленные тесты будут выделены красным, а успешные - зелёным
using NELDER_MEAD;
using HELPERS;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration.Ini;

namespace Tests
{
    [TestClass]
    public class NelderMeadTests
    {
        // Создаём тест
        [TestMethod]
        // Название метода - описание теста
        public void NelderMeadResultReturnsCorrectSolution()
        {
            Simplex InitialPoints = new(new Point(0, 0), new Point(1, 0), new Point(0, 1));
            var result = new NelderMead(InitialPoints).GetResult();
            var actual = result.Solution;
            // Проверям координаты решения
            Assert.AreEqual(1, actual.X);
            Assert.AreEqual(4, actual.Y);
        }
        [TestMethod]
        public void ResultSmallVarianceReturnsCorrect()
        {
            Simplex InitialPoints = new(new Point(2, 4), new Point(2.002, 3.98), new Point(1.9, 4.2));
            var result = new NelderMead(InitialPoints).GetResult();
            var actual = result.Solution;
            double delta = 0.4;
            Assert.AreEqual(1, actual.X, delta);
            Assert.AreEqual(4, actual.Y, delta);
        }
        [TestMethod]
        public void ResultPointsFarFromSolutionReturnsCorrect()
        { 
            Simplex InitialPoints = new(new Point(100, 100), new Point(100, 101), new Point(101, 100));
            var result = new NelderMead(InitialPoints).GetResult();
            var actual = result.Solution;
            double delta = 0.4;
            Assert.AreEqual(1, actual.X, delta);
            Assert.AreEqual(4, actual.Y, delta);
        }
        [TestMethod]
        public void ResultTwoSamePointsReturnsWrong()
        {
            Simplex InitialPoints = new(new Point(0, 0), new Point(0, 0), new Point(0, 1));
            var result = new NelderMead(InitialPoints).GetResult();
            var actual = result.Solution;
            double delta = 0.4;
            Assert.AreNotEqual(1, actual.X, delta);
            Assert.AreNotEqual(4, actual.Y, delta);
        }
    }
    [TestClass]
    public class SimplexTests
    {
        [TestMethod]
        public void PointConstructorWrongInputThrowsExc()
        {
            Point point;
            Assert.ThrowsException<FormatException>(() => point = new(0, Convert.ToDouble("Cat")));
        }
        [TestMethod]
        public void PointConstructorCorrectInput()
        {
            Point point = new(3, 4);
            Assert.AreEqual(point.X, 3);
            Assert.AreEqual(point.Y, 4);
        }
        [TestMethod]
        public void SimplexConstructorWrongInputThrowsExc()
        {
            Simplex InitialPoints;
            Assert.ThrowsException<FormatException>(() => InitialPoints = new(new Point(0, 0), new Point(1, 0), new Point(0, Convert.ToDouble("Cat"))));
        }
        [TestMethod]
        public void SimplexConstructorCorrectInput()
        {
            Point Best = new(0, 1);
            Point Good = new(1, 0);
            Point Worst = new(0, 0);

            Simplex actual = new(Best, Good, Worst);

            Assert.AreEqual(actual.Best.X, Best.X);
            Assert.AreEqual(actual.Best.Y, Best.Y);
            Assert.AreEqual(actual.Good.X, Good.X);
            Assert.AreEqual(actual.Good.Y, Good.Y);
            Assert.AreEqual(actual.Worst.X, Worst.X);
            Assert.AreEqual(actual.Worst.Y, Worst.Y);
        }
        public void SimplexToString()
        {
            var InitialPoints = new Simplex(new Point(0, 0), new Point(1, 0), new Point(0, 1));
            string expected = "Best: 0, 1, Good: 1, 0, Worst: 0, 0";

            var result = InitialPoints.ToString();

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void SimplexSort()
        {
            // Для тестовой функции
            var Good = new Point(3.3, 5); //f(point1) = -12.41
            var Worst = new Point(-2, -4); //f(point2) = 76
            var Best = new Point(3.14, 2.71); // f(point3) = -17.51
            var InitialPoints = new Simplex(Good, Worst, Best);

            InitialPoints.Sort();
            var actual = (Simplex)InitialPoints.Clone();

            Assert.AreEqual(actual.Best.X, Best.X);
            Assert.AreEqual(actual.Best.Y, Best.Y);
            Assert.AreEqual(actual.Good.X, Good.X);
            Assert.AreEqual(actual.Good.Y, Good.Y);
            Assert.AreEqual(actual.Worst.X, Worst.X);
            Assert.AreEqual(actual.Worst.Y, Worst.Y);
        }

        [TestMethod]
        public void SimplexReflectionSimple()
        {
            var InitialPoints = new Simplex(new Point(0, 0), new Point(1, 0), new Point(0, 1));
            var expected = new Point(1, 1);

            var actual = InitialPoints.Reflection();

            Assert.AreEqual(expected.ToString(), actual.ToString());
        }
        [TestMethod]
        public void SimplexReflection()
        {
            var InitialPoints = new Simplex(new Point(0.00001, 1), new Point(2.25, 3.67), new Point(-6.33, -2.01));
            var expected = new Point(8.58, 6.68);
            var delta = 0.01;

            var actual = InitialPoints.Reflection();

            Assert.AreEqual(expected.X, actual.X,delta);
            Assert.AreEqual(expected.Y,actual.Y,delta);
        }
        [TestMethod]
        public void SimplexExpansionSimple()
        {
            var InitialPoints = new Simplex(new Point(0, 0), new Point(1, 0), new Point(0, 1));
            var expected = new Point(1.5, 1.5);

            var actual = InitialPoints.Expansion();

            Assert.AreEqual(expected.ToString(), actual.ToString());
        }
        [TestMethod]
        public void SimplexExpansion()
        {
            var InitialPoints = new Simplex(new Point(1, 1), new Point(2.25, 3.67), new Point(-6.33, -2.01));
            var expected = new Point(17.53, 11.02);
            var delta = 0.01;

            var actual = InitialPoints.Expansion();

            Assert.AreEqual(expected.X, actual.X, delta);
            Assert.AreEqual(expected.Y, actual.Y, delta);
        }
        [TestMethod]
        public void SimplexExpansionWithSamePoints()
        {
            var InitialPoints = new Simplex(new Point(1, 1), new Point(1, 1), new Point(-6.33, -2.01));
            var expected = new Point(15.66, 7.02);
            var delta = 0.01;

            var actual = InitialPoints.Expansion();

            Assert.AreEqual(expected.X, actual.X, delta);
            Assert.AreEqual(expected.Y, actual.Y, delta);
        }
        [TestMethod]
        public void SimplexContractSimple()
        {
            var InitialPoints = new Simplex(new Point(0, 0), new Point(1, 0), new Point(0, 1));
            var expected = new Point(0.25, 0.25);
            var delta = 0.01;

            var actual = InitialPoints.Contract();

            Assert.AreEqual(expected.X, actual.X, delta);
            Assert.AreEqual(expected.Y, actual.Y, delta);
        }
        [TestMethod]
        public void SimplexContract()
        {
            var InitialPoints = new Simplex(new Point(1, 1), new Point(2.25, 3.67), new Point(-6.33, -2.01));
            var expected = new Point(-2.35, 0.16);
            var delta = 0.01;

            var actual = InitialPoints.Contract();

            Assert.AreEqual(expected.X, actual.X, delta);
            Assert.AreEqual(expected.Y, actual.Y, delta);
        }
        [TestMethod]
        public void SimplexShrinkSimple()
        {
            var InitialPoints = new Simplex(new Point(0, 0), new Point(1, 0), new Point(0, 1));
            var expected = new Simplex(new Point(0, 1), new Point(0.5, 0.5), new Point(0, 0.5));

            InitialPoints.Shrink();
            var actual = InitialPoints.Clone();

            Assert.AreEqual(expected.ToString(), actual.ToString());
        }
        [TestMethod]
        public void SimplexShrink()
        {
            var InitialPoints = new Simplex(new Point(1, 1), new Point(2.25, 3.67), new Point(-6.33, -2));
            var expected = new Simplex(new Point(2.25, 3.67), new Point(1.625, 2.335), new Point(-2.04, 0.835));

            InitialPoints.Shrink();
            var actual = InitialPoints.Clone();

            Assert.AreEqual(expected.ToString(), actual.ToString());
        }
    }
}
