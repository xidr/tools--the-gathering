using System;
using System.Linq;
using Tools.Extension_Methods;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Patterns.StateMachine.GA_HSM {
    public class PlayerStateDriver : MonoBehaviour {
        
        public PlayerContext ctx = new PlayerContext();
        public Transform groundCheck;
        public float groundRadius = 0.2f;
        public LayerMask groundMask;
        public bool drawGizmos = true;
        string _lastPath;
        
        Rigidbody _rb;
        StateMachine _machine;
        State _root;

        void Awake() {
            _rb = GetComponent<Rigidbody>();
            _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            
            ctx.rb = _rb;
            ctx.anim = GetComponentInChildren<Animator>();
            
            // TODO Initialize StateMachine
            _root = new PlayerRoot(null, ctx);
            var builder = new StateMachineBuilder(_root);
            _machine = builder.Build();
        }

        void Update() {
            float x = 0f;
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) x -= 1f;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) x += 1f;

            ctx.jumpPressed = Keyboard.current.spaceKey.wasPressedThisFrame;
            ctx.move.x = Mathf.Clamp(x, -1f, 1f);
            
            ctx.grounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundMask);
            
            _machine.Tick(Time.deltaTime);

            var path = StatePath(_machine.root.Leaf());
            
            if (path != _lastPath) {
                Debug.Log($"State: {path}");
                _lastPath = path;
            }
        }
        
        void FixedUpdate() {
            var v = _rb.linearVelocity;
            v.x = ctx.velocity.x;
            _rb.linearVelocity = v;
            
            ctx.velocity.x = _rb.linearVelocity.x;
            
        }

        void OnDrawGizmosSelected() {
            if (!drawGizmos || groundCheck == null) return;
            
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
        }

        static string StatePath(State state) {
            return string.Join(" > ", state.PathToRoot().Reverse().Select(x => x.GetType().Name));
        }


    }
    
    [Serializable]
    public class PlayerContext {
        public Vector3 move;
        public Vector3 velocity;
        public bool grounded;
        public float moveSpeed = 6f;
        public float accel = 40f;
        public float jumpSpeed = 7f;
        public bool jumpPressed;
        public Animator anim;
        public Rigidbody rb;
    }
}