using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyscript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]int goldReward = 25;
    [SerializeField]int goldPenalty = 25;
    BankSystem bank;
    void Start()
    {
        bank = FindObjectOfType<BankSystem>();
    }

    // Update is called once per frame
   public void RewardGold(){
    if(bank == null){ return; }
    bank.Deposit(goldReward);
   }

   public void PenaltyGold(){
    if(bank == null){ return; }
    bank.Withdrawal(goldPenalty);
   }
}
 