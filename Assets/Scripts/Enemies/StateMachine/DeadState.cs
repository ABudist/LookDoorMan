namespace Enemies.StateMachine
{
  public class DeadState : IState
  {
    private Enemy _enemy;
    
    public void Construct(Enemy enemy)
    {
      _enemy = enemy;
    }
    
    public void Enter()
    {
      _enemy.SetInactive();
    }

    public void Exit()
    {
    }
  }
}