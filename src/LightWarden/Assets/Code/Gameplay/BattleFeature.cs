// Created by Anton Piruev in 2025. Any direct commercial use of derivative work is strictly prohibited.

using Assets.Code.Gameplay.Features.Movement;
using Assets.Code.Gameplay.Input.Systems;

using Code.Gameplay.Common.Time;
using Code.Gameplay.Input.Service;

namespace Assets.Code.Gameplay
{
  internal class BattleFeature : Feature
  {
    public BattleFeature(GameContext gameContext, ITimeService timeService, IInputService inputService)
    {
      Add(new MovementFeature(gameContext, timeService));

      Add(new InputFeature(gameContext, inputService));
    }
  }
}
