
public abstract class UnitState {

    static protected FightingState fighting;
    static protected IdleState idle;
    static protected PersuingState persuing;
    static protected OnAttackIdleState onAttackIdle;

    public abstract UnitState HandleTransitions(Unit unit);
    public abstract void Update(Unit unit);
    public abstract void OnExit(Unit unit);
    public abstract void OnEnter(Unit unit);
}
