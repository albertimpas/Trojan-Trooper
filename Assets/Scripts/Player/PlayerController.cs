using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Player player;

    public void OnJumpButtonClick()
    {
        player.Jump();
    }
    
    public void OnFireButtonClick()
    {
        player.Fire();
    }

    public void OnReloadButtonClick()
    {
        player.Reload();
    }

    public void OnLeftButtonPress()
    {
        player.StartMovingLeft();
    }

    public void OnLeftButtonRelease()
    {
        player.StopMovingLeft();
    }

    public void OnRightButtonPress()
    {
        player.StartMovingRight();
    }

    public void OnRightButtonRelease()
    {
        player.StopMovingRight();
    }

}
