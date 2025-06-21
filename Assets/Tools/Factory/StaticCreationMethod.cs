using UnityEngine;

namespace Tools.Factory {
    public class StaticCreationMethod {
        public string name { get; private set; }
        public int health {get; private set;}

        // And the constructor is private
        StaticCreationMethod(string name, int health) {
            this.name = name;
            this.health = health;
        
            //// This is Logic in question
            // Debug.Log(name + " created");
        }
        
        // Here
        public static StaticCreationMethod CreateAndDebug(string name, int health) {
            StaticCreationMethod staticMethod = new StaticCreationMethod(name, health);
            Debug.Log(name + " created");
            
            return staticMethod;
        }
    }
}