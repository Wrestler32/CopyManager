using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MessageBox = System.Windows.MessageBox; //Si no da un error con los message box porque tienen que ser mas especificos

namespace CopyManager
{
    /// <summary>
    /// Lógica de interacción para CopiasBBDD.xaml
    /// </summary>
    public partial class CopiasBBDD : Window
    {
        Boolean idioma = false;
        public CopiasBBDD(Boolean idioma)
        {
            InitializeComponent();
            sacarBackups();
            this.idioma = idioma;
            if (idioma == true) //Poner en inglés
                setEnglish();

            string ruta = System.Environment.CurrentDirectory;
            string conexion = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + ruta + @"\Users.mdf"; //Ruta relativa para la base de datos
            SqlConnection sqlCon = new SqlConnection(conexion); //Abrir conexión            try //Try catch para evitar errores
            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed) //Comprobar que no este abierta para no causar problemas de conexión
                    sqlCon.Open();
                String query = "Select Grupo FROM Grupos"; //Crear la string
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon); //Tipo de query que se crea
                sqlCmd.CommandType = System.Data.CommandType.Text; //Programar el agregar información a la consulta
                SqlDataReader reader = sqlCmd.ExecuteReader();
                grupoComboBox.Items.Clear();
                while (reader.Read()) //Guardar los grupos para mostrarlos
                {
                    grupoComboBox.Items.Add(reader.GetString(0));
                }
                reader.Close();
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

        private void setEnglish() //Poner ne inglés
        {
            volver.Content = "Back";
            verBase.Content = "See information";
            crearCopia.Content = "Create Backup";
            origenText.Text = "Source";
            destinoText.Text = "Destination";
            nombreText.Text = "Name";
            tittle.Content = "Backups";
            seeBackup.Content = "See Backup";
            addNew.Content = "Add new";
        }

        private void verBase_Click(object sender, RoutedEventArgs e) //Ver la información de la copia de seguridad seleccionada
        {
            string ruta = System.Environment.CurrentDirectory;
            string conexion = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + ruta + @"\Users.mdf"; //Ruta relativa para la base de datos
            SqlConnection sqlCon = new SqlConnection(conexion); //Abrir conexión
            try //Try catch para evitar errores
            {
                String nombre = nombreComboBox.Text;
                if (sqlCon.State == System.Data.ConnectionState.Closed) //Comprobar que no este abierta para no causar problemas de conexión
                    sqlCon.Open();
                String query = "Select Count(1) FROM Backups Where Nombre =@name"; //Crear la string
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon); //Tipo de query que se crea
                sqlCmd.Parameters.AddWithValue("@name", nombre);
                sqlCmd.CommandType = System.Data.CommandType.Text; //Programar el agregar información a la consulta
                int salida = Convert.ToInt32(sqlCmd.ExecuteScalar());
                BBDDSeleccionada seleccion = new BBDDSeleccionada(nombre, idioma);
                if (salida == 1) //Si existe la copia
                {
                    seleccion.ShowDialog();
                    sacarBackups();
                }
                else
                {
                    if (idioma == true)
                        MessageBox.Show("Select a backup");
                    else
                        MessageBox.Show("Selecciona una copia");
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

        private void volver_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void crearCopia_Click(object sender, RoutedEventArgs e) //Crear nueva copia
        {
            String origen = origenText.Text;
            String nombre = nombreText.Text;
            String destino = destinoText.Text + "/" + nombre + ".zip";
            String grupo = grupoComboBox.Text;
            string ruta = System.Environment.CurrentDirectory;
            string conexion = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + ruta + @"\Users.mdf"; //Ruta relativa para la base de datos
            SqlConnection sqlCon = new SqlConnection(conexion); //Abrir conexión
            try
            {
                metodoCopiaYCompresion.comprimir(origen, destino);
                if (sqlCon.State == System.Data.ConnectionState.Closed) //Comprobar que no haya otra conexión abierta
                    sqlCon.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Insert into [Backups](Nombre, RutaOrigen, RutaDestino, Fecha, Grupo)values(@name, @ruteO, @ruteD, @date, @group);"; //Crear la string
                cmd.Parameters.AddWithValue("@name", nombre); //Añadir todos los valores
                cmd.Parameters.AddWithValue("@ruteO", origen);
                cmd.Parameters.AddWithValue("@ruteD", destino);
                cmd.Parameters.AddWithValue("@date", DateTime.Now);
                cmd.Parameters.AddWithValue("@group", grupo);
                cmd.Connection = sqlCon;
                int i = cmd.ExecuteNonQuery();
                if (i == 1) //Si se ha realizado la inserción de la copia
                {
                    if (idioma == true)
                        MessageBox.Show("Backup has been added");
                    else
                        MessageBox.Show("Se ha añadido la copia de seguridad");
                    i = 0;
                    sacarBackups();
                }
            }
            catch
            {
                if (idioma == true)
                    MessageBox.Show("Could not make the backup");
                else
                    MessageBox.Show("No se ha podido realizar la copia");
            }
            finally
            {
                sqlCon.Close();
            }
        }

        private void sacarBackups() //Mostrar las copias existentes
        {
            string ruta = System.Environment.CurrentDirectory;
            string conexion = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + ruta + @"\Users.mdf"; //Ruta relativa para la base de datos
            SqlConnection sqlCon = new SqlConnection(conexion); //Abrir conexión
            try //Try catch para evitar errores
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed) //Comprobar que no este abierta para no causar problemas de conexión
                    sqlCon.Open();
                String query = "Select Nombre FROM Backups"; //Crear la string
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon); //Tipo de query que se crea
                sqlCmd.CommandType = System.Data.CommandType.Text; //Programar el agregar información a la consulta
                SqlDataReader reader = sqlCmd.ExecuteReader();
                nombreComboBox.Items.Clear();
                while (reader.Read())
                {
                    nombreComboBox.Items.Add(reader.GetString(0));
                }
                reader.Close();
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

        private void Button_Click(object sender, RoutedEventArgs e) //Sacar el explorador de archivos para las rutas de origen y desitno
        {
            using(var fd = new FolderBrowserDialog())
            {
                if(fd.ShowDialog() == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fd.SelectedPath)) //Comprobar que la ruta esté completa
                {
                    if(sender == destinoButton) 
                        destinoText.Text = fd.SelectedPath;
                    else
                        origenText.Text = fd.SelectedPath;
                }
            }
        }
    }
}
