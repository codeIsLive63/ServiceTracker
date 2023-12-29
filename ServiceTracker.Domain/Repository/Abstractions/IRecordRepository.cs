using ServiceTracker.Domain.Entities;

namespace ServiceTracker.Domain.Repository.Abstractions;

public interface IRecordRepository
{
    IAsyncEnumerable<Record> GetAllRecordsAsync();

    Task<Record> GetRecordByIdAsync(Guid id);

    Task SaveRecordAsync(Record record);

    Task DeleteRecordByIdAsync(Guid id);
}
