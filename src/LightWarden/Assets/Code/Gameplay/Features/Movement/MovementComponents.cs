// Created by Anton Piruev in 2025. Any direct commercial use of derivative work is strictly prohibited.

using Entitas;

using UnityEngine;

namespace Code.Gameplay.Features.Movement
{
  [Game] public class Speed : IComponent { public float Value; };
  [Game] public class Direction : IComponent { public Vector2 Value; };
  [Game] public class Moving : IComponent { };
}
