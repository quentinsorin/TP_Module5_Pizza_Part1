using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP_Module5_Pizza_Part1.Utils
{
    public class FakeDbPizza
    {
        private static FakeDbPizza _instance;
        static readonly object instanceLock = new object();

        private FakeDbPizza()
        {
            pizzas = this.GetPizzas();
        }

        public static FakeDbPizza Instance
        {
            get
            {
                if (_instance == null) 
                {
                    lock (instanceLock)
                    {
                        if (_instance == null) 
                            _instance = new FakeDbPizza();
                    }
                }
                return _instance;
            }
        }

        private List<Pizza> pizzas;

        public List<Pizza> Pizzas
        {
            get { return pizzas; }
        }
    }
}