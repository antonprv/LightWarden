using System;
using System.Linq;
using Reflex.Core;
using Reflex.Enums;

namespace Code.Common.Extensions.ReflexExtensions
{
  public static class ReflexZenjectExtensions
  {
    public static BindingBuilder<T> Bind<T>(this ContainerBuilder builder)
    {
      return new BindingBuilder<T>(builder);
    }
  }
}
