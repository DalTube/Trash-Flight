using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private GameObject[] weapons;
    private int weaponIndex = 0;

    [SerializeField]
    private Transform shootTransform;

    [SerializeField]
    private float shootInterval = 0.5f;
    private float lastShotTime = 0f;

    // Update is called once per frame
    void Update()
    {
        // float horizontalInput = Input.GetAxisRaw("Horizontal");
        // float verticalInput = Input.GetAxisRaw("Vertical");
        // Vector3 moveTo = new Vector3(horizontalInput, 0f, 0f);
        // transform.position += moveTo * moveSpeed * Time.deltaTime;

        // Vector3 moveTo = new Vector3(moveSpeed * Time.deltaTime, 0,0);
        // if(Input.GetKey(KeyCode.LeftArrow)) {
        //     transform.position -= moveTo;
        // }else if(Input.GetKey(KeyCode.RightArrow)){
        //     transform.position += moveTo;
        // }

        // Debug.Log(Input.mousePosition);

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Debug.Log(Input.mousePosition);

        float toX = Mathf.Clamp(mousePosition.x, -2.35f, 2.35f);
        transform.position = new Vector3(toX,transform.position.y,transform.position.z);

        // transform.position = new Vector3(Input.mousePosition.x,transform.position.y,transform.position.z);

        if(GameManager.instance.isGameOver == false){
            Shoot();
        }
    }

    void Shoot(){
        // 10 - 0 > 0.05f
        // 
        if(Time.time - lastShotTime > shootInterval){
            Instantiate(weapons[weaponIndex],shootTransform.position, Quaternion.identity);
            lastShotTime = Time.time;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss"){
            GameManager.instance.SetGameOver();
            Destroy(gameObject);
        }else if(other.gameObject.tag == "Coin"){
            GameManager.instance.IncreaseCoin();
            Destroy(other.gameObject);
        }
    }

    public void UpgradeWeapon(){
        weaponIndex += 1;
        
        //무기 업글 제한
        if(weaponIndex >= weapons.Length){
            weaponIndex = weapons.Length - 1;
        }

        //미사일 속도 0.1제한
        Debug.Log("shootInterval : " + shootInterval);
        if(shootInterval >= 0.2f){
            shootInterval -= 0.1f;
        }
    }
}
