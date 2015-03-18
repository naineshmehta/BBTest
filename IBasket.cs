using System.Collections.Generic;

namespace BBTest
{
    public interface IBasket
    {
        void Add(string items);
        List<string> Items { get; set; }
    }
}