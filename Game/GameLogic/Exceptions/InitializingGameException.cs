using System.Runtime.Serialization;

namespace GameLogic
{
    [Serializable]
    class InitializingGameException : GameException
    {
        public InitializingGameException()
        {
        }

        public InitializingGameException(string message) : base(message)
        {
        }

        public InitializingGameException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InitializingGameException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}