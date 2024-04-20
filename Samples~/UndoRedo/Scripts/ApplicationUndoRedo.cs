using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UndoRedo {

    public class ApplicationUndoRedo : MonoBehaviour {

        [SerializeField] private UndoRedoService undoRedoService;

        private void Awake() {
            ServiceManager.ClearServices();
            ServiceManager.AddService(undoRedoService);
        }
    }
}