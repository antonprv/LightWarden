// Created by Anton Piruev in 2025. Any direct commercial use of derivative work is strictly prohibited.

namespace Code.Infrastructure.Identifiers
{
  public class IdentifierService : IIdentifierService
  {
    private int _lastId = 1;

    public int Next() =>
      ++_lastId;
  }
}
