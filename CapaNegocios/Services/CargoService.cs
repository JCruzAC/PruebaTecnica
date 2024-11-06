using CapaDatos.Repositories;
using CapaEntidades;
using Microsoft.EntityFrameworkCore;

namespace CapaNegocios.Services
{
    public class CargoService : ICargoService
    {
        public bool _isMessageError { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string _message { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        private readonly IGenericRepository<Cargo> _repo;
        public CargoService(IGenericRepository<Cargo> repo)
        {
            _repo = repo;
        }
        public async Task<bool> CreateCargo(Cargo model)
        {
            bool result = await _repo.Create(model);
            _message = _repo._message;
            _isMessageError = _repo._isMessageError;
            return result;
        }
        public async Task<List<Cargo>> GetAllCargoByEstado(int estado)
        {
            IQueryable<Cargo> query = _repo.GetAll();
            if (estado != 2)
                query = query.Where(w=>w.EstadoRegistro == estado);
            List<Cargo> lista = await query.ToListAsync();
            return lista;
        }
    }
}
