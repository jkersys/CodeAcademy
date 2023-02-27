//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using TowerOfHanoi.Service;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using TowerOfHanoi.Models;
//using TowerOfHanoi.Interface;

//namespace TowerOfHanoi.Service.Tests
//{
//    [TestClass()]
//    public class LoggerTests
//    {
//        [TestMethod()]
//        public void LogTest()
//        {
//            ILog logger = new Logger(true, false, false);
//            var fakeGame = new Game(logger, 3, 5);

//            string[] Data = fakeGame.GetLogData();
//            var expected = Data[0] + "," + 0 + "," + 1 + "," + 1 + "," + 1 + "," + 1;



//            //Act
//            var actual = logger.CreateCsvLogText(Data).ToString();
//            foreach (var data in Data)
//            {
//                Console.WriteLine(data);
//            }

//            //Assert
//            Assert.AreEqual(expected, actual);
//        }
//        [TestMethod()]
//        public void LogTest1()
//        {
//            ILog logger = new Logger(true, false, false);
//            var fakeGame = new Game(logger, 3, 5);

//            string[] Data = fakeGame.GetLogData();
//            var expected = $"<tr>{Environment.NewLine}" +
//                $"<td>{Data[0]}</td>{Environment.NewLine}" +
//                $"<td>0</td>{Environment.NewLine}" +
//                $"<td>1</td>{Environment.NewLine}" +
//                $"<td>1</td>{Environment.NewLine}" +
//                $"<td>1</td>{Environment.NewLine}" +
//                $"<td>1</td>{Environment.NewLine}" +
//                $"</tr>{Environment.NewLine}" +
//                $"</table>";

//            //Act
//            var actual = logger.CreateHtmlLogText(Data);

//            //Assert
//            Assert.AreEqual(expected, actual);
//        }
//    }
//}