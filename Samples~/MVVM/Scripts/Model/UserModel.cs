using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MVVM
{
    /// <summary>
    /// The model holds the data and has event mechanism to tell if some datas has changed.
    /// Here we use the INotifyPropertyChanged interface to do it for properties
    /// And we use ObservableCollection for collection properties.
    /// You can put to this class functions to Save and Load the model from database or whatever
    /// or you can do it with a dedicated service.
    /// </summary>
    public class UserModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string firstName;
        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                NotifyPropertyChanged();
            }
        }

        private string lastName;
        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                NotifyPropertyChanged();
            }
        }

        private int age;
        public int Age
        {
            get => age;
            set
            {
                age = value;
                NotifyPropertyChanged();
            }
        }

        private string address;
        public string Address
        {
            get => address;
            set
            {
                address = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<UserModel> Childs { get; } = new();

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new(propertyName));
        }
    }
}