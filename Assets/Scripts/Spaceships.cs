using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEditor;

public enum SPACESHIPS
{
    Battleship = 0,
    Explorer = 1,
    Interceptor = 2,
    Miner = 3
}

public class SpaceshipObject
{
    public string Name { get; set; }
    public string SpecialAbility { get; set; }
    public SPACESHIPS ClassName { get; set; }
    public float WarpSpeed { get; set; }
    public int Strength { get; set; }
    public int WarpRange { get; set; }

    public void LoadObject(SpaceshipObject _s)
    {
        Name = _s.Name;
        SpecialAbility = _s.SpecialAbility;
        ClassName = _s.ClassName;
        WarpSpeed = _s.WarpSpeed;
        Strength = _s.Strength;
        WarpRange = _s.WarpRange;
    }
}

public class SpaceshipComponent: MonoBehaviour
{
    public string Name;
    public string SpecialAbility;
    public SPACESHIPS ClassName;
    public float WarpSpeed;
    public int Strength;
    public int WarpRange;

    public void LoadObject(SpaceshipObject _s)
    {
        Name = _s.Name;
        SpecialAbility = _s.SpecialAbility;
        ClassName = _s.ClassName;
        WarpSpeed = _s.WarpSpeed;
        Strength = _s.Strength;
        WarpRange = _s.WarpRange;
    }
}

public class Spaceships : EditorWindow
{
    private SpaceshipObject spaceship = new SpaceshipObject();
    private SpaceshipObject spaceship_stored = new SpaceshipObject();
    private GameObject g;
    private string prefabAddress;
    private Vector3 prefabPosition;

    [MenuItem("PROG56693 Tools Project/Space Ships")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(Spaceships));
    }

    private void OnPrefabLoaded(AsyncOperationHandle<GameObject> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            g = handle.Result;
            g.name = spaceship.Name;

            Undo.RegisterCreatedObjectUndo(g, "Create " + g.name);
            SpaceshipComponent spaceship_component = g.AddComponent<SpaceshipComponent>();
            spaceship_component.LoadObject(spaceship);
            spaceship_stored.LoadObject(spaceship);
        }
        else
        {
            Debug.LogError("Failed to load prefab: " + prefabAddress);
        }
    }

    private void OnGUI()
    {
        GUILayout.Label("Spaceship editor", EditorStyles.boldLabel);

        spaceship.Name = EditorGUILayout.TextField("Name: ", spaceship.Name);
        spaceship.ClassName = (SPACESHIPS)EditorGUILayout.EnumPopup("Class: ", spaceship.ClassName);
        spaceship.Strength = EditorGUILayout.IntField("Strength: ", spaceship.Strength);
        spaceship.SpecialAbility = EditorGUILayout.TextField("Special Ability: ", spaceship.SpecialAbility);
        spaceship.WarpRange = EditorGUILayout.IntSlider("Warp Range: ", spaceship.WarpRange, 0, 500);
        spaceship.WarpSpeed = EditorGUILayout.Slider("Warp Speed: ", spaceship.WarpSpeed, 0, 500);

        if (GUILayout.Button("Add Spaceship"))
        {
            Debug.Log("Inserting...");

            if (spaceship.ClassName == SPACESHIPS.Battleship)
            {
                prefabAddress = "StarSparrow1";
                prefabPosition = new Vector3(0, 1.0f, 0);
            }
            else if (spaceship.ClassName == SPACESHIPS.Explorer)
            {
                prefabAddress = "StarSparrow2";
                prefabPosition = new Vector3(0f, 3.0f, 0.0f);
            }
            else if (spaceship.ClassName == SPACESHIPS.Interceptor)
            {
                prefabAddress = "StarSparrow3";
                prefabPosition = new Vector3(0, 5.0f, 0.0f);
            }
            else if (spaceship.ClassName == SPACESHIPS.Miner)
            {
                prefabAddress = "StarSparrow4";
                prefabPosition = new Vector3(0f, 7.0f, 0.0f);
            }
            else
            {
                Debug.LogError("Unrecognized class: " + spaceship.ClassName);
            }

            Addressables.InstantiateAsync(prefabAddress, prefabPosition, Quaternion.identity).Completed += OnPrefabLoaded;            
        }

        GUILayout.Label("Name: " + spaceship_stored.Name);
        GUILayout.Label("Class: " + spaceship_stored.ClassName);
        GUILayout.Label("Strength: " + spaceship_stored.Strength);
        GUILayout.Label("Special Ability: " + spaceship_stored.SpecialAbility);
        GUILayout.Label("Warp Range: " + spaceship_stored.WarpRange);
        GUILayout.Label("Warp Speed: " + spaceship_stored.WarpSpeed);
    }
}
