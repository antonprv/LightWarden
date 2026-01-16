using Assets.Code.Infrastructure.EcsRunner;

using Code.Common.Extensions.ReflexExtensions;
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
using Code.Infrastructure.Loading;

using Reflex.Core;

using UnityEngine;

namespace Code.Infrastructure.Installers
{
  public class GameRootInstaller : ProjectRootInstaller
  {
    public override void InstallBindings(ContainerBuilder builder)
    {
      BindRunner(builder);
      BindContexts(builder);
      BindCameraProvider(builder);
      BindGameplayServices(builder);
      BindInfrastructureServices(builder);
      BindAssetManagementServices(builder);
      BindCommonServices(builder);
      BindInputService(builder);
    }

    private void BindRunner(ContainerBuilder builder)
    {
      var runner = GameObject.FindAnyObjectByType<EcsRunner>();
      builder.Bind<IEcsRunner>().FromInstance(runner).AsSingle();
    }

    private void BindContexts(ContainerBuilder builder)
    {
      builder.Bind<Contexts>().FromInstance(Contexts.sharedInstance).AsSingle();

      builder.Bind<GameContext>().FromInstance(Contexts.sharedInstance.game).AsSingle();
    }

    private void BindCameraProvider(ContainerBuilder builder)
    {
      builder.Bind<CameraProvider>().BindInterfacesAndSelf().AsSingle();
    }

    private void BindGameplayServices(ContainerBuilder builder)
    {
      builder.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
      builder.Bind<ILevelDataProvider>().To<LevelDataProvider>().AsSingle();
    }
    private void BindInfrastructureServices(ContainerBuilder builder)
    {
      builder.Bind<GameRootInstaller>().FromInstance(this).AsSingle();

      builder.Bind<IIdentifierService>().To<IdentifierService>().AsSingle();
    }

    private void BindAssetManagementServices(ContainerBuilder builder)
    {
      builder.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
    }

    private void BindCommonServices(ContainerBuilder builder)
    {
      builder.Bind<IRandomService>().To<UnityRandomService>().AsSingle();
      builder.Bind<ICollisionRegistry>().To<CollisionRegistry>().AsSingle();
      builder.Bind<IPhysicsService>().To<PhysicsService>().AsSingle();
      builder.Bind<ITimeService>().To<UnityTimeService>().AsSingle();
      builder.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
    }

    private void BindInputService(ContainerBuilder builder)
    {
      builder.Bind<IInputService>().To<StandaloneInputService>().AsSingle();
    }
  }
}
