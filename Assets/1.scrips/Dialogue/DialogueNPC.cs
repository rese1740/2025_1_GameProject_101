using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNPC : MonoBehaviour
{
    public DialogueDataSO myDialogue;                
    private DialogueManager dialogueManager;            

    // Start
    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();

        if (dialogueManager == null)
        {
            Debug.Log("���̾�α� �Ŵ����� �����ϴ�. ");
        }
    }

    void OnMouseDown()              
    {
        if (dialogueManager == null) return;                
        if (dialogueManager.IsDialogueActive()) return;         
        if (myDialogue == null) return;                      

        dialogueManager.StartDialogue(myDialogue);             
    }
}
