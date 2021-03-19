using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputationGame.Entity
{
    public interface INumber
    {
        int GetNumber();
    }
    public class OneDigitNumber : INumber
    {

        public int GetNumber()
        {
            var rnd = new Random();
            return rnd.Next(1, 10);
        }
    }
    public class TwoDigitsNumber : INumber
    {
        public int GetNumber()
        {
            var rnd = new Random();
            //expected to be 10, 15, 25 ... etc
            return rnd.Next(2, 20) * 5;
        }
    }
    public class ThreeDigitsNumber : INumber
    {
        public int GetNumber()
        {
            var rnd = new Random();
            return rnd.Next(100, 1000);
        }
    }
}
