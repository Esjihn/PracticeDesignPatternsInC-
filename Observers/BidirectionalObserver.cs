using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Observers.Annotations;

namespace Observers
{
    public class Product : INotifyPropertyChanged
    {
        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                // guard eliminate recursion
                if (value == _name) return;
                
                _name = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return $"Product: {Name}";
        }
    }

    public class Window2 : INotifyPropertyChanged
    {
        private string _productName;

        // Product and Product object => should be the same.
        public string ProductName
        {
            get => _productName;
            set
            {
                // guard eliminate recursion
                if (value == _productName) return;
                _productName = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return $"Window: {ProductName}";
        }
    }

    public class BidirectionalObserver
    {
        // change to Main to run.
        public static void Main(string[] args)
        {
            // bidirectional binding
            var product = new Product{Name = "Book"};
            var window = new Window2{ProductName = "Book"};


            product.PropertyChanged += (sender, eventArgs) =>
            {
                if (eventArgs.PropertyName == "Name")
                {
                    Console.WriteLine("Name changes in product");
                    window.ProductName = product.Name;
                }
            };

            window.PropertyChanged += (sender, eventArgs) =>
            {
                if (eventArgs.PropertyName == "ProductName")
                {
                    Console.WriteLine("Name changes in Window");
                    product.Name = window.ProductName;
                }
            };

            product.Name = "Smart Book";
            Console.WriteLine(product);
            Console.WriteLine(window);
        }
    }
}
