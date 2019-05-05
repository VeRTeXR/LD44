using UnityEngine;
using UnityEngine.EventSystems;

public class GhettoSelection : ISelection
{
    
    void Start()
    {
        Animator = GetComponent<Animator>();
    }


    public override void OnPointerEnter(PointerEventData eventData)
    {
        Animator.SetBool("Selected", true);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        
        Animator.SetBool("Selected", false);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}

