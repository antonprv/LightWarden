// Created by Anton Piruev in 2025. Any direct commercial use of derivative work is strictly prohibited.

using Entitas;

namespace Code.Common.Entity.ToStrings
{
  public interface INamedEntity : IEntity
  {
    string EntityName(IComponent[] components);
    string BaseToString();
  }
}
