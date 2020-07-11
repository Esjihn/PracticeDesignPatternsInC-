using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
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

    // Custom Bidirectional Binding class
    public sealed class BidirectionalBinding : IDisposable
    {
        private bool _disposed;

        // first second
        // firstProperty, secondProperty

        public BidirectionalBinding(INotifyPropertyChanged first,
            Expression<Func<object>> firstProperty, // () => x.Foo
            INotifyPropertyChanged second, 
            Expression<Func<object>> secondProperty)
        {
            // xxxProperty is MemberExpression x.Foo
            // Member ↑↑↑ PropertyInfo (involves expression trees via MemberExpression
            if (firstProperty.Body is MemberExpression firstExpr
            && secondProperty.Body is MemberExpression secondExpr)
            {
                if (firstExpr.Member is PropertyInfo firstProp
                    && secondExpr.Member is PropertyInfo secondProp)
                {
                    first.PropertyChanged += (sender, args) =>
                    {
                        if (!_disposed)
                        {
                            secondProp.SetValue(second,
                                firstProp.GetValue(first));
                        }
                    };
                    second.PropertyChanged += (sender, args) =>
                    {
                        if (!_disposed)
                        {
                            firstProp.SetValue(first,
                                secondProp.GetValue(second));
                        }
                    };
                }


            }
        }

        public void Dispose()
        {
            _disposed = true;
        }
    }

    public class BidirectionalObserver
    {
        // change to Main to run.
        public static void none(string[] args)
        {
            var product = new Product{Name = "Book"};
            var window = new Window2{ProductName = "Book"};


            //product.PropertyChanged += (sender, eventArgs) =>
            //{
            //    if (eventArgs.PropertyName == "Name")
            //    {
            //        Console.WriteLine("Name changes in product");
            //        window.ProductName = product.Name;
            //    }
            //};

            //window.PropertyChanged += (sender, eventArgs) =>
            //{
            //    if (eventArgs.PropertyName == "ProductName")
            //    {
            //        Console.WriteLine("Name changes in Window");
            //        product.Name = window.ProductName;
            //    }
            //};

            // Expression trees () =>
            using var binding = new BidirectionalBinding(
                product,
                () => product.Name,
                window,
                () => window.ProductName);

            product.Name = "Smart Book";
            window.ProductName = "Really smart book";

            Console.WriteLine(product);
            Console.WriteLine(window);
        }
    }
}
