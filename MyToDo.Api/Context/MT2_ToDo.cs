namespace MyToDo.Api.Context
{
    public class MT2_ToDo:MT2_BaseEntity
    {

        public string Title { get; set; }   
        public string Content { get; set; }
        public int Status {  get; set; }

    }
}
