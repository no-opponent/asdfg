using UnityEngine;

public class InstantiateMinerals : MonoBehaviour
{
    public GameObject copperN;
    public GameObject silverN;
    public GameObject goldN;
    public GameObject diamond;
    public GameObject gemstone_01;
    public GameObject gemstone_02;
    public GameObject gemstone_03;
    public GameObject rock_gems_01;
    public GameObject rock_gems_02;
    public GameObject rock_gems_03;
    public GameObject rock_gems_04;


    public GameObject GetMinerals(int ID)
    {
        GameObject go = null;

        switch (ID)
        {
            case 200:
                go = copperN;
                break;
            case 201:
                go = silverN;
                break;
            case 202:
                go = goldN;
                break;
            case 203:
                go = diamond;
                break;
            case 204: // red        common
            case 205: // green      common
                go = gemstone_01;
                break;
            case 206: // purple     uncommon
            case 207: // cyan       uncommon
                go = gemstone_02;
                break;
            case 208: // black      legendary
            case 209: // pink       rare
                go = gemstone_03;
                break;
            case 210: // Mineable rock for gems - size 1
                go = rock_gems_01;
                break;
            case 211: // Mineable rock for gems - size 2
                go = rock_gems_02;
                break;
            case 212: // Mineable rock for gems - size 3
                go = rock_gems_03;
                break;
            case 213: // Mineable rock for gems - size 4
                go = rock_gems_04;
                break;
            default:

                break;
        }
        return (go);

    }
    
}
