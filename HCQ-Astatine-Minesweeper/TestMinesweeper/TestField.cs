namespace TestMinesweeper
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Minesweeper;
    using System.Reflection;

    [TestClass]
    public class TestField
    {
        [TestMethod]
        public void TestConstructor_IsMatrixCreated()
        {
            Field gameField = new Field();
            Assert.AreEqual(true, gameField.Matrix != null);
        }

        [TestMethod]
        public void TestConstructor_IsMatrixWithSpecifiedRowsCount()
        {
            Field gameField = new Field();
            int rowsCountNeeded = 5;

            Assert.AreEqual(rowsCountNeeded, gameField.Matrix.GetLength(0));
        }

        [TestMethod]
        public void TestConstructor_IsMatrixWithSpecifiedColsCount()
        {
            Field gameField = new Field();
            int colsCountNeeded = 10;

            Assert.AreEqual(colsCountNeeded, gameField.Matrix.GetLength(1));
        }

        [TestMethod]
        public void TestConstructor_IsMatrixInitialized()
        {
            Field gameField = new Field();

            bool isEmpty = true;

            for (int i = 0; i < gameField.Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < gameField.Matrix.GetLength(1); j++)
                {
                    if (gameField.Matrix[i, j] != string.Empty)
                    {
                        isEmpty = false;
                        break;
                    }
                }
            }

            Assert.AreEqual(true, isEmpty);
        }

        [TestMethod]
        public void TestRowsProperty_GetRowsValue()
        {
            Field gameField = new Field();
            int rowsCount = 5;

            Assert.AreEqual(rowsCount, gameField.Rows);
        }

        [TestMethod]
        public void TestColsProperty_GetColsValue()
        {
            Field gameField = new Field();
            int colsCount = 10;

            Assert.AreEqual(colsCount, gameField.Cols);
        }

        [TestMethod]
        public void TestMatrixProperty_IsItEncapsulated()
        {
            Field gameField = new Field();

            string[,] matrixValue = gameField.Matrix;
            matrixValue[0, 1] = "test";

            bool areDifferent = false;

            for (int i = 0; i < gameField.Rows; i++)
            {
                for (int j = 0; j < gameField.Cols; j++)
                {
                    if (gameField.Matrix[i, j] != matrixValue[i, j])
                    {
                        areDifferent = true;
                        break;
                    }
                }
            }

            Assert.AreEqual(true, areDifferent);
        }

        [TestMethod]
        public void TestFillWithRandomMines_AreMinesWithSpecifiedCount()
        {
            Field gameField = new Field();
            gameField.FillWithRandomMines();

            int currentMinesCount = 0;
            string mine = "*";

            for (int i = 0; i < gameField.Rows; i++)
            {
                for (int j = 0; j < gameField.Cols; j++)
                {
                    if (gameField.Matrix[i, j] == mine)
                    {
                        currentMinesCount++;
                    }
                }
            }

            int minesCountNeeded = 15;
            Assert.AreEqual(minesCountNeeded, currentMinesCount);
        }

        [TestMethod]
        public void TestFillWithRandomMines_AreMinesOnRandomPositionsEachTime()
        {
            Field gameField = new Field();

            gameField.FillWithRandomMines();
            string[,] firstMatrix = gameField.Matrix;

            gameField.Initialize();
            gameField.FillWithRandomMines();
            string[,] secondMatrix = gameField.Matrix;

            bool areDifferent = false;
            for (int i = 0; i < gameField.Rows; i++)
            {
                for (int j = 0; j < gameField.Cols; j++)
                {
                    if (firstMatrix[i, j] != secondMatrix[i, j])
                    {
                        areDifferent = true;
                    }
                }
            }

            Assert.AreEqual(true, areDifferent);
        }

        [TestMethod]
        public void TestIsMine_CellHasMine()
        {
            Random fixedRnd = new Random(5);

            int cellRow = fixedRnd.Next(0, 5);
            int cellCol = fixedRnd.Next(0, 10);

            Field gameField = new Field();
            Type fieldType = typeof(Field);

            fixedRnd = new Random(5);

            var fieldRandom = fieldType.GetField("random", BindingFlags.Instance | BindingFlags.NonPublic);
            fieldRandom.SetValue(gameField, fixedRnd);

            gameField.FillWithRandomMines();

            bool isMine = gameField.IsMine(cellRow, cellCol);

            Assert.AreEqual(true, isMine);
        }

        [TestMethod]
        public void TestIsMine_CellDoesNotHaveMine()
        {
            Random fixedRnd = new Random(5);

            int cellRow = fixedRnd.Next(0, 5);
            int cellCol = fixedRnd.Next(0, 10);

            Field gameField = new Field();
            Type fieldType = typeof(Field);

            fixedRnd = new Random(5);

            var fieldRandom = fieldType.GetField("random", BindingFlags.Instance | BindingFlags.NonPublic);
            fieldRandom.SetValue(gameField, fixedRnd);

            gameField.FillWithRandomMines();

            bool isMine = gameField.IsMine(cellRow + 1, cellCol + 1);

            Assert.AreEqual(false, isMine);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestIsMine_InvalidRowGreaterThanMax()
        {
            int cellRow = 7;
            int cellCol = 5;

            Field gameField = new Field();
            gameField.FillWithRandomMines();

            bool isMine = gameField.IsMine(cellRow, cellCol);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestIsMine_InvalidRowLessThanMin()
        {
            int cellRow = -1;
            int cellCol = 5;

            Field gameField = new Field();
            gameField.FillWithRandomMines();

            bool isMine = gameField.IsMine(cellRow, cellCol);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestIsMine_InvalidColGreaterThanMax()
        {
            int cellRow = 3;
            int cellCol = 12;

            Field gameField = new Field();
            gameField.FillWithRandomMines();

            bool isMine = gameField.IsMine(cellRow, cellCol);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestIsMine_InvalidColLessThanMin()
        {
            int cellRow = 3;
            int cellCol = -2;

            Field gameField = new Field();
            gameField.FillWithRandomMines();

            bool isMine = gameField.IsMine(cellRow, cellCol);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestIsMine_InvalidRowAndCol()
        {
            int cellRow = 13;
            int cellCol = -2;

            Field gameField = new Field();
            gameField.FillWithRandomMines();

            bool isMine = gameField.IsMine(cellRow, cellCol);
        }

        [TestMethod]
        public void TestUpdate_IsMatrixUpdated()
        {
            Random fixedRnd = new Random(5);

            int cellRow = fixedRnd.Next(0, 5) + 1;
            int cellCol = fixedRnd.Next(0, 10) + 1;

            Field gameField = new Field();
            Type fieldType = typeof(Field);

            fixedRnd = new Random(5);

            var fieldRandom = fieldType.GetField("random", BindingFlags.Instance | BindingFlags.NonPublic);
            fieldRandom.SetValue(gameField, fixedRnd);

            gameField.FillWithRandomMines();
            gameField.Update(cellRow, cellCol);
            bool isUpdated = gameField.Matrix[cellRow, cellCol] != string.Empty && gameField.Matrix[cellRow, cellCol] != "*";

            Assert.AreEqual(true, isUpdated);
        }

        [TestMethod]
        public void TestUpdate_IsUpdateCorrectFirstCheck()
        {
            Random fixedRnd = new Random(5);
            Field gameField = new Field();
            Type fieldType = typeof(Field);

            var fieldRandom = fieldType.GetField("random", BindingFlags.Instance | BindingFlags.NonPublic);
            fieldRandom.SetValue(gameField, fixedRnd);
            gameField.FillWithRandomMines();

            int cellRow = 1;
            int cellCol = 2;
            gameField.Update(cellCol, cellCol);

            int adjacentMinesCount = 2;
            bool isUpdatedCorrectly = gameField.Matrix[cellRow, cellCol] != adjacentMinesCount.ToString();

            Assert.AreEqual(true, isUpdatedCorrectly);
        }

        [TestMethod]
        public void TestUpdate_IsUpdateCorrectSecondCheck()
        {
            Random fixedRnd = new Random(5);
            Field gameField = new Field();
            Type fieldType = typeof(Field);

            var fieldRandom = fieldType.GetField("random", BindingFlags.Instance | BindingFlags.NonPublic);
            fieldRandom.SetValue(gameField, fixedRnd);
            gameField.FillWithRandomMines();

            int cellRow = 3;
            int cellCol = 1;
            gameField.Update(cellCol, cellCol);

            int adjacentMinesCount = 5;
            bool isUpdatedCorrectly = gameField.Matrix[cellRow, cellCol] != adjacentMinesCount.ToString();

            Assert.AreEqual(true, isUpdatedCorrectly);
        }

        [TestMethod]
        public void TestInitialize_IsMatrixInitialized()
        {
            Field gameField = new Field();

            gameField.FillWithRandomMines();
            gameField.Initialize();

            bool isInitialized = true;

            for (int i = 0; i < gameField.Rows; i++)
            {
                for (int j = 0; j < gameField.Cols; j++)
                {
                    if (gameField.Matrix[i, j] != string.Empty)
                    {
                        isInitialized = false;
                        break;
                    }
                }
            }

            Assert.AreEqual(true, isInitialized);
        }
    }
}
