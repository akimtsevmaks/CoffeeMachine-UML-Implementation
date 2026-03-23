using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeMachine.Ingredients
{
    internal class Ice(double weight) : Ingredient(weight)
    {
        protected override string Name => "Лед";
        protected override string Unit => "г";
    }
}
