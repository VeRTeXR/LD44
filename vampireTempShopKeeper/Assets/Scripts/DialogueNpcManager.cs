using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNpcManager : MonoBehaviour
{
    private SpriteRenderer _characterSprite;
    private Animator _animator;

    void Start()
    {
        _characterSprite = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    public void SetCharacter(LevelConfiguration.Dialogue key)
    {
        switch (key)
        {
            case LevelConfiguration.Dialogue.SleepInCoffin :
            {
                Debug.LogError("sleep");
                break;
            }
        }
    }

    private void SetAnimator()
    {
    }
}
