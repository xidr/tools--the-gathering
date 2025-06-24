using System.Collections.Generic;
using UnityEngine;

namespace Patterns.IoS_ServiceLocator.GA {
    public interface ILocalization {
        string GetLocalizedWord(string key);
    }

    public class MockLocalization : ILocalization {
        readonly List<string> _words = new() {
            "hund",
            "katt",
            "fisk",
            "bil",
            "hus"
        };
        readonly System.Random _random = new System.Random();

        public string GetLocalizedWord(string key) {
            return _words[_random.Next(_words.Count)];
        }
    }

    public interface ISerializer {
        void Serialize();
    }

    public class MockSerializer : ISerializer {
        public void Serialize() {
            Debug.Log("Serializing mock serializer");
        }
    }

    public interface IAudioService {
        void Play();
    }

    public class MockAudioService : IAudioService {
        public void Play() {
            Debug.Log("Playing mock audio service");
        }
    }

    public interface IGameService {
        void Start();
    }

    public class MockGameService : IGameService {
        public void Start() {
            Debug.Log("Starting mock game service");
        }
    }

    public class MockMapService : IGameService {
        public void Start() {
            Debug.Log("Starting mock map service");
        }
    }
}