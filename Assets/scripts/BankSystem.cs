 
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BankSystem : MonoBehaviour
{
    [SerializeField]int startingBalance = 150;
    [SerializeField]int currentBalance;
    [SerializeField]TextMeshProUGUI displayBalance;
    public int CurrentBalance {get {return currentBalance;} }


    private void Awake() {
        currentBalance = startingBalance;
        DisplayBalance();
    }

    void DisplayBalance(){
        displayBalance.text = "GOLD : " + currentBalance;
    }
    
    public void Deposit(int amount){
        currentBalance += Mathf.Abs(amount);
        DisplayBalance();

        if(currentBalance >= 300){
           Invoke("WonGame", 3f);
        }
    }

    public void Withdrawal(int amount){
        currentBalance -= Mathf.Abs(amount);
        DisplayBalance();

        if(currentBalance < 0){
            StopGameplay();
        }
    }

   void StopGameplay()
    {
        
        SceneManager.LoadScene(0);
    }

    void WonGame(){
             SceneManager.LoadScene(0);
    }


}
