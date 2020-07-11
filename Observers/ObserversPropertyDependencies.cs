using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using Observers.Annotations;

namespace Observers
{
    public class PropertyNotificationSupport
    {
        private readonly Dictionary<string, HashSet<string>> _affectedBy
            = new Dictionary<string, HashSet<string>>();

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged
            ([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            foreach (var affected in _affectedBy.Keys)
                if (_affectedBy[affected].Contains(propertyName))
                    OnPropertyChanged(affected);
        }

        protected Func<T> property<T>(string name, Expression<Func<T>> expr)
        {
            Console.WriteLine($"Creating computed property for expression {expr}");

            var visitor = new MemberAccessVisitor(GetType());
            visitor.Visit(expr);

            if (visitor.PropertyNames.Any())
            {
                if (!_affectedBy.ContainsKey(name))
                    _affectedBy.Add(name, new HashSet<string>());

                foreach (var propName in visitor.PropertyNames)
                    if (propName != name)
                        _affectedBy[name].Add(propName);
            }

            return expr.Compile();
        }
    }

    public class MemberAccessVisitor : ExpressionVisitor
    {
        private readonly Type _declaringType;
        public readonly IList<string> PropertyNames = new List<string>();


        public MemberAccessVisitor(Type declaringType)
        {
            this._declaringType = declaringType;
        }

        public override Expression Visit(Expression expr)
        {
            if (expr != null && expr.NodeType == ExpressionType.MemberAccess)
            {
                var memberExpr = (MemberExpression) expr;
                if (memberExpr.Member.DeclaringType == _declaringType)
                {
                    PropertyNames.Add(memberExpr.Member.Name);
                }
            }

            return base.Visit(expr);
        }
    }

    // works only if a base class is available. 
    public class Person3 : PropertyNotificationSupport
    {
        private int age;

        public int Age
        {
            get => age;
            set
            {
                if (value == age) return;
                age = value;
                OnPropertyChanged();
            }
        }

        public bool Citizen
        {
            get => citizen;
            set
            {
                if (value == citizen) return;
                citizen = value;
                OnPropertyChanged();
            }
        }

        private readonly Func<bool> canVote;
        private bool citizen;
        public bool CanVote => canVote();

        public Person3()
        {
            canVote = property(nameof(CanVote), () => Citizen && Age >= 16);
        }
    }


    public class ObserversPropertyDependencies
    {
        // change to Main to run.
        public static void none(string[] args)
        {
            var p = new Person3();
            p.PropertyChanged += (sender, eventArgs) =>
            {
                Console.WriteLine($"{eventArgs.PropertyName} changed.");
            };
            p.Age = 15;
            p.Citizen = true;
        }
    }
}
