using System;
using UnityEngine;

namespace Patterns.IoS_ServiceLocator.GA {
    public class MiniMap : MonoBehaviour {
        IAudioService _audioService;
        IGameService _gameService;

        void Awake() {
            ServiceLocator.global.Register<IAudioService>(_audioService =  new MockAudioService());
            ServiceLocator.ForSceneOf(this).Register<IGameService>(_gameService  = new MockGameService());
        }

        void Start() {
            Debug.Log("MiniMap Start");
            
            ServiceLocator.For(this)
                .Get(out _audioService)
                .Get(out _gameService);
            
            _audioService.Play();
            _gameService.Start();
        }
    }
}