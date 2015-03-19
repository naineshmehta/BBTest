using NUnit.Framework;

namespace BBTest
{
    class BasketTest
    {
        private IBasket _basket;
        private ICheckout _checkout;

        [SetUp]
        public void Setup()
        {
            _basket = new Basket();
            _checkout = new Checkout(_basket);
        }

        [Test]
        public void CanCreateBasket()
        {
            Assert.IsNotNull(_basket);
        }

        [TestCase("bread")]
        public void CanAddItemToBasket(string item)
        {
            _basket.Add(item);
            Assert.AreEqual(1,_basket.Items.Count);
        }

        [TestCase("bread,milk", 2)]
        [TestCase("bread,milk,butter", 3)]
        public void CanAddMultipleItemsToBasket(string items, int expected)
        {
            _basket.Add(items);
            Assert.AreEqual(expected, _basket.Items.Count);
        }

        [TestCase("bread", 1.00)]
        [TestCase("milk", 1.15)]
        [TestCase("butter", 0.80)]
        public void AddItemToBasketAndCalculateCorrectItemTotal(string item, double expected)
        {
            _basket.Add(item);
            Assert.AreEqual(expected, _checkout.CalculateTotal());
        }

        [TestCase("unknown", 0.00)]
        public void AddUnknownItemReturnsZero(string item, double expected)
        {
            _basket.Items.Add(item);
            Assert.AreEqual(expected, _checkout.CalculateTotal());
        }

        [TestCase("bread,milk,butter", 2.95)]
        public void AddBreakMilkButterShouldReturnCorrectTotal(string items, double expected)
        {
            _basket.Add(items);
            Assert.AreEqual(expected, _checkout.CalculateTotal());
        }

        [TestCase("butter,butter,bread", 2.10)]
        [TestCase("milk,milk,milk,milk", 3.45)]
        [TestCase("butter,butter,bread,milk,milk,milk,milk,milk,milk,milk,milk", 9.00)]      
        public void BasketWithMultipleItemsandSpecialOffers(string items, double expected)
        {
            _basket.Add(items);
            Assert.AreEqual(expected, _checkout.CalculateTotal());
        }
    }
}
