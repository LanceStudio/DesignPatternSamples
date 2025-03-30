using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace MVVM {

    /// <summary>
    /// The viewModel has a reference to the model and register to event to know when the model has changed.
    /// In NotifyPropertyChanged you can check the name of the property and propagate the event only if you are interested in this data,
    /// Here we are not interesting in the user address, so we don't fire the event if the address is modified.
    /// In ChildsCollectionChanged you can check NotifyCollectionChangedEventArgs to see what type of action just happened (Add, Delete...)
    /// and react according to it.
    /// You can have multiple ViewModel linked to the same Model, for different view purpose.
    /// Note : Here I added IDisposable to unregister all events if we don't need the ViewModel anymore.
    /// </summary>
    public class UserViewModel : INotifyPropertyChanged, IDisposable {

        public event PropertyChangedEventHandler PropertyChanged;

        public string UserName => userModel.FirstName + " " + userModel.LastName;
        public int Age { get => userModel.Age; set => userModel.Age = value; }
        public string Address => userModel.Address;
        public int NumChild => userModel.Childs.Count;

        private UserModel userModel;

        public UserViewModel(UserModel user) {
            userModel = user;
            userModel.PropertyChanged += NotifyPropertyChanged;
            userModel.Childs.CollectionChanged += ChildsCollectionChanged;
        }

        private void NotifyPropertyChanged(object sender, PropertyChangedEventArgs e) {
            if(e.PropertyName == nameof(UserModel.Address))
                return;
            PropertyChanged?.Invoke(this, e);
        }

        private void ChildsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            NotifyPropertyChanged(this, new(nameof(NumChild)));
        }

        public void Dispose() {
            userModel.PropertyChanged -= NotifyPropertyChanged;
            userModel.Childs.CollectionChanged -= ChildsCollectionChanged;
        }
    }
}