using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitGame : MonoBehaviour
{
    public GameObject[] fruitPrefabs;

    public float[] fruitSizes = { 0.5f, 0.7f, 0.9f, 1.1f, 1.3f, 1.5f, 1.7f, 1.9f };

    public GameObject currentFruit;
    public int currentFruitType;

    public float gameWidth = 5.0f;
    public float fruitStartHeight = 6.0f;
    public float gameHeight;
    public bool isGameOver = false;
    public Camera mainCamera;

    public float fruitTimer;


    private void Start()
    {
        mainCamera = Camera.main;
        SpawnNewfruit();
        fruitTimer = -3.0f;
        gameHeight = fruitStartHeight + 0.5f;
    }

    private void Update()
    {
        if(isGameOver) return;

        if (currentFruit != null)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = mainCamera.WorldToScreenPoint(currentFruit.transform.position).z; // �Ÿ� ����
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

            Vector3 newPosition = currentFruit.transform.position;
            newPosition.x = worldPosition.x;

            float halfFruitSize = fruitSizes[currentFruitType] / 2;
            if (newPosition.x < -gameWidth / 2 + halfFruitSize)
            {
                newPosition.x = -gameWidth / 2 + halfFruitSize;
            }
            if (newPosition.x > gameWidth / 2 + halfFruitSize)
            {
                newPosition.x = gameWidth / 2 + halfFruitSize;
            }

            currentFruit.transform.position = newPosition;

            if (fruitTimer >= 0)
            {
                fruitTimer -= Time.deltaTime;
            }

            if(fruitTimer < 0 && fruitTimer > -2)
            {
                CheckGameOver();
                SpawnNewfruit();
                fruitTimer = -3.0f;
            }

            if (Input.GetMouseButtonDown(0) && fruitTimer == -3.0f)
            {
                DropFruit();
            }
        }
    }

    void DropFruit()
    {
        Rigidbody2D rb = currentFruit.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 1f;
            currentFruit = null;
            fruitTimer = 1.0f;
        }

    }
    void SpawnNewfruit()
    {
        if (!isGameOver)
        {
            currentFruitType = Random.Range(0, 3);

            Vector3 mousePosition = Input.mousePosition;
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

            Vector3 spawnPosition = new Vector3(worldPosition.x,fruitStartHeight,0);

            float halfFruitSize = fruitSizes[currentFruitType] / 2;

            spawnPosition.x = Mathf.Clamp(spawnPosition.x, -gameWidth / 2 + halfFruitSize, gameWidth /2 - halfFruitSize);

            currentFruit = Instantiate(fruitPrefabs[currentFruitType],spawnPosition, Quaternion.identity);
            currentFruit.transform.localScale = new Vector3(fruitSizes[currentFruitType], fruitSizes[currentFruitType], 1);

            Rigidbody2D rb = currentFruit.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale = 0f;
            }
        }
    }

    public void MergeFruits(int fruitType, Vector3 position)
    {
        if (fruitType < fruitPrefabs.Length - 1)
        {
            GameObject newFruit = Instantiate(fruitPrefabs[fruitType + 1], position, Quaternion.identity);
            newFruit.transform.localScale = new Vector3(fruitSizes[fruitType + 1], fruitSizes[fruitType + 1], 1.0f);
        }
    }

    public void CheckGameOver()
    {
        Fruit[] allFruits = FindObjectsOfType<Fruit>();
        float gameOverHeight = gameHeight;

        for (int i = 0; i < allFruits.Length; i++)
        {
            if (allFruits[i] != null)
            {
                Rigidbody2D rb = allFruits[i].GetComponent<Rigidbody2D>();

                if (rb != null && rb.velocity.magnitude < 0.1f && allFruits[i].transform.position.y > gameOverHeight)
                {
                    isGameOver = true;
                    Debug.Log("���� ����");

                    break;
                }
            }
        }
    }
}
