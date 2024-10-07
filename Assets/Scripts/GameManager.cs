using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private int coin = 0;
    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private GameObject gameOverPanel;

    [HideInInspector]
    public bool isGameOver = false;

    //Start() 보다 먼저 실행
    void Awake() {
        if(instance == null){
            instance = this;
        }
    }

    //코인 증가
    public void IncreaseCoin(){
        coin += 1;
        text.SetText(coin.ToString());

        //코인 획득 수 마다 무기 업그레이드
        if(coin % 1 == 0){ // 30, 60, 90.....
            Player player = FindObjectOfType<Player>();
            if(player != null){
                player.UpgradeWeapon();
            }
        }
    }

    //GmaeOver
    public void SetGameOver(){
        isGameOver = true;

        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
        if(enemySpawner != null){
            enemySpawner.StopEnemyRoutine();
        }

        Invoke("ShowGameOverPanel",2);        
    }

    void ShowGameOverPanel(){
        gameOverPanel.SetActive(true);
    }

    public void PlayAgain(){
        SceneManager.LoadScene("SampleScene");
    }
}
