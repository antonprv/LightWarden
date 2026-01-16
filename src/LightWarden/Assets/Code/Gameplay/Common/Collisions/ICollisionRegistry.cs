// Created by Anton Piruev in 2025. Any direct commercial use of derivative work is strictly prohibited.

using Entitas;

namespace Code.Gameplay.Common.Collisions
{
  public interface ICollisionRegistry
  {
    void Register(int instanceId, IEntity entity);
    void Unregister(int instanceId);
    TEntity Get<TEntity>(int instanceId) where TEntity : class;
  }
}
