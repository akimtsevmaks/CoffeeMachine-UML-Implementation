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

        protected Action()
        {
            _elements = [];
        }
        protected Action(params IElement[] elements)
        {
            if (elements == null || elements.Length == 0) throw new ArgumentException("empty action");            // eh
            _elements = [.. elements];
        }

        public static Action CreateRoot() => new RootAction();

        private sealed class RootAction : Action
        {
            protected override string ActionName => "Рецепт";
        }

        public void AddElement(IElement element, int index = -1)
        {
            if (element == null) throw new ArgumentException("empty action");              // eh

            if (index == -1 || index >= _elements.Count)
                _elements.Add(element);
            else
                _elements.Insert(index, element);
        }

        public void RemoveElementAt(int index)
        {
            if (index <  0 || index >= _elements.Count)
                throw new ArgumentOutOfRangeException(nameof(index), "idex out");            // eh

            _elements.RemoveAt(index);
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
