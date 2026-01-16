// Created by Anton Piruev in 2025. Any direct commercial use of derivative work is strictly prohibited.

namespace Code.Common.Extensions
{
  public static class NumericExtensions
  {
    public static float ZeroIfNegative(this float value) => value >= 0 ? value : 0;

    public static int ZeroIfNegative(this int value) => value >= 0 ? value : 0;
  }
}
