using Code.Gameplay.Cameras.Provider;
using Code.Gameplay.Common.Collisions;
using Code.Gameplay.Common.Physics;
using Code.Gameplay.Common.Random;
using Code.Gameplay.Common.Time;
using Code.Gameplay.Input.Service;
using Code.Gameplay.Levels;
using Code.Gameplay.StaticData;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Identifiers;
using Code.Infrastructure.Installers;
using Code.Infrastructure.Loading;

using Code.Common.Extensions;

using Reflex.Core;
using UnityEngine;
using System;

namespace Code.Common.Extensions.ReflexExtensions
{
  public abstract class ProjectRootInstaller : MonoBehaviour, IInstaller
  {
    public static Container RootContainer { get; private set; }

    private void Awake()
    {
      // If RootContainer is already created - do not create a second time
      if (RootContainer != null)
        return;

      // Create builder
      var builder = new ContainerBuilder();

      // Register all dependencies
      InstallBindings(builder);

      // Build container and save it as RootContainer
      RootContainer = builder.Build();
    }

    public abstract void InstallBindings(ContainerBuilder builder);
  }
}
