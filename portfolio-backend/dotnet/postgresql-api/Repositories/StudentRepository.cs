﻿using Microsoft.EntityFrameworkCore;
using postgresql_api.Db;
using postgresql_api.Exceptions;
using postgresql_api.Models;
using Serilog;

namespace postgresql_api.Repository;

public class StudentRepository : IStudentRepository
{
    private readonly AppDbContext _context;
    private readonly int BAD_REQUEST = 400;
    private readonly int NOT_FOUND = 404;
    private readonly string INVALID_ID = "Invalid id, should be greater than 0.";
    private readonly string NOT_FOUND_STUDENT = "Student not found.";
    private readonly string STUDENT_NULL = "Student not should be null";

    public StudentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Student>> GetAll()
    {
        Log.Information("GetAll() of student repository called...");
        return await _context.Students.ToListAsync();
    }

    public async Task<Student?> Get(int id)
    {
        if (id <= 0)
        {
            throw new HttpResponseException(BAD_REQUEST, INVALID_ID);
        }

        var studentItem = await _context.Students.FindAsync(id);
        if (studentItem is null)
        {
            throw new HttpResponseException(NOT_FOUND, NOT_FOUND_STUDENT);
        }

        return studentItem;
    }

    public async Task<Student> Create(Student student)
    {
        if (student is null)
        {
            throw new HttpResponseException(BAD_REQUEST, STUDENT_NULL);
        }

        var createStudent = await _context.AddAsync(student);
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

        return createStudent.Entity;
    }

    public async Task Update(int id, Student student)
    {
        if (id <= 0)
        {
            throw new HttpResponseException(BAD_REQUEST, INVALID_ID);
        }

        if (student is null)
        {
            throw new HttpResponseException(BAD_REQUEST, STUDENT_NULL);
        }

        var studentItem = await _context.Students.FindAsync(id);
        if (studentItem is null)
        {
            throw new HttpResponseException(NOT_FOUND, NOT_FOUND_STUDENT);
        }

        studentItem.Id = student.Id;
        studentItem.Name = student.Name;
        studentItem.BornDate = student.BornDate;
        studentItem.FathersName = student.FathersName;
        studentItem.MothersName = studentItem.MothersName;

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

        var studentItem = await _context.Students.FindAsync(id);
        if (studentItem is null)
        {
            throw new Exception();
        }

        _context.Students.Remove(studentItem);

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
