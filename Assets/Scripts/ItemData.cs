using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public bool isStackable = true;
    public GameObject prefab;          // Decoration prefab
    public Vector3 placementOffset;    // Offset relative to placement zone
}
