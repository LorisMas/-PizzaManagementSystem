namespace PizzaManagementSystem.DAL.Interfaces
{
    /// <summary>
    /// Interfaccia implementata da tutte le entità presenti nel DB per cui si vuole
    /// configurare repository e servizio per accedere in maniera diretta ai dati.
    /// </summary>
    public interface IBaseEntity
    {
        int ID { get; set; }
    }
}
