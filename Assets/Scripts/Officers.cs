using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEditor;
using System.Collections.Generic;

public class OfficerComponent : MonoBehaviour
{
    public string Name;
    public string Race;
    public int AttackStrength;
    public int DefenceStrength;
    public int HealthStrength;
    public int OverallStrength;
    public string ShipSpecialty;
    public string HomePlanetSystem;

    public void LoadObject(OfficerObject _s, List<string> _l1, List<string> _l2)
    {
        Name = _s.Name;
        Race = _s.Race;
        AttackStrength = _s.AttackStrength;
        DefenceStrength = _s.DefenceStrength;
        HealthStrength = _s.HealthStrength;
        OverallStrength = _s.OverallStrength;
        ShipSpecialty = _s.GetItemOrNull(_l1, _s.ShipSpecialty);
        HomePlanetSystem = _s.GetItemOrNull(_l2, _s.HomePlanetSystem);
    }
}

public class OfficerObject
{
    public string Name { get; set; }
    public string Race { get; set; }
    public int AttackStrength { get; set; }
    public int DefenceStrength { get; set; }
    public int HealthStrength { get; set; }
    public int OverallStrength { get; set; }
    public int ShipSpecialty { get; set; }
    public int HomePlanetSystem { get; set; }

    public OfficerObject()
    {
        ShipSpecialty = -1;
        HomePlanetSystem = -1;
    }

    public void LoadObject(OfficerObject _s)
    {
        Name = _s.Name;
        Race = _s.Race;
        AttackStrength = _s.AttackStrength;
        DefenceStrength = _s.DefenceStrength;
        HealthStrength = _s.HealthStrength;
        OverallStrength = _s.OverallStrength;
        ShipSpecialty = _s.ShipSpecialty;
        HomePlanetSystem = _s.HomePlanetSystem;
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

public class Officers : EditorWindow
{
    private OfficerObject officer = new OfficerObject();
    private OfficerObject officer_stored = new OfficerObject();
    private GameObject g;
    private Vector3 prefabPosition = new Vector3(16.0f, 16.0f, 25.0f);
    private List<string> classNames = new List<string>();
    private List<string> planetNames = new List<string>();
    private List<string> prefabAddresses = new List<string> {
        "Officer1", "Officer2", "Officer3", "Officer4",
        "Officer5", "Officer6"
    };
    private int prefabIdx = 0;

    [MenuItem("PROG56693 Tools Project/Officers")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(Officers));
    }

    private void OnPrefabLoaded(AsyncOperationHandle<GameObject> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            g = handle.Result;
            g.name = officer.Name;

            Undo.RegisterCreatedObjectUndo(g, "Create " + g.name);
            OfficerComponent officer_component = g.AddComponent<OfficerComponent>();
            officer_component.LoadObject(officer, classNames, planetNames);
            officer_stored.LoadObject(officer);

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
        GUILayout.Label("Officer editor", EditorStyles.boldLabel);

        PlanetComponent[] planetComponents = FindObjectsByType<PlanetComponent>(FindObjectsSortMode.None);
        SpaceshipComponent[] spaceshipComponents = FindObjectsByType<SpaceshipComponent>(FindObjectsSortMode.None);
        planetNames.Clear();
        classNames.Clear();
        foreach (PlanetComponent component in planetComponents)
        {
            planetNames.Add(component.Name);
        }
        foreach (SpaceshipComponent component in spaceshipComponents)
        {
            if (!classNames.Contains(component.ClassName.ToString()))
            {
                classNames.Add(component.ClassName.ToString());
            }
        }

        officer.Name = EditorGUILayout.TextField("Name: ", officer.Name);
        officer.Race = EditorGUILayout.TextField("Rewards: ", officer.Race);
        officer.AttackStrength = EditorGUILayout.IntSlider("Attack Strength: ", officer.AttackStrength, 0, 500);
        officer.DefenceStrength = EditorGUILayout.IntSlider("Defence Strength: ", officer.DefenceStrength, 0, 500);
        officer.HealthStrength = EditorGUILayout.IntSlider("Health Strength: ", officer.HealthStrength, 0, 500);
        officer.OverallStrength = EditorGUILayout.IntSlider("Overall Strength: ", officer.OverallStrength, 0, 500);
        officer.ShipSpecialty = EditorGUILayout.Popup("Ship Specialty: ", officer.ShipSpecialty, classNames.ToArray());
        officer.HomePlanetSystem = EditorGUILayout.Popup("Home Planet System: ", officer.HomePlanetSystem, planetNames.ToArray());

        if (GUILayout.Button("Add Officer"))
        {
            Debug.Log("Inserting...");

            Addressables.InstantiateAsync(prefabAddresses[prefabIdx], prefabPosition, Quaternion.identity).Completed += OnPrefabLoaded;
        }

        GUILayout.Label("Name: " + officer_stored.Name);
        GUILayout.Label("Race: " + officer_stored.Race);
        GUILayout.Label("Attack Strength: " + officer_stored.AttackStrength);
        GUILayout.Label("Defence Strength: " + officer_stored.DefenceStrength);
        GUILayout.Label("Health Strength: " + officer_stored.HealthStrength);
        GUILayout.Label("Overall Strength: " + officer_stored.OverallStrength);
        GUILayout.Label("Ship Specialty: " + officer_stored.GetItemOrNull(classNames, officer_stored.ShipSpecialty));
        GUILayout.Label("Home Planet System: " + officer_stored.GetItemOrNull(planetNames, officer_stored.HomePlanetSystem));
    }

    private void OnDisable()
    {
        prefabIdx = 0;
        prefabPosition = new Vector3(16.0f, 16.0f, 25.0f);
    }
}
