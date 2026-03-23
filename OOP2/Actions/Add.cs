using CoffeeMachine.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeMachine.Actions
{
    internal class Add(params IElement[] elements) : Action(elements)
    {
        protected override string ActionName => "Добавить";
    }
}
