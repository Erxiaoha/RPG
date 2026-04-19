using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManeger : MonoBehaviour
{
    private Dictionary<QuestSO, Dictionary<QuestObjective, int>> questProgress = new();
    private List<QuestSO> completeQuests = new();

    #region Quest Accept Login
    public bool IsQuestAccepted(QuestSO questSO)
    {
        return questProgress.ContainsKey(questSO);
    }

    public void AcceptQuest(QuestSO questSO)
    {
        questProgress[questSO] = new Dictionary<QuestObjective, int>();
        foreach (var objective in questSO.objectives)
        {
            UpdateObjectiveProgress(questSO, objective);
        }
    }

    public List<QuestSO> GetActiveQuests()
    {
        return new List<QuestSO>(questProgress.Keys);
    }
    #endregion

    private void OnEnable()
    {
        QuestEvents.IsQuestComplete += IsQuestComplete;
    }

    private void OnDisable()
    {
        QuestEvents.IsQuestComplete -= IsQuestComplete;
    }

    #region Quest Complete Login
    public bool IsQuestComplete(QuestSO questSO)
    {
        if(!questProgress.TryGetValue(questSO, out var progressDict))
        {
            return false;
        }

        foreach (var objective in questSO.objectives)
        {
            UpdateObjectiveProgress(questSO, objective);
        }

        foreach (var objective in questSO.objectives)
        {
            if(progressDict[objective] < objective.requireAmount)
            {
                return false;
            }
        }
        return true;
    }

    public void CompleteQuest(QuestSO questSO)
    {
        questProgress.Remove(questSO);
        completeQuests.Add(questSO);
        // TODO Granting rewards
        foreach (var reward in questSO.rewards)
        {
            InventoryManager.Instance.AddItem(reward.itemSO, reward.quantity);
        }
    }

    #endregion
    public string GetProgressText(QuestSO questSO, QuestObjective objective)
    {
        int currentAmount = GetCurrentAmount(questSO, objective);

        if(currentAmount >= objective.requireAmount)
        {
            return "Complete";
        }
        else if(objective.targetItem != null)
        {
            return $"{currentAmount}/ {objective.requireAmount}";
        }
        else
        {
            return "In Progress";
        }
    }

    public bool GetCompleteQuest(QuestSO questSO)
    {
        return completeQuests.Contains(questSO);
    }

    public void UpdateObjectiveProgress(QuestSO questSO, QuestObjective objective)
    {
        if (!questProgress.ContainsKey(questSO))
        {
            //questProgress[questSO] = new Dictionary<QuestObjective, int>();
            return;
        }
        var progressDictionary = questProgress[questSO];
        int newAmount = 0;

        if (objective.targetItem != null)
        {
            newAmount = InventoryManager.Instance.GetItemQuantity(objective.targetItem);
        }
        else if (objective.targetLocation != null && GameManager.Instance.LocationHistoryTracker.HasVisted(objective.targetLocation))
        {
            newAmount = objective.requireAmount;
        }
        else if(objective.targetNPC != null && GameManager.Instance.DialogueHistoryTracker.HasSpokenWith(objective.targetNPC))
        {
            newAmount = objective.requireAmount;
        }

        progressDictionary[objective] = newAmount;
    }

    public int GetCurrentAmount(QuestSO questSO, QuestObjective objective)
    {
        if(questProgress.TryGetValue(questSO, out var objectiveDictionary))
        {
            if(objectiveDictionary.TryGetValue(objective, out int amount))
            {
                return amount;
            }
        }
        return 0;
    }
}
