using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Proxies.Annotations;

namespace Proxies
{
    // from Model View View Model (MVVM) pattern
    
    // Model 
    public class Person 
    {
        public string FirstName, LastName;
    }

    // View = User Interface

    // ViewModel = data that you want to show on the user interface
    public class PersonViewModel
        : INotifyPropertyChanged
    {
        private readonly Person _person;

        public PersonViewModel(Person person)
        {
            _person = person;
        }

        // 1) Proxy implementing existing Person members First and Last Name
        public string FirstName
        {
            get { return _person.FirstName; }
            set
            {
                if (_person.FirstName == value) return;
                _person.FirstName = value;
                OnPropertyChanged(nameof(FirstName));
                OnPropertyChanged(nameof(FullName));
            }
        }

        public string LastName
        {
            get { return _person.LastName; }
            set
            {
                if (_person.LastName == value) return;
                _person.LastName = value;
                OnPropertyChanged(nameof(LastName));
                OnPropertyChanged(nameof(FullName));
            }
        }

        // 2) Changes Proxy to Decorator since we also Augment Person and then subsequently add FullName
        // functionality. Can also create a child viewModel if you want to separate the design pattern implementation
        // but not really necessary. 
        public string FullName
        {
            get { return $"{FirstName} {LastName}".Trim(); }
            set
            {
                if (value == null)
                {
                    _person.FirstName = null;
                    _person.LastName = null;
                    return;
                }

                var items = value.Split();
                if (items.Length > 0)
                    FirstName = items[0];
                if (items.Length > 1)
                    LastName = items[1];

                OnPropertyChanged(nameof(LastName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


    public class ViewModel_Proxy
    {
        // change to Main to run.
        public static void none(string[] args)
        {
            
        }
    }
}
