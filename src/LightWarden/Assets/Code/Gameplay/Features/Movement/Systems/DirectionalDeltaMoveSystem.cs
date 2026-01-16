// Created by Anton Piruev in 2025. Any direct commercial use of derivative work is strictly prohibited.

using Code.Gameplay.Common.Time;

using Entitas;

using UnityEngine;


namespace Code.Gameplay.Features.Movement.Systems
{
  public class DirectionalDeltaMoveSystem : IExecuteSystem
  {
    private ITimeService _time;
    private IGroup<GameEntity> _movers;

    public DirectionalDeltaMoveSystem(
      GameContext gameContext, ITimeService timeService
      )
    {
      _time = timeService;
      _movers = gameContext.GetGroup(GameMatcher.
        AllOf(
          GameMatcher.WorldPosition,
          GameMatcher.Direction,
          GameMatcher.Speed,
          GameMatcher.Moving));
    }

    public void Execute()
    {
      foreach (GameEntity mover in _movers)
      {
        mover.ReplaceWorldPosition(
          (Vector2)mover.WorldPosition +
          mover.Direction * mover.Speed * _time.DeltaTime);
      }
    }
  }
}
