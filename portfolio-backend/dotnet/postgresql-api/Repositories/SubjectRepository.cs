using postgresql_api.Db;
using postgresql_api.Models;
using postgresql_api.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace postgresql_api.Repository;

public class SubjectRepository : ISubjectRepository
{
  private readonly AppDbContext _context;
  private readonly int BAD_REQUEST = 400;
  private readonly int NOT_FOUND = 404;
  private readonly string INVALID_ID = "Invalid id, should be greater than 0.";
  private readonly string NOT_FOUND_SUBJECT = "Subject not found.";
  private readonly string SUBJECT_NULL = "Subject not should be null";
  public SubjectRepository(AppDbContext context)
  {
    _context = context;
  }

  public async Task<IEnumerable<Subject>> GetAll()
  {
    return await _context.Subjects.ToListAsync();
  }

  public async Task<Subject?> Get(int id)
  {
    if (id <= 0)
    {
      throw new HttpResponseException(BAD_REQUEST, INVALID_ID);
    }

    var subjectItem = await _context.Subjects.FindAsync();
    if (subjectItem is null)
    {
      throw new HttpResponseException(NOT_FOUND, NOT_FOUND_SUBJECT);
    }

    return subjectItem;
  }

  public async Task<Subject> Create(Subject subject)
  {
    if (subject is null)
    {
      throw new HttpResponseException(BAD_REQUEST, SUBJECT_NULL);
    }

    var subjectItem = await _context.Subjects.AddAsync(subject);

    try
    {
      await _context.SaveChangesAsync();
    }
    catch (OperationCanceledException e)
    {
      throw e;
    }
    catch (DbUpdateConcurrencyException e)
    {
      throw e;
    }
    catch (DbUpdateException e)
    {
      throw e;
    }

    return subjectItem.Entity;
  }
  public async Task Update(int id, Subject subject)
  {
    if (id <= 0)
    {
      throw new HttpResponseException(BAD_REQUEST, INVALID_ID);
    }

    if (subject is null)
    {
      throw new HttpResponseException(BAD_REQUEST, SUBJECT_NULL);
    }

    var subjectItem = await _context.Subjects.FindAsync(id);
    if (subjectItem is null)
    {
      throw new HttpResponseException(NOT_FOUND, NOT_FOUND_SUBJECT);
    }

    subjectItem.Id = subject.Id;
    subjectItem.Name = subject.Name;
    try
    {
      await _context.SaveChangesAsync();
    }
    catch (OperationCanceledException e)
    {
      throw e;
    }
    catch (DbUpdateConcurrencyException e)
    {
      throw e;
    }
    catch (DbUpdateException e)
    {
      throw e;
    }
  }

  public async Task Delete(int id)
  {
    if (id <= 0)
    {
      throw new HttpResponseException(BAD_REQUEST, INVALID_ID);
    }

    var subjectItem = await _context.Subjects.FindAsync(id);
    if (subjectItem is null)
    {
      throw new HttpResponseException(NOT_FOUND, NOT_FOUND_SUBJECT);
    }
    _context.Subjects.Remove(subjectItem);

    try
    {
      await _context.SaveChangesAsync();
    }
    catch (OperationCanceledException e)
    {
      throw e;
    }
    catch (DbUpdateConcurrencyException e)
    {
      throw e;
    }
    catch (DbUpdateException e)
    {
      throw e;
    }
  }
}
