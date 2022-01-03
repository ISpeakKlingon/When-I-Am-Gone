//Thanks to Nicky B from I Heart Gamedev YouTube "How to Program in Unity: Hierarchical State Machine Refactor [Built-In Character Controller #5]" video
public abstract class HandInterfaceBaseState
{
    protected HandInterfaceStateMachine _ctx; //_ctx is short for "context"
    protected HandInterfaceStateFactory _factory;
    public HandInterfaceBaseState(HandInterfaceStateMachine currentContext, HandInterfaceStateFactory handInterfaceStateFactory)
    {
        _ctx = currentContext;
        _factory = handInterfaceStateFactory;
    }

    public abstract void EnterState();

    public abstract void UpdateState();

    public abstract void ExitState();

    public abstract void CheckSwitchStates();

    public abstract void InitializeSubState();

    void UpdateStates() { }

    protected void SwitchState(HandInterfaceBaseState newState)
    {
        //current state exits state
        ExitState();

        //new state enters state
        newState.EnterState();

        //switch current state of context
        _ctx.CurrentState = newState;
    }

    protected void SetSuperState() { }

    protected void SetSubState() { }
}
