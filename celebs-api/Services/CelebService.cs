
using CelebApi.Exceptions;
using CelebApi.Models;
using CelebApi.Repositories;

namespace CelebApi.Services;

public class CelebService(CelebRepository repo)
{
    private readonly CelebRepository _repo = repo;

    public List<Celeb> GetAll(string? search = null)
    {
        try
        {
            Console.WriteLine($"[Service: GetAll] 🔍 Retrieving celebs with search: '{search ?? "none"}'");
            
            var allCelebs = _repo.GetAll();
            Console.WriteLine($"[Service: GetAll] 📊 Retrieved {allCelebs.Count} celebs from repository");
            
            if (string.IsNullOrWhiteSpace(search))
            {
                Console.WriteLine($"[Service: GetAll] ✅ Returning all {allCelebs.Count} celebs");
                return allCelebs;
            }
            
            var filteredCelebs = allCelebs.Where(c => 
                c.Name?.Contains(search, StringComparison.OrdinalIgnoreCase) == true)
                .ToList();
            
            Console.WriteLine($"[Service: GetAll] 🔍 Filtered to {filteredCelebs.Count} celebs matching '{search}'");
            return filteredCelebs;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Service: GetAll] ❌ Error retrieving celebs: {ex.Message}");
            throw;
        }
    }

    public Celeb GetById(Guid id)
    {
        try
        {
            Console.WriteLine($"[Service: GetById] 🔍 Looking for celeb with ID: {id}");
            
            var allCelebs = _repo.GetAll();
            var celeb = allCelebs.FirstOrDefault(c => c.Id == id);
            
            if (celeb == null)
            {
                Console.WriteLine($"[Service: GetById] ❌ Celeb with ID '{id}' not found");
                throw new NotFoundException($"Celeb with ID '{id}' not found.");
            }
            
            Console.WriteLine($"[Service: GetById] ✅ Found celeb: {celeb.Name}");
            return celeb;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Service: GetById] ❌ Error retrieving celeb {id}: {ex.Message}");
            throw;
        }
    }

    public Celeb Add(CelebCreateDto celebCreateDto)
    {
        var newCeleb = new Celeb
        {
            Id = Guid.NewGuid(),
            Name = celebCreateDto.Name,
            BirthDate = celebCreateDto.BirthDate,
            Gender = celebCreateDto.Gender,
            Roles = celebCreateDto.Roles,
            Image = celebCreateDto.Image
        };

        var all = _repo.GetAll();
        all.Add(newCeleb);
        _repo.SaveAll(all);
        return newCeleb;

    }

    public Celeb Update(Guid Id, CelebUpdateDto updated)
    {
        try
        {
            Console.WriteLine($"[Service: Update] 🔄 Updating celeb with ID: {Id}");
            
            var all = _repo.GetAll();
            var existing = all.FirstOrDefault(celeb => celeb.Id == Id);

            if (existing == null)
            {
                Console.WriteLine($"[Service: Update] ❌ Celeb with ID '{Id}' not found");
                throw new NotFoundException($"Celeb with ID '{Id}' not found.");
            }

            Console.WriteLine($"[Service: Update] 📝 Updating celeb: {existing.Name}");

            if (!string.IsNullOrWhiteSpace(updated.Name))
            {
                Console.WriteLine($"[Service: Update] 📝 Updating name: {existing.Name} -> {updated.Name}");
                existing.Name = updated.Name;
            }

            if (!string.IsNullOrWhiteSpace(updated.BirthDate))
            {
                Console.WriteLine($"[Service: Update] 📝 Updating birth date: {existing.BirthDate} -> {updated.BirthDate}");
                existing.BirthDate = updated.BirthDate;
            }

            if (!string.IsNullOrWhiteSpace(updated.Gender))
            {
                Console.WriteLine($"[Service: Update] 📝 Updating gender: {existing.Gender} -> {updated.Gender}");
                existing.Gender = updated.Gender;
            }

            if (updated.Roles != null && updated.Roles.Any())
            {
                Console.WriteLine($"[Service: Update] 📝 Updating roles: [{string.Join(", ", existing.Roles)}] -> [{string.Join(", ", updated.Roles)}]");
                existing.Roles = updated.Roles;
            }

            if (!string.IsNullOrWhiteSpace(updated.Image))
            {
                Console.WriteLine($"[Service: Update] 📝 Updating image URL");
                existing.Image = updated.Image;
            }

            _repo.SaveAll(all);
            Console.WriteLine($"[Service: Update] ✅ Successfully updated celeb: {existing.Name}");
            return existing;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Service: Update] ❌ Error updating celeb {Id}: {ex.Message}");
            throw;
        }
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
