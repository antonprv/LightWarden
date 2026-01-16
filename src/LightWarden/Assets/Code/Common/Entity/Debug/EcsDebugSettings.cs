using UnityEngine;

[CreateAssetMenu(fileName = "EcsDebugSettings", menuName = "Scriptable Objects/EcsDebugSettings")]
public class EcsDebugSettings : ScriptableObject
{
  public bool EnableInspectorDebug = false;
  public bool EnableWindowDebug = true;
}
