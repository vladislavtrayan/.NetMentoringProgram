using System;
using System.Collections.Generic;

namespace Sudoku
{
    public class Sudoku
    {
        private static HashSet<int> validSet = new HashSet<int>(){1,2,3,4,5,6,7,8,9};
        public static bool IsValid(int[,] solution, int size = 9)
        {
            for (int i = 0; i < size; i++)
            {
                var rowSet = new HashSet<int>();
                var columnSet = new HashSet<int>();
                for (int j = 0; j < size; j++)
                {
                    rowSet.Add(solution[i, j]);
                    columnSet.Add(solution[j, i]);
                }

                if (!rowSet.IsSupersetOf(validSet) || !columnSet.IsSupersetOf(validSet))
                    return false;
            }

            return true;
        }
    }
}