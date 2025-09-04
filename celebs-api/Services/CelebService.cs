
using CelebApi.Exceptions;
using CelebApi.Models;
using CelebApi.Repositories;

namespace CelebApi.Services;

public class CelebService(CelebRepository repo)
{
    private readonly CelebRepository _repo = repo;

    public List<Celeb> GetAll()
    {
        return _repo.GetAll();
    }

    public Celeb GetById(Guid id)
    {
        Celeb? celeb = _repo.GetAll().FirstOrDefault(c => c.Id == id);
        if (celeb == null)
        {
            throw new NotFoundException($"Celeb with ID '{id}' not found.");
        }
        return celeb;
    }

    public Celeb Add(CelebCreateDto celebCreateDto)
    {
        var newCeleb = new Celeb
        {
            Id = Guid.NewGuid(),
            Name = celebCreateDto.Name,
            BirthDate = celebCreateDto.BirthDate,
            Gender = celebCreateDto.Gender,
            Role = celebCreateDto.Role,
            Image = celebCreateDto.Image
        };

        var all = _repo.GetAll();
        all.Add(newCeleb);
        _repo.SaveAll(all);
        return newCeleb;

    }

    public Celeb Update(Guid Id, CelebUpdateDto updated)
    {
        var all = _repo.GetAll();
        Celeb? existing = all.FirstOrDefault(celeb => celeb.Id == Id);

        if (existing == null)
        {
            throw new NotFoundException($"Celeb with ID '{Id}' not found.");
        }

        if (!string.IsNullOrWhiteSpace(updated.Name))
            existing.Name = updated.Name;

        if (!string.IsNullOrWhiteSpace(updated.BirthDate))
            existing.BirthDate = updated.BirthDate;

        if (!string.IsNullOrWhiteSpace(updated.Gender))
            existing.Gender = updated.Gender;

        if (!string.IsNullOrWhiteSpace(updated.Role))
            existing.Role = updated.Role;

        if (!string.IsNullOrWhiteSpace(updated.Image))
            existing.Image = updated.Image;

        _repo.SaveAll(all);
        return existing;
    }


    public void Delete(Guid id)
    {
        var all = _repo.GetAll();
        var updated = all.Where(c => c.Id != id).ToList();

        if (all.Count == updated.Count)
        {
            Console.WriteLine($"[Service: Delete] ⚠️ No celeb found with ID: {id}");
        }

        _repo.SaveAll(updated);
    }

    public void Reset()
    {
        _repo.Reset();
    }
}
