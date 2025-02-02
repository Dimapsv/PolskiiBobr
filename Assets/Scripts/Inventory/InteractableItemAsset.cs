using UnityEngine;

[CreateAssetMenu]
public class InteractableItemAsset : ScriptableObject
{
    public ItemType itemType;
    public string itemName;
    public Sprite icon;

    // поля для деревьев
    public int countOfTree;

    // поля для здоровья
    public int valueOfHealth;

    // поля для коллектеблс
    public int idOfCollactable; // 0 - KeyFromLesopika // 1 - Axe 
    // поля для способностей
    public int idOfAblity; // 0 - Dash, 1 - Push, ... , 3 - xxx

    public int idOfNote;


}
