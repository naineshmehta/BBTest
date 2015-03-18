using System.Linq;

namespace BBTest
{
    public class Checkout : ICheckout
    {
        public decimal CalculateTotal(IBasket basket)
        {
            var result = new decimal(0.00);
            foreach (var item in basket.Items)
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
            result = ApplySpecialOffers(basket, result);

            return result;
        }

        private static decimal ApplySpecialOffers(IBasket basket, decimal result)
        {
            //we need to see if there are any offers to apply
            var butterCount = basket.Items.Count(s => s == "butter");
            if (butterCount > 0)
            {
                var butterOffers = butterCount % 2;

                if (butterOffers == 0)
                {
                    //for each 2 butters, 1 bread is 50% off
                    result = result - (decimal)0.5;
                }
            }

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