namespace MyToDo.Api.Context
{
    public class MT2_User : MT2_BaseEntity
    {
        public string Account {  get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}
