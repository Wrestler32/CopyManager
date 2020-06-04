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
    /// Lógica de interacción para BorrarUsuario.xaml
    /// </summary>
    public partial class BorrarUsuario : Window
    {
        Boolean idioma = false;
        public BorrarUsuario(Boolean idioma)
        {
            InitializeComponent();
            this.idioma = idioma;
            if (idioma == true) //Poner en inglés
                setEnglish();

            string ruta = System.Environment.CurrentDirectory;
            string conexion = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + ruta + @"\Users.mdf"; //Ruta relativa para la base de datos
            SqlConnection sqlCon = new SqlConnection(conexion); //Abrir conexión
            if (sqlCon.State == System.Data.ConnectionState.Closed) //Comprobar que no este abierta para no causar problemas de conexión
            sqlCon.Open();
            try //Try catch para evitar errores
            {
                String query1 = "Select Usuario FROM Cuentas"; //Crear la string
                SqlCommand sqlCmd1 = new SqlCommand(query1, sqlCon); //Tipo de query que se crea
                sqlCmd1.CommandType = System.Data.CommandType.Text; //Programar el agregar información a la consulta
                SqlDataReader reader = sqlCmd1.ExecuteReader();
                usuarioComboBox.Items.Clear();
                while (reader.Read()) //Guardar los usuarios para mostrarlos
                {
                    usuarioComboBox.Items.Add(reader.GetString(0));
                }
                reader.Close();
            }
            catch (Exception ex) //Si se producen errores de conexión, muestra el problema
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }

        private void setEnglish() //Poner en inglés
        {
            user.Content = "User";
            tittle.Content = "Delete User";
            borrar.Content = "Delete";
        }

        private void enviar_Click(object sender, RoutedEventArgs e) //Borrar usuarios
        {
            if (usuarioComboBox.Text == "Admin") //Evitar que se borre admin
            {
                if (idioma == true)
                    MessageBox.Show("Admin user can't be deleted");
                else
                    MessageBox.Show("No se puede borrar el usuario Admin");
            }
            else
            {
                string ruta = System.Environment.CurrentDirectory;
                string conexion = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + ruta + @"\Users.mdf"; //Ruta relativa para la base de datos
                SqlConnection sqlCon = new SqlConnection(conexion); //Abrir conexión
                try //Try catch para evitar errores
                {
                    if (sqlCon.State == System.Data.ConnectionState.Closed) //Comprobar que no haya otra conexión abierta
                        sqlCon.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "Delete From Cuentas Where Usuario=@Username;"; //Crear la string
                    cmd.Parameters.AddWithValue("@Username", usuarioComboBox.Text);
                    cmd.Connection = sqlCon;
                    int i = cmd.ExecuteNonQuery();
                    if (i == 1)
                    {
                        if (idioma == true)
                            MessageBox.Show("User has been deleted");
                        else
                            MessageBox.Show("Se ha borrado el usuario"); //Mensaje de muestra de que el usuario se ha creado
                        this.Close();
                    }
                    else
                    {
                        if (idioma == true)
                            MessageBox.Show("Could not delete the user");
                        else
                            MessageBox.Show("No se ha podido borrar el usuario");
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
}
