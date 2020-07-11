using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Observers.Annotations;

namespace Observers
{
    public class PropertyNotificationSupport : INotifyPropertyChanged
    {
        // CanVote → Age, Citizenship
        private readonly Dictionary<string, HashSet<string>> affectedBy =
            new Dictionary<string, HashSet<string>>();

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            foreach (string affected in affectedBy.Keys)
            {
                if (affectedBy[affected].Contains(propertyName))
                {
                    // Another problem if there are circular dependencies
                    OnPropertyChanged(affected);
                }
            }
        }
    }

    // works only if a base class is available. 
    public class Person3 : PropertyNotificationSupport
    {
        private bool _canVote;
        private int _age;

        public int Age
        {
            get => _age;
            set
            {
                // 4 → 5
                // false → false
                // var oldCanVote = CanVote;

                if (value == _age) return;
                _age = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanVote));

            }
        }

        public bool CanVote
        {
            get
            {
                if (Age >= 16) 
                {
                    return _canVote = true;
                }

                return _canVote;
            }
            set
            {
                _canVote = value;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


    public class ObserversPropertyDependencies
    {
        // change to Main to run.
        public static void Main(string[] args)
        {
            
        }
    }
}
