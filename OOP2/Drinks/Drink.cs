using CoffeeMachine.Interface;
using CoffeeMachine;
using System;
using System.Collections.Generic;
using System.Text;

using Action = CoffeeMachine.Actions.Action;


namespace CoffeeMachine.Drinks
{
    internal class Drink(string name)
    {
        public string Name { get; private set; } = name;

        private readonly IElement _root = Action.CreateRoot();

        private Action Root => (Action)_root;

        public void SetName(string name) => Name = name;
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
                sb.AppendLine($"    {i + 1}) {step.AsSpan(4)}");
            }
            return sb.ToString().TrimEnd();
        }
    }
}
