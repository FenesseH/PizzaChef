using System;
using System.Collections.Generic;

class ToppingsManager
{
    private HashSet<string> toppings;

    public ToppingsManager()
    {
        toppings = new HashSet<string>();
    }

    public List<string> ListToppings()
    {
        return new List<string>(toppings);
    }

    public bool AddTopping(string topping)
    {
        if (!toppings.Contains(topping))
        {
            toppings.Add(topping);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool DeleteTopping(string topping)
    {
        if (toppings.Contains(topping))
        {
            toppings.Remove(topping);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool UpdateTopping(string oldTopping, string newTopping)
    {
        if (toppings.Contains(oldTopping) && !toppings.Contains(newTopping))
        {
            toppings.Remove(oldTopping);
            toppings.Add(newTopping);
            return true;
        }
        else
        {
            return false;
        }
    }
}

class PizzaManager
{
    private Dictionary<string, List<string>> pizzas;
    private ToppingsManager toppingsManager;

    public PizzaManager(ToppingsManager toppingsManager)
    {
        pizzas = new Dictionary<string, List<string>>();
        this.toppingsManager = toppingsManager;
    }

    public Dictionary<string, List<string>> ListPizzas()
    {
        return new Dictionary<string, List<string>>(pizzas);
    }

    public bool CreatePizza(string pizzaName, List<string> toppings)
    {
        if (!pizzas.ContainsKey(pizzaName) && toppings.TrueForAll(t => toppingsManager.ListToppings().Contains(t)))
        {
            pizzas.Add(pizzaName, toppings);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool DeletePizza(string pizzaName)
    {
        if (pizzas.ContainsKey(pizzaName))
        {
            pizzas.Remove(pizzaName);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool UpdatePizza(string pizzaName, List<string> newToppings)
    {
        if (pizzas.ContainsKey(pizzaName) && newToppings.TrueForAll(t => toppingsManager.ListToppings().Contains(t)))
        {
            pizzas[pizzaName] = newToppings;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool UpdatePizzaToppings(string pizzaName, List<string> addedToppings, List<string> removedToppings)
    {
        if (pizzas.ContainsKey(pizzaName) && addedToppings.TrueForAll(t => toppingsManager.ListToppings().Contains(t)) && removedToppings.TrueForAll(t => pizzas[pizzaName].Contains(t)))
        {
            var currentToppings = new HashSet<string>(pizzas[pizzaName]);
            currentToppings.UnionWith(addedToppings);
            currentToppings.ExceptWith(removedToppings);
            pizzas[pizzaName] = new List<string>(currentToppings);
            return true;
        }
        else
        {
            return false;
        }
    }
}