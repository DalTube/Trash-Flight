using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemis;
    
    [SerializeField]
    private GameObject boss;

    private float[] arrPositionX = {-2.2f, -1.1f , 0f, 1.1f, 2.2f};

    [SerializeField]
    private float spawnInterval = 2f;

    // Start is called before the first frame update
    void Start()
    {
        StartEnemyRoutine();
    }

    void StartEnemyRoutine(){
        StartCoroutine("EnemyRoutine");
    }

    public void StopEnemyRoutine(){
        StopCoroutine("EnemyRoutine");
    }

    IEnumerator EnemyRoutine(){
        yield return new WaitForSeconds(3f);

        int spawnCount = 0;
        int enemyIndex = 0;
        float moveSpeed = 5f;

        while(true){
            foreach(float positionX in arrPositionX){
                // int index = Random.Range(0,enemis.Length);
                SpawnEnemy(positionX,enemyIndex,moveSpeed);
            }

            spawnCount++;

            if(spawnCount % 10 == 0){
                enemyIndex++;
                moveSpeed += 2;
            }

            if(enemyIndex >= enemis.Length){
                SpawnBoss();
                enemyIndex = 0;
                moveSpeed = 5f;
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    //일반몹 소환
    void SpawnEnemy(float positionX, int index, float moveSpeed){
        Vector3 spawnPos = new Vector3(positionX, transform.position.y, transform.position.z);
        
        if(Random.Range(0,5) == 0){
            index += 1;
        }

        if(index >= enemis.Length){
            index = enemis.Length - 1;
        }

        GameObject enemyObject = Instantiate(enemis[index], spawnPos, Quaternion.identity);
        Enemy enemy = enemyObject.GetComponent<Enemy>();
        // Enemy enemy = new Enemy();
        enemy.SetMoveSpeed(moveSpeed);
    }

    //보스 소환
    void SpawnBoss(){
        Instantiate(boss, transform.position,Quaternion.identity);
    }

}
