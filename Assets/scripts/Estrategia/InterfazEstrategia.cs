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
        gameManager.CambiarModoDefensivo(NPC.Equipo.Spain);
        gameManager.CambiarModoDefensivo(NPC.Equipo.France);
    }



    public void CambiarModoSpa(bool offensive) {
        if (totalWar) {

            totalWar = false;

            if (FraAttack) {
                gameManager.CambiarModoOfensivo(NPC.Equipo.France);
                
            } else {
                gameManager.CambiarModoOfensivo(NPC.Equipo.France);
            }

            SpaAttack = offensive;

            if (SpaAttack) {

                gameManager.CambiarModoOfensivo(NPC.Equipo.Spain);
                
            } else {
                gameManager.CambiarModoDefensivo(NPC.Equipo.Spain);
                
            }
            
        } else if (SpaAttack != offensive) {
            SpaAttack = offensive;

            if (SpaAttack) {
                gameManager.CambiarModoOfensivo(NPC.Equipo.Spain);
            } else {
                gameManager.CambiarModoDefensivo(NPC.Equipo.Spain);
                
            }
                
        }
    }
    
    public void CambiarModoFra(bool offensive) {
        if (totalWar) {

            totalWar = false;
            if (SpaAttack) {
                gameManager.CambiarModoOfensivo(NPC.Equipo.Spain);
            } else {
                gameManager.CambiarModoDefensivo(NPC.Equipo.Spain);          
            }

            FraAttack = offensive;

            if (FraAttack) {
                gameManager.CambiarModoOfensivo(NPC.Equipo.France);    
            } else {
                gameManager.CambiarModoDefensivo(NPC.Equipo.France);
            }

        } else if (FraAttack != offensive) {
            FraAttack = offensive;

            if (FraAttack) {
                gameManager.CambiarModoOfensivo(NPC.Equipo.France);
                
            } else {
                gameManager.CambiarModoDefensivo(NPC.Equipo.France);
                
            }
                
        }
    }


    public void TotalWarMode() {
        if (totalWar)
            return;
        totalWar = true;

        if (SpaAttack){
            SpaAttack = false;
        }

        if (FraAttack){
            FraAttack = false;
        }

        gameManager.ModoGuerraTotal();
    }

    public void Restart() {
        
        gameManager.Restart();
    }

    public void CambiarVista() {
        
        gameManager.cambiarVista();
    }
}
