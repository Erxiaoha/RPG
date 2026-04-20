using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueSO", menuName = "Dialogue/DialogueNode")]
public class DialogueSO : ScriptableObject
{
    public DialogueLine[] lines;
    public DialoguOption[] options;

    [Header("Quest Offer (Optional)")]
    public QuestSO offerQuestOnEnd;

    [Header("Completed Quest Requirements (Optioanl)")]
    public QuestSO[] requiredCompleteQuests;

    [Header("Quest Turn-In (Optional)")]
    public QuestSO turnInQuestOnEnd;

    [Header("Conditional Requirements (Optional)")]
    public ActorSO[] requireNPCs;
    public LocationSO[] requireLocations;
    public ItemSO[] requireItems;

    [Header("Control Flags")]
    public bool removeAfterPlay;
    public List<DialogueSO> removeTheseOnPlay;

    public bool IsConditionMet()
    {
        // 1. 先判断单例是否存在，不存在直接返回false，避免报错
        if (GameManager.Instance.LocationHistoryTracker == null)
        {
            Debug.LogError("LocationHistoryTracker 单例未初始化！请检查场景内是否有该组件");
            return false;
        }
        if (GameManager.Instance.DialogueHistoryTracker == null)
        {
            Debug.LogError("DialogueHistoryTracker 单例未初始化！请检查场景内是否有该组件");
            return false;
        }

        // 2. 判断requireNPCs数组是否为null/长度>0，再遍历
        if (requireNPCs != null && requireNPCs.Length > 0)
        {
            foreach (var npc in requireNPCs)
            {
                // 3. 跳过空元素，避免空引用
                if (npc == null) continue;

                if (!GameManager.Instance.DialogueHistoryTracker.HasSpokenWith(npc))
                {
                    return false;
                }
            }
        }

        // 4. 判断requireLocations数组是否为null/长度>0，再遍历
        if (requireLocations != null && requireLocations.Length > 0)
        {
            foreach (var location in requireLocations)
            {
                // 5. 跳过空元素，避免空引用
                if (location == null) continue;

                if (!GameManager.Instance.LocationHistoryTracker.HasVisted(location))
                {
                    return false;
                }
            }
        }

        if (requireItems != null && requireItems.Length > 0)
        {
            foreach (var item in requireItems)
            {
                if (item == null) continue;
                if (!InventoryManager.Instance.HasItem(item))
                {
                    return false;
                }
            }
        }

        if (requiredCompleteQuests != null && requiredCompleteQuests.Length > 0)
        {
            foreach (var quest in requiredCompleteQuests)
            {
                if (!GameManager.Instance.QuestManager.IsQuestComplete(quest))
                {
                    return false;
                }
            }
        }

        return true;
    }
}

[System.Serializable]
public class DialogueLine
{
    public ActorSO speaker;
    [TextArea(3,5)] public string text;
}

[System.Serializable]
public class DialoguOption
{
    public string optionText;
    public DialogueSO nextDialogue;
    public QuestSO offerQuest;
}
