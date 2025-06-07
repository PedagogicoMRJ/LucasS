using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public GameObject leaderboardItem;
    LeaderboardItem[] leaderboardItems;

    void Awake()
    {
        VerticalLayoutGroup leaderboardLayoutGroup = GetComponentInChildren<VerticalLayoutGroup>();
        LapCounter[] carLapCounter = FindObjectsOfType<LapCounter>();
        leaderboardItems = new LeaderboardItem[carLapCounter.Length];
        for (int i=0; i < carLapCounter.Length; i++)
        {
            GameObject leaderboardInfo = Instantiate(leaderboardItem, leaderboardLayoutGroup.transform);
            leaderboardItems[i] = leaderboardInfo.GetComponent<LeaderboardItem>();
            leaderboardItems[i].PositionText($"{i + 1}.");
        }
    }

    public void UpdateList(List<LapCounter>lapCounters)
    {
        for(int i = 0; i<lapCounters.Count; i++)
        {
            leaderboardItems[i].NameText(lapCounters[i].gameObject.name);
        }
    }
   
}
