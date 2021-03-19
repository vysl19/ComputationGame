using ComputationGame.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputationGame.Business
{
    public interface IComputationService
    {
        /// <summary>
        /// return 7 numbers.
        /// First 5 numbers are one digit numbers
        /// Sixth Number are two digits numbers
        /// Last Number is three digits numbers.
        /// Expected to find last number while making computations with first six numbers
        /// </summary>
        /// <returns></returns>
        List<int> GetNumbers();

        /// <summary>
        /// Validate if computations is corrrect
        /// </summary>
        /// <param name="sb"></param>
        /// <returns></returns>
        bool CheckSolution(List<int> numbers, string solution);
    }
    public class ComputationService : IComputationService
    {
        public List<int> GetNumbers()
        {
            var numbers = new List<int>();
            for (int i = 0; i < 5; i++)
            {
                numbers.Add(new OneDigitNumber().GetNumber());
            }
            numbers.Add(new TwoDigitsNumber().GetNumber());
            numbers.Add(new ThreeDigitsNumber().GetNumber());
            return numbers;
        }
        public bool CheckSolution(List<int> numbers, string solution)
        {
            //the first six numbers are usable to access seventh number
            var usableNumbers = numbers.Take(6).ToList();
            var accessNumber = numbers[6];
            using (var stringReader = new StringReader(solution))
            {
                var line = stringReader.ReadLine();
                while (line != null)
                {
                    var lineArr = line.Split((char)Operations.Equals);
                    var leftLine = lineArr[0];
                    var rightLine = lineArr[1];
                    IOperation operation = OperationFactory.Create(leftLine);
                    var result = operation.Do();
                    if (result.ToString() != rightLine)
                    {
                        throw new Exception("WrongComputation");
                    }
                    var usedNumbers = operation.GetUsedNumbers();
                    if (!usableNumbers.Any(x => x == usedNumbers.Item1) || !numbers.Any(x => x == usedNumbers.Item2))
                    {
                        throw new Exception("WrongNumberUsed");
                    }
                    usableNumbers.Remove(usedNumbers.Item1);
                    usableNumbers.Remove(usedNumbers.Item2);
                    usableNumbers.Add(result);
                    if (result == accessNumber)
                    {
                        return true;
                    }
                    line = stringReader.ReadLine();
                }
            }
            return false;
        }
    }
    public enum Operations
    {
        Adding = '+',
        Difference = '-',
        Multiplication = '*',
        Dividing = ':',
        Equals = '='
    }
    public interface IOperation
    {
        int Do();
        Tuple<int, int> GetUsedNumbers();
    }
    public abstract class BaseOperation : IOperation
    {
        protected int FirstNumber;
        protected int SecondNumber;
        public BaseOperation(int firstNumber, int secondNumber)
        {
            FirstNumber = firstNumber;
            SecondNumber = secondNumber;
        }
        public abstract int Do();

        public Tuple<int, int> GetUsedNumbers()
        {
            return Tuple.Create(FirstNumber, SecondNumber);
        }
    }
    public class AddOperation : BaseOperation
    {
        public AddOperation(int firstNumber, int secondNumber) : base(firstNumber, secondNumber)
        {

        }
        public override int Do()
        {
            return FirstNumber + SecondNumber;
        }
    }
    public class DifferenceOperation : BaseOperation
    {
        public DifferenceOperation(int firstNumber, int secondNumber) : base(firstNumber, secondNumber)
        {
        }

        public override int Do()
        {
            return FirstNumber - SecondNumber;
        }

    }
    public class MultiplyOperation : BaseOperation
    {
        public MultiplyOperation(int firstNumber, int secondNumber) : base(firstNumber, secondNumber)
        {
        }

        public override int Do()
        {
            return FirstNumber * SecondNumber;
        }
    }
    public class DivideOperation : BaseOperation
    {
        public DivideOperation(int firstNumber, int secondNumber) : base(firstNumber, secondNumber)
        {
        }

        public override int Do()
        {
            return FirstNumber / SecondNumber;
        }
    }
    public class OperationFactory
    {
        public static IOperation Create(string text)
        {
            var arr = text.Split('+', '-', '*', ':');

            if (text.Contains((char)Operations.Adding))
            {
                return new AddOperation(int.Parse(arr[0]), int.Parse(arr[1]));
            }
            else if (text.Contains((char)Operations.Difference))
            {
                return new DifferenceOperation(int.Parse(arr[0]), int.Parse(arr[1]));
            }
            else if (text.Contains((char)Operations.Multiplication))
            {
                return new MultiplyOperation(int.Parse(arr[0]), int.Parse(arr[1]));
            }
            //Todo after last if, can throw an error
            return new DivideOperation(int.Parse(arr[0]), int.Parse(arr[1]));
        }
    }
}
