using GameLogic.Attributes;
using Net.Clients;
using System;
using WPFApplication.Extensions;
using WPFApplication.Model;

namespace WPFApplication.ViewModel
{
    public class NightScreenFactory : IScreenFactory
    {
        private IClient client;

        public NightScreenFactory(IClient client)
        {
            this.client = client;
        }

        public ScreenState Create(RoleVisual role, bool isAlive)
        {
            if(!isAlive) return new DeadNightScreenState(client, role);
            else switch(role.GetExecutorType())
                {
                    case ExecutorType.NONE:
                        return new NonNightScreenState(client, role);
                    case ExecutorType.TARGET:
                        return new TNightScreenState(client, role);
                    case ExecutorType.TARGET_TARGET:
                        return new TTNightScreenState(client, role);
                    case ExecutorType.EXECUTOR_TARGER:
                        return new ETNightScreenState(client, role);
                    default:
                        throw new ArgumentNullException(
                            "Role value does not have executor type attribute",
                            nameof(role));
                }
        }
    }
}