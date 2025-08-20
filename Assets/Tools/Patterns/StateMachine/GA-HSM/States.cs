using UnityEngine;

namespace Patterns.StateMachine.GA_HSM {
    
    public class Move : State {
        readonly PlayerContext _ctx;
        
        public Move(StateMachine m, State parent, PlayerContext ctx) : base(m, parent) {
            _ctx = ctx;
        }

        protected override State GetTransition() {
            if (!_ctx.grounded) return ((PlayerRoot)parent).airborne;
            
            return Mathf.Abs(_ctx.move.x) < 0.01f ? ((Grounded)parent).idle : null;
        }

        protected override void OnUpdate(float deltaTime) {
            var target = _ctx.move.x * _ctx.moveSpeed;

            _ctx.velocity.x = Mathf.MoveTowards(_ctx.velocity.x, target, _ctx.accel * deltaTime);
        }
    }
    
    public class Idle : State {
        readonly PlayerContext _ctx;
        
        public Idle(StateMachine m, State parent, PlayerContext ctx) : base(m, parent) {
            _ctx = ctx;
        }

        protected override State GetTransition() {
            return Mathf.Abs(_ctx.move.x) > 0.01f ? ((Grounded)parent).move : null;
        }

        protected override void OnEnter() {
            _ctx.velocity.x = 0f;
        }
    }
    
    public class Grounded : State {
        readonly PlayerContext _ctx;
        
        public readonly Idle idle;
        public readonly Move move;

        public Grounded(StateMachine m, State parent, PlayerContext ctx) : base(m, parent) {
            this._ctx = ctx;
            idle = new Idle(m, this, ctx);
            move = new Move(m, this, ctx);
            
            Add(new DelayActivationActivity {delay = 0.5f});
        }

        protected override State GetInitialState() => idle;

        protected override State GetTransition() {
            if (_ctx.jumpPressed) {
                _ctx.jumpPressed = false;
                var rb = _ctx.rb;

                if (rb != null) {
                    var v = rb.linearVelocity;
                    v.y = _ctx.jumpSpeed;
                    rb.linearVelocity = v;
                }

                return ((PlayerRoot)parent).airborne;
            }
            return _ctx.grounded ? null : ((PlayerRoot)parent).airborne;
        }
    }
    
    public class Airborne : State {
        
        
        readonly PlayerContext _ctx;

        public Airborne(StateMachine m, State parent, PlayerContext ctx) : base(m, parent) {
            _ctx = ctx;
            
        }
        
        protected override State GetTransition() => _ctx.grounded ? ((PlayerRoot)parent).grounded : null;

        protected override void OnEnter() {
            // TODO: Update Animator through ctx.anim
        }
    }
    
    public class PlayerRoot : State {
        public readonly Grounded grounded;
        public readonly Airborne airborne;
        readonly PlayerContext _ctx;

        public PlayerRoot(StateMachine m, PlayerContext ctx) : base(m, null) {
            this._ctx = ctx;
            grounded = new Grounded(m, this, ctx);
            airborne = new Airborne(m, this, ctx);
        }

        protected override State GetInitialState() => grounded;
        protected override State GetTransition() => _ctx.grounded ? null : airborne;
    }
    
}