namespace EmailSender.Core.Domain.Entities
{
    public class ContactGroupMaster
    {
        public short Id { get; set; }
        public string? GroupName { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsRemoved { get; set; }
    }
}