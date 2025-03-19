using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int PlayerHealth = 100;

    // ù �����ӿ� ȣ��
    void Start()
    {
        PlayerHealth += 100;
    }

    // �� ������ ȣ��
    void Update()
    {
        CheckDeath();
    }
  

    public void CharacterHit(int Damage)
    {
        PlayerHealth -= Damage;
    }

    void CheckDeath()
    {
        if (PlayerHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
