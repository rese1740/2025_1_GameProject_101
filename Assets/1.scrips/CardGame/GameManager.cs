using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{

    //������ ���ҽ�
    public GameObject cardPrefab;
    public Sprite[] cardImages;
    //���� ���
    public Transform deckArea;
    public Transform handArea;
    //UI ���
    public Button drawButton;
    public TextMeshProUGUI deckCountText;
    //���� ��
    public float cardSpacing = 2.0f;
    public int maxHandSize = 6;

    //�迭 ����
    public GameObject[] deckCards;
    public int deckcount;

    public GameObject[] handCards;
    public int handCount;

    //�̸� ���ǵ� �� ī�� ��� (���ڸ�)
    public int[] prefedinedDeck = new int[]
    {
        1,1,1,1,1,1,1,1, //1�� 8�� 
        2,2,2,2,2,2,     //2�� 6��
        3,3,3,3,    //3�� 4��
        4,4   //4�� 2�� 
    };

    // Start is called before the first frame update
    void Start()
    {
        //�迭 �ʱ�ȭ
        deckCards = new GameObject[prefedinedDeck.Length];
        handCards = new GameObject[maxHandSize];

        InitializeDeck();
        ShuffleDeck();

        if(drawButton != null)
        {
            drawButton.onClick.AddListener(OnDrawButtonClicked);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //�� ����

    void ShuffleDeck()        //Fisher-Yates ���� �˰���
    {
        for (int i = 0; i < deckcount - 1; i++)
        {
            int j = Random.Range(i, deckcount);
            //�迭 �� ī�� ��ȯ
            GameObject temp = deckCards[i];
            deckCards[i] = deckCards[j];
            deckCards[j] = temp;
        }
    }


    //�� �ʱ�ȭ - ������ ī�� ����
    private void InitializeDeck()
    {
        deckcount = prefedinedDeck.Length;

        for (int i = 0; i < prefedinedDeck.Length; i++)
        {
            int value = prefedinedDeck[i];

            int imageIndex = value - 1;
            if(imageIndex >= cardImages.Length || imageIndex < 0)
            {
                imageIndex = 0; 
            }

            //ī�� ������Ʈ ���� (����ġ)
            GameObject newCardObj = Instantiate(cardPrefab, deckArea.position, Quaternion.identity);
            newCardObj.transform.SetParent(deckArea);
            newCardObj.SetActive(false);

            Card cardComp = newCardObj.GetComponent<Card>();
            if (cardComp != null )
            {
                cardComp.InitCard(value, cardImages[imageIndex]);
            }
            deckCards[i] = newCardObj;

        }

    }




    public void ArrangeHand()
    {
        if (handCount == 0)
            return;

        float startX = -(handCount - 1) * cardSpacing / 2;

        for (int i = 0; i <handCount; i++)
        {
            if (handCards[i] != null)
            {
                Vector3 newPos = handArea.position + new Vector3(startX + i * cardSpacing, 0, -0.005f);
                handCards[i].transform.position = newPos;
            }

        }

    }

    void OnDrawButtonClicked()
    {
        DrawCardToHand();
    }

    public void DrawCardToHand()
    {
        if (handCount >= maxHandSize)
        {
            Debug.Log("���а� ���� á���ϴ�.!");
            return;
        }

        if (deckcount <= 0)
        {
            Debug.Log("���� �� �̻� ī�尡 �����ϴ�.");
            return;
        }

        GameObject drawnCard = deckCards[0];

        for (int i = 0; i < deckcount -1; i++) 
            {
            deckCards[i] = deckCards[i + 1];
            }
        deckcount--;

        drawnCard.SetActive(true);
        handCards[handCount] = drawnCard;
        handCount++;

        drawnCard.transform.SetParent(handArea);

        ArrangeHand();
    }

   
}
