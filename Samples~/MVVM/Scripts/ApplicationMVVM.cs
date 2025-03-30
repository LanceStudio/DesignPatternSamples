using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MVVM {

    [DefaultExecutionOrder(-1)]
    public class ApplicationMVVM : MonoBehaviour {

        [SerializeField] private UserView userViewPrefab;
        [SerializeField] private ScrollRect scrollView;

        private UserModel userModel;
        private UserViewModel userViewModel;
        private UserView userView;

        void Start() {
            userModel = new UserModel {
                FirstName = "Maxime",
                LastName = "Vican",
                Age = 34,
                Address = "Toulouse"
            };

            userViewModel = new(userModel);
            userView = Instantiate(userViewPrefab, scrollView.content);
            userView.AttachViewModel(userViewModel);
        }

        public void AddChild() {
            UserModel childModel = new() {
                FirstName = "Child " + userModel.Childs.Count,
                LastName = "Child " + userModel.Childs.Count,
                Age = Random.Range(1, 10),
                Address = "Toulouse"
            };
            userModel.Childs.Add(childModel);
        }

        public void RemoveChild() {
            if(userModel.Childs.Count > 0)
                userModel.Childs.RemoveAt(userModel.Childs.Count - 1);
        }
    }
}