using UnityEngine;

public class InstantiateFood : MonoBehaviour
{
    public GameObject potato;


    public GameObject GetFood (int ID)
    {
        GameObject go = null;

        switch (ID)
        {
            case 100:
                go = potato;
                break;
            default:
                
                break; 
        }
        return (go);

    }

}
