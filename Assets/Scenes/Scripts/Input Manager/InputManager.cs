 using Unity.VisualScripting;
 using UnityEngine;

 public class InputManager : Singleton<InputManager>
 {
     public MyInputActions my_input_actions;

     protected override void Awake()
     {
         base.Awake();
         my_input_actions = new MyInputActions();
     }

     private void OnEnable()
     {
         my_input_actions.Enable();
         my_input_actions.AfterLifeActions.Enable();
     }

     private void OnDisable()
     {
         my_input_actions.Disable();
     }
 }

