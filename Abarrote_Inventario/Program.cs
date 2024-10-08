using Interfaz;

namespace Abarrote_Inventario
{
    internal static class Program
    {
        
        [STAThread]
        static void Main()
        {
           
            ApplicationConfiguration.Initialize();
            Application.Run(new LoginSesion());
        }
    }
}