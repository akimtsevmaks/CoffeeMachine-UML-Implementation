using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeMachine.Ingredients
{
    internal class CoffeeBean(double weight) : Ingredient(weight)
    {
        protected override string Name => "Кофейные зерна";
        protected override string Unit => "г";
    }
}
