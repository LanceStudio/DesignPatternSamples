using System.Collections.Generic;
using UnityEngine;

namespace DragAndDrop {

    public class ServiceManager {

        private static readonly List<object> services = new();

        public static T GetService<T>() where T : class {
            foreach(var service in services) {
                if(service is T t)
                    return t;
            }
            return null;
        }

        public static void AddService<T>(T sevice) where T : class {
            if(services.Exists(elem => elem is T)) {
                Debug.LogWarning($"The service {typeof(T).Name} is already registered");
                return;
            }
            services.Add(sevice);
        }

        public static void ClearServices() {
            services.Clear();
        }
    }
}