using UnityEngine;

namespace Modul_17
{
    public interface IAction
    {
        // Нужен просто для отображения связи остальных интерфейсов. Можно его удалить. Хз как лучше.
    }

    public interface ISimpleMovable : IAction
    {
        public Vector3 GetDirectionToMove();
    }

    public interface ITargetMovable : IAction
    {
        public Vector3 GetDirectionToMove(GameObject target);
    }

    public interface IEffectAction : IAction
    {
        public void Action(ParticleSystem particle);
    }
}