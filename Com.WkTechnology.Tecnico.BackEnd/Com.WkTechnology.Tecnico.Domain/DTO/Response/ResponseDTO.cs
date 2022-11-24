namespace Com.WkTechnology.Tecnico.Domain.DTO.Response
{
    public class ResponseDTO
    {
        public int TotalRows { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public int HTTPStatusCode { get; set; }
        public bool Successfully { get; set; }
    }
}
