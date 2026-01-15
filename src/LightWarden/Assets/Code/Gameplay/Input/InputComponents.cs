using Entitas;

using UnityEngine;

namespace Assets.Code.Gameplay.Input
{
  [Game] public class AxisInput : IComponent { public Vector2 Value; };
  [Game] public class Input : IComponent { };
}
