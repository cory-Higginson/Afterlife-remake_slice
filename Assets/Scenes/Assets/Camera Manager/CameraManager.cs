using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Camera))]
public class CameraManager : MonoBehaviour
{
    public float move_speed = 1;
    public float zoom_sensitivity = 1;

    private bool top_down_view = false;
    private Camera cam;
    private InputAction move_input;
    private InputAction zoom_input;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void Start()
    {
        InputManager.Instance.my_input_actions.AfterLifeActions.ToggleTopDownView.started += toggleTopDownView;
        move_input = InputManager.Instance.my_input_actions.AfterLifeActions.MoveView;
        zoom_input = InputManager.Instance.my_input_actions.AfterLifeActions.Zoom;
    }

    private void toggleTopDownView(InputAction.CallbackContext context)
    {        
        top_down_view = !top_down_view;

        if (!top_down_view)
        {
            transform.Rotate(transform.right, -90);
        }
        else
        {
            transform.Rotate(transform.right, 90);
        }
    }

    private void FixedUpdate()
    {
        Vector3 move_direction = new Vector3(move_input.ReadValue<Vector2>().x * move_speed, move_input.ReadValue<Vector2>().y * move_speed, 0);
        Vector3 mouse_world_pos = cam.ScreenToWorldPoint(Input.mousePosition);
        mouse_world_pos.z = cam.transform.position.z - 100;

        if (!top_down_view)
        {
            move_direction = Quaternion.FromToRotation(Vector3.forward, transform.forward) * move_direction;
            mouse_world_pos = Quaternion.FromToRotation(Vector3.forward, transform.forward) * mouse_world_pos;
        }
        else
        {
            move_direction = Quaternion.FromToRotation(Vector3.up, transform.up) * move_direction;
            mouse_world_pos = Quaternion.FromToRotation(Vector3.up, transform.up) * mouse_world_pos;
        }

        transform.position += move_direction;
        
        if (zoom_input.ReadValue<Single>() != 0)
        {
            transform.position += mouse_world_pos;
        }
    }
}
