using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int PlayerHealth = 100;

    // 첫 프레임에 호출
    void Start()
    {
        PlayerHealth += 100;
    }

    // 매 프레임 호출
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
