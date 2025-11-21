using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEditor;
using System.Collections.Generic;

public class MissionComponent : MonoBehaviour
{
    public string Name;
    public string Rewards;
    public string Description;
    public string Location;

    public void LoadObject(MissionObject _s, List<string> _l)
    {
        Name = _s.Name;
        Rewards = _s.Rewards;
        Description = _s.Description;
        Location = _s.GetItemOrNull(_l, _s.Location);
    }
}

public class MissionObject
{
    public string Name { get; set; }
    public string Rewards { get; set; }
    public string Description { get; set; }
    public int Location { get; set; }

    public MissionObject()
    {
        Location = -1;
    }

    public void LoadObject(MissionObject _s)
    {
        Name = _s.Name;
        Rewards = _s.Rewards;
        Description = _s.Description;
        Location = _s.Location;
    }

    public T GetItemOrNull<T>(List<T> _l, int _idx)
    {
        if (_l == null || _idx < 0 || _idx >= _l.Count)
        {
            return default(T);
        }
        return _l[_idx];
    }
}

public class Missions : EditorWindow
{
    private MissionObject mission = new MissionObject();
    private MissionObject mission_stored = new MissionObject();
    private GameObject g;
    private Vector3 prefabPosition = new Vector3(20.0f, 10.0f, 20.0f);
    private List<string> planetNames = new List<string> ();
    private List<string> prefabAddresses = new List<string> {
        "Mission1", "Mission2", "Mission3", "Mission4"
    };
    private int prefabIdx = 0;

    [MenuItem("PROG56693 Tools Project/Missions")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(Missions));
    }

    private void OnPrefabLoaded(AsyncOperationHandle<GameObject> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            g = handle.Result;
            g.name = mission.Name;

            Undo.RegisterCreatedObjectUndo(g, "Create " + g.name);
            MissionComponent mission_component = g.AddComponent<MissionComponent>();
            mission_component.LoadObject(mission, planetNames);
            mission_stored.LoadObject(mission);

            prefabIdx = prefabIdx >= prefabAddresses.Count - 1 ? 0 : prefabIdx + 1;
            prefabPosition.y += 2.0f;
        }
        else
        {
            Debug.LogError("Failed to load prefab: " + prefabAddresses);
        }
    }

    private void OnGUI()
    {
        GUILayout.Label("Mission editor", EditorStyles.boldLabel);

        PlanetComponent[] customComponents = FindObjectsByType<PlanetComponent>(FindObjectsSortMode.None);
        planetNames.Clear();
        foreach (PlanetComponent component in customComponents)
        {
            planetNames.Add(component.Name);
        }

        mission.Name = EditorGUILayout.TextField("Name: ", mission.Name);
        mission.Rewards = EditorGUILayout.TextField("Rewards: ", mission.Rewards);
        mission.Description = EditorGUILayout.TextField("Description: ", mission.Description);
        mission.Location = EditorGUILayout.Popup("Location: ", mission.Location, planetNames.ToArray());

        if (GUILayout.Button("Add Mission"))
        {
            Debug.Log("Inserting...");

            Addressables.InstantiateAsync(prefabAddresses[prefabIdx], prefabPosition, Quaternion.identity).Completed += OnPrefabLoaded;
        }

        GUILayout.Label("Name: " + mission_stored.Name);
        GUILayout.Label("Rewards: " + mission_stored.Rewards);
        GUILayout.Label("Description: " + mission_stored.Description);
        GUILayout.Label("Location: " + mission_stored.GetItemOrNull(planetNames, mission_stored.Location));
    }

    private void OnDisable()
    {
        prefabIdx = 0;
        prefabPosition = new Vector3(20.0f, 10.0f, 20.0f);
    }
}
