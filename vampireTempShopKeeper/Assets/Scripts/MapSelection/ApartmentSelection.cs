using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ApartmentSelection : ISelection
{
    private int _bloodRestore;
    private ActionCanvasController actionCanvas;

    void Start()
    {
        Animator = GetComponent<Animator>();
        actionCanvas = GetComponentInChildren<ActionCanvasController>();
        if (actionCanvas == null)
            Debug.LogError("actionCanvas not found");
    }


    public override void Setup(LevelConfiguration.State state, int currentDate)
    {
        if (state != LevelConfiguration.State.Day)
        {
            DisableThisEntry();
        }
        else
        {
            var actionList = GenerateActionList();
            actionCanvas.GenerateActions(actionList);
        }
    }

    private List<UnityAction> GenerateActionList()
    {
        var actionList = new List<UnityAction>();
        actionList.Add(SleepInCoffin);
        actionList.Add(CatchUpOnTheNews);
        return actionList;
    }

//
//    SleepInCoffin,
//    MeetingWithLocalGoons,
//    CatchUpOnTheNews,
//    CheckMaterialStock,
//    PrepareTheMerchandise,
//    PactMaking,
//    SaleOperation,
//    HanginWithTheBois,

    private void SleepInCoffin()
    {
        Debug.LogError("Sleeep");
    }

    private void CatchUpOnTheNews()
    {
        Debug.LogError("News");
    }


    private void DisableThisEntry()
    {
        var renderer = gameObject.GetComponent<Renderer>();
        renderer.material.color = Color.black;
        IsDisabled = true;
    }

    private void GoToApartmentSequence()
    {
        GameStateManager.Instance.AddBlood(_bloodRestore);
    }


    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (!IsDisabled)
            Animator.SetBool("Selected", true);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (!IsDisabled)
            Animator.SetBool("Selected", false);
    }


    public override void OnPointerDown(PointerEventData eventData)
    {
        if (!IsDisabled)
        {
//        GoToApartmentSequence();
            ShowAvailableActions();
        }
    }

    private void ShowAvailableActions()
    {
        Debug.LogError("ShowAvailableActions");
//        var actionList = Instantiate();
    }
}
