using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeMachine.Ingredients
{
    internal class Milk(double weight) : Ingredient(weight)
    {
        protected override string Name => "Молоко";
        protected override string Unit => "мл";
    }
}
