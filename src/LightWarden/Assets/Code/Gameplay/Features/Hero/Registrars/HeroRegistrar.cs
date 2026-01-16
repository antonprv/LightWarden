using Code.Common.Entity;

using UnityEngine;

namespace Assets.Code.Gameplay.Features.Hero.Registrars
{
  public class HeroRegistrar : MonoBehaviour
  {
    public float Speed = 2;
    private GameEntity _entity;

    private void Awake()
    {
      _entity = CreateEntity.Empty().
        AddWorldPosition(transform.position)
        .AddDirection(Vector2.zero)
        .AddSpeed(Speed)
        ;
      _entity.isMoving = true;
    }
  }
}
