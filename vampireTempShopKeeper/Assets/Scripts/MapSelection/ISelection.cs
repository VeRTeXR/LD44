using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ISelection : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{

    public Animator Animator;
    public bool IsDisabled;

    public virtual void Setup(LevelConfiguration.State state, int currentDate)
    {
        throw new System.NotImplementedException();
    }
    
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

}
