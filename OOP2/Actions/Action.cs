using CoffeeMachine.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeMachine.Actions
{
    abstract internal class Action : IElement
    {
        private readonly List<IElement> _elements;
        public IReadOnlyList<IElement> Elements => _elements;

        protected abstract string ActionName { get; }
        public string Description => ActionName;

        protected Action(params IElement[] elements)
        {
            if (elements == null || elements.Length == 0) throw new ArgumentException("empty action");
            _elements = [.. elements];
        }

        public virtual string GetActionStep(int depth = 0)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{new(' ', depth * 4)}[{ActionName}]:");

            foreach (var el in _elements)
            {
                sb.AppendLine(el.GetActionStep(depth + 1));
            }

            return sb.ToString().TrimEnd();
        }
    }
}
