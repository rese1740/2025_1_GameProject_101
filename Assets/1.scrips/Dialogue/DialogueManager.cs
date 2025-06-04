using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("UI ��� - Instector���� ����")]
    public GameObject DialoguePanel;                      
    public Image characterImage;                                
    public TextMeshProUGUI characternameText;              
    public TextMeshProUGUI dialogueText;                  
    public Button nextButton;                           

    [Header("�⺻ ����")]
    public Sprite defaultCharacterImage;                   

    [Header("Ÿ���� ȿ�� ����")]
    public float typingSpeed = 0.05f;                   
    public bool skipTypibgOnClick = true;                   

    private DialogueDataSO currentDialogue;                    
    public int currentLineIndex = 0;                           
    private bool isDialogueActive = false;                     
    private bool isTyping = false;                            
    private Coroutine typingCoroutine;                        

    void Start()
    {
        DialoguePanel.SetActive(false);                             
        nextButton.onClick.AddListener(HandleNextInput);         
    }

    void Update()
    {
        if (isDialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            HandleNextInput();                 
        }
    }

    public void StartDialogue(DialogueDataSO dialogue)          
    {
        if (dialogue == null || dialogue.dialogueLines.Count == 0) return;     

        currentDialogue = dialogue;                   
        currentLineIndex = 0;                           
        isDialogueActive = true;                       

        DialoguePanel.SetActive(true);                
        characternameText.text = dialogue.characterName;   

        if (characterImage != null)
        {
            if (dialogue.characterImage != null)
            {
                characterImage.sprite = dialogue.characterImage;       
            }
            else
            {
                characterImage.sprite = defaultCharacterImage;       
            }
        }

        ShowCurrentLine();                                        
    }

    IEnumerator TypeText(string textToText)       
    {
        isTyping = true;                  
        dialogueText.text = "";           

        for (int i = 0; i < textToText.Length; i++)
        {
            dialogueText.text += textToText[i];           
            yield return new WaitForSeconds(typingSpeed);  
        }

        isTyping = false;                 
    }

    private void CompleteTyping()              
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);    
        }

        isTyping = false;                   

        if (currentDialogue != null & currentLineIndex < currentDialogue.dialogueLines.Count)
        {
            dialogueText.text = currentDialogue.dialogueLines[currentLineIndex];
        }
    }

    void ShowCurrentLine()      
    {
        if (currentDialogue != null && currentLineIndex < currentDialogue.dialogueLines.Count)  
        {
            if (typingCoroutine != null)        
            {
                StopCoroutine(typingCoroutine);
            }

            string currentText = currentDialogue.dialogueLines[currentLineIndex];
            typingCoroutine = StartCoroutine(TypeText(currentText));
        }
    }

    public void ShowNextLine()     
    {
        currentLineIndex++;       

        if (currentLineIndex >= currentDialogue.dialogueLines.Count)
        {
            EndDialogue();
        }
        else
        {
            ShowCurrentLine();    
        }
    }

    void EndDialogue()                     
    {
        if (typingCoroutine != null)      
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }

        isDialogueActive = false;          
        isTyping = false;                  
        DialoguePanel.SetActive(false);     
        currentLineIndex = 0;              
    }

    public void HandleNextInput()            
    {
        if (isTyping && skipTypibgOnClick)
        {
            CompleteTyping();              
        }
        else
        {
            ShowNextLine();                     
        }
    }

    public void SkipDialogue()       
    {
        EndDialogue();
    }

    public bool IsDialogueActive()      
    {
        return isDialogueActive;
    }
}
