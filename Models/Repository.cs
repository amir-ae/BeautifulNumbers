namespace BeautifulNumbers.Models
{
    public static class Repository
    {
        public static Response Response { get; set; }
        public static void SaveResponse(Response response)
        {
            Response = response;
        }
    }
}