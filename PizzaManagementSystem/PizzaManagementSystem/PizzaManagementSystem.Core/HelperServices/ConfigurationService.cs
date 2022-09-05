namespace PizzaManagementSystem.Core.HelperServices
{
    public class ConfigurationService
    {
        public bool IsDevelopment { get; set; }
        public string AppName { get; set; }
        public string ConnectionString { get; set; }
    }
}
