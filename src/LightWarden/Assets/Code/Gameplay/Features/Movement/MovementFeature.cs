// Created by Anton Piruev in 2025. Any direct commercial use of derivative work is strictly prohibited.

using Code.Gameplay.Common.Time;
using Code.Gameplay.Features.Movement.Systems;

namespace Assets.Code.Gameplay.Features.Movement
{
  public class MovementFeature : Feature
  {
    public MovementFeature(GameContext gameContext, ITimeService timeService)
    {
      Add(new DirectionalDeltaMoveSystem(gameContext, timeService));
    }
  }
}
