//using UnityEngine;

//namespace Modul_17
//{
//    public class BehaviourCreator
//    {
//        public IBehaviour GetIdleBehaviour(IdleBehaviourTypes idleBehaviour, Enemy enemy)
//        {
//            IBehaviour behaviour = null;

//            switch (idleBehaviour)
//            {
//                case IdleBehaviourTypes.Stay:
//                    behaviour = new Stay();
//                    break;
//                case IdleBehaviourTypes.Patrol:
//                    behaviour = new Patrol(enemy.Mover, enemy.Area);
//                    break;
//                case IdleBehaviourTypes.Roam:
//                    behaviour = new Roam(enemy.Mover);
//                    break;
//                default:
//                    Debug.Log("SetBehaviours: Not valid IdleBehaviour");
//                    break;
//            }

//            return behaviour;
//        }

//        public IBehaviour GetActiveBehaviour(ActiveBehaviourTypes activeBehaviour, Enemy enemy)
//        {
//            IBehaviour behaviour = null;

//            switch (activeBehaviour)
//            {
//                case ActiveBehaviourTypes.RunAway:
//                    behaviour = new RunAway(enemy.Mover);
//                    break;
//                case ActiveBehaviourTypes.Pursue:
//                    behaviour = new Pursue(enemy.Mover);
//                    break;
//                case ActiveBehaviourTypes.Die:
//                    behaviour = new Die(enemy.Mover);
//                    break;
//                default:
//                    Debug.Log("SetBehaviours: Not valid ActionBehaviour");
//                    break;
//            }

//            return behaviour;
//        }
//    }
//}