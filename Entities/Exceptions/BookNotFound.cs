namespace Entities.Exeptions
{
    public sealed class BookNotFound : NotFound
    {
        public BookNotFound(int id)  
            : base($"the book with id : {id} cloud not found")
        {

        }
    }
}
