using UnityEngine;
using UnityEngine.EventSystems;

public class PubSelection : ISelection
{
    private LevelConfiguration _levelConf;

    void Start()
    {
        Animator= GetComponent<Animator>();
        _levelConf = GameStateManager.Instance.GetLevelConfigurator();
    }
   
    public override void Setup(LevelConfiguration.State state, int currentDate)
    {
//        Debug.LogError();
    }

    private void StartPubSequence()
    {
//        _levelConf.GoToDialogueSequence(LevelConfiguration.Dialogue.SaleOperation);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        Animator.SetBool("Selected" , true);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        Animator.SetBool("Selected" , false);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        StartPubSequence();
    }
}
