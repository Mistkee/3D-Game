using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform player;
    public float spawnRadius = 10f;
    public int wavesCount = 3;
    public float waveWaitDuration = 180f; // 3 minutes
    public Text waveText;
    public Text timerText;
    private float timer;

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        for (int wave = 1; wave <= wavesCount; wave++)
        {
            DisplayWaveMessage(wave);

            yield return new WaitForSeconds(2f); // Wave delay

            SpawnEnemies(wave * 5);

            yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length == 0); // Enemy check
            if (wave + 1 <= wavesCount)
            {
                StartTimer();

                yield return new WaitForSeconds(waveWaitDuration); // Next wave timer
                StopTimer();
            }
        }
        waveText.text = "All waves completed!";
        Debug.Log("All waves completed!");
    }

    void SpawnEnemies(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 randomPos = player.position + Random.insideUnitSphere * spawnRadius;
            randomPos.y = player.position.y+3;
            Instantiate(enemyPrefab, randomPos, Quaternion.identity);
        }
    }

    void DisplayWaveMessage(int wave)
    {
        waveText.text = "Wave " + wave + " started!";
        Debug.Log("Wave " + wave + " started!");
        StartCoroutine(FadeOutWaveText());
    }
    IEnumerator FadeOutWaveText()
    {
        yield return new WaitForSeconds(5f);
        waveText.text = "";
    }
    void StartTimer()
    {
        timer = waveWaitDuration;
        StartCoroutine(UpdateTimer());
    }

    void StopTimer()
    {
        StopCoroutine(UpdateTimer());
        timerText.text = " ";
    }

    IEnumerator UpdateTimer()
    {
        while (timer > 0)
        {
            timerText.text = "Time left: " + Mathf.FloorToInt(timer)+ "s";
            yield return new WaitForSeconds(1f);
            timer--;
        }
    }
}