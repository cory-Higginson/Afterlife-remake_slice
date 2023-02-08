using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Camera))]
public class CameraManager : MonoBehaviour
{
    public float move_speed = 1;
    public float zoom_sensitivity = 1;
    public float shift_click_zoom_step = 20.0f;

    private bool top_down_view = false;
    private float cam_down_tilt;

    private Camera cam;
    private GameObject cam_rig;
    private InputAction move_input;
    private InputAction zoom_input;

    private void Awake()
    {
        // TO BE REMOVED
        Cursor.visible = true;
        // TO BE REMOVED BECAUSE EM'S STUFF DOES IT AND DON'T WANT TO DO TWICE. 
        
        cam = GetComponent<Camera>();
        cam_rig = transform.parent.gameObject;
        cam_down_tilt = cam.transform.localRotation.eulerAngles.x;
    }

    private void Start()
    {
        // Subscribing necessary functions to InputAction delegates.
        InputManager.Instance.my_input_actions.AfterLifeActions.ToggleTopDownView.started += toggleTopDownView;
        InputManager.Instance.my_input_actions.AfterLifeActions.CenterANDZoomIn.started += centreIn;
        InputManager.Instance.my_input_actions.AfterLifeActions.CenterANDZoomOut.started += centreOut;
        InputManager.Instance.my_input_actions.AfterLifeActions.JumpViewToHeaven.started += jumpToHeaven;
        InputManager.Instance.my_input_actions.AfterLifeActions.JumpViewToHell.started += jumpToHell;
        InputManager.Instance.my_input_actions.AfterLifeActions.CentreViewToCursor.started += centreToMouseInputActionFriendly;

        // Store references to the InputActions that's state needs to be used in an update function.
        move_input = InputManager.Instance.my_input_actions.AfterLifeActions.MoveView;
        zoom_input = InputManager.Instance.my_input_actions.AfterLifeActions.Zoom;
    }

    private void toggleTopDownView(InputAction.CallbackContext context)
    {        
        top_down_view = !top_down_view;

        if (!top_down_view)
        {
            cam.transform.Rotate(transform.right, -90 + cam_down_tilt);
        }
        else
        {
            cam.transform.Rotate(transform.right, 90 - cam_down_tilt);
        }
    }

    private void FixedUpdate()
    {
        moveView(move_input.ReadValue<Vector2>());
        ZoomView(zoom_input.ReadValue<Single>());
    }

    private void moveView(Vector2 move_vector)
    {
        if (move_vector.magnitude != 0.0)
        {
            Vector3 move_direction = new Vector3(move_vector.x * move_speed, move_vector.y * move_speed, 0);
            if (top_down_view)
            {
                move_direction = Quaternion.FromToRotation(Vector3.up, cam_rig.transform.forward) * move_direction;
            }
            cam_rig.transform.position += move_direction;
        }
    }

    private void ZoomView(float zoom_step)
    {
        if (zoom_step != 0)
        {
            //cam_rig.transform.position += zoom_step * zoom_sensitivity * cam.transform.forward;
            // Above line is more appropriate for a perspective camera and has no zoom effect on an orthographic camera. 
            cam.orthographicSize += zoom_sensitivity * zoom_step;
        }
    }

    private void centreIn(InputAction.CallbackContext context)
    {
        centreViewToMousePos();
        ZoomView(shift_click_zoom_step);
    }

    private void centreOut(InputAction.CallbackContext context)
    {
        centreViewToMousePos();
        ZoomView(-shift_click_zoom_step);
    }

    private void jumpToHell(InputAction.CallbackContext context)
    {
        // get position of hell from Game Manager and set cam_rig position on x & y to it, z to a comfortable distance.
    }

    private void jumpToHeaven(InputAction.CallbackContext context)
    {
        // get position of heaven from Game Manager and set cam_rig position on x & y to it, z to a comfortable distance.
    }

    public void toggleHell()
    {
        // Toggles using XOR assignment.
        cam.cullingMask ^= 1 << LayerMask.NameToLayer("Hell");
    }

    public void toggleHeaven(InputAction.CallbackContext context)
    {
        cam.cullingMask ^= 1 << LayerMask.NameToLayer("Heaven");
    }

    public void toggleGridTiles()
    {
        cam.cullingMask ^= 1 << LayerMask.NameToLayer("GridTiles");
    }

    public void KarmaStructures()
    {
        cam.cullingMask ^= 1 << LayerMask.NameToLayer("KarmaStructures");
    }

    private void centreViewToMousePos()
    {
        Vector3 cursor_world_pos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (top_down_view)
        {
            cam_rig.transform.position = new Vector3(cursor_world_pos.x, cam_rig.transform.position.y, cursor_world_pos.z);
        }
        else
        {
            cam_rig.transform.position = new Vector3(cursor_world_pos.x, cursor_world_pos.y, cam_rig.transform.position.z);
        }
    }

    private void centreToMouseInputActionFriendly(InputAction.CallbackContext context)
    {
        centreViewToMousePos();   
    }
}
