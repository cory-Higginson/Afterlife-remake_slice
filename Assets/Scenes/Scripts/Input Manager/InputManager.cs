 using Unity.VisualScripting;
 using UnityEngine;

 public class InputManager : Singleton<InputManager>
 {
     /*
     private static InputManager instance;
     public static InputManager Instance
     {
         get
         {
             if (instance.IsUnityNull())
             {
                 Debug.Log("Game manager is null");
             }
             return instance;
         }
     }
     */
     
     public MyInputActions my_input_actions;

     protected override void Awake()
     {
         /*
         if (instance != null && instance != this)
         {
             Destroy(instance.gameObject);
         }
         else
         {
             instance = this;
         }
         */
         
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

