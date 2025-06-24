using System;
using UnityEngine;

namespace Patterns.IoS_ServiceLocator.GA {
    public class Hero : MonoBehaviour {
        
        ILocalization _localization;
        ISerializer _serializer;
        IAudioService _audioService;
        IGameService _gameService;

        void Awake() {
            ServiceLocator.global.Register<ILocalization>(_localization = new MockLocalization());
            ServiceLocator.ForSceneOf(this).Register<IGameService>(_gameService = new MockGameService());
            ServiceLocator.For(this).Register<ISerializer>(_serializer = new MockSerializer());
        }

        void Start() {
            Debug.Log("Hero Start");
            ServiceLocator.For(this)
                .Get(out _serializer)
                .Get(out _localization)
                .Get(out _gameService)
                .Get(out _audioService);

            Debug.Log(_localization.GetLocalizedWord("dog"));
        }
    }
}