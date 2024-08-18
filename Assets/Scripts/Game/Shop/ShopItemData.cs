using UnityEngine;

public enum ItemEnums
{
    Wood,
    WoodBundle,
    Net,
    Helmet,
    MetalRam
}


[CreateAssetMenu(fileName = "New Shop Item", menuName = "Scriptable Objects/Shop Item")]
public class ShopItemData : ScriptableObject
{
    [field: SerializeField] public ItemEnums Enum { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public int Price { get; private set; }

    [field: Multiline]
    [field: SerializeField] public string Description { get; private set; }



}
