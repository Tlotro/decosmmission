using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerBase player = collision.gameObject.GetComponent<PlayerBase>();
        player.interactAnimator.SetTrigger("Fade_In");
        if (player != null)
            player.ApproachedObject = this;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerBase player = collision.gameObject.GetComponent<PlayerBase>();
        player.interactAnimator.SetTrigger("Fade_Out");
        if (player != null && player.ApproachedObject == this)
            player.ApproachedObject = null;
    }

    public abstract void Interact();
}
