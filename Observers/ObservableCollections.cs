using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Observers.Annotations;

namespace Observers
{
    public class Market // observable aka publisher
    {
        //private List<float> prices = new List<float>();

        // change to Binding list allows you to remove event handler. 
        public BindingList<float> Prices = new BindingList<float>();

        public void AddPrice(float price)
        {
            Prices.Add(price);
        }

    }

    public class ObservableCollections // observer aka subscriber
    {
        // change to Main to run.
        public static void none(string[] args)
        {
            var market = new Market();

            // regular event
            //market.PriceAdded += (sender, f) =>
            //{
            //    Console.WriteLine($"We got a price of {f}");
            //};

            market.Prices.ListChanged += (sender, eventArgs) =>
            {
                if (eventArgs.ListChangedType == ListChangedType.ItemAdded)
                {
                    float price = ((BindingList<float>) sender)[eventArgs.NewIndex];
                    Console.WriteLine($"Binding list got a price of {price}");
                }

            };
            market.AddPrice(123);
        }
    }
}
