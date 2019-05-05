using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayIcon : MonoBehaviour
{
    private SpriteRenderer _iconSprite;
    [SerializeField]private Sprite[] _daySprites;

    void Awake()
    {
        _iconSprite = GetComponent<SpriteRenderer>();
    }


    public void SetDaySprite(int date)
    {
        date = date - 1;
        _iconSprite.sprite = _daySprites[date];
    }
}
