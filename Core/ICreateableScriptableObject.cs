namespace iCare
{
    public interface ICreateableScriptableObject
    {
        void OnCreated();
    }

    public interface IDeletableScriptableObject
    {
        void OnDeleted();
    }
}