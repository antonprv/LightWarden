// Created by Anton Piruev in 2025. Any direct commercial use of derivative work is strictly prohibited.

using UnityEngine;

[CreateAssetMenu(fileName = "EcsDebugSettings", menuName = "Scriptable Objects/EcsDebugSettings")]
public class EcsDebugSettings : ScriptableObject
{
  public bool EnableInspectorDebug = false;
  public bool EnableWindowDebug = true;
}
