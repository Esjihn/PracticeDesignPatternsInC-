﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SOLID
{
    public enum Color
    {
        Red, Green, Blue
    }

    public enum Size
    {
        Small, Medium, Large, Yuge
    }
    
    public class Product
    {
        public string Name;
        public Color Color;
        public Size Size;

        public Product(string name, Color color, Size size)
        {
            if (name == null)
            {
                throw new ArgumentNullException(paramName: nameof(name));
            }
            Name = name;
            Color = color;
            Size = size;
        }
    }

    public class ProductFilter
    {
        // boss says filter by size
        public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
        {
            foreach (var p in products)
            {
                if (p.Size == size)
                    yield return p;
            }
        }

        // boss says filter by color
        public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
        {
            foreach (var p in products)
            {
                if (p.Color == color)
                    yield return p;
            }
        }

        // boss says needs filter by size and color, see this breaks open closed.
        // should be open to extension closed for modification. We are modifying the ProductFilter 
        // class every time we need to add a method to add new filter functionality.
        // for instance a specific version of Product filter is already shipped to the customer
        public IEnumerable<Product> FilterBySizeAndColor(IEnumerable<Product> products, Size size, Color color)
        {
            foreach (var p in products)
            {
                if (p.Size == size && p.Color == color)
                    yield return p;
            }
        }

        // Answer is interfaces and abstraction.
    }

    // Open / Closed solution
    public interface ISpecification<T>
    {
        bool IsSatisfied(T t);
    }

    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> products, ISpecification<T> spec);
    }

    public class AndSpecification<T> : ISpecification<T>
    {
        private readonly ISpecification<T> _first, _second;

        public AndSpecification(ISpecification<T> first, ISpecification<T> second)
        {
            if (first == null)
            {
                throw new ArgumentNullException(nameof(first));
            }
            if (second == null)
            {
                throw new ArgumentNullException(nameof(second));
            }

            this._first = first;
            this._second = second;
        }

        public bool IsSatisfied(T t)
        {
            return _first.IsSatisfied(t) && _second.IsSatisfied(t);
        }
    }

    public class SizeSpecification : ISpecification<Product>
    {
        private readonly Size _size;

        public SizeSpecification(Size size)
        {
            this._size = size;
        }

        public bool IsSatisfied(Product t)
        {
            return t.Size == _size;
        }
    }

    public class ColorSpecification : ISpecification<Product>
    {
        private readonly Color _color;

        public ColorSpecification(Color color)
        {
            this._color = color;
        }

        public bool IsSatisfied(Product t)
        {
            return t.Color ==  _color;
        }
    }

    public class BetterFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> products, ISpecification<Product> spec)
        {
            foreach (Product product in products)
            {
                if (spec.IsSatisfied(product))
                    yield return product;
            }
        }
        // should be closed for modification. i.e. no new code in here. 
    }

    public class OpenClosed
    {
        // change to Main to run
        public static void none(string[] args)
        {
            var apple = new Product("Apple", Color.Green, Size.Small);
            var tree = new Product("Tree", Color.Green, Size.Large);
            var house = new Product("House", Color.Blue, Size.Large);

            Product[] products = {apple, tree, house};
            var pf = new ProductFilter();
            Console.WriteLine("Green products (old):");
            foreach (Product p in pf.FilterByColor(products, Color.Green))
            {
                Console.WriteLine($" - {p.Name} is green");
            }

            var bf = new BetterFilter();
            Console.WriteLine("Green products (new):");
            foreach (Product p in bf.Filter(products, new ColorSpecification(Color.Green)))
            {
                Console.WriteLine($" - {p.Name} is green");
            }

            Console.WriteLine("Large blue items");
            foreach (var p in bf.Filter(products, new AndSpecification<Product>(
                new ColorSpecification(Color.Blue), new SizeSpecification(Size.Large))))
            {
                Console.WriteLine($" - {p.Name} is big and blue");
            }
        }
    }
}
