// Created by Anton Piruev in 2025. Any direct commercial use of derivative work is strictly prohibited.

using Code.Gameplay.Input.Service;

using Entitas;

using UnityEngine;

namespace Assets.Code.Gameplay.Input.Systems
{
  public class EmitInputSystem : IExecuteSystem
  {
    private readonly IInputService _inputService;

    private readonly IGroup<GameEntity> _inputs;

    public EmitInputSystem(GameContext gameContext, IInputService inputService)
    {
      _inputService = inputService;

      _inputs = gameContext.GetGroup(GameMatcher.Input);
    }

    public void Execute()
    {
      foreach (GameEntity input in _inputs)
      {
        if (_inputService.HasAxisInput())
        {
          input.ReplaceAxisInput(new Vector2(
            _inputService.GetHorizontalAxis(),
            _inputService.GetVerticalAxis()
            ));
        }
        else if (input.hasAxisInput)
        {
          input.RemoveAxisInput();
        }
      }
    }
  }
}
