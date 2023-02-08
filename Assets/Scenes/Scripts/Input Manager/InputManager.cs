 using UnityEngine;

 public class InputManager : MonoBehaviour
 {
     private static InputManager instance;
     
     public static InputManager Instance
     {
         get { return instance;}
     }

     public MyInputActions my_input_actions;

     private void Awake()
     {
         if (instance != null && instance != this)
         {
             Destroy(instance.gameObject);
         }
         else
         {
             instance = this;
         }
         
         my_input_actions = new MyInputActions();

         Debug.Log("created instance");
     }

     private void OnEnable()
     {
         my_input_actions.Enable();
         my_input_actions.AfterLifeActions.Enable();
         my_input_actions.AfterLifeActions.MoveView.Enable();
     }

     private void OnDisable()
     {
         my_input_actions.Disable();
     }
 }

