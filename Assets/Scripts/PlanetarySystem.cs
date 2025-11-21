using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEditor;
using System.Collections.Generic;

public class PlanetComponent : MonoBehaviour
{
    public string Name;
    public string IndigenousRace;
    public int NumberPlanets;

    public void LoadObject(PlanetObject _s)
    {
        Name = _s.Name;
        IndigenousRace = _s.IndigenousRace;
        NumberPlanets = _s.NumberPlanets;
    }
}
public class PlanetObject
{
    public string Name { get; set; }
    public string IndigenousRace { get; set; }
    public int NumberPlanets { get; set; }

    public void LoadObject(PlanetObject _s)
    {
        Name = _s.Name;
        IndigenousRace = _s.IndigenousRace;
        NumberPlanets = _s.NumberPlanets;
    }
}

public class Planets: EditorWindow
{
    private PlanetObject planet = new PlanetObject();
    private PlanetObject planet_stored = new PlanetObject();
    private GameObject g;
    private Vector3 prefabPosition = new Vector3(1.5f, 1.0f, 6.0f);
    private List<string> prefabAddresses = new List<string> {
        "Planet1", "Planet2", "Planet3", "Planet4",
        "Planet5", "Planet6", "Planet7", "Planet8"
    };
    private int prefabIdx = 0;

    [MenuItem("PROG56693 Tools Project/PlanetarySystem")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(Planets));
    }

    private void OnPrefabLoaded(AsyncOperationHandle<GameObject> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            g = handle.Result;
            g.name = planet.Name;

            Undo.RegisterCreatedObjectUndo(g, "Create " + g.name);
            PlanetComponent planet_component = g.AddComponent<PlanetComponent>();
            planet_component.LoadObject(planet);
            planet_stored.LoadObject(planet);

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
        GUILayout.Label("Planet System editor", EditorStyles.boldLabel);

        planet.Name = EditorGUILayout.TextField("Name: ", planet.Name);
        planet.IndigenousRace = EditorGUILayout.TextField("Indigenous Race: ", planet.IndigenousRace);
        planet.NumberPlanets = EditorGUILayout.IntSlider("Number of Planets: ", planet.NumberPlanets, 0, 500);

        if (GUILayout.Button("Add Planet System"))
        {
            Debug.Log("Inserting...");

            Addressables.InstantiateAsync(prefabAddresses[prefabIdx], prefabPosition, Quaternion.identity).Completed += OnPrefabLoaded;
        }

        GUILayout.Label("Name: " + planet_stored.Name);
        GUILayout.Label("Indigenous Race: " + planet_stored.IndigenousRace);
        GUILayout.Label("Number of Planets: " + planet_stored.NumberPlanets);
    }

    private void OnDisable()
    {
        prefabIdx = 0;
        prefabPosition = new Vector3(1.5f, 1.0f, 6.0f);
    }
}
