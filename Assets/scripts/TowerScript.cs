using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    // Start is called before the first frame update
  [SerializeField]int cost = 75 ;
  public bool CreateTower(TowerScript tower, Vector3 position){

    BankSystem bank = FindObjectOfType<BankSystem>();
    if(bank == null){
        // Debug.Log("banl not found");
        return false;
    }
        if(bank.CurrentBalance >= cost){
            Instantiate(tower,position,Quaternion.identity);
            bank.Withdrawal(cost);
            // Debug.Log("balance is now " + bank.CurrentBalance);
            return true;
        }
        return false;
  }
}
