using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeMachine.Interface
{
    internal interface IElement
    {
        string Description { get; }
        string GetActionStep(int depth = 0);
    }
}
