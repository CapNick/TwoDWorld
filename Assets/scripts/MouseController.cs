using UnityEngine;
using System.Collections;

public class MouseController : MonoBehaviour {

	public GameObject circleCursor;

	// The world-position of the mouse last frame.
	Vector3 lastFramePosition;
	Vector3 currFramePosition;

	// The world-position start of our left-mouse drag operation
	Vector3 dragStartPosition;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		currFramePosition = Camera.main.ScreenToWorldPoint( Input.mousePosition );
		currFramePosition.z = 0;

		UpdateCursor();
		UpdateCameraMovement();

		// Save the mouse position from this frame
		// We don't use currFramePosition because we may have moved the camera.
		lastFramePosition = Camera.main.ScreenToWorldPoint( Input.mousePosition );
		lastFramePosition.z = 0;
	}

	void UpdateCursor() {
		// Update the circle cursor position
		Tile tileUnderMouse = WorldController.Instance.GetTileAtWorldCoord(currFramePosition);
		if(tileUnderMouse != null) {
			circleCursor.SetActive(true);
			Vector3 cursorPosition = new Vector3(tileUnderMouse.Y, tileUnderMouse.X, 0);
			circleCursor.transform.position = cursorPosition;
		}
		else {
			// Mouse is outside of the valid tile space, so hide the cursor.
			circleCursor.SetActive(false);
		}
	}
		

	void UpdateCameraMovement() {
		// Handle screen panning
		if( Input.GetMouseButton(1) || Input.GetMouseButton(2) ) {	// Right or Middle Mouse Button
			
			Vector3 diff = lastFramePosition - currFramePosition;
			Camera.main.transform.Translate( diff );
			
		}

		Camera.main.orthographicSize -= Camera.main.orthographicSize * Input.GetAxis ("Mouse ScrollWheel");
		Camera.main.orthographicSize = Mathf.Clamp (Camera.main.orthographicSize, 3f, 40f);
	}

}
