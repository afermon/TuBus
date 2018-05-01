namespace Entities
{
    public class BaseEntity
    {
        public string GetEntityInformation()
        {
            var dump = ObjectDumper.Dump(this);
            return dump;
        }
    }
}
