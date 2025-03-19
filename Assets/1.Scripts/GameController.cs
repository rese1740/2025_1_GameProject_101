using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public float Timer = 1.0f;
    public GameObject Monstergo;
   
    void Update()
    {
        Timer -= Time.deltaTime;

        if (Timer <= 0)
        {
            Timer = 5.0f;

            GameObject Temp = Instantiate(Monstergo);
            Temp.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(-4, 4), 0.0f);
        }

        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                   hit.collider.gameObject.GetComponent<Monster>().CharacterHit(10);
                }
            }
        }
    }
}
