using Microsoft.EntityFrameworkCore;
using ServiceTracker.Domain.Entities;
using ServiceTracker.Domain.Repository.Abstractions;

namespace ServiceTracker.Domain.Repository.EntityFramework;

public class EFRecordRepository(ApplicationDbContext context) : IRecordRepository
{
    public async Task DeleteRecordByIdAsync(Guid id)
    {
        var record = await GetRecordByIdAsync(id);

        if (record != default)
        {
            context.Records.Remove(record);
            await context.SaveChangesAsync();
        }

        else
        {
            throw new InvalidOperationException($"Запись с идентификатором {id} не найдена");
        }
    }

    public async IAsyncEnumerable<Record> GetAllRecordsAsync()
    {
        await foreach (var record in context.Records)
            yield return record;
    }

    public async Task<Record> GetRecordByIdAsync(Guid id)
    {
        return await context.Records.FirstOrDefaultAsync(r => r.RecordId == id);
    }

    public async Task SaveRecordAsync(Record record)
    {
        if (record.RecordId == default)
            context.Entry(record).State = EntityState.Added;

        else
            context.Entry(record).State = EntityState.Modified;

        await context.SaveChangesAsync();
    }
}
