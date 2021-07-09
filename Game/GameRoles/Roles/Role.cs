using System;

namespace GameLogic.Roles
{
    /// <summary>
    /// Обобщающий класс для всех ролей. Содержит основные свойства.
    /// </summary>
    public abstract class Role
    {
        protected byte priority;

        public Teams Team { get; protected set; }
        public int Priority { get => priority; }

        public event ActionHandler WasTargeted;

        public abstract void OnWasDied();


    }

    /// <summary>
    /// Перечисление сторон, к которым относятся те или иные роли.
    /// </summary>
    public enum Teams
    {
        CITY,
        MAFIA,
        CULTUS,
        UNDEAD,
        NEUTRAL
    }
}