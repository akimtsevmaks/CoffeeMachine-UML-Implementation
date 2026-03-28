using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeMachine.Drinks
{
    internal class DrinkStorage
    {
        private readonly List<Drink> _drinks = [];
        public IReadOnlyList<Drink> Drinks => _drinks;

        public void Add(Drink drink) => _drinks.Add(drink);

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= _drinks.Count)
                throw new ArgumentOutOfRangeException(nameof(index), "index out");          // eh
            _drinks.RemoveAt(index);
        }
    }
}
