using CoffeeMachine.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeMachine.Ingredients
{
    abstract internal class Ingredient(double weight) : IElement
    {
        public string Description => $"{Name} ({Weight} {Unit})";

        protected double Weight { get; } = weight;
        protected abstract string Name { get; }
        protected abstract string Unit { get; }

        public string GetActionStep(int depth = 0) => $"{new(' ', depth * 4)}- {Description}";
    }
}
