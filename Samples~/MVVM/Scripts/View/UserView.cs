using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;

namespace MVVM
{
    /// <summary>
    /// The view has a reference to the ViewModel, it shouldn't have direct access to the model.
    /// Multiple views can be linked to the same viewModel, for different view purpose.
    /// When the viewModel fire update event we trigger an updateRequest instead of executing the UpdatedView to avoid multiple updates per frame.
    /// You can also check what property has been updated and update only the interesting parts.
    /// The view can be recycle and attached to an other viewModel if necessary (Pooling)
    /// </summary>
    public class UserView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI userName;
        [SerializeField] private TextMeshProUGUI age;
        [SerializeField] private TextMeshProUGUI numChild;

        private UserViewModel attachedViewModel;
        private bool updateRequested;


        private void Update()
        {
            if (updateRequested && attachedViewModel != null)
            {
                UpdateView();
            }
        }

        private void UpdateView()
        {
            updateRequested = false;
            userName.text = attachedViewModel.UserName;
            age.text = attachedViewModel.Age + " ans";
            numChild.text = attachedViewModel.NumChild.ToString() + " enfants";
        }


        public void AttachViewModel(UserViewModel viewModel)
        {
            DetachViewModel();
            attachedViewModel = viewModel;
            attachedViewModel.PropertyChanged += OnPropertyChanged;
            RequestViewUpdate();
        }

        public void DetachViewModel()
        {
            if (attachedViewModel != null)
            {
                attachedViewModel.PropertyChanged -= OnPropertyChanged;
                attachedViewModel = null;
            }
        }


        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RequestViewUpdate();
        }

        private void RequestViewUpdate()
        {
            updateRequested = true;
        }

        public void AddAge(int ageToAdd)
        {
            if(attachedViewModel != null)
            {
                attachedViewModel.Age += ageToAdd;
            }
        }
    }
}