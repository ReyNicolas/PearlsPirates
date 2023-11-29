using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public  class PlayerGenerator
{
    PlayerRespawnGenerator respawnGenerator;
    PositionGenerator positionGenerator;
    InputSetterManager inputSetterManager;
    MatchSO matchData;

    public PlayerGenerator(PlayerRespawnGenerator respawnGenerator, PositionGenerator positionGenerator, InputSetterManager inputSetterManager, MatchSO matchData)
    {
        this.respawnGenerator = respawnGenerator;
        this.positionGenerator = positionGenerator;
        this.inputSetterManager = inputSetterManager;
        this.matchData = matchData;
    }

    public  void  GeneratePlayerFromData(PlayerSO playerData)
    {
        GameObject shipGO;

        if (playerData.InputDevice.Contains(InputsTypesNames.BOT))
        {
            shipGO = GameObject.Instantiate(matchData.botShipPrefab, Vector3.zero, Quaternion.identity);
            SetBotData(playerData, shipGO.GetComponent<IAMoveControlLogic>());
        }
        else
        {
            shipGO = GameObject.Instantiate(matchData.playerShipPrefab, Vector3.zero, Quaternion.identity);
            SetPlayerInput(playerData.InputDevice, shipGO.GetComponent<PlayerInput>());
        }

        SetPlayerDataInShip(playerData, shipGO.GetComponent<PearlCollectorsManager>(), shipGO.GetComponent<ShipMovement>());
        
        SetPositionForMapGameobject(shipGO);
        SetATransformLookToZeroCoord(shipGO.transform);
        
        respawnGenerator.Listen(shipGO.GetComponent<IDestroy>());
    }

    void SetPlayerInput(string inputDevice, PlayerInput playerInput)
        => inputSetterManager.SetPlayerInput(inputDevice, playerInput);
    void SetPositionForMapGameobject(GameObject gameobject)
       => positionGenerator.AssignPosition(gameobject);
    static  void SetPlayerDataInShip(PlayerSO playerData, PearlCollectorsManager collectorsManager, ShipMovement shipMovement)
    {
        collectorsManager.playerData = playerData;
        shipMovement.playerData = playerData;
        shipMovement.GetComponent<SpriteRenderer>().sprite = playerData.shipSprite;
    }
     static void SetBotData(PlayerSO playerData, IAMoveControlLogic iAMoveControlLogic) 
        => iAMoveControlLogic.data = playerData;
    static void SetATransformLookToZeroCoord(Transform aTransform)
       => aTransform.up = -aTransform.position;
   
}

