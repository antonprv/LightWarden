using Assets.Code.Gameplay;

using Code.Gameplay.Common.Time;
using Code.Gameplay.Input.Service;

using Reflex.Attributes;

using UnityEngine;

namespace Assets.Code.Infrastructure.EcsRunner
{
  public class EcsRunner : MonoBehaviour, IEcsRunner
  {
    private GameContext _gameObject;
    private ITimeService _timeService;
    private IInputService _inputService;
    private BattleFeature _battleFeature;

    [Inject]
    private void Construct(GameContext gameObject, ITimeService timeService, IInputService inputService)
    {
      _gameObject = gameObject;
      _timeService = timeService;
      _inputService = inputService;
    }

    // Use this for initialization
    private void Start()
    {
      _battleFeature = new BattleFeature(_gameObject, _timeService, _inputService);

      _battleFeature.Initialize();
    }

    private void Update()
    {
      _battleFeature.Execute();
      _battleFeature.Cleanup();
    }

    private void OnDestroy()
    {
      _battleFeature.TearDown();
    }

  }
}
