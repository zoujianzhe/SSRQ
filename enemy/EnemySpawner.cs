using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public Wave[] waves;
    public Transform START;
    public  float waveRate=0.3f;

    public static int CountEnemyAlive = 0;

    private Coroutine coroutine;
    void Start()
    {
        coroutine= StartCoroutine(SpawnEnemy());
    }
    public void Stop()
    { 
        StopCoroutine(coroutine);
    }
    IEnumerator SpawnEnemy()
    {
        foreach (Wave wave in waves) 
        {
            for (int i = 0; i < wave.count; i++)
            {
                GameObject.Instantiate(wave.enemyPerfab, START.position, Quaternion.identity);
                CountEnemyAlive++;
                if(i!=wave.count-1)
                    yield return new WaitForSeconds(wave.rate);
            }
            while (CountEnemyAlive > 0)
            {
                yield return 0;
            }
            yield return new WaitForSeconds(waveRate);
        }
        while (CountEnemyAlive > 0)
        {
            yield return 0;
        }
        GaneManager.Instance.Win();
       
    }
}
