using UnityEngine;
using System.Collections;

public class DiagonalGrid : MonoBehaviour {

    public enum OrientationX
    {
        Left,
        Right
    }

    public enum OrientationY
    {
        Up,
        Down
    }

    public OrientationX orientationX = OrientationX.Left;
    public OrientationY orientationY = OrientationY.Down;
    public int maxPerScreen = 3;
    public int cellWidth = 200;
    public int cellHeight = 200;
    public bool repositionNow = false;
    public bool sorted = false;

    // Use this for initialization
	void Start () {
        Reposition();
	}

    void Update()
    {
        if (repositionNow)
        {
            repositionNow = false;
            Reposition();
        }
    }

    /// <summary>
    /// Reposition the child elements
    /// </summary>
    public void Reposition()
    {
        int i = 1;
        int x = 1; //operator for horizontal
        int y = 1; //operator for vertical
        Vector3 newPosition = Vector3.zero;
        x = (orientationX == OrientationX.Left) ? -1 : 1;
        y = (orientationY == OrientationY.Down) ? -1 : 1;

        //Iterate each child on grid
        foreach (Transform child in transform)
        {
            child.localPosition = newPosition; //Move Child
            i++;
            float tempX;
            float tempY;
            if (i > maxPerScreen)
            {
                i = 1; //reset counter
                tempX = 0;
            }else
                tempX = newPosition.x + ((float)(x * cellWidth));
            tempY = newPosition.y + ((float) (y * cellHeight));
            newPosition = new Vector3(tempX, tempY, 0f);
        }
    }
}
