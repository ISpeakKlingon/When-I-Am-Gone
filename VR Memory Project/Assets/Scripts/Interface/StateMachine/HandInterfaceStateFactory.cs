//Thanks to Nicky B from I Heart Gamedev YouTube "How to Program in Unity: Hierarchical State Machine Refactor [Built-In Character Controller #5]" video
public class HandInterfaceStateFactory
{
    HandInterfaceStateMachine _context;

    public HandInterfaceStateFactory(HandInterfaceStateMachine currentContext)
    {
        _context = currentContext;
    }

    public HandInterfaceBaseState Docked()
    {
        return new HandInterfaceDockedState(_context, this);
    }

    public HandInterfaceBaseState Undocked()
    {
        return new HandInterfaceUndockedState(_context, this);
    }
}
