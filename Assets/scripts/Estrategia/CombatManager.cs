using UnityEngine;

public static class CombatManager {

    // Initialized from the GUIKillFeed itself so this class continues to stay static
    //public static GUIKillFeed GuiKillFeed;
    
    public static void AtaqueMelee(NPC attacker, NPC target) {
        int crit = Random.Range(0, 51);
        if (crit == 50) {
            // Critical attack
            target.health -= attacker.meleeDamageCrit;
            //GUIManager.TriggerAnimation(target.CriticalHitAnimator);
            if (target.health == 0 && !target.IsDead){}
               // GuiKillFeed.AddKill(attacker, target, true, true);
        }
        else {
            // Default attack
            target.health -= attacker.meleeDamage;
            if (target.health == 0 && !target.IsDead){}
                //GuiKillFeed.AddKill(attacker, target, true, false);
        }
    }

    public static void AtaqueRango(NPC attacker, NPC target) {
        int crit = Random.Range(0, 51);
        if (crit == 50) {
            // Critical attack
            if (target.team == attacker.team) {
                target.health += attacker.rangedDamageCrit * attacker.municionPorTiro;
            }
            else {
                target.health -= attacker.rangedDamageCrit * attacker.municionPorTiro;
            }
            attacker.municionActual -= attacker.municionPorTiro;
            //GUIManager.TriggerAnimation(target.CriticalHitAnimator);
            if (target.health == 0 && !target.IsDead){}
                //GuiKillFeed.AddKill(attacker, target, false, true);
        }
        else {
            // Default attack
            if (target.team == attacker.team) {
                target.health += attacker.rangedDamage * attacker.municionPorTiro;
            }
            else {
                target.health -= attacker.rangedDamage * attacker.municionPorTiro;
            }
            attacker.municionActual -= attacker.municionPorTiro;
            if (target.health == 0 && !target.IsDead){}
                //GuiKillFeed.AddKill(attacker, target, false, false);
        }
    }

    public static void Restart() {
        //GuiKillFeed.Restart();
    }

}