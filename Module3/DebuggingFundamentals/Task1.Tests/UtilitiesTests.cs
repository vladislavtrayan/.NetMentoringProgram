using NUnit.Framework;
using System;
using System.Linq;
using Task1;

namespace Task1.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Sort_Numbers_ReturnsAscendingSortedNumbers()
        {
            int[] numbers = new[] { 4, 2, 1, 3, -5 };
            numbers.Sort();

            CollectionAssert.AreEqual(new[] { -5, 1, 2, 3, 4 }, numbers);
        }

        [Test]
        public void Sort_Null_ThrowsArgumentNullException()
        {
            Assert.That(() => Utilities.Sort(null), Throws.InstanceOf<ArgumentNullException>());
        }

        [Test]
        public void Sort_EmptyArray_ReturnsEmptyArray()
        {
            int[] numbers = new int[0];
            
            numbers.Sort();

            CollectionAssert.AreEqual(new int[0], numbers);
        }
        
        [Test]
        public void Sort_ObjectsArray_ReturnsSortedArray()
        {
            var products = new Product[]
            {
                new Product("name", 10.0d),
                new Product("na", 20.0d),
                new Product("namee", 30.0d),
                new Product("nam", 30.0d),
            };
            
            products.Sort(p => p.Name, (x, y) => x.Length > y.Length);
            
            var expectedResult = new Product[]
            {
                new Product("na", 20.0d),
                new Product("nam", 30.0d),
                new Product("name", 10.0d),
                new Product("namee", 30.0d),
            };

            CollectionAssert.AreEqual(expectedResult.Select(p => p.Name),
                products.Select(p => p.Name));
        }

        [Test]
        public void IndexOf_Products_ReturnsTwo()
        {
            var products = new Product[]
            {
                new Product("Product 1", 10.0d),
                new Product("Product 2", 20.0d),
                new Product("Product 3", 30.0d),
            };
            var productToFind = new Product("Product 3", 30.0d);

            int index = Utilities.IndexOf(products, product => product.Equals(productToFind));

            Assert.That(index, Is.EqualTo(2));
        }

        [Test]
        public void IndexOf_NoMatch_ReturnsMinusOne()
        {
            var products = new Product[]
            {
                new Product("Product 1", 10.0d),
                new Product("Product 2", 20.0d),
                new Product("Product 3", 30.0d),
            };
            var productToFind = new Product("Product 4", 30.0d);

            int index = Utilities.IndexOf<Product>(products, product => product.Equals(productToFind));

            Assert.That(index, Is.EqualTo(-1));
        }

        [Test]
        public void IndexOf_EqualsWithNull_ReturnsMinusOne()
        {
            var products = new Product[]
            {
                new Product("Product 1", 10.0d),
                new Product("Product 2", 20.0d),
                new Product("Product 3", 30.0d),
            };
            Product productToFind = null;

            int index = Utilities.IndexOf<Product>(products, product => product.Equals(productToFind));

            Assert.That(index, Is.EqualTo(-1));
        }

        [Test]
        public void IndexOf_SearchForNonProductTypeObject_ReturnsMinusOne()
        {
            var products = new Product[]
            {
                new Product("Product 1", 10.0d),
                new Product("Product 2", 20.0d),
                new Product("Product 3", 30.0d),
            };
            var productToFind = 42;

            int index = Utilities.IndexOf<Product>(products, product => product.Equals(productToFind));

            Assert.That(index, Is.EqualTo(-1));
        }

        [Test]
        public void IndexOf_NullProducts_ThrowsArgumentNullException()
        {
            Assert.That(() =>
            {
                var productToFind = new Product("Product 3", 30.0d);
                int index = Utilities.IndexOf<Product>(null, product => product.Equals(productToFind));
            }, Throws.InstanceOf<ArgumentNullException>());
        }

        [Test]
        public void IndexOf_NullPredicate_ThrowsArgumentNullException()
        {
            Assert.That(() =>
            {
                var products = new Product[] { new Product("Product 1", 10.0d) };
                int index = Utilities.IndexOf(products, null);
            }, Throws.InstanceOf<ArgumentNullException>());
        }

        [Test]
        public void IndexOf_EmptyArray_ReturnsMinusOne()
        {
            var products = new Product[0];
            var productToFind = new Product("Product 3", 30.0d);

            int index = Utilities.IndexOf(products, product => product.Equals(productToFind));

            Assert.That(index, Is.EqualTo(-1));
        }
    }
}