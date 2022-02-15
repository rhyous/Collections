using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhyous.UnitTesting;
using System;

namespace Rhyous.Collections.Tests
{
    [TestClass]
    public class ArrayExtensionsTests
    {
        #region ToJson
        [TestMethod]
        public void ArrayExtensions_ToJson_T_NotPrimitive_Test()
        {
            // Arrange
            var array = new object[0];

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() =>
            {
                array.ToJson();
            });
        }

        [TestMethod]
        [ArrayNullOrEmpty(typeof(int))]
        public void ArrayExtensions_ToJson_T_Int_Primitive_NullOrEmpty_Test(int[] array)
        {
            // Arrange
            // Act
            var actual = array.ToJson();

            // Assert
            Assert.AreEqual("[]", actual);
        }

        [TestMethod]
        public void ArrayExtensions_ToJson_T_Int_Primitive_Populated_Test()
        {
            // Arrange
            var array = new int[] { 1, 2, 3, 4, 5 }; 
            // Act
            var actual = array.ToJson();

            // Assert
            Assert.AreEqual("[1,2,3,4,5]", actual);
        }

        [TestMethod]
        [ArrayNullOrEmpty(typeof(string))]
        public void ArrayExtensions_ToJson_String_NullOrEmpty_Test(string[] array)
        {
            // Arrange
            // Act
            var actual = array.ToJson();

            // Assert
            Assert.AreEqual("[]", actual);
        }

        [TestMethod]
        public void ArrayExtensions_ToJson_String_Primitive_Populated_Test()
        {
            // Arrange
            var array = new string[] { "1", "2", "3", "4", "5", "ABC" };
            // Act
            var actual = array.ToJson();

            // Assert
            Assert.AreEqual("['1','2','3','4','5','ABC']", actual);
        }

        [TestMethod]
        [ArrayNullOrEmpty(typeof(Guid))]
        public void ArrayExtensions_ToJson_Guid_NullOrEmpty_Test(Guid[] array)
        {
            // Arrange
            // Act
            var actual = array.ToJson();

            // Assert
            Assert.AreEqual("[]", actual);
        }

        [TestMethod]
        public void ArrayExtensions_ToJson_Guid_Primitive_Populated_Test()
        {
            // Arrange
            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();
            var array = new Guid[] {guid1, guid2 };
            // Act
            var actual = array.ToJson();

            // Assert
            Assert.AreEqual($"['{guid1}','{guid2}']", actual);
        }
        #endregion

        #region Fill 1
        [TestMethod]
        public void ArrayExtensions_Fill_1_InvalidInput_Test()
        {
            // Arrange
            var value = 9;
            int[,] array = null;

            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                array.Fill(value);
            });
        }

        [TestMethod]
        public void ArrayExtensions_Fill_1_ValidInput_Test()
        {
            // Arrange
            var value = 9;
            var array = new int[3];

            // Act
            array.Fill(value);

            // Assert
            for (int i = 0; i < array.Length; i++)
            {
                Assert.AreEqual(value, array[i]);
            }
        }
        #endregion

        #region Fill 2
        [TestMethod]
        public void ArrayExtensions_Fill_2_InvalidInput_Test()
        {
            // Arrange
            var value = 9;
            int[,] array = null;

            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                array.Fill(value);
            });
        }

        [TestMethod]
        public void ArrayExtensions_Fill_2_ValidInput_Test()
        {
            // Arrange
            var value = 9;
            var array = new int[3, 3];

            // Act
            array.Fill(value);

            // Assert
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.GetLength(0); j++)
                    for (int k = 0; k < array.GetLength(1); k++)
                        Assert.AreEqual(value, array[j, k]);
            }
        }
        #endregion

        #region Fill 3
        [TestMethod]
        public void ArrayExtensions_Fill_3_InvalidInput_Test()
        {
            // Arrange
            var value = 9;
            int[,,] array = null;

            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                array.Fill(value);
            });
        }

        [TestMethod]
        public void ArrayExtensions_Fill_3_ValidInput_Test()
        {
            // Arrange
            var value = 9;
            var array = new int[3, 3, 3];

            // Act
            array.Fill(value);

            // Assert
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.GetLength(0); j++)
                    for (int k = 0; k < array.GetLength(1); k++)
                        for (int l = 0; l < array.GetLength(2); l++)
                            Assert.AreEqual(value, array[j, k, l]);
            }
        }
        #endregion

        #region Fill 4
        [TestMethod]
        public void ArrayExtensions_Fill_4_InvalidInput_Test()
        {
            // Arrange
            var value = 9;
            int[,,,] array = null;

            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                array.Fill(value);
            });
        }

        [TestMethod]
        public void ArrayExtensions_Fill_4_ValidInput_Test()
        {
            // Arrange
            var value = 9;
            var array = new int[3, 3, 3, 3];

            // Act
            array.Fill(value);

            // Assert
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.GetLength(0); j++)
                    for (int k = 0; k < array.GetLength(1); k++)
                        for (int l = 0; l < array.GetLength(2); l++)
                            for (int m = 0; m < array.GetLength(3); m++)
                                Assert.AreEqual(value, array[j, k, l, m]);
            }
        }
        #endregion

        #region Fill 5
        [TestMethod]
        public void ArrayExtensions_Fill_5_InvalidInput_Test()
        {
            // Arrange
            var value = 9;
            int[,,,,] array = null;

            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                array.Fill(value);
            });
        }

        [TestMethod]
        public void ArrayExtensions_Fill_5_ValidInput_Test()
        {
            // Arrange
            var value = 9;
            var array = new int[3, 4, 3, 3, 4];

            // Act
            array.Fill(value);

            // Assert
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.GetLength(0); j++)
                    for (int k = 0; k < array.GetLength(1); k++)
                        for (int l = 0; l < array.GetLength(2); l++)
                            for (int m = 0; m < array.GetLength(3); m++)
                                for (int n = 0; n < array.GetLength(4); n++)
                                    Assert.AreEqual(value, array[j, k, l, m, n]);
            }
        }
        #endregion
    }
}
