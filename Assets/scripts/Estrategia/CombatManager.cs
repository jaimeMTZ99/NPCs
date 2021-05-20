using UnityEngine;

public static class CombatManager {

    // Initialized from the GUIKillFeed itself so this class continues to stay static
    //public static GUIKillFeed GuiKillFeed;
    
    public static void AtaqueMelee(NPC attacker, NPC target) {
        int crit = Random.Range(0, 51);
        if (crit == 50) {
           // Debug.Log("El jugador " + attacker.name +" ha golpeado con un ataque crítico a " + target.name);
            // Critical attack
            target.health -= attacker.meleeDamageCrit;
            //GUIManager.TriggerAnimation(target.CriticalHitAnimator);
            if (target.health == 0 && !target.IsDead){}
               // GuiKillFeed.AddKill(attacker, target, true, true);
        }
        else {
            // Debug.Log("El jugador " + attacker.name +" ha golpeado con un ataque básico a " + target.name);
            // Default attack
            target.health -= attacker.meleeDamage;
            if (target.health == 0 && !target.IsDead){}
                //GuiKillFeed.AddKill(attacker, target, true, false);
        }
    }

    public static void AtaqueRango(NPC attacker, NPC target) {
        int crit = Random.Range(0, 51);
        if (crit == 50) {
            // Debug.Log("El jugador " + attacker.name +" ha golpeado con un ataque crítico a " + target.name);
            // Critical attack
            if (target.team == attacker.team) {
                target.health += attacker.rangedDamageCrit;
            }
            else {
                target.health -= attacker.rangedDamageCrit;
            }
            
            //GUIManager.TriggerAnimation(target.CriticalHitAnimator);
            if (target.health == 0 && !target.IsDead){}
                //GuiKillFeed.AddKill(attacker, target, false, true);
        }
        else {
            // Debug.Log("El jugador " + attacker.name +" ha golpeado con un ataque básico a " + target.name);
            // Default attack
            if (target.team == attacker.team) {
                target.health += attacker.rangedDamage;
            }
            else {
                target.health -= attacker.rangedDamage;
            }
            if (target.health == 0 && !target.IsDead){}
                //GuiKillFeed.AddKill(attacker, target, false, false);
        }
    }

    public static void Restart() {
        //GuiKillFeed.Restart();
    }

}