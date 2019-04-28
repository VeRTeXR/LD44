using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealTracker : MonoBehaviour
{

    private int _dealStartDate;

    private int _dealEndDate;
    private bool _contactFulfilled;
    private bool _isContactInProgress; 

    public void SetDealStartDate(int date)
    {
        _dealStartDate = date;
        _isContactInProgress = true;
    }


    public void SetDealEndDate(int date)
    {
        _dealEndDate = date;
    }

    public bool IsContractInProgress()
    {
        return _isContactInProgress;
    }

    public void ContractFulfilled()
    {
        // money ++, blood - 
        _isContactInProgress = false;
    }

    public void CheckIfContractIsBeingFulfilled(int currentDate)
    {
        if (currentDate >= _dealEndDate && _contactFulfilled == false)
        {
             Debug.LogError("GameOver got fired");    //Game over got fired.   
        }
    }
    

}
