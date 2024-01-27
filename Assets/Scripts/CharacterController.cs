using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    [SerializeField] private Sprite Normal;
    [SerializeField] private Sprite Angry;
    [SerializeField] private Sprite Rage;
    [SerializeField] private Sprite Smile_small;
    [SerializeField] private Sprite Smile_large;
    [SerializeField] private Sprite Laugh;

    [SerializeField] private SpriteRenderer Face;

    public CharacterEmote CurrentEmote;

    public enum CharacterEmote
    {
        Normal,
        Angry,
        Rage,
        Smile_small,
        Smile_large,
        Laugh,
    }


    // Start is called before the first frame update
    void Start()
    {
        CurrentEmote = CharacterEmote.Normal;
    }

    // Update is called once per frame
    void Update()
    {
        FaceUpdate();
    }
        
    void FaceUpdate()
    {
        if (CurrentEmote == CharacterEmote.Normal) { Face.sprite = Normal; }
        else if (CurrentEmote == CharacterEmote.Angry) { Face.sprite = Angry; }
        else if (CurrentEmote == CharacterEmote.Rage) { Face.sprite = Rage; }
        else if (CurrentEmote == CharacterEmote.Smile_small) { Face.sprite = Smile_small; }
        else if (CurrentEmote == CharacterEmote.Smile_large) { Face.sprite = Smile_large; }
        else if (CurrentEmote == CharacterEmote.Laugh) { Face.sprite = Laugh; }
    }
}
