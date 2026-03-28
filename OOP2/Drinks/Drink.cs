using CoffeeMachine.Interface;
using System;
using System.Collections.Generic;
using System.Text;

using Action = CoffeeMachine.Actions.Action;

namespace CoffeeMachine.Drinks
{
    internal class Drink
    {
        public string Name { get; }

        private readonly IElement _root;

        private Action Root => (Action)_root;

        public Drink(string name)
        {
            Name = name;
            _root = Action.CreateRoot();
        }

        public IReadOnlyList<IElement> Steps => Root.Elements;

        public void AddStep(IElement step, int index = -1) =>
            Root.AddElement(step, index);

        public void RemoveStep(int index) =>
            Root.RemoveElementAt(index);

        public string GetDescription()
        {
            var sb = new StringBuilder();
            sb.AppendLine($" Рецепт \"{Name}\":");

            var children = Root.Elements;
            for (int i = 0; i < children.Count; i++)
            {
                string step = children[i].GetActionStep(1);
                sb.Append($"\t{i + 1}) ");
                sb.Append(step.Substring(4));
            }
            return sb.ToString().TrimEnd();
        }
    }
}
