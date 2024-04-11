// Импортируем пространство имён из файла NelderMead
// Тесты запускаются сочетанием клавиш Ctrl+R, затем a (вероятно Run All)
// Проваленные тесты будут выделены красным, а успешные - зелёным
using NELDER_MEAD;
using HELPERS;

namespace Tests
{
    [TestClass]
    public class NelderMeadTests
    {
        // Создаём тест
        [TestMethod]
        // Назвение метода - описание теста
        public void NelderMeadResultReturnsCorrectSolution()
        {
            var result = NelderMead.GetResult();
            var solution = result.Solution;
            // Проверям координаты решения
            Assert.AreEqual(1, solution.X);
            Assert.AreEqual(4, solution.Y);
        }

        [TestMethod]
        public void ThisTestWillFail()
        {
            Assert.AreEqual(666, NelderMead.GetResult());
        }
    }
}