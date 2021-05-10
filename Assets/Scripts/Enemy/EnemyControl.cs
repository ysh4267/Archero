using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//적 종류마다 적용할 코드의 프레임
public class EnemyControl : MonoBehaviour {
    public GameObject enemyCanvasGo;
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {

    }
    private void OnCollisionEnter(Collision collision) {
        if (collision.transform.CompareTag("Potato")) {
            enemyCanvasGo.GetComponent<EnemyHpBar>().Dmg();
        }
    }
}
