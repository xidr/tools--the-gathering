using System.Collections.Generic;
using UnityEngine;

namespace Patterns.Command.GA {
    public class AnimationManager : MonoBehaviour {
        Animator _animator;

        #region  Animation Hashes and Duration

        static readonly int AttackHash = Animator.StringToHash("Attack");
        static readonly int SpinHash = Animator.StringToHash("Spin");
        static readonly int JumpHash = Animator.StringToHash("Jump");
        static readonly int IdleHash = Animator.StringToHash("Idle");
        const float CROSSFADE_DURATION = 0.1f;
        
        readonly Dictionary<int, float> _animationDurations = new() {
            { AttackHash, 0.5f },
            { SpinHash, 0.5f },
            { JumpHash, 0.5f },
            { IdleHash, 0.5f },
        };
        
        #endregion

        void Awake() => _animator = GetComponent<Animator>();

        public float Attack() => PlayAnimation(AttackHash);
        public float Spin() => PlayAnimation(SpinHash);
        public float Jump() => PlayAnimation(JumpHash);
        public float Idle() => PlayAnimation(IdleHash);

        float PlayAnimation(int animationHash) {
            _animator.CrossFade(animationHash, CROSSFADE_DURATION);
            return _animationDurations[animationHash];
        }
    }
}