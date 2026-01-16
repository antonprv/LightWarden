// Created by Anton Piruev in 2025. Any direct commercial use of derivative work is strictly prohibited.

using UnityEngine;

namespace Code.Gameplay.Cameras.Provider
{
  public interface ICameraProvider
  {
    Camera MainCamera { get; }
    float WorldScreenHeight { get; }
    float WorldScreenWidth { get; }
    void SetMainCamera(Camera camera);
  }
}
