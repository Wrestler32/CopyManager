using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CopyManager
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Boolean idioma = false;
        public MainWindow()
        {
            Ventana_LogIn inicio = new Ventana_LogIn(); //Iniciar la ventana de inicio de sesión antes de mostrar esta propia ventana para poder obtener los datos
            inicio.ShowDialog();
            String usuario = inicio.nombre; //Coger los datos del usuario
            this.idioma = inicio.idioma;
            if (usuario == null) //Comprobar que se haya iniciado sesión correctamente
            {
                this.Close(); //Si no se ha iniciado sesión correctamente se cierra la aplicación

            }
            else //Se ha iniciado la aplicación correctamente
            {
                InitializeComponent();
                if (idioma == true) //Poner en ingles
                    setEnglish();
                String grupoObtenido = null;
                string ruta = System.Environment.CurrentDirectory;
                string conexion = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + ruta + @"\Users.mdf"; //Ruta relativa para la base de datos
                SqlConnection sqlCon = new SqlConnection(conexion); //Abrir conexión
                try //Try catch para evitar errores
                {
                    if (sqlCon.State == System.Data.ConnectionState.Closed) //Comprobar que no este abierta para no causar problemas de conexión
                    sqlCon.Open();
                    String query = "Select Grupo FROM Cuentas WHERE Usuario=@Username"; //Crear la string
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon); //Tipo de query que se crea
                    sqlCmd.CommandType = System.Data.CommandType.Text; //Programar el agregar información a la consulta
                    sqlCmd.Parameters.AddWithValue("@Username", usuario); //Agregar la información a la consulta
                    grupoObtenido = sqlCmd.ExecuteScalar().ToString(); //Guardar el grupo del usuario en una variable
                    if(grupoObtenido == "Administradores") //Comprobar si pertenece a este grupo
                    {
                        logout2.Visibility = Visibility.Hidden;
                        sacarBackups(grupoObtenido);
                        sacarUsuarios();
                    }
                    else //Comprobar si pertenece a otro grupo
                    {
                        //Esconder las opciones a las que no tiene acceso los usuarios
                        addUser.Visibility = Visibility.Hidden;
                        delUser.Visibility = Visibility.Hidden;
                        cuentasDataGrid.Visibility = Visibility.Hidden;
                        copiasButton.Visibility = Visibility.Hidden;
                        this.Width = 430;
                        logout.Visibility = Visibility.Hidden;

                        try
                        {
                            String query2 = "Select Nombre FROM Backups Where Grupo =@name"; //Crear la string
                            SqlCommand sqlCmd2 = new SqlCommand(query2, sqlCon); //Tipo de query que se crea
                            sqlCmd2.CommandType = System.Data.CommandType.Text; //Programar el agregar información a la consulta
                            sqlCmd2.Parameters.AddWithValue("@name", grupoObtenido);
                            SqlDataReader reader = sqlCmd2.ExecuteReader();
                            while (reader.Read())
                            {
                                comboBoxBackups.Items.Add(reader.GetString(0));
                            }
                            reader.Close();
                        }
                        catch (Exception ex) //Si se producen errores de conexión, muestra el problema
                        {
                            MessageBox.Show(ex.Message);
                        }
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

        private void setEnglish() //Poner los componentes a inglés
        {
            addUser.Content = "Add User";
            delUser.Content = "Delete User";
            copiasButton.Content = "Backups";
            logout.Content = "Log out";
            logout2.Content = "Log out";
            verCopia.Content = "See Backup";
        }

        public class Usuarios //Clase para poder mostrar los usuarios
        {
            public string nombre { get; set; }
            public string grupo { get; set; }
        }

        private void logout_Click(object sender, RoutedEventArgs e) //Volver a iniciar sesión
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location); //Iniciar la aplicación de nuevo
            Application.Current.Shutdown(); //Cierra la aplicación actual
        }

        private void nuevoUsuario_Click(object sender, RoutedEventArgs e)
        {
            NuevoUsuario users = new NuevoUsuario(idioma); //Ir a la ventana de creación de usuario
            users.ShowDialog();
            sacarUsuarios();
        }

        private void borrarUsuario_Click(object sender, RoutedEventArgs e)
        {
            BorrarUsuario borrar = new BorrarUsuario(idioma); //Ir a la ventana de borrar usuario
            borrar.ShowDialog();
            sacarUsuarios();
        }

        private void copias_Click(object sender, RoutedEventArgs e)
        {
            CopiasBBDD basesDatos = new CopiasBBDD(idioma); //Ir a la ventana de copias
            basesDatos.ShowDialog();
            sacarBackups("Administradores");
        }

        private void verCopia_Click(object sender, RoutedEventArgs e) //Ver la copia de seguridad
        {
            string ruta = System.Environment.CurrentDirectory;
            string conexion = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + ruta + @"\Users.mdf"; //Ruta relativa para la base de datos
            SqlConnection sqlCon = new SqlConnection(conexion); //Abrir conexión
            String rutaArchivo = "";
            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed) //Comprobar que no haya otra conexión abierta
                    sqlCon.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Select RutaDestino From Backups Where Nombre =@name;"; //Crear la string
                cmd.Parameters.AddWithValue("@name", comboBoxBackups.Text); //Añadir todos los valores
                cmd.Connection = sqlCon;
                rutaArchivo = cmd.ExecuteScalar().ToString();

                if (Directory.Exists("C:/CopiaTemporal")) //Comprueba si el directorio existe para eliminarlo si hace falta
                {
                    Directory.Delete("C:/CopiaTemporal", true);
                    ZipFile.ExtractToDirectory(rutaArchivo, "C:/CopiaTemporal"); //Extraer la copia para ver su información
                    Process ventana = Process.Start(@"C:/CopiaTemporal");
                }
                else
                {
                    ZipFile.ExtractToDirectory(rutaArchivo, "C:/CopiaTemporal");
                    Process.Start(@"C:/CopiaTemporal");
                }
            }
            catch
            {
                if (idioma == true)
                    MessageBox.Show("Could not find the backup");
                else
                    MessageBox.Show("No se ha podido encontrar la copia");
            }
            finally
            {
                sqlCon.Close();
            }
        }
        private void sacarUsuarios() //Método para mostrar los usuarios
        {

            string ruta = System.Environment.CurrentDirectory;
            string conexion = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + ruta + @"\Users.mdf"; //Ruta relativa para la base de datos
            SqlConnection sqlCon = new SqlConnection(conexion); //Abrir conexión
            List<Usuarios> users = new List<Usuarios>();

            if (sqlCon.State == System.Data.ConnectionState.Closed) //Comprobar que no este abierta para no causar problemas de conexión
                sqlCon.Open();
            String query2 = "Select Usuario, Grupo FROM Cuentas"; //Crear la string
            SqlCommand sqlCmd2 = new SqlCommand(query2, sqlCon); //Tipo de query que se crea
            sqlCmd2.CommandType = System.Data.CommandType.Text; //Programar el agregar información a la consulta
            SqlDataReader reader2 = sqlCmd2.ExecuteReader();
            while (reader2.Read()) //Añadir 
            {
                users.Add(new Usuarios() { nombre = reader2.GetString(0), grupo = reader2.GetString(1) });
            }
            cuentasDataGrid.ItemsSource = users;
            reader2.Close();
        }

        private void sacarBackups(String grupo) //Método para mostrar las copias de seguridad
        {
            string ruta = System.Environment.CurrentDirectory;
            string conexion = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + ruta + @"\Users.mdf"; //Ruta relativa para la base de datos
            SqlConnection sqlCon = new SqlConnection(conexion); //Abrir conexión
            if (sqlCon.State == System.Data.ConnectionState.Closed) //Comprobar que no este abierta para no causar problemas de conexión
            sqlCon.Open();
            try //Try catch para evitar errores
            {
                String query1 = "Select Nombre FROM Backups"; //Crear la string
                SqlCommand sqlCmd1 = new SqlCommand(query1, sqlCon); //Tipo de query que se crea
                sqlCmd1.CommandType = System.Data.CommandType.Text; //Programar el agregar información a la consulta
                SqlDataReader reader = sqlCmd1.ExecuteReader();
                comboBoxBackups.Items.Clear();
                while (reader.Read())
                {
                    comboBoxBackups.Items.Add(reader.GetString(0));
                }
                reader.Close();
            }
            catch (Exception ex) //Si se producen errores de conexión, muestra el problema
            {
                MessageBox.Show(ex.Message);
            }
            sqlCon.Close();
        }
    }
}
