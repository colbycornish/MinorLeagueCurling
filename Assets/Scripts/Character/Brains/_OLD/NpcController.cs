using UnityEngine;
using Animancer;
using Animancer.FSM;

public class NpcController : MonoBehaviour
{
    [SerializeField] private AnimancerComponent _animancer;
    [SerializeField] private AvatarMask _upperBodyMask;
    [SerializeField] private LinearMixerTransition _locomotionMixer;
    [SerializeField] private ClipTransition _talkAnimation;

    private AnimancerLayer _locomotionLayer;
    private AnimancerLayer _actionLayer;
    private StateMachine<NpcState> _fsm;

    private void Awake()
    {
        _locomotionLayer = _animancer.Layers[0];
        _actionLayer = _animancer.Layers[1];
        _actionLayer.Mask = _upperBodyMask;
        _fsm = new StateMachine<NpcState>();
    }

    private void Update()
    {
        // Example logic
        
        if (Input.GetKeyUp(KeyCode.W))
        {
            _fsm.TrySetState(new WalkState(this));
        }

        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            _fsm.TrySetState(new TalkState(this));
        }

        _fsm.CurrentState?.Update();
    }

    // Example States for the FSM
    public abstract class NpcState : IState
    {
        protected NpcController _npc;
        public NpcState(NpcController npc) => _npc = npc;
        public virtual void OnEnterState() { }
        public virtual void OnExitState() { }
        public virtual void Update() { }
        public virtual bool CanEnterState => true;
        public virtual bool CanExitState => true;
    }

    public class WalkState : NpcState
    {
        public WalkState(NpcController npc) : base(npc) { }
        public override void OnEnterState()
        {
            _npc._locomotionLayer.Play(_npc._locomotionMixer);
        }

        public override void Update()
        {
            // float speed = 0.5f;   
            if (Input.GetKeyUp(KeyCode.Alpha0))
            {
                float speed = 0f; 
                _npc._locomotionMixer.State.Parameter = speed;  
            }
            if (Input.GetKeyUp(KeyCode.Alpha9))
            {
                float speed = 1f;   
                _npc._locomotionMixer.State.Parameter = speed; 
            }
            if (Input.GetKeyUp(KeyCode.Alpha8))
            {
                float speed = 2f;   
                _npc._locomotionMixer.State.Parameter = speed; 
            }
        
            // Update the mixer parameter based on movement speed
            // / Example speed
            // _npc._locomotionMixer.State.Parameter = speed;
        }
    }

    public class TalkState : NpcState
    {
        public TalkState(NpcController npc) : base(npc) { }
        public override void OnEnterState()
        {
            _npc._actionLayer.Play(_npc._talkAnimation);
            _npc._actionLayer.Weight = 1;
        }

        public override void OnExitState()
        {
            _npc._actionLayer.StartFade(0, 0.5f);
        }
    }
}