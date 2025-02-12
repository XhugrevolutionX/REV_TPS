using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "ItemValues", menuName = "Scriptable Objects/ItemValues")]
public class ItemValues : ScriptableObject
{
    public string itemName;
    public int scoreValue;
}
