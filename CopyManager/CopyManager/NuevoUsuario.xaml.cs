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
    /// Lógica de interacción para NuevoUsuario.xaml
    /// </summary>
    public partial class NuevoUsuario : Window
    {
        Boolean idioma = false;
        public NuevoUsuario(Boolean idioma)
        {
            InitializeComponent();
            this.idioma = idioma;
            if (idioma == true) //Poner en inglés
                setEnglish();

            string ruta = System.Environment.CurrentDirectory;
            string conexion = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + ruta + @"\Users.mdf"; //Ruta relativa para la base de datos
            SqlConnection sqlCon = new SqlConnection(conexion); //Abrir conexión
            try //Try catch para evitar errores
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed) //Comprobar que no este abierta para no causar problemas de conexión
                    sqlCon.Open();
                String query = "Select Grupo FROM Grupos"; //Crear la string
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon); //Tipo de query que se crea
                sqlCmd.CommandType = System.Data.CommandType.Text; //Programar el agregar información a la consulta
                SqlDataReader reader = sqlCmd.ExecuteReader();
                grupoComboBox.Items.Clear();
                while (reader.Read())
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

        private void setEnglish() //Poner en inglés
        {
            user.Content = "User";
            password.Content = "Password";
            group.Content = "Group";
            enviar.Content = "Add";
        }

        private void enviar_Click(object sender, RoutedEventArgs e) //Añadir usuario
        {
            string ruta = System.Environment.CurrentDirectory;
            string conexion = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + ruta + @"\Users.mdf"; //Ruta relativa para la base de datos
            SqlConnection sqlCon = new SqlConnection(conexion); //Abrir conexión
            try //Try catch para evitar errores
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed) //Comprobar que no haya otra conexión abierta
                    sqlCon.Open();
                //--Consulta para determinar el id---
                String query = "Select MAX(IdCuenta) FROM Cuentas;"; //Crear la string
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon); //Tipo de query
                int numId = Convert.ToInt32(sqlCmd.ExecuteScalar()) + 1;
                //--Solo se permiten cinco usuarios--
                String query2 = "Select Count(Usuario) From Cuentas";
                SqlCommand sqlCmd2 = new SqlCommand(query2, sqlCon);
                int numUsers = Convert.ToInt32(sqlCmd2.ExecuteScalar());
                if (numUsers >= 5)
                    if (idioma == true)
                        MessageBox.Show("Only five users are allowed");
                    else
                        MessageBox.Show("Solo se permiten cinco usuarios");
                else
                {
                    if (Usuario.Text != "" && Contraseña.Text != "") //Comprobar los datos
                    {
                        //-------------Insert----------------
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandText = "Insert into [Cuentas](IdCuenta, Usuario, Contraseña, Grupo)values(@id, @user, @pass, @group);"; //Crear la string
                        cmd.Parameters.AddWithValue("@id", numId);
                        cmd.Parameters.AddWithValue("@user", Usuario.Text);
                        cmd.Parameters.AddWithValue("@pass", Contraseña.Text);
                        cmd.Parameters.AddWithValue("@group", grupoComboBox.Text);
                        cmd.Connection = sqlCon;
                        int i = cmd.ExecuteNonQuery();
                        if (i == 1) //Si se ha insertado el usuario
                        {
                            if (idioma == true)
                                MessageBox.Show("User has been added");
                            else
                                MessageBox.Show("Se ha añadido el usuario");
                            this.Close();
                        }
                        else
                        {
                            if (idioma == true)
                                MessageBox.Show("Could not add the user");
                            else
                                MessageBox.Show("No se ha podido añadir el usuario");
                        }
                    }
                    else
                    {
                        if (idioma == true)
                            MessageBox.Show("Please fill all the fields");
                        else
                            MessageBox.Show("Por favor, rellena todos los campos");
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
}
