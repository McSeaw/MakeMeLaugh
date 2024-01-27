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
    private Animator Anim;

    private bool TakeOverAnim = false;
    private bool TakeOverEmote = false;

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
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        StateUpdate();
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

    void StateUpdate()
    {
        if (!TakeOverEmote) return;

        float currentTime = LevelManager.Instance._currentTime;
        float timeLimit = LevelManager.Instance._timeLimit;

        if (currentTime < timeLimit * 0.25) { CurrentEmote = CharacterEmote.Angry; }
        else if (currentTime >= timeLimit * 0.25 && currentTime < timeLimit * 0.5) { CurrentEmote = CharacterEmote.Normal; }
        else if (currentTime >= timeLimit * 0.5 && currentTime < timeLimit * 0.75) { CurrentEmote = CharacterEmote.Smile_small; }
        else if (currentTime >= timeLimit * 0.75) { CurrentEmote = CharacterEmote.Smile_large; }
    }


    public void OnTouch()
    {
        Anim.SetBool("Touch", true);
    }
    public void OnTouchEnd()
    {
        Anim.SetBool("Touch", false);
    }


    public void OnReject()
    {
        TakeOverEmote = true;
        CurrentEmote = CharacterEmote.Angry;
        Anim.SetBool("Reject", true);
    }
    public void OnRejectEnd()
    {
        TakeOverEmote = false;
        Anim.SetBool("Reject", false);
    }


    public void OnEvent()
    {
        TakeOverEmote = true;
        CurrentEmote = CharacterEmote.Angry;
    }
    public void OnEventEnd()
    {
        TakeOverEmote = false;
    }


}
