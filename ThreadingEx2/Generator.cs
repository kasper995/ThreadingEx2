using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadingEx2
{
    class Generator
    {
        private int[][] _result;
        private const int MAXVALUE = 1000000;
        private Random rnd = new Random();

        public Generator()
        {

        }

        public int[][] result
        {
            get { return _result; }
        }

        public void generate(int howManyArrays, int howManyInEachArray)
        {
            _result = new int[howManyArrays][];

            for (int i = 0; i < howManyArrays; i++)
            {
                Task hmTaska = Task.Run(() =>
                {
                    _result[i] = new int[howManyInEachArray];

                });
                for (int j = 0; j < howManyInEachArray; j++)
                {
                    Task eaTaska = Task.Run(() =>
                    {
                        _result[i][j] = rnd.Next(MAXVALUE*2) - MAXVALUE;
                    });
                }
            }
        }
    }
}
