using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    
    public Transform mainmenuPanel;
    public Transform gamePanel;
    public Transform gameoverPanel;

    // the Tile Prefab
    public Transform tilePrefab;
    // Use a Image for the Puzzle or only numbers?
    public bool useImage = true;
    bool showNumbers = true;
    // if useImage is true than one of these Images will be used for the puzzle
    public Texture2D[] puzzleImages;
    // the Image that is used for the Puzzle will be sliced into smaller pieces
    Sprite[] slicedPuzzleImage;
    public RectTransform puzzleGrid;

    public Color defaultBackgroundColor;
    public Color[] difficultyBackgroundColors;
    
    // used to check if the puzzle has been solved
    [HideInInspector]
    public Vector2[] comparePosition;
    [HideInInspector]
    public Vector2[] currentPosition;

    int puzzleSize;

    public Text textTime;           // the Text Object that holds the current Time displayed in the GamePanel
    public Text textMoves;          // the Text Object that holds the current Moves displayed in the GamePanel
    public Text textCurrentTime;    // the Text Object that holds the current Time displayed in the GameoverPanel
    public Text textRecordTime;     // the Text Object that holds the record Time displayed in the GameoverPanel
    public Text textCurrentMoves;   // the Text Object that holds the current Moves displayed in the GameoverPanel
    public Text textRecordMoves;    // the Text Object that holds the record Moves displayed in the GameoverPanel

    float startTime;                // when a Game Starts this will be set to the actual Time
    int recordMoves = 9999;
    float recordTime = 60 * 60 * 60;
    [HideInInspector]
    public int currentMoves = 0;
    float currentTime = 0f;
    int difficulty = 0;

    float tileSize = 0f;
    /// <summary>
    /// Tile Size in Local Space used to scale the Tile to fit into the Puzzle panel
    /// </summary>
    public float TileSize
    {
        get
        {
            if (tileSize == 0f)
                return GetTileSize();
            return tileSize;
        }
        private set
        {
            tileSize = value;
        }
    }
    /// <summary>
    /// tileDistance is the actual distance between 2 tiles
    /// this does not match the tilesize because of Canvas scaling, 
    /// the only time both values are identical is when the Screen 
    /// Resolution matches the Canvas reference Resolution
    /// </summary>
    public float TileDistance { get; private set; }
    int lastShuffledIndex = -1;
    float shuffleDuration;

    void Awake()
    {
        Cursor.visible = Application.isEditor || !Application.isMobilePlatform;
        
        if (Instance)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        GameState.State = State.Menu;

        //ShowMenu(gamePanel);
        //HideMenu(gameoverPanel);
        //ShowMenu(mainmenuPanel);
    }

    void GetRecords()
    {
        recordMoves = PlayerPrefs.GetInt(string.Format("[{0}]RecordMoves", difficulty), 9999);
        recordTime = PlayerPrefs.GetFloat(string.Format("[{0}]RecordTime", difficulty), 60*60*60);

        if (recordMoves <= 0) // 0 or less is not possible
            recordMoves = 9999;

        if (recordTime <= 0) // 0 or less is not possible
            recordTime = 60 * 60 * 60;

        if (recordMoves > currentMoves)
            PlayerPrefs.SetInt(string.Format("[{0}]RecordMoves", difficulty), currentMoves);

        if (recordTime > currentTime)
            PlayerPrefs.SetFloat(string.Format("[{0}]RecordTime", difficulty), currentTime);
    }
    
    void Update()
    {
        UpdateLevelTime();
        //UpdateMovesText();
        CheckGameOver();
    }

    /// <summary>
    /// Slice the selected Image into equal smaller pieces
    /// // i.e. if the Image is 300 pixels width and the puzzleSize is 3 than One slice is 100x100
    /// </summary>
    void SliceImage()
    {
        slicedPuzzleImage = new Sprite[puzzleSize * puzzleSize];

        // first we select an Image for the Puzzle
        int theChosenImage = Random.Range(0, puzzleImages.Length);
        
        float imageSize = Mathf.Floor(puzzleImages[theChosenImage].width / puzzleSize);
		Debug.Log ("image width " + puzzleImages [theChosenImage].width);
		Debug.Log ("grid size xMax : " + puzzleGrid.rect.xMax + ", xMin : " + puzzleGrid.rect.xMin + ", yMax : " + puzzleGrid.rect.yMax + ", yMin "
		+ puzzleGrid.rect.yMin);
        
        float x = 0;
        float y = puzzleImages[theChosenImage].width - imageSize;
		for (int i = 0; i < puzzleSize * puzzleSize; i++)
        {
            if (i > 0 && i % puzzleSize == 0)
            {
                y -= imageSize;
                x = 0f;
            }

            slicedPuzzleImage[i] = Sprite.Create(puzzleImages[theChosenImage], 
                    new Rect(x, y, imageSize, imageSize), 
                    new Vector2(0.5f, 0.5f));

            x += imageSize;
        }
    }
    
    /// <summary>
    /// If the Puzzle has been solved show the GameoverPanel
    /// </summary>
    void CheckGameOver()
    {
        if (GameState.State == State.Play)
        {
            if (GameOver())
            {
                GameState.State = State.Menu;

                ShowSolvedImage();

                Invoke("ShowGameoverMenu", 3f);
            }
        }
    }

    void ShowSolvedImage()
    {
        if (!useImage)
            return;
        
        for(int i =  0; i < puzzleGrid.childCount; i++)
        {
            puzzleGrid.GetChild(i).gameObject.SetActive(true);
            puzzleGrid.GetChild(i).GetComponentInChildren<Text>().text = "";
        }
    }

    void ShowGameoverMenu()
    {
        // slide the GameoverPanel in
        HideMenu(mainmenuPanel);
        HideMenu(gamePanel);
        ShowMenu(gameoverPanel);

        SetBackgroundColor(defaultBackgroundColor);

        GetRecords();

        textCurrentMoves.text = "" + currentMoves;
        textRecordMoves.text = "" + recordMoves;
        textCurrentTime.text = currentTime.ToTimeString();
        textRecordTime.text = recordTime.ToTimeString();
    }

    void UpdateMovesText()
    {
        textMoves.text = "Moves " + currentMoves;
    }

    void UpdateLevelTime()
    {
        if (GameState.State != State.Play)
            return;

        if (startTime == 0f)
            startTime = Time.time;

        currentTime = Time.time - startTime;

        // display the time
        //textTime.text = currentTime.ToTimeString();
    }

    bool GameOver()
    {
        // compare stored positions to the positions of the tiles
        // if they match -> solved!
        return comparePosition.SequenceEqual(currentPosition);
    }

    /// <summary>
    /// Sets the difficulty
    /// </summary>
    /// <param name="size">Size of the Puzzle</param>
    /// <param name="shuffleTime">Shuffle duration in seconds</param>
    /// <param name="showNumbers">Show numbers on the Tiles?</param>
    void SetDifficulty(int size, float shuffleTime, bool showNumbers = true)
    {
        puzzleSize = size;
        shuffleDuration = shuffleTime;
        this.showNumbers = showNumbers;
    }

    /// <summary>
    /// Sets the Background Color of the GamePanel
    /// used to match the Background Color to the selected difficulty Button Color
    /// </summary>
    /// <param name="color"></param>
    public void SetBackgroundColor(Color color)
    {
        Camera.main.backgroundColor = color;
    }

    /// <summary>
    /// Starts a new Game with the selected difficulty
    /// This is called from the Mainmenu Buttons OnClick event
    /// </summary>
    /// <param name="difficulty"></param>
    public void StartGame(int difficulty)
    {
        if (GameState.State != State.Menu)
            return;

        GameState.State = State.Shuffle;

        this.difficulty = difficulty;
        
        switch (difficulty)
        {
            case 0:
                SetDifficulty(3, 4f);
                break;

            case 1:
                SetDifficulty(4, 6f);
                break;

            case 2:
                SetDifficulty(5, 8f, false);
                break;
        }
        
        Reset();
        
        if(useImage)
            SliceImage();

        CreatePuzzle();

        // slide the game panel in
		//for test
        //HideMenu(mainmenuPanel);
        //HideMenu(gameoverPanel);
        ShowMenu(gamePanel);

        SetBackgroundColor(difficultyBackgroundColors[difficulty]);

        ShufflePuzzle();
    }

    /// <summary>
    /// Called from Close Button in the GameoverPanel
    /// </summary>
    public void CloseGameoverMenu()
    {
        GameState.State = State.Menu;

        Reset();
        
        HideMenu(gameoverPanel);
        HideMenu(gamePanel);
        ShowMenu(mainmenuPanel);

        SetBackgroundColor(defaultBackgroundColor);
    }

    /// <summary>
    /// Called from the Close Button in the GamePanel
    /// </summary>
    public void ToMainMenu()
    {
        GameState.State = State.Menu;

        Reset();

        HideMenu(gameoverPanel);
        HideMenu(gamePanel);
        ShowMenu(mainmenuPanel);

        SetBackgroundColor(defaultBackgroundColor);
    }

    /// <summary>
    /// Called from the Restart Button in the GamePanel
    /// </summary>
    public void RestartGame()
    {
        GameState.State = State.Menu;
        StartGame(difficulty);
    }

    void ShowMenu(Transform menu)
    {
        menu.GetComponent<Animator>().SetBool("show", true);
    }

    void HideMenu(Transform menu)
    {
        menu.GetComponent<Animator>().SetBool("show", false);
    }

    void Reset()
    {
        StopAllCoroutines();

        lastShuffledIndex = -1;
        startTime = 0f;
        currentMoves = 0;
        //textTime.text = "00:00";
        //textMoves.text = "Moves: 0";
        TileController.isSliding = false;

        // clear the puzzle
        for (int i = 0; i < puzzleGrid.childCount; i++)
        {
            puzzleGrid.GetChild(i).GetComponent<TileController>().StopAllCoroutines();
            Destroy(puzzleGrid.GetChild(i).gameObject);
        }
    }
    
    /// <summary>
    /// Creates the Puzzle
    /// </summary>
    void CreatePuzzle()
    {
        // get the actual Tilesize to take screenresolution into account
        TileSize = GetTileSize();
        
        float x = -(puzzleSize / 2f) + 0.5f;
        float y = -(puzzleSize / 2f) - 0.5f;

        comparePosition = new Vector2[puzzleSize * puzzleSize];
        currentPosition = new Vector2[puzzleSize * puzzleSize];

        int number = 1; // the Number displayed on the tiles
        for (int i = 0; i < puzzleSize * puzzleSize; i++)
        {
            if (i % puzzleSize == 0)
            {
                y += 1f;
                x = -(puzzleSize / 2f) + 0.5f;
            }
            
            // Instantiate new Tile
            Transform tile = Instantiate<Transform>(tilePrefab);
            
            tile.name = string.Format("Number_{0}", number);
            // if we dont use an Image just set the number of the tile
            if (!useImage)
                tile.GetComponentInChildren<Text>().text = number.ToString();
            else
            {
                // want numbers over the Image?
                if (showNumbers)
                {
                    tile.GetComponentInChildren<Text>().text = number.ToString();
                    tile.GetComponentInChildren<Text>().transform.localScale *= 0.75f;
                }
                else
                {
                    tile.GetComponentInChildren<Text>().text = "";
                }
                tile.GetComponentInChildren<Image>().sprite = slicedPuzzleImage[number-1];
                tile.GetComponentInChildren<Image>().color = Color.white;
            }
            // set the size of the tile and its collider
            tile.GetComponent<RectTransform>().sizeDelta = new Vector2(TileSize, TileSize);
            tile.GetComponent<BoxCollider2D>().size = new Vector2(TileSize, TileSize) * 0.9f;
            
            tile.transform.SetParent(puzzleGrid);
            tile.localPosition = new Vector2(x * TileSize, -y * TileSize);
            // sometimes the scaling gets messed up so we make sure the scaling is back to 1,1
            tile.localScale = Vector2.one;
            
            currentPosition[i] = new Vector2(x, y);
            comparePosition[i] = currentPosition[i];

            // we set the last tile to be inactive
            // it will be active when we solved the puzzle to show the full image
            if (number == puzzleSize * puzzleSize)
                tile.gameObject.SetActive(false);

            number++;
            x += 1f;
        }

        TileDistance = GetTileDistance();
    }

    /// <summary>
    /// Shuffles the Puzzle
    /// </summary>
    public void ShufflePuzzle()
    {
        GameState.State = State.Shuffle;

        StartCoroutine(DoShufflePuzzle(shuffleDuration));
    }

    /// <summary>
    /// Shuffles the Puzzle
    /// </summary>
    /// <returns></returns>
    /// <param name="duration">how long to shuffle</param>
    IEnumerator DoShufflePuzzle(float duration)
    {
        yield return new WaitWhile(() => gamePanel.GetComponent<AnimationHelper>().isVisible != true);

        float i = 0;

        while (i < duration)
        {
            if (!TileController.isSliding)
            {
                int index = Random.Range(1, puzzleSize * puzzleSize);
                
                // only move the selected Tile if it hasn't been moved previously
                if (index != lastShuffledIndex)
                {
                    Transform go = puzzleGrid.Find("Number_" + index);
                    if(go.GetComponent<TileController>().Slide(48))
                        lastShuffledIndex = index; // update lastShuffledIndex if the tile has been successfully moved
                }
            }

            i += Time.deltaTime;

            yield return null;
        }

        // wait a bit and change state to Play
        yield return new WaitForSeconds(0.1f);

        GameState.State = State.Play;

        yield return 0;
    }

    /// <summary>
    /// Gets the distance between 2 tiles in World space
    /// </summary>
    /// <returns></returns>
    float GetTileDistance()
    {
        TileController[] tiles = GameObject.FindObjectsOfType<TileController>();

        float smallest = 0;
        float largest = 0;

        for (int i = 0; i < tiles.Length; i++)
        {
            // instantiate smallest and largest to compare to
            if (i == 0)
            {
                smallest = tiles[i].transform.position.x;
                largest = tiles[i].transform.position.x;
            }

            // find the smallest x position
            if (tiles[i].transform.position.x < smallest)
                smallest = tiles[i].transform.position.x;

            // find the largest x position
            if (tiles[i].transform.position.x > largest)
                largest = tiles[i].transform.position.x;
        }

        return (largest - smallest) / (puzzleSize - 1);
    }

    /// <summary>
    /// Get the Tilesize in local space
    /// </summary>
    /// <returns></returns>
    float GetTileSize()
    {
		Debug.Log ("puzzle grid size " + puzzleGrid.sizeDelta.x);
        return puzzleGrid.sizeDelta.x / puzzleSize;
    }
}