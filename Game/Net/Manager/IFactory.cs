namespace Net.Manager
{
    public interface IFactory<M> where M : Manager
    {
        M Create();
    }
}