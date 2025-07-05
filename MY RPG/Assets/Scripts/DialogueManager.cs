using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour
{
    public GameObject dialogObj;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI speechText;
    public float typingSpeed;
    private string[] sentences;
    private int index;
    public void Speech(string nametxt, string[] speechtxt)
    {
        dialogObj.SetActive(true);
        nameText.text = nametxt;
        sentences = speechtxt;
        StartCoroutine(TypeSentence());
    }
    IEnumerator TypeSentence()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
    public void NextSentence()
    {
        if(speechText.text == sentences[index])
        {
            if (index < sentences.Length - 1)
            {
                index++;
                speechText.text = "";
                StartCoroutine(TypeSentence());
            }
            else
            {
                speechText.text = "";
                index = 0;
                dialogObj.SetActive(false);
            }
        }
    }
}
