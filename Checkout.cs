using System.Linq;

namespace BBTest
{
    public class Checkout : ICheckout
    {
        private readonly IBasket _basket;

        public Checkout(IBasket basket)
        {
            _basket = basket;
        }

        public decimal CalculateTotal()
        {
            var result = new decimal(0.00);
            foreach (var item in _basket.Items)
            {
                switch (item)
                {
                    case "bread":
                        result += (decimal)1.00;
                        break;
                    case "milk":
                        result += (decimal)1.15;
                        break;
                    case "butter":
                        result += (decimal)0.80;
                        break;
                }
            }

            //this will return the total without applying the special offers
            result = ApplySpecialOffers(_basket, result);

            return result;
        }

        private static decimal ApplySpecialOffers(IBasket basket, decimal result)
        {
            //for each 2 butters, 1 bread is 50% off
            var butterCount = basket.Items.Count(s => s == "butter");
            if (butterCount > 0)
            {
                var butterOffers = butterCount / 2;
                var breadCount = basket.Items.Count(s => s == "bread");

                if (breadCount > 0)
                {
                    if (butterOffers > breadCount)
                    {
                        result = result - (breadCount * (decimal)0.5);
                    }
                    else
                    {
                        result = result - (butterOffers * (decimal)0.5);                        
                    }
                }
            }

            //buy 3 milk and get the 4th free
            var milkCount = basket.Items.Count(s => s == "milk");
            if (milkCount > 0 && milkCount % 4 == 0)
            {
                var numberOffers = milkCount / 4;
                result = result - (numberOffers * (decimal)1.15);
            }

            return result;
        }
    }
}