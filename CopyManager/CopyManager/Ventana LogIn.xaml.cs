using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CopyManager
{
    /// <summary>
    /// Lógica de interacción para Ventana_LogIn.xaml
    /// </summary>
    public partial class Ventana_LogIn : Window
    {
        public String nombre; //Variable para guardar el nombre del usuario y pasarla a la ventana principal
        public Boolean idioma = false;
        public Ventana_LogIn()
        {
            InitializeComponent();
            Usuario.Focus();
        }
        private void enviar_Click(object sender, RoutedEventArgs e)
        {
            string ruta = System.Environment.CurrentDirectory;
            string conexion = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + ruta + @"\Users.mdf"; //Ruta relativa para la base de datos
            SqlConnection sqlCon = new SqlConnection(conexion); //Abrir conexión
            try //Try catch para evitar errores
            {
                if (ingles.IsChecked == true) //Poner en inglés
                    idioma = true;
                if (sqlCon.State == System.Data.ConnectionState.Closed) //Comprobar que no haya otra conexión abierta
                    sqlCon.Open();
                String query = "Select Count(1) FROM Cuentas WHERE Usuario=@Username AND Contraseña=@Password"; //Crear la string
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon); //Tipo de query
                sqlCmd.CommandType = System.Data.CommandType.Text; //Anticipar la introducción de información a la sentencia
                nombre = Usuario.Text; //Guardar el usuario en la variable publica
                sqlCmd.Parameters.AddWithValue("@Username", nombre); //Meter el primer valor (Usuario) en la consulta
                sqlCmd.Parameters.AddWithValue("@Password", Contraseña.Password); //Meter el segundo valor (Contraseña) en la consulta
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar()); //Pasar el resultado a valor int para comprobar si hay cuenta o no
                if (count == 1) //Si hay cuenta
                {
                    this.Close(); //Se cierra y avanzamos a la otra aplicación
                }
                else //No hay cuenta
                {
                    nombre = null; //Volver a dejar la variable en null por un prosible problema de seguridad de entrada sin cuenta
                    if (idioma == true)
                        MessageBox.Show("User or password are incorrect");
                    else
                        MessageBox.Show("El usuario o contraseña son incorrectos"); //Mostrar mensaje de advertencia
                    Contraseña.Password = ""; //Vaciar la contraseña
                }
            }
            catch (Exception ex) //Si se producen errores de conexión, muestra el problema
            {
                MessageBox.Show(ex.Message);
            }
            finally //Al terminar cierra la conexión
            {
                sqlCon.Close();
            }
        }
    }
}
