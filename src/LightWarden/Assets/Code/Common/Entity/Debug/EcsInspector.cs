// Created by Anton Piruev in 2025. Any direct commercial use of derivative work is strictly prohibited.

#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Entitas;
using Assets.Code.Infrastructure.EcsRunner;

[CustomEditor(typeof(MonoBehaviour), true)]
public class EcsInspector : Editor
{
  private IEcsRunner Runner => target as IEcsRunner;

  private bool _showEntities = true;
  private Vector2 _scroll;
  private string _componentFilter = string.Empty;
  private string _entityFilter = string.Empty;
  private bool _hideTagComponents = false;

  private static EcsDebugSettings _settings;

  private static readonly Dictionary<int, bool> EntityFoldouts = new();
  private static readonly Dictionary<string, bool> ComponentFoldouts = new();
  private static readonly Dictionary<int, HashSet<Type>> EntityComponentCache = new();

  private void LoadSettings()
  {
    if (_settings == null)
    {
      _settings = Resources.Load<EcsDebugSettings>("EcsDebugSettings");
      if (_settings == null)
      {
        Debug.LogWarning("EcsDebugSettings.asset not found in Resources! Debug will be disabled.");
        _settings = ScriptableObject.CreateInstance<EcsDebugSettings>();
      }
    }
  }

  public override void OnInspectorGUI()
  {
    LoadSettings();

    // если выключено в настройках, показываем стандартный инспектор
    if (!_settings.EnableInspectorDebug || Runner == null)
    {
      DrawDefaultInspector();
      return;
    }

    DrawDefaultInspector();

    var context = GetGameContext(Runner);
    if (context == null)
    {
      EditorGUILayout.HelpBox("GameContext is null", MessageType.Info);
      return;
    }

    DrawToolbar();

    _showEntities = EditorGUILayout.Foldout(_showEntities, $"Entities ({context.count})", true);
    if (!_showEntities)
      return;

    _scroll = EditorGUILayout.BeginScrollView(_scroll, GUILayout.MaxHeight(500));

    foreach (var entity in context.GetEntities())
    {
      if (!PassEntityFilter(entity))
        continue;

      DrawEntity(entity);
    }

    EditorGUILayout.EndScrollView();
  }

  private static GameContext GetGameContext(IEcsRunner runner)
  {
    var field = runner.GetType().GetField("_gameObject", BindingFlags.NonPublic | BindingFlags.Instance);
    return field?.GetValue(runner) as GameContext;
  }

  private void DrawToolbar()
  {
    EditorGUILayout.BeginVertical("box");

    _entityFilter = EditorGUILayout.TextField("Entity Filter (id)", _entityFilter);
    _componentFilter = EditorGUILayout.TextField("Component Filter", _componentFilter);
    _hideTagComponents = EditorGUILayout.Toggle("Hide Tag Components", _hideTagComponents);

    EditorGUILayout.EndVertical();
    EditorGUILayout.Space();
  }

  private bool PassEntityFilter(GameEntity entity)
  {
    if (string.IsNullOrEmpty(_entityFilter))
      return true;

    return entity.creationIndex.ToString().Contains(_entityFilter);
  }

  private void DrawEntity(GameEntity entity)
  {
    var id = entity.creationIndex;
    EntityFoldouts.TryAdd(id, false);

    if (!EntityComponentCache.TryGetValue(id, out var cachedTypes))
    {
      cachedTypes = new HashSet<Type>();
      EntityComponentCache[id] = cachedTypes;
    }

    EditorGUILayout.BeginVertical("box");

    EntityFoldouts[id] = EditorGUILayout.Foldout(EntityFoldouts[id], $"Entity {id}", true);

    if (!EntityFoldouts[id])
    {
      EditorGUILayout.EndVertical();
      return;
    }

    foreach (var component in entity.GetComponents())
    {
      if (!PassComponentFilter(component))
        continue;

      if (_hideTagComponents && IsTagComponent(component))
        continue;

      var type = component.GetType();
      var key = $"{id}:{type.FullName}";

      if (!cachedTypes.Contains(type))
      {
        cachedTypes.Add(type);
        ComponentFoldouts[key] = true;
      }

      DrawComponent(id, component);
    }

    EditorGUILayout.EndVertical();
  }

  private bool PassComponentFilter(IComponent component)
  {
    if (string.IsNullOrEmpty(_componentFilter))
      return true;

    return component.GetType().Name.IndexOf(_componentFilter, StringComparison.OrdinalIgnoreCase) >= 0;
  }

  private static bool IsTagComponent(IComponent component)
  {
    return component.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public).Length == 0;
  }

  private void DrawComponent(int entityId, IComponent component)
  {
    var type = component.GetType();
    var key = $"{entityId}:{type.FullName}";
    ComponentFoldouts.TryAdd(key, false);

    var isTag = IsTagComponent(component);
    var prevColor = GUI.backgroundColor;

    if (isTag)
      GUI.backgroundColor = new Color(0.85f, 0.9f, 1f);

    EditorGUILayout.BeginVertical("helpbox");

    ComponentFoldouts[key] = EditorGUILayout.Foldout(ComponentFoldouts[key], type.Name, true);

    if (ComponentFoldouts[key] && !isTag)
      DrawFields(component, type);

    EditorGUILayout.EndVertical();
    GUI.backgroundColor = prevColor;
  }

  private static void DrawFields(object component, Type type)
  {
    var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public);

    foreach (var field in fields)
      DrawValue(field.Name, field.FieldType, field.GetValue(component));
  }

  private static void DrawValue(string name, Type type, object value)
  {
    EditorGUILayout.BeginHorizontal();
    EditorGUILayout.LabelField(name, GUILayout.Width(150));

    if (value == null)
      EditorGUILayout.LabelField("null");
    else if (type == typeof(Vector2))
      EditorGUILayout.Vector2Field(GUIContent.none, (Vector2)value);
    else if (type == typeof(Vector3))
      EditorGUILayout.Vector3Field(GUIContent.none, (Vector3)value);
    else if (type == typeof(Quaternion))
      EditorGUILayout.Vector3Field(GUIContent.none, ((Quaternion)value).eulerAngles);
    else if (type.IsEnum)
      EditorGUILayout.EnumPopup((Enum)value);
    else if (value is GameEntity entityRef)
      EditorGUILayout.LabelField($"Entity {entityRef.creationIndex}");
    else
      EditorGUILayout.LabelField(value.ToString());

    EditorGUILayout.EndHorizontal();
  }

  public override bool RequiresConstantRepaint()
  {
    return _settings.EnableInspectorDebug && Application.isPlaying;
  }
}

#endif
