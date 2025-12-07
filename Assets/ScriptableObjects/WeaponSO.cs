using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "SOData/Weapons")]
public abstract class WeaponSO : ScriptableObject
{
    [SerializeField] private string weaponName;
    [SerializeField] private Sprite sprite;
    [SerializeField] private float value;
    [SerializeField] private float damage;

    public string Name => weaponName;
    public Sprite Sprite => sprite;
    public float Value => value;
    public float Damage => damage;


}
