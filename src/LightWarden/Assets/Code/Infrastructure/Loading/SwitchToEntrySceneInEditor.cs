// Created by Anton Piruev in 2025. Any direct commercial use of derivative work is strictly prohibited.

using Code.Common.Extensions.ReflexExtensions;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure.Loading
{
  // Has execution order to start before every other script
  public class SwitchToEntrySceneInEditor : MonoBehaviour
  {
#if UNITY_EDITOR
    private const int EntrySceneIndex = 0;

    private void Awake()
    {
      if (RootContext.HasInstance)
        return;

      foreach (GameObject root in gameObject.scene.GetRootGameObjects())
        root.SetActive(false);

      SceneManager.LoadScene(EntrySceneIndex);
    }
#endif
  }
}
