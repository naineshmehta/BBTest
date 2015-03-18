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
            _checkout = new Checkout();
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
            Assert.AreEqual(_basket.Items.Count, 1);
        }

        [TestCase("bread,milk", 2)]
        [TestCase("bread,milk,butter", 3)]
        public void CanAddMultipleItemsToBasket(string items, int expected)
        {
            _basket.Add(items);
            Assert.AreEqual(_basket.Items.Count, expected);
        }

        [TestCase("bread", 1.00)]
        [TestCase("milk", 1.15)]
        [TestCase("butter", 0.80)]
        public void AddItemToBasketAndCalculateCorrectItemTotal(string item, double expected)
        {
            _basket.Add(item);
            Assert.AreEqual(_checkout.CalculateTotal(_basket), expected);
        }

        [TestCase("bread,milk,butter", 2.95)]
        //[TestCase("bread,bread,butter,butter", 3.10)]
        public void AddBreakMilkButterShouldReturnCorrectTotal(string items, double expected)
        {
            _basket.Add(items);
            Assert.AreEqual(_checkout.CalculateTotal(_basket), expected);
        }

        [TestCase("butter,butter,bread", 2.10)]
        [TestCase("butter,butter,bread,bread", 3.10)]
        [TestCase("milk,milk,milk,milk", 3.45)]
        [TestCase("butter,butter,bread,milk,milk,milk,milk,milk,milk,milk,milk", 9.00)]
        public void BasketWithMultipleItemsandSpecialOffers(string items, double expected)
        {
            _basket.Add(items);
            Assert.AreEqual(_checkout.CalculateTotal(_basket), expected);
        }
    }
}
