using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(Enemyscript))]
public class EnemyHealth : MonoBehaviour
{
    // Start is called before the first frame update
    int currentHealth = 0;
    [SerializeField]int maxHealth = 5;
    [SerializeField]int difficultyRamp = 1;
    [SerializeField]ParticleSystem explosion;
    [SerializeField]ParticleSystem explosion2;

    AudioSource audioSource;

    [SerializeField]AudioClip boom;
    [SerializeField]AudioClip Explosion;

    Enemyscript enemyscript;
    private void OnEnable() {
        currentHealth = maxHealth;
    }

    private void Start() {
        enemyscript = GetComponent<Enemyscript>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame

    void Update(){
        // switchScene();
    }
    private void OnParticleCollision(GameObject other) {
        DecreaseHealth();
         
    }

    void DecreaseHealth(){
        currentHealth--;
        explosion.Play();
        audioSource.PlayOneShot(boom);
        if(currentHealth <= 0){
            explosion2.Play();
            audioSource.PlayOneShot(Explosion);
            maxHealth += difficultyRamp;
            Invoke("DestroyObj",1f);
            
        }

       
       
    }



    void DestroyObj(){
        gameObject.SetActive(false);
        enemyscript.RewardGold();
    }

    // void switchScene(){
    //      if(currentHealth >= 300){
    //         switchScene();
    //     }
    //     SceneManager.LoadScene(0);
    // }
}
