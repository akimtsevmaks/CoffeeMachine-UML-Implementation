using CoffeeMachine.Actions;
using CoffeeMachine.Drinks;
using CoffeeMachine.Interface;
using CoffeeMachine.Ingredients;
using System;
using System.Collections.Generic;
using System.Threading.Channels;

using Action = CoffeeMachine.Actions.Action;
using System.Security.AccessControl;


namespace CoffeeMachine
{
    internal class Program
    {
        private static readonly DrinkStorage _storage = new();

        static void Main()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("╔══════════════════════════════╗");
                Console.WriteLine("║        Основное меню         ║");
                Console.WriteLine("╠══════════════════════════════╣");
                Console.WriteLine("║  1 Список напитков           ║");
                Console.WriteLine("║  2 Создать напиток           ║");
                Console.WriteLine("║  3 Редактировать напиток     ║");
                Console.WriteLine("║  4 Удалить напиток           ║");
                Console.WriteLine("║  0 Выход                     ║");
                Console.WriteLine("╚══════════════════════════════╝");

                Console.WriteLine("\n Введите действие: ");

                switch (Console.ReadLine()?.Trim() ?? "")
                {
                    case "1": ShowDrinks(); break;
                    case "2": CreateDrink(); break;
                    case "3": break;
                    case "4": DeleteDrink(); break;
                    case "0": break;
                    default: break;
                }
            }
        }
        
        static void ShowDrinks()
        {
            Console.Clear();
            var drinks = _storage.Drinks;

            if (drinks.Count == 0)
            {
                Console.WriteLine("Список пуст");
            }
            else
            {
                for (int i = 0; i < drinks.Count; i++)
                {
                    Console.WriteLine($"\n\n[{i + 1}] {drinks[i].GetDescription()}");
                }
            }
            Console.WriteLine("\n\nНажмите любую клавишу...");
            Console.ReadKey();
        }

        static void CreateDrink()
        {
            Console.Clear();

            Console.WriteLine("Введите название напитка: ");
            string name = Console.ReadLine()?.Trim() ?? "";

            if (string.IsNullOrEmpty(name))
            {
                // eh
                return;
            }

            var drink = new Drink(name);
            FillDrink(drink);

            if (drink.Steps.Count == 0)
            {
                Console.WriteLine($"Напиток не создан! Отсутствуют шаги в рецепте");
            }
            else
            {
                _storage.Add(drink);
                Console.WriteLine($"Напиток \"{name}\" создан!");
            }
            Console.ReadKey();
        }

        static void FillDrink(Drink drink)
        {
            while (true)
            {
                Console.WriteLine($"\n Добавьте шаг в рецепт напитка \"{drink.Name}\"");
                Console.WriteLine("  1 Действие");
                Console.WriteLine("  0 Завершить");

                Console.WriteLine("\n Введите действие: ");

                switch (Console.ReadLine()?.Trim() ?? "")
                {
                    case "1":
                        {
                            var action = GetAction();
                            if (action != null) drink.AddStep(action);
                            break;
                        }
                    case "0": return;
                    default: break; // eh
                }
            }
        }

        static Action? GetAction()
        {
            Console.WriteLine("\n Возможные типы действия:");
            Console.WriteLine("  1 Добавить");
            Console.WriteLine("  2 Вскипятить");
            Console.WriteLine("  3 Взбить");
            Console.WriteLine("  4 Перемолоть");
            Console.WriteLine("  5 Пролить");
            Console.WriteLine("  6 Перемешать");

            Console.WriteLine("\n Введите действие: ");

            string choice = Console.ReadLine()?.Trim() ?? "";

            List<IElement> collected = [];

            Console.WriteLine("\n\n Заполните содержимое действия:");


            bool filling = true;
            while (filling)
            {
                Console.WriteLine($"\n Добавить в текущий шаг \"{choice}\"");

                Console.WriteLine("  1 Ингредиент");
                Console.WriteLine("  2 Действие");
                Console.WriteLine("  0 Завершить");

                Console.WriteLine("\n Введите действие: ");

                switch (Console.ReadLine()?.Trim() ?? "")
                {
                    case "1":
                        {
                            var ingredient = GetIngredient();
                            if (ingredient != null) collected.Add(ingredient);
                            break;
                        }
                    case "2":
                        {
                            var nestedAction = GetAction();
                            if (nestedAction != null) collected.Add(nestedAction);
                            break;
                        }
                    case "0": filling = false; break;
                    default: break;                                  //eh
                }
            }
            if (collected.Count == 0)
            {
                //eh
                return null;
            }

            var elems = collected.ToArray();
            return choice switch
            {
                "1" => new Add(elems),
                "2" => new Boil(elems),
                "3" => new Froth(elems),
                "4" => new Grind(elems),
                "5" => new Spill(elems),
                "6" => new Mix(elems),
                _ => null,
            };
        }

        static Ingredient? GetIngredient()
        {
            Console.WriteLine("\n Возможные ингредиенты:");
            Console.WriteLine("  1 Кофейные зерна (г)");
            Console.WriteLine("  2 Лед (г)");
            Console.WriteLine("  3 Молоко (мл)");
            Console.WriteLine("  4 Сироп (мл)");
            Console.WriteLine("  5 Вода (мл)");

            Console.WriteLine("\n Введите ингредиент: ");
            string choice = Console.ReadLine()?.Trim() ?? "";

            Console.WriteLine("Введите кол-во: ");
            
            if (!double.TryParse(Console.ReadLine()?.Trim() ?? "", out double weight) || weight <= 0)
            {
                // eh
                return null;
            }

            return choice switch
            {
                "1" => new CoffeeBean(weight),
                "2" => new Ice(weight),
                "3" => new Milk(weight),
                "4" => new Syrup(weight),
                "5" => new Water(weight),
                _ => null,
            };
        }

        static void DeleteDrink()
        {
            Console.Clear();
            var drinks = _storage.Drinks;

            if (drinks.Count == 0)
            {
                // eh
                return;
            }

            PrintDrinkNames(drinks);
            Console.WriteLine("Введите номер удаляемого напитка: ");
            if (!int.TryParse(Console.ReadLine()?.Trim() ?? "", out int idx) | idx < 1 || idx > drinks.Count)
            {
                // eh
                return;
            }

            string name = drinks[idx].Name;

            _storage.RemoveAt(idx);
            Console.WriteLine($"\n Напиток {name} удален!");
            Console.ReadKey();
        }

        static void PrintDrinkNames(IReadOnlyList<Drink> drinks)
        {
            for (int i = 0; i < drinks.Count; i++)
                Console.WriteLine($" {i + 1}) {drinks[i].Name}");
        }
    }
}