using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeButton : MonoBehaviour
{
    // Start is called before the first frame update
    
        public void startGame(){
            SceneManager.LoadScene(1);
        }

        public void startCastleLevel(){
            SceneManager.LoadScene(2);
        }
}
