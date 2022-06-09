using Microsoft.VisualStudio.TestTools.UnitTesting;
using VirusWarGameServer;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace VirusWarGameTests
{
    [TestClass]
    public class UnitTest1
    {
        //[TestMethod]
        //public void TestMethod1()
        //{
            //Console.WriteLine((short)Math.Abs(-0.2));

            //object[] obj = { 3, 3 };
            //Assert.AreEqual(obj[0], obj[1]);

            //bool test1 = false;
            //bool test2 = true;

            //Assert.AreEqual(test1 & test2, true);
            //Assert.AreEqual(test1 & test2, false);


            //Assert.AreEqual(Math.Pow(2, 4), 16);
            //Assert.AreNotEqual(Math.Pow(2, 1), 4.0);
        //}

        [TestMethod]
        public void ItAlreadyExistsInTheGameBoard()
        {
            GameBoard gameBoard = new GameBoard();

            /*이동할려는 위치에 바이러스가 존재하도록 설정*/
            gameBoard.AddVirus(16, new Player());

            ExecuteDecorator decorator = new ExecuteDecorator();
            bool executionResult = decorator.Execute(new TheSamePlace(new PreventMovementMoreThanTwoCell(new DuplicateLocation())),
                                                      (short)0,
                                                      (short)16,
                                                      gameBoard);

            Assert.AreEqual(executionResult, false);
        }

        [TestMethod]
        public void OutOfRangeOfMovement()
        {
            GameBoard gameBoard = new GameBoard();
            ExecuteDecorator decorator = new ExecuteDecorator();
            bool executionResult = decorator.Execute(new TheSamePlace(new PreventMovementMoreThanTwoCell(new DuplicateLocation())),
                                                      (short)0,
                                                      (short)17,
                                                      gameBoard);

            Assert.AreEqual(executionResult, false);

            executionResult = decorator.Execute(new TheSamePlace(new PreventMovementMoreThanTwoCell(new DuplicateLocation())),
                                               (short)0,
                                               (short)16,
                                               gameBoard);

            Assert.AreEqual(executionResult, true);
        }

        [TestMethod]
        public void GoTotheSamePlace()
        {
            GameBoard gameBoard = new GameBoard();
            gameBoard.AddVirus(3, new Player());


            ExecuteDecorator decorator = new ExecuteDecorator();
            bool executionResult = decorator.Execute(new TheSamePlace(new PreventMovementMoreThanTwoCell(new DuplicateLocation())),
                                                      (short)0,
                                                      (short)3,
                                                      gameBoard);

            Assert.AreEqual(executionResult, false);

            executionResult = decorator.Execute(new TheSamePlace(new PreventMovementMoreThanTwoCell(new DuplicateLocation())),
                                          (short)0,
                                          (short)4,
                                          gameBoard);

            Assert.AreEqual(executionResult, true);

        }
    }
}
