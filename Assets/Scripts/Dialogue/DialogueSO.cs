using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string Speaker;
    [TextArea(3, 10)]
    public string Text;
    public AudioClip VoiceClip; // Optional for voice acting
}

[CreateAssetMenu(fileName = "DialogueData", menuName = "Dialogue/DialogueData")]
public class DialogueSO : ScriptableObject
{
    public List<DialogueLine> Lines; // List of all dialogue lines
}
