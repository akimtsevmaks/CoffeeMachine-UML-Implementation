using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeMachine.Ingredients
{
    internal class Syrup(double weight) : Ingredient(weight)
    {
        protected override string Name => "Сироп";
        protected override string Unit => "мл";
    }
}
