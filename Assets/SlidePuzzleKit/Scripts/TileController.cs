using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

/// <summary>
/// Controlls the Behaviour of a Puzzle Tile
/// Clicking on it 
/// Sliding it 
/// etc.
/// </summary>
public class TileController : MonoBehaviour, IPointerClickHandler
{ 
    // variables for sliding a tile
    private Vector2 startPosition;
    private Vector2 endPosition;
    private float t;
    private Vector2 slideDirection;

    // how fast the tile slides
    private float moveSpeed = 8f;
    public static bool isSliding = false;
    public LayerMask checkLayer;
    
    void Start()
    {
        isSliding = false;
    }

    // overrides the moveSpeed and calls Slide()
    // called from GameController.ShufflePuzzle()
    public bool Slide(float speed)
    {
        float s = moveSpeed;

        moveSpeed = speed;

        bool slided = Slide();

        moveSpeed = s;

        return slided;
    }

    // perform slide if possible
    public bool Slide()
    {
        if (!isSliding)
        {
            slideDirection = new Vector2();

            // Check the 4 directions
            if (CanSlide(Vector2.down))
            {
                slideDirection = Vector2.down;
            }
            else if (CanSlide(Vector2.up))
            {
                slideDirection = Vector2.up;
            }
            else if (CanSlide(Vector2.left))
            {
                slideDirection = Vector2.left;
            }
            else if (CanSlide(Vector2.right))
            {
                slideDirection = Vector2.right;
            }

            // finally slide (if possible)
            if (slideDirection != Vector2.zero)
            {
                StartCoroutine(DoSlide());

                return true;
            }
        }
        return false;
    }
    
    // Test if the selected Tile can slide to the destination
    bool CanSlide(Vector2 direction)
    {
        // Check if the desired position (destination) is inside the grid
        RectTransform grid = GameController.Instance.puzzleGrid;

        // this check has to be against the local position
        Vector2 position = transform.localPosition;
        Vector2 destination = position + direction * GameController.Instance.TileSize;
        
        // if destination is not inside the grid return false
        if (destination.x < grid.rect.xMin ||
            destination.x > grid.rect.xMax ||
            destination.y < grid.rect.yMin ||
            destination.y > grid.rect.yMax)
            return false;

        // now check if there is a tile at the destination
        // this time the check has to be against the world position
        position = transform.position;
        destination = position + direction * GameController.Instance.TileDistance;

        Collider2D coll = Physics2D.OverlapPoint(destination, checkLayer);

        // if there is a collider at the destination return false
        if (coll)
        {
            return false;
        }
        
        // if both checks fail we can move to the destination so return true
        return true;
    }

    IEnumerator DoSlide()
    {
        isSliding = true;
        startPosition = transform.localPosition;
        t = 0;

        endPosition = new Vector2(startPosition.x + System.Math.Sign(slideDirection.x) * GameController.Instance.TileSize,
                                  startPosition.y + System.Math.Sign(slideDirection.y) * GameController.Instance.TileSize);

        while (t < 1f)
        {
            t += Time.deltaTime * moveSpeed;
            transform.localPosition = Vector2.Lerp(startPosition, endPosition, t);
            yield return null;
        }

        // increment Moves Counter
        if (GameState.State == State.Play)
            GameController.Instance.currentMoves++;

        // Update the current "Position"
        int tile = name.Substring("Number_".Length).ToInt() - 1;
        GameController.Instance.currentPosition[tile] += slideDirection; 

        isSliding = false;
        yield return 0;
    }

    // Clicked on this Tile
    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameState.State != State.Play)
            return;

        Slide();
    }
}
