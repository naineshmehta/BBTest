using System.Collections.Generic;

namespace BBTest
{
    public class Basket : IBasket
    {
        public Basket()
        {
            Items = new List<string>();
        }

        public void Add(string items)
        {
            foreach (var item in items.Split(','))
            {
                Items.Add(item);
            }
        }

        public List<string> Items { get; set; }
    }
}