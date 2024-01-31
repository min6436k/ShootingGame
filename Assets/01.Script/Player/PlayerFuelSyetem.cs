using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFuelSyetem : MonoBehaviour
{
    [HideInInspector]
    public int MaxPlayerFuel = 100;
    public float FuelDecreaseSpeed = 2f;

    public void Initfuel()
    {
        GameInstance.Instance.PlayerFuel = 100;
    }

    private void Update()
    {
        GameInstance.Instance.PlayerFuel -= FuelDecreaseSpeed*Time.deltaTime;
    }

}
