using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObejctPool : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]GameObject EnemyShip;
    [SerializeField][Range(0,50)]int poolSize = 5;
    [SerializeField][Range(0.1f,30f)]float InstantiateInterval = 2f;
    GameObject[] pool;

    void Awake() {
            PopulatePool();
            
    }
    void Start()
    {
        StartCoroutine(Spawnenemy());
       
    }
void PopulatePool()
{
    pool = new GameObject[poolSize];
    for (int i = 0; i < pool.Length; i++)
    {
        Vector3 spawnPosition = new Vector3(transform.position.x,3.5f, transform.position.z);

        pool[i] = Instantiate(EnemyShip, spawnPosition, Quaternion.identity);
        pool[i].SetActive(false);

        // Debug.Log($"Instantiated at position: {pool[i].transform.position}");
    }
}

void SpawnObjectInPool(){
    for(int i =0; i < pool.Length; i++){
        if(pool[i].activeInHierarchy == false){
            pool[i].SetActive(true);
            return;
        }
    }
}

IEnumerator Spawnenemy(){
    while(true){
        SpawnObjectInPool();
        yield return new WaitForSeconds(InstantiateInterval);
    }
}

    
}
