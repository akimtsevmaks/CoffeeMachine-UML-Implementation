using CoffeeMachine.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeMachine.Actions
{
    internal class Froth(params IElement[] elements) : Action(elements)
    {
        protected override string ActionName => "Взбить";
    }
}
