using CapaEntidades;

namespace CapaNegocios.Services
{
    public interface ICargoService
    {
        bool _isMessageError { get; set; }
        string _message { get; set; }
        Task<bool> CreateCargo(Cargo model);
        Task<List<Cargo>> GetAllCargoByEstado(int estado);
    }
}
