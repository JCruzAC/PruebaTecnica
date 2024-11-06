using CapaDatos.DataContext;
using CapaEntidades;
using Microsoft.EntityFrameworkCore;

namespace CapaDatos.Repositories
{
    public class CargoRepository : IGenericRepository<Cargo>
    {
        private readonly AppDBContext _db;
        public bool _isMessageError { get ; set; }
        public string _message { get; set; } = string.Empty;
        public CargoRepository(AppDBContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Cargo model)
        {
            using (var transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    _db.Cargos.Add(model);
                    await _db.SaveChangesAsync();
                    transaction.Commit();
                    _message = "El registro ha sido creado";
                    return true;
                }
                catch (DbUpdateException ex)
                {
                    _message = ex.Message;
                }
                catch (Exception ex)
                {
                    _message = ex.Message;
                }
                transaction.Rollback();
                _isMessageError = true;
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            Cargo model = new()
            {
                IdCargo = id,
                RegistroEliminado = true
            };
            _db.Cargos.Attach(model);
            _db.Entry(model).Property(p=>p.EstadoRegistro).IsModified = true;
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
                    _message = ex.Message;
                }
                catch (Exception ex)
                {
                    _message= ex.Message;
                }
                _isMessageError = true;
                transaction.Rollback();
                return false;
            }
        }

        public IQueryable<Cargo> GetAll()
        {
            IQueryable<Cargo> query = _db.Cargos
                .Where(w => !w.RegistroEliminado);
            return query;
        }

        public async Task<Cargo?> GetById(int id)
        {
            try
            {
                Cargo? model = await _db.Cargos
                    .Where(w => w.IdCargo == id && !w.RegistroEliminado)
                    .FirstOrDefaultAsync();
                return model;
            }
            catch (DbUpdateException ex)
            {
                _message = ex.Message;
            }
            catch (Exception ex)
            {
                _message = ex.Message;
            }
            _isMessageError = true;
            return null;
        }

        public async Task<bool> Update(Cargo model)
        {
            using (var transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    _db.Cargos.Update(model);
                    await _db.SaveChangesAsync();
                    _message = "El registro ha sido actualizado";
                    transaction.Commit();
                    return true;
                }
                catch (DbUpdateException ex)
                {
                    _message = ex.Message;
                }
                catch (Exception ex)
                {
                    _message = ex.Message;
                }
                transaction.Rollback();
                _isMessageError = true;
                return false;
            }
        }
    }
}
