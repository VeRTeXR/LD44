using UnityEngine;
using UnityEngine.EventSystems;

public class GhettoSelection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        _animator.SetBool("Selected", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
        _animator.SetBool("Selected", false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
