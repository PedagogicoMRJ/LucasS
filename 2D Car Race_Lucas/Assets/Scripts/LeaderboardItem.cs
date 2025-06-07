using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardItem : MonoBehaviour
{
    public Text positionText;
    public Text nameText;

    public void PositionText(string newPosition)
    {
        positionText.text = newPosition;
    }

    public void NameText(string newName)
    {
        nameText.text = newName;
    }
    
}
