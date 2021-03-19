using ComputationGame.Business;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputationGame.Tests
{
    public class Tests
    {
        //private static readonly object[] _Numbers ={
        //        new object[] {new List<int> { 1, 1, 2, 3, 4, 10, 150 } },   //case 1
        //        new object[] {new List<int> {4,5,4,1,5,25,651}} //case 2
        //};
        //private static readonly object[] _Solution ={
        //        new object[] {"1+2=3\r\n3*4=12\r\n12+3=15\r\n15*10=150" },   //case 1
        //        new object[] {"5*5=25\r\n25+1=26\r\n26*25=650\r\n4:4=1\r\n650+1=651"} //case 2
        //};
        public static IEnumerable<object[]> GetMultiTestElements()
        {
            yield return
               new object[]
               {
                    new List<int> { 1, 1, 2, 3, 4, 10, 150 } ,
                 "1+2=3\r\n3*4=12\r\n12+3=15\r\n15*10=150"
                };

            yield return
                new object[]
                {
                    new List<int> {4,5,4,1,5,25,651},
                    "5*5=25\r\n25+1=26\r\n26*25=650\r\n4:4=1\r\n650+1=651"
                };
        }
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void DigitsOfNumbersShouldBeCorrect()
        {
            //Arrange
            IComputationService service = new ComputationService();
            //Act
            var numbers = service.GetNumbers();

            //Assert
            Assert.False(numbers.Take(5).Any(x => x < 1 || x > 9));
            Assert.True(numbers[5] > 9 && numbers[5] < 100 && numbers[5] % 5 == 0);
            Assert.True(numbers[6] > 99 && numbers[6] < 1000);
        }

        [Test]
        [TestCaseSource("GetMultiTestElements")]                
        public void Solution_IsCorrrect_ReturnTrue(List<int> numbers, string solution)
        {
            //Arrange
            IComputationService service = new ComputationService();
            
            //Act
            var result = service.CheckSolution(numbers, solution);

            //Assert
            Assert.True(result);
        }
    }
}