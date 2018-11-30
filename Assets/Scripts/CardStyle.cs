using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
public class CardStyle : MonoBehaviour {
    public Sprite CardBack;//the back of card
    public SpriteAtlas Fronts;//the atlas of cards
    private Sprite[] CardFront;

    public void initCardStyle(Card[] cards)
    {
        CardFront = new Sprite[Fronts.spriteCount];
        Fronts.GetSprites(CardFront);
        CardBack = Fronts.GetSprite("back");
        for(int i=0;i<cards.Length;i++)
        {
            cards[i].mBackSprite = CardBack;
            cards[i].mFrontSprite = CardFront[cards[i].mNum];
            cards[i].SetSprite();
        }
    }
}
