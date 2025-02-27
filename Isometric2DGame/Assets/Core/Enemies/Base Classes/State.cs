using UnityEngine;

[System.Serializable]
public abstract class State
{
    public abstract void EnterState(Enemy enemy);
    
    public abstract void UpdateState(Enemy enemy);
    
    public abstract void ExitState(Enemy enemy);
}
