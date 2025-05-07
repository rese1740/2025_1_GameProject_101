using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{
    public int cardValue;                          //ī�� �� (ī�� �ܰ�)
    public Sprite cardImage;                      //ī�� �̹���
    public TextMeshPro cardText;                 //ī�� �ؽ�Ʈ

    //ī�� ���� �ʱ�ȭ �Լ�

    public void InitCard(int calue, Sprite image)
    {
        cardValue = calue;
        cardImage = image;

        //ī�� �̹��� ����
        GetComponent<SpriteRenderer>().sprite = image;

        //ī�� �ؽ�Ʈ ���� (�ִ°��)
        if (cardText != null)
        {
            cardText.text = cardValue.ToString();
        }
        
    }


   
}
