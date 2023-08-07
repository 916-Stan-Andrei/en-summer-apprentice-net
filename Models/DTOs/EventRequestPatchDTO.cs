namespace TMS.Models.DTOs
{
    public class EventRequestPatchDTO
    {
        public int EventId { get; set; }
        public string? Description { get; set; }
        public string? Name{ get; set; }
    }
}
