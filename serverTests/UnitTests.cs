// Импортируем пространство имён из файла NelderMead.cs
// Тесты запускаются сочетанием клавиш Ctrl+R, затем a (вероятно Run All)
// Проваленные тесты будут выделены красным, а успешные - зелёным
using NELDER_MEAD;
using HELPERS;

namespace Tests
{
    [TestClass]
    public class NelderMeadTests
    {
        public Point[] InitialPoints = new Point[3]{
            new Point(0, 0),
            new Point(1, 0),
            new Point(0, 1)
        };
        // Создаём тест
        [TestMethod]
        // Название метода - описание теста
        public void NelderMeadResultReturnsCorrectSolution()
        {
            var result = new NelderMead(InitialPoints).GetResult();
            var solution = result.Solution;
            // Проверям координаты решения
            Assert.AreEqual(1, solution.X);
            Assert.AreEqual(4, solution.Y);
        }

        [TestMethod]
        public void ThisTestWillFail()
        {
            Assert.AreEqual(666, new NelderMead(InitialPoints).GetResult());
        }
    }
}