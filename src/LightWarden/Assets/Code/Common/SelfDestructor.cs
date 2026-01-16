// Created by Anton Piruev in 2025. Any direct commercial use of derivative work is strictly prohibited.

using UnityEngine;

namespace Code.Common
{
  public class SelfDestructor : MonoBehaviour
  {
    public float Countdown = 3.0f;

    private void Update()
    {
      Countdown -= UnityEngine.Time.deltaTime;
      if (Countdown <= 0)
        Destroy(gameObject);
    }
  }
}
