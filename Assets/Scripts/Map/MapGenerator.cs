using UnityEngine;
using System;

/// <summary>
/// Generate a random room using an array of prefabs. 
/// </summary>
/// <returns></returns>
[SerializeField]
public class MapGenerator : MonoBehaviour
{
    public int mapSeed;
    public bool mapOfTheDay;
    public GameObject[] gridPrefabs;
    public int rows, cols;
    public float roomWidth, roomHeight = 50;
    private Room[,] grid;
  
    [Obsolete]
    void Start() => GenerateMap(); // generate the map on start
    
    // get random prefabs between 0 and the range using the length of the gridPrefabs we will generate soon
    public GameObject RandomRoomPrefab() => gridPrefabs[UnityEngine.Random.Range(0, gridPrefabs.Length)];

    // convert the current date to an integer. 
    public int DateToInt(DateTime dateToUse) => dateToUse.Year + dateToUse.Month + dateToUse.Day + dateToUse.Hour + dateToUse.Minute + dateToUse.Second + dateToUse.Millisecond;

    [Obsolete]
    public void GenerateMap()
    {
        // clear out the grid
        grid = new Room[cols, rows];

        /*
        If mapOfTheDay is true, the seed value will be different each time the game is played. 
        This can be useful for generating different levels each day.
        If mapOfTheDay is false, the seed value will be fixed, which can be useful for creating consistent levels across different play sessions.
         */
        UnityEngine.Random.seed = !mapOfTheDay ? mapSeed : DateToInt(DateTime.Now);


        for (int currentRow = 0; currentRow < rows; currentRow++)
        {
            for (int currentCol = 0; currentCol < cols; currentCol++)
            {

                #region Generation

                // figure out the x and y position of the tiles. 
                float xPos = roomWidth * currentCol;
                float zPos = roomHeight * currentRow;
                Vector3 newPos = new(xPos, 0, zPos);

                // create map tile
                GameObject tempRoomObj = Instantiate(RandomRoomPrefab(), newPos, Quaternion.identity);

                // set map tile parent
                tempRoomObj.transform.parent = transform;

                // give it a name
                tempRoomObj.name = $"Room_{currentCol}, {currentRow}";

                // get room obj
                Room tempRoom = tempRoomObj.GetComponent<Room>();

                //save it to the grid array
                grid[currentCol, currentRow] = tempRoom;
                #endregion Generation

                #region Doors

                // open doors
                // if bottom row open north door
                switch (currentRow)
                {
                    case 0:
                        tempRoom.doorNorth.SetActive(false);

                        break;
                    default:
                        if (currentRow != rows - 1)
                        {
                            tempRoom.doorNorth.SetActive(false);
                            tempRoom.doorSouth.SetActive(false);
                        }
                        else
                        {
                            tempRoom.doorSouth.SetActive(false);
                        }

                        break;
                }
                switch (currentCol)
                {
                    case 0:
                        tempRoom.doorEast.SetActive(false);

                        break;
                    default:
                        if (currentCol != cols - 1)
                        {
                            tempRoom.doorEast.SetActive(false);
                            tempRoom.doorWest.SetActive(false);
                        }
                        else
                        {
                            tempRoom.doorWest.SetActive(false);
                        }

                        break;
                }

                #endregion Doors
            }
        }
    }
}
