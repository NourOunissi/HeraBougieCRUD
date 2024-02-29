

using MySql.Data.MySqlClient;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace HeraBougieCRUD.Vue
{
    public partial class Articles : UserControl
    {
        // Objet nécessaires pour SQL
        const string _dsn =
            "server=localhost;database=hb;username=root;password=;";
        private MySqlConnection _connexion = new MySqlConnection(_dsn); // connection
        private MySqlCommand _command; //requete 
        private MySqlDataAdapter _adapter; // lire des donner et remplir et une table 

        private DataTable _dt; // Ne fait pas partie de MySQL.Data (objet .NET)


        public Articles()
        {
            InitializeComponent();
            afficher();
        }

        //fonction prive renvoie rien 
        private void afficher()
        {

            try
            {
                _adapter = new MySqlDataAdapter("SELECT * FROM produits;", _connexion);  // instancier
                _dt = new DataTable(); // instancie une table
                _adapter.Fill(_dt); // on demande à l'adapteur de remplir la table ce qui trouve dans  _dt
                dgArticle.ItemsSource = _dt.DefaultView; //transfere les donnees de _dt vers dgEtudiants
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                _command.Dispose();
                _connexion.Close();
                _connexion.Dispose();

            }
        }


        private void effacer()
        {
            txtId.Text = "";
            txtNom.Text = "";
            txtDescription.Text = "";
            txtPrix.Text = "";
            txtTaille.Text = "";
            txtType.Text = "";
            txtImage.Text = "";
            txtId_Couleur.Text = "";
            txtId_Cire.Text = "";
            txtId_Parfum.Text = "";


        }

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("btnAjouter_Click");
        }

        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("btnModifier_Click");
        }

        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            string _sql = "DELETE FROM produits WHERE id = @Id;";
            _command = new MySqlCommand(_sql, _connexion);
            _command.Parameters.AddWithValue("@Id", txtId.Text);
            _connexion.Open();
            _command.ExecuteNonQuery();
            _connexion.Close();

            afficher();
            effacer();
        }

        private void dgArticle_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if ((DataRowView)dgArticle.SelectedItem != null)
            {
                DataRowView _drv = (DataRowView)dgArticle.SelectedItem;

                txtId.Text = _drv.Row["id"].ToString();
                txtNom.Text = _drv.Row["nom"].ToString();
                txtPrix.Text = _drv.Row["prix"].ToString();
                txtType.Text = _drv.Row["type"].ToString();
                txtTaille.Text = _drv.Row["taille"].ToString();
                txtImage.Text = _drv.Row["image"].ToString();
                txtDescription.Text = _drv.Row["description"].ToString();
                txtId_Couleur.Text = _drv.Row["id_couleur"].ToString();
                txtId_Cire.Text = _drv.Row["id_cire"].ToString();
                txtId_Parfum.Text = _drv.Row["id_parfum"].ToString();
            }

        }

    }
}




