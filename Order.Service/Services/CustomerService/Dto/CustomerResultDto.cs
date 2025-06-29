namespace Order.Service.Services.CustomerService.Dto
{
    public class CustomerResultDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }


        //string[] GetUserName(string Email)
        //{
        //    var userName = Email.ToLower().Split('@');

        //    return userName;
        //}
    }
}
