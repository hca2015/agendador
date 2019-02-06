namespace Tcc.Entity
{
    public interface IContext
    {
        bool add(Modelo prEntity);

        bool update(Modelo prEntity);

        bool delete(Modelo prEntity);
    }
}
