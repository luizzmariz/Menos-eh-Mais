using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkins : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    public Sprite currentSpriteUp;
    public Sprite currentSpriteLeft;
    public Sprite currentSpriteRight;
    public Sprite currentSpriteDown;
    private int currentSpriteSet = 0;
    
    void Start()
    {
        currentSpriteUp = sprites[0];
        currentSpriteLeft = sprites[1];
        currentSpriteRight = sprites[2];
        currentSpriteDown = sprites[3];
    }
    
    public void ChangeSprites()
    {
        if (currentSpriteSet < 2)
        {
            currentSpriteSet++;
            currentSpriteUp = sprites[0+4*currentSpriteSet];
            currentSpriteLeft = sprites[1+4*currentSpriteSet];
            currentSpriteRight = sprites[2+4*currentSpriteSet];
            currentSpriteDown = sprites[3+4*currentSpriteSet];
        }
    } 
}
