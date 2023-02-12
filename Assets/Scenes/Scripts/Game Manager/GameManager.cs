using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public CameraManager camera_manager;
    public InputManager input_manager;
    public WorldManager world_manager;
    public EconomyManager economy_manager;
    public advisor_script advisor_manager;
    public SoulManager soul_manager;
    public FileManager file_manager;
}
