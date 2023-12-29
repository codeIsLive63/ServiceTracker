namespace ServiceTracker.Domain.Entities;

public class Record
{
    public Guid RecordId { get; set; }

    public string Name { get; set; }

    public DateOnly Date {  get; set; }

    public string ReasonDescription { get; set; }

    public ReturnStatus ReturnStatus { get; set; }
}
