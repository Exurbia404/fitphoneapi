using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FitPhoneBackend.Infrastructure;
using FitPhoneBackend.Business.Entities;

namespace FitphoneBackend.Business.Services
{
    public class EducationService
    {
        private readonly ApplicationDbContext _context;

        public EducationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Education>> GetAllAsync()
        {
            return await _context.Educations.ToListAsync();
        }

        public async Task<Education> GetByIdAsync(int id)
        {
            return await _context.Educations.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Education> CreateAsync(Education education)
        {
            _context.Educations.Add(education);
            await _context.SaveChangesAsync();
            return education;
        }

        public async Task UpdateAsync(int id, Education updatedEducation)
        {
            var education = await _context.Educations.FirstOrDefaultAsync(e => e.Id == id);
            if (education == null)
            {
                throw new KeyNotFoundException($"Education with Id {id} not found.");
            }

            education.Name = updatedEducation.Name;
            education.Description = updatedEducation.Description;
            education.VideoURL = updatedEducation.VideoURL;
            education.ArticleURL = updatedEducation.ArticleURL;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var education = await _context.Educations.FirstOrDefaultAsync(e => e.Id == id);
            if (education == null)
            {
                throw new KeyNotFoundException($"Education with Id {id} not found.");
            }

            _context.Educations.Remove(education);
            await _context.SaveChangesAsync();
        }
    }
}
