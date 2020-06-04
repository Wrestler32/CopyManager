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
    /// Lógica de interacción para BBDDSeleccionada.xaml
    /// </summary>
    public partial class BBDDSeleccionada : Window
    {
        Boolean idioma = false;
        public BBDDSeleccionada(String nombre, Boolean idioma)
        {
            InitializeComponent();
            this.idioma = idioma;
            if (idioma == true) //Poner en inglés
                setEnglish();
            this.Title = nombre;

            List<Backup> copias = new List<Backup>(); 
            string ruta = System.Environment.CurrentDirectory;
            string conexion = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + ruta + @"\Users.mdf"; //Ruta relativa para la base de datos
            SqlConnection sqlCon = new SqlConnection(conexion); //Abrir conexión
            try //Try catch para evitar errores
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed) //Comprobar que no este abierta para no causar problemas de conexión
                    sqlCon.Open();
                String query = "Select Nombre, RutaOrigen, RutaDestino, Fecha, Grupo FROM Backups Where Nombre =@name"; //Crear la string
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon); //Tipo de query que se crea
                sqlCmd.CommandType = System.Data.CommandType.Text; //Programar el agregar información a la consulta
                sqlCmd.Parameters.AddWithValue("@name", nombre);
                SqlDataReader reader = sqlCmd.ExecuteReader();
                while (reader.Read()) //Guardar la información de la base para mostrarla en los componentes
                {
                    copias.Add(new Backup() { nombre = reader.GetString(0), rutaOrigen = reader.GetString(1), rutaDestino = reader.GetString(2), fecha = reader.GetDateTime(3), grupo = reader.GetString(4)});
                }
                baseDataGrid.ItemsSource = copias;
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

        private void setEnglish()
        {
            delete.Content = "Delete";
            back.Content = "Back";
        }

        public class Backup //Clase para sacar las copias
        {
            public string nombre { get; set; }
            public string rutaOrigen { get; set; }
            public string rutaDestino { get; set; }
            public DateTime fecha { get; set; }
            public string grupo { get; set; }
        }

        private void delete_Click(object sender, RoutedEventArgs e) //Eliminar copia
        {
            string ruta = System.Environment.CurrentDirectory;
            string conexion = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + ruta + @"\Users.mdf"; //Ruta relativa para la base de datos
            SqlConnection sqlCon = new SqlConnection(conexion); //Abrir conexión
            try //Try catch para evitar errores
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed) //Comprobar que no haya otra conexión abierta
                    sqlCon.Open();
                SqlCommand cmd = new SqlCommand();
                //--Consulta para buscar la ruta---
                String query = "Select RutaDestino FROM Backups where Nombre =@name1;"; //Crear la string
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon); //Tipo de query
                sqlCmd.Parameters.AddWithValue("@name1", this.Title);
                String archivoEliminar = sqlCmd.ExecuteScalar().ToString();
                //--Borrado de la base de datos----
                cmd.CommandText = "Delete From Backups Where Nombre=@name;"; //Crear la string
                cmd.Parameters.AddWithValue("@name", this.Title);
                cmd.Connection = sqlCon;
                int i = cmd.ExecuteNonQuery();
                if (i == 1) //Si la copia existe se borra
                {
                    metodoCopiaYCompresion.borrar(archivoEliminar);
                    if (idioma == true)
                        MessageBox.Show("Backup has been deleted");
                    else
                        MessageBox.Show("Se ha borrado la base de datos"); //Mensaje de muestra de que la base de datos se ha borrado
                    this.Close();
                }
                else
                {
                    if (idioma == true)
                        MessageBox.Show("Could not delete the backup");
                    else
                        MessageBox.Show("No se ha podido borrar la base de datos");
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

        private void back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
