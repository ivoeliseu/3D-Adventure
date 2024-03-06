using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    
    public List<GunBase> gunBase;
    public Transform gunPosition;
    public KeyCode gun1 = KeyCode.R;
    public KeyCode gun2 = KeyCode.T;
    public KeyCode gun3 = KeyCode.Y;
    
    private GunBase _currentGun;
    protected override void Init()
    {
        base.Init();
        CreateGun(0);
        

        //Se foi performado a ação do input, adiciona um callback para ele.
        //Nesse exemplo, chama Shoot();
        inputs.Gameplay.Shoot.performed += ctx => StartShoot();
        //Nesse exemplo, quando cancela (para de apertar o input), chama CancelShoot();
        inputs.Gameplay.Shoot.canceled += ctx => CancelShoot();
    }

    private void Update()
    {
        CheckGun();
    }

    private void CheckGun()
    {
        if (Input.GetKeyDown(gun1))
        {
            CreateGun(0);
        } 
        else if (Input.GetKeyDown(gun2))
        {
            CreateGun(1);
        }
        else if (Input.GetKeyDown(gun3))
        {
            CreateGun(2);
        }
    }

    private void CreateGun(int gunIndex)
    {
        if (_currentGun != null) Destroy(_currentGun);

        _currentGun = Instantiate(gunBase[gunIndex], gunPosition);

        _currentGun.transform.localPosition = _currentGun.transform.localEulerAngles = Vector3.zero;
    }
    private void StartShoot()
    {
        Debug.Log("Shoot");
        _currentGun.StartShoot();
    }

    private void CancelShoot()
    {
        Debug.Log("Cancel");
        _currentGun.StopShoot();
    }
}
