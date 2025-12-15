using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace mozijegyfoglalo
{
    public class Mozi
    {
        public string cim {  get; set; }
        public DateTime idopont { get; set; }
        public string terem { get; set; }
        public int szabadhelyek { get; set; }
        public bool _3D { get; set; }

        public Mozi(string cim, DateTime idopont, string terem, int szabadhelyek, bool _3D)
        {
            this.cim = cim;
            this.idopont = idopont;
            this.terem = terem;
            this.szabadhelyek = szabadhelyek;
            this._3D = _3D;
        }
    }

    public partial class MainWindow : Window
    {
        public List<Mozi> MoziFilmek = new List<Mozi>();
        private ICollectionView moziView; 
        public MainWindow()
        {
            InitializeComponent();
            
            moziView = CollectionViewSource.GetDefaultView(MoziFilmek);
            datagrid.ItemsSource = moziView;

            MoziFilmek.Add(
                new Mozi("Gyűrűk Ura", new DateTime(2025, 12, 15, 19, 30, 0), "1-es terem", 12, false));
            MoziFilmek.Add(
                new Mozi("Venom", new DateTime(2025, 12, 15, 20, 25, 0), "2-es terem", 10, true));
            MoziFilmek.Add(
                new Mozi("Up", new DateTime(2025, 12, 15, 14, 0, 0), "4-es terem", 20, false));
            MoziFilmek.Add(
                new Mozi("Step Up", new DateTime(2025, 12, 15, 19, 50, 0), "3-es terem", 8, true));
            MoziFilmek.Add(
                new Mozi("FNAF 2", new DateTime(2025, 12, 16, 10, 50, 0), "1-es terem", 0, true));
            MoziFilmek.Add(
                new Mozi("IT", new DateTime(2025, 12, 15, 23, 0, 0), "2-es terem", 1, true));
            datagrid.ItemsSource = MoziFilmek;
        }

        private void adatokbetoltese(object sender, RoutedEventArgs e)
        {
            datagrid.ItemsSource = MoziFilmek;
        }

        private void foglalas(object sender, RoutedEventArgs e)
        {
            if(datagrid.SelectedItem is Mozi)
            {
                ((Mozi)datagrid.SelectedItem).szabadhelyek = ((Mozi)datagrid.SelectedItem).szabadhelyek - 1;
                datagrid.Items.Refresh();
            }
        }

        private void csakaholvanhely(object sender, RoutedEventArgs e)
        {
            List<Mozi> csakaholhelyvan1 = new List<Mozi>();
            foreach (var mozi in MoziFilmek)
            {
                if(mozi.szabadhelyek>0)
                    csakaholhelyvan1.Add(mozi);
            }
            datagrid.ItemsSource=csakaholhelyvan1;
            datagrid.Items.Refresh();
        }

        private void legnepszerubb(object sender, RoutedEventArgs e)
        {
            var legnepszerubbFilmek = MoziFilmek.Where(m => m.szabadhelyek == 0).ToList();
            datagrid.ItemsSource = legnepszerubbFilmek;
        }

        private void atlagosszabadhely(object sender, RoutedEventArgs e)
        {
            if (MoziFilmek.Count == 0)
            {
                MessageBox.Show("Nincs adat a listában!");
                return;
            }

            double osszesSzabadHely = 0;
            foreach (var mozi in MoziFilmek)
            {
                osszesSzabadHely += mozi.szabadhelyek;
            }

            double atlag = osszesSzabadHely / MoziFilmek.Count;

            MessageBox.Show($"Átlagos szabad helyek száma: {atlag:F1} hely",
                            "Átlagos szabad helyek",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);

            datagrid.ItemsSource = MoziFilmek;
            datagrid.Items.Refresh();
        }

        private void csak3d(object sender, RoutedEventArgs e)
        {
            List<Mozi> harminddotFilmek = new List<Mozi>();
            foreach (var mozi in MoziFilmek)
            {
                if (mozi._3D == true)
                    harminddotFilmek.Add(mozi);
            }

            datagrid.ItemsSource = harminddotFilmek;
            datagrid.Items.Refresh();
        }
    }
}