using CapaDatos.DataContext;
using CapaEntidades;
using Microsoft.EntityFrameworkCore;

namespace CapaDatos.Repositories
{
    public class PersonalRepository : IGenericRepository<Personal>
    {
        private readonly AppDBContext _db;
        public bool _isMessageError { get; set; }
        public string _message { get; set; } = string.Empty;
        public PersonalRepository(AppDBContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Personal model)
        {
            using (var transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    _db.Personals.Add(model);
                    await _db.SaveChangesAsync();
                    transaction.Commit();
                    _message = "El registro ha sido creado";
                    return true;
                }
                catch (DbUpdateException ex)
                {
                    _message = "Error: " + ex.Message;
                }
                catch (Exception ex)
                {
                    _message = "Error: " + ex.Message;
                }
                transaction.Rollback();
                _isMessageError = true;
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            Personal model = new()
            {
                IdCargo = id,
                RegistroEliminado = true
            };
            _db.Personals.Attach(model);
            _db.Entry(model).Property(p => p.EstadoRegistro).IsModified = true;
            using (var transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    await _db.SaveChangesAsync();
                    _message = "El registro ha sido eliminado";
                    transaction.Commit();
                    return true;
                }
                catch (DbUpdateException ex)
                {
                    _message = "Error: " + ex.Message;
                }
                catch (Exception ex)
                {
                    _message = "Error: " + ex.Message;
                }
                _isMessageError = true;
                transaction.Rollback();
                return false;
            }
        }

        public IQueryable<Personal> GetAll()
        {
            IQueryable<Personal> query = _db.Personals
                .Include(i=>i.IdCargoNavigation)
                .Where(w => !w.RegistroEliminado);
            return query;
        }

        public async Task<Personal?> GetById(int id)
        {
            try
            {
                Personal? model = await _db.Personals
                    .Where(w => w.IdPersonal == id && !w.RegistroEliminado)
                    .FirstOrDefaultAsync();
                return model;
            }
            catch (DbUpdateException ex)
            {
                _message = "Error: " + ex.Message;
            }
            catch (Exception ex)
            {
                _message = "Error: " + ex.Message;
            }
            _isMessageError = true;
            return null;
        }

        public async Task<bool> Update(Personal model)
        {
            using (var transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    _db.Personals.Update(model);
                    await _db.SaveChangesAsync();
                    _message = "El registro ha sido grabado";
                    transaction.Commit();
                    return true;
                }
                catch (DbUpdateException ex)
                {
                    _message = "Error: " + ex.Message;
                }
                catch (Exception ex)
                {
                    _message = "Error: " + ex.Message;
                }
                transaction.Rollback();
                _isMessageError = true;
                return false;
            }
        }
    }
}
