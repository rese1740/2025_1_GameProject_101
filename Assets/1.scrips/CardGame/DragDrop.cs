using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    public bool isDragging = false;        //�巡������ �Ǻ��ϴ� bool��
    public Vector3 startPosition;     //�巡�� ���� ��ġ
    public Transform startParent;       //�巡�� ���۽� �ִ� ����

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        startParent = transform.parent;

        gameManager = FindObjectOfType<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if(isDragging)              //�巡�� ���̸� ���콺 ��ġ�� ī�� �̵�
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            transform.position = mousePos;
        }
    }

    private void OnMouseDown()     //���콺 Ŭ�� �� �巡�� ����
    {
        isDragging =  true;

        startPosition = transform.position;
        startParent = transform.parent;

        GetComponent<SpriteRenderer>().sortingOrder = 10;
    }

    void OnMouseUp()            //���콺 ��ư ���� ��
    {
        isDragging = false;
        GetComponent<SpriteRenderer>().sortingOrder = 1;

        if (gameManager == null)
        {
        RetrunToOriginalPosition();
            return;
        }
        bool wasInMergeArea = startParent == gameManager.MergeArea;

       
        if (IsOverArea(gameManager.handArea))
        {
            Debug.Log("���� ������ �̵�");
            if (wasInMergeArea)
            {
                Debug.Log(wasInMergeArea);
                for (int i = 0; i < gameManager.mergeCount; i++)
                {
                    if (gameManager.mergeCards[i] == gameObject)
                    {
                        for (int j = i; j < gameManager.mergeCount - 1; j++)
                        {
                            gameManager.mergeCards[j] = gameManager.mergeCards[j + 1];
                        }
                        gameManager.mergeCards[gameManager.mergeCount - 1] = null;
                        gameManager.mergeCount--;

                        transform.SetParent(gameManager.handArea);
                        gameManager.handCards[gameManager.handCount] = gameObject;
                        gameManager.handCount++;

                        gameManager.ArrangeHand();
                        gameManager.ArrangeMerge();
                        break;
                    }
                }
            }
            else
            {
                Debug.Log(wasInMergeArea);
                gameManager.ArrangeHand();
            }
        }
        else if (IsOverArea(gameManager.MergeArea))
        {
            if (gameManager.mergeCount >= gameManager.maxMergeSize)
            {
                RetrunToOriginalPosition();
            }
            else
            {
                gameManager.MoveCardToMerge(gameObject);
            }
        }
        else
        {
            RetrunToOriginalPosition();
        }

        if (wasInMergeArea)
        {
            if(gameManager.mergeButton != null)
            {
                bool canMerge = (gameManager.mergeCount == 2 || gameManager.mergeCount == 3);
                gameManager.mergeButton.interactable = canMerge;
            }
        }
    }

    //���� ��ġ�� ���ư��� �Լ�

    void RetrunToOriginalPosition()
    {
        transform.position = startPosition;
        transform.SetParent(startParent);

        if(gameManager != null)
        {
            if(startParent == gameManager.handArea)
            {
                gameManager.ArrangeHand();
            }
            if(startParent == gameManager.MergeArea)
            {
                gameManager.ArrangeMerge();
            }
        }
    }

    bool IsOverArea(Transform area) //ī�尡 Ư�� ���� ���� �ִ��� Ȯ��
    {
        if (area == null)
        {
            return false;
        }

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
        mousePosition.z = 0; //2D �̱� ������

        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, Vector2.zero); 

        foreach (RaycastHit2D hit in hits) 
        {
            if (hit.collider != null && hit.collider.transform == area) 
            {
                Debug.Log(area.name + " ���� ������");
                return true;
            }
        }

        return false;
    }



}
