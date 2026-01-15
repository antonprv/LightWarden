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
