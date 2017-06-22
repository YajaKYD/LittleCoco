using UnityEngine;

/// <summary>
/// A Helper Class used in an Animation Event to make it easier to check if an Animation has ended
/// This is used in GameController.DoShufflePuzzle to make sure Shuffle 
/// doesn't start before the SlideIn animation of the GamePanel has ended
/// </summary>
public class AnimationHelper : MonoBehaviour
{
    public bool isVisible = false;

    public void HasEnded()
    {
        isVisible = true;
    }

    public void HasStarted()
    {
        isVisible = false;
    }
}