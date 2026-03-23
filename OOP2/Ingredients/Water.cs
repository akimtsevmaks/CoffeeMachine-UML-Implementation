using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeMachine.Ingredients
{
    internal class Water(double weight) : Ingredient(weight)
    {
        protected override string Name => "Вода";
        protected override string Unit => "мл";
    }
}
