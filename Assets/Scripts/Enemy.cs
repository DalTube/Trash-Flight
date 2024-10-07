using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;
    // Start is called before the first frame update

    private float minY = -7f;

    [SerializeField]
    private float hp = 1f;

    [SerializeField]
    private GameObject coin;

    public void SetMoveSpeed(float moveSpeed){
        this.moveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        if(transform.position.y < minY){
            Destroy(gameObject);
        }
    }

    //충돌 트리거 
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Weapon"){
            Weapon weapon = other.gameObject.GetComponent<Weapon>();
            hp -= weapon.damage;

            //몹 잡음
            if(hp <= 0){

                //보스 잡으면 게임 끝
                if(gameObject.tag == "Boss"){
                    GameManager.instance.SetGameOver();
                }else{
                    Destroy(gameObject);
                    Instantiate(coin, transform.position, Quaternion.identity);
                }
            }

            Destroy(other.gameObject);
        }   
    }
}
