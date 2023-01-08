using Domain;
using Infrastructure.Context;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    /*
     Repositorio para crud das moedas
     */
    public class MoneyRepository : IRepository<Money>
    {
        private readonly ApplicationDbContext _context;

        public MoneyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(Money entity)
        {
            _context.Database.EnsureCreated();
            _context.Money.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Database.EnsureCreated();
            var model = _context.Money.Where(x => x.Id == id);
            _context.Remove(model);
            _context.SaveChanges();
        }

        public IList<Money> GetAll()
        {
            _context.Database.EnsureCreated();
            return _context.Money.ToList();
        }

        public Money GetById(int id)
        {
            _context.Database.EnsureCreated();
            return _context.Money.Where(m => m.Id == id).FirstOrDefault();
        }

        public void Update(Money entity)
        {
            _context.Database.EnsureCreated();
            _context.Update(entity);
            _context.SaveChanges();
        }
    }
}
