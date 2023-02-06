using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Camera))]
public class CameraManager : MonoBehaviour
{
    public float move_speed = 1;
    public float zoom_sensitivity = 1;
    public float shift_click_zoom_step = 5.0f;

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
        
        cam = GetComponent<Camera>();
        cam_rig = transform.parent.gameObject;
        cam_down_tilt = cam.transform.localRotation.eulerAngles.x;
    }

    private void Start()
    {
        InputManager.Instance.my_input_actions.AfterLifeActions.ToggleTopDownView.started += toggleTopDownView;
        InputManager.Instance.my_input_actions.AfterLifeActions.CenterANDZoomIn.started += centerIn;
        InputManager.Instance.my_input_actions.AfterLifeActions.CenterANDZoomOut.started += centerOut;
        InputManager.Instance.my_input_actions.AfterLifeActions.JumpViewToHeaven.started += jumpToHeaven;
        InputManager.Instance.my_input_actions.AfterLifeActions.JumpViewToHell.started += jumpToHell;

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
            cam_rig.transform.position += zoom_step * zoom_sensitivity * cam.transform.forward;
        }
    }

    private void centerIn(InputAction.CallbackContext context)
    {
        Vector3 cursor_world_pos = cam.ScreenToWorldPoint(Input.mousePosition);
        cam_rig.transform.position = new Vector3(cursor_world_pos.x, cursor_world_pos.y, cam_rig.transform.position.z);
        
        ZoomView(shift_click_zoom_step);
    }

    private void centerOut(InputAction.CallbackContext context)
    {
        Vector3 cursor_world_pos = cam.ScreenToWorldPoint(Input.mousePosition);
        cam_rig.transform.position = new Vector3(cursor_world_pos.x, cursor_world_pos.y, cam_rig.transform.position.z);
        
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
}
