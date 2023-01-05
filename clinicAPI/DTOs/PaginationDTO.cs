namespace clinicAPI.DTOs
{
    public class PaginationDTO
    {
        public int Page { get; set; } = 1;
        private int recordPerPage = 10;
        private readonly int maxRecordsPerPage = 50;
        public int RecordPerPage
        {
            get
            { 
                return recordPerPage; 
            }
            set
            {
                recordPerPage = (value > maxRecordsPerPage) ? maxRecordsPerPage : value;
            }
        }
    }
}
