using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Specialized;

namespace Rhyous.Collections.Tests.Extensions
{    
    public partial class NameValueCollectionExtensionsTests
    {
        [TestMethod]
        public void NameValueCollectionExtensions_Null_Test()
        {
            // Arrange
            NameValueCollection collection = null;
            var expected = "";

            // Act
            var actual = collection.ToUrlQueryString();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NameValueCollectionExtensions_Empty_Test()
        {
            // Arrange
            var collection = new NameValueCollection();
            var expected = "";

            // Act
            var actual = collection.ToUrlQueryString();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NameValueCollectionExtensions_ToQueryString_Test()
        {
            // Arrange
            var collection = new NameValueCollection();
            collection.Add("$filter", "Id eq 1");
            var expected = "?%24filter=Id%20eq%201";

            // Act
            var actual = collection.ToUrlQueryString();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NameValueCollectionExtensions_ToQueryString_EmptyStart_Test()
        {
            // Arrange
            var collection = new NameValueCollection();
            collection.Add("$filter", "Id eq 1");
            var expected = "%24filter=Id%20eq%201";

            // Act
            var actual = collection.ToUrlQueryString(string.Empty);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NameValueCollectionExtensions_ToQueryString_DefaultBoolKey_Test()
        {
            // Arrange
            var collection = new NameValueCollection();
            collection.Add("IsAwesome",null);
            var expected = "?IsAwesome";

            // Act
            var actual = collection.ToUrlQueryString();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NameValueCollectionExtensions_ToQueryString_10KeysWith1ValueEach_Test()
        {
            // Arrange
            var collection = new NameValueCollection
            {
                { "Prop0", "Val0" },
                { "Prop1", "Val1" },
                { "Prop2", "Val2" },
                { "Prop3", "Val3" },
                { "Prop4", "Val4" },
                { "Prop5", "Val5" },
                { "Prop6", "Val6" },
                { "Prop7", "Val7" },
                { "Prop8", "Val8" },
                { "Prop9", "Val9" }
            };
            var expected = "?Prop0=Val0&Prop1=Val1&Prop2=Val2&Prop3=Val3&Prop4=Val4&Prop5=Val5&Prop6=Val6&Prop7=Val7&Prop8=Val8&Prop9=Val9";

            // Act
            var actual = collection.ToUrlQueryString();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NameValueCollectionExtensions_ToQueryString_1KeyWith10values_Test()
        {
            // Arrange
            var collection = new NameValueCollection
            {
                { "Prop", "Val0" },
                { "Prop", "Val1" },
                { "Prop", "Val2" },
                { "Prop", "Val3" },
                { "Prop", "Val4" },
                { "Prop", "Val5" },
                { "Prop", "Val6" },
                { "Prop", "Val7" },
                { "Prop", "Val8" },
                { "Prop", "Val9" }
            };
            var expected = "?Prop=Val0&Prop=Val1&Prop=Val2&Prop=Val3&Prop=Val4&Prop=Val5&Prop=Val6&Prop=Val7&Prop=Val8&Prop=Val9";

            // Act
            var actual = collection.ToUrlQueryString();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NameValueCollectionExtensions_ToQueryString_EmptyKeyIgnored_Test()
        {
            // Arrange
            var collection = new NameValueCollection
            {
                { "Prop0", "Val0" },
                { "Prop1", "Val1" },
                { "Prop2", "Val2" },
                { "", "Val3" },
                { "Prop4", "Val4" },
                { "Prop5", "Val5" },
                { "Prop6", "Val6" },
                { "Prop7", "Val7" },
                { "Prop8", "Val8" },
                { "Prop9", "Val9" }
            };
            var expected = "?Prop0=Val0&Prop1=Val1&Prop2=Val2&Prop4=Val4&Prop5=Val5&Prop6=Val6&Prop7=Val7&Prop8=Val8&Prop9=Val9";

            // Act
            var actual = collection.ToUrlQueryString();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NameValueCollectionExtensions_ToQueryString_ManyKeysWithAssumedValues_Test()
        {
            // Arrange
            var collection = new NameValueCollection
            {
                { "Prop0", "Val0" },
                { "Prop1", "Val1" },
                { "Prop2", "Val2" },
                { "Prop3", null },
                { "Prop4", "Val4" },
                { "Prop5", null },
                { "Prop6", "Val6" },
                { "Prop7", null },
                { "Prop8", null },
                { "Prop9", "Val9" }
            };
            var expected = "?Prop0=Val0&Prop1=Val1&Prop2=Val2&Prop3&Prop4=Val4&Prop5&Prop6=Val6&Prop7&Prop8&Prop9=Val9";

            // Act
            var actual = collection.ToUrlQueryString();

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
