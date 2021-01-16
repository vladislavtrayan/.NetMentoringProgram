using System;
using Xunit;
using Labyrinth;

namespace Tests
{
    public class Tests
    {
        private char[,] validLabyrinth = new char[,] { 
            { '*','*','*','*','*','*'},
            { '_','*','_','_','_','*'},
            { '*','s','_','*','_','*'},
            { '*','*','*','*','_','*'},
            { '_','_','e','_','_','*'},
            { '_','*','*','*','_','*'}
        };

        [Fact]
        public void ReturnsNullArgumentExceptionIfArgumnentIsNull()
        {
            Directions[] directions;
            Assert.Throws<ArgumentNullException>(() => LabyrinthSolver.GetSolution(null, out directions));
        }

        [Fact]
        public void ReturnsTrueAndValidSolutionAsOutParamWhenLabyrinthIsCorrect()
        {
            Directions[] directions;
            Directions[] expectedSolution = new Directions[] {
                Directions.Right,
                Directions.Up,
                Directions.Right,
                Directions.Right,
                Directions.Down,
                Directions.Down,
                Directions.Down, 
                Directions.Left, 
                Directions.Left 
            };

            Assert.True(LabyrinthSolver.GetSolution(validLabyrinth, out directions));
            Assert.Same(expectedSolution, directions);
        }
    }
}
