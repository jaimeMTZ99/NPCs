using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfazEstrategia : MonoBehaviour
{

    public bool SpaAttack = false;
    public bool FraAttack = false;
    public bool totalWar = false;

    public GameManager gameManager;


    void Start()
    {
        totalWar = false;
        gameManager.ToggleDefensiveMode(NPC.Equipo.Spain);
        gameManager.ToggleDefensiveMode(NPC.Equipo.France);
    }



    public void ToggleRedMode(bool offensive) {
        if (totalWar) {

            totalWar = false;

            if (FraAttack) {
                gameManager.ToggleOffensiveMode(NPC.Equipo.France);
                
            } else {
                gameManager.ToggleDefensiveMode(NPC.Equipo.France);
            }

            SpaAttack = offensive;

            if (SpaAttack) {

                gameManager.ToggleOffensiveMode(NPC.Equipo.Spain);
                
            } else {
                gameManager.ToggleDefensiveMode(NPC.Equipo.Spain);
                
            }
            
        } else if (SpaAttack != offensive) {
            SpaAttack = offensive;

            if (SpaAttack) {
                gameManager.ToggleOffensiveMode(NPC.Equipo.Spain);
            } else {
                gameManager.ToggleDefensiveMode(NPC.Equipo.Spain);
                
            }
                
        }
    }
    
    public void ToggleBluMode(bool offensive) {
        if (totalWar) {

            totalWar = false;
            if (SpaAttack) {
                gameManager.ToggleOffensiveMode(NPC.Equipo.Spain);
            } else {
                gameManager.ToggleDefensiveMode(NPC.Equipo.Spain);          
            }

            FraAttack = offensive;

            if (FraAttack) {
                gameManager.ToggleOffensiveMode(NPC.Equipo.France);    
            } else {
                gameManager.ToggleDefensiveMode(NPC.Equipo.France);
            }

        } else if (FraAttack != offensive) {
            FraAttack = offensive;

            if (FraAttack) {
                gameManager.ToggleOffensiveMode(NPC.Equipo.France);
                
            } else {
                gameManager.ToggleDefensiveMode(NPC.Equipo.France);
                
            }
                
        }
    }


        public void EnableTotalWarMode() {
        if (totalWar)
            return;
        
        totalWar = true;

        if (SpaAttack){

        }
        else{

        }

        if (FraAttack){

        }

        else{

        }

        gameManager.EnableTotalWar();
    }

    public void Restart() {
        
        gameManager.Restart();
    }
}
