using GameLogic.Attributes;
using System.Linq;
using System.Windows.Media;
using WPFApplication.Extensions;

namespace WPFApplication.Model.PlayerData
{
    public struct CommonPlayerState
    {
        private RoleVisual role;
        private bool isAlive;
        private string nickname;
        private Color nColor;
        private ChatScope mainInputChatScope;

        public RoleVisual Role
        {
            get => role;
            set
            {
                role = value;

                var scope = value.GetChatScopes()
                    .Where(s => s.canWrite)
                    .Select(s => s.scope)
                    .Where(s => s != ChatScope.GENERAL_ALIVE && s != ChatScope.DEAD);

                mainInputChatScope = scope.Any() ? scope.Single() : ChatScope.GENERAL_ALIVE;
            }
        }

        public bool IsAlive
        {
            get => isAlive;
            set => isAlive = value;
        }

        public string Nickname
        {
            get => nickname;
            set => nickname = value;
        }

        public Color NColor
        {
            get => nColor;
            set => nColor = value;
        }

        public ChatScope MainInputChatScope
        {
            get => mainInputChatScope;
        }
    }
}