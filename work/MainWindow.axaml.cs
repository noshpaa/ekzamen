using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;
namespace work;
public partial class MainWindow : Window
{
    private MySqlConnection _connection;
    private string connectionString = "server=localhost;port=3306;database=abd;user id=root;password=root)";
    private List<users_pon> _fitnes;
    private List<filters> _filters;
    private List<users_pon> _regis;

    public MainWindow()
    {
        InitializeComponent();
        string sql = "SELECT * FROM fitness";
        ShowTable(sql);
        filter_user();
    }
    public class users_pon
    {
        
        public string fam { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public object yearbh { get; set; }
        public string Pol { get; set; }
        public string Skidka { get; set; }
    }

    public class filters
    {
        public string name { get; set; }
        public string fam { get; set; }
    }

    private void ShowTable(string sql)
    {
        
        _fitnes = new List<users_pon>();
        _connection = new MySqlConnection(connectionString);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var current = new users_pon()
            {
                fam = reader.GetString("fam"),
                name = reader.GetString("name"),
                phone = reader.GetString("phone"),
                yearbh= reader.GetDateTime("yearbh"),
                Pol= reader.GetString("Pol"),
                Skidka = reader.GetString("skidka")
            };
            _fitnes.Add(current);
        }

        Grid.ItemsSource = _fitnes;
    }

    private void Add_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            _connection = new MySqlConnection(connectionString);
            _connection.Open();
            string insert = "INSERT INTO fitnes (name, fam, phone, yearbh, pol, skidka) VALUES ('"+text2.Text+"', '"+text3.Text+"', '"+text4.Text+"', '"+text5.Text+"')";
            MySqlCommand command = new MySqlCommand(insert, _connection);
            command.ExecuteNonQuery();
            _connection.Close();
            text1.Text = "Succesfully data";
        }
        catch (Exception exception)
        {
            text1.Text = "Incorrect data";
        }
    }

    private void Update_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            _connection = new MySqlConnection(connectionString);
            _connection.Open();
            string update = "UPDATE fitnes SET name = '"+text2.Text+"', phone = '"+text4.Text+"', yearbh = '"+text5.Text+"', pol = '\"+text6.Text+\"', skidka = '\"+text7.Text+\"', WHERE fam = '"+text1.Text+"'";
            MySqlCommand command = new MySqlCommand(update, _connection);
            command.ExecuteNonQuery();
            _connection.Close();
            text1.Text = "Succesfully data";
        }
        catch (Exception exception)
        {
            text1.Text = "Incorrect data";
        }
    }

    private void Delete_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            _connection = new MySqlConnection(connectionString);
            _connection.Open();
            string update = "DELETE FROM fitnes WHERE name = '"+text1.Text+"'";
            MySqlCommand command = new MySqlCommand(update, _connection);
            command.ExecuteNonQuery();
            _connection.Close();
            text1.Text = "Succesfully data";
        }
        catch (Exception exception)
        {
            text1.Text = "Incorrect data";
        }
    }

    private void Save_OnClick(object? sender, RoutedEventArgs e)
    {
        _regis = new List<users_pon>();
        _connection = new MySqlConnection(connectionString);
        _connection.Open();
        string sql = "SELECT * FROM fitnes";
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read() && reader.HasRows)
        {
            var current = new users_pon()
            {
                fam = reader.GetString("fam"),
                name = reader.GetString("name"),
                phone = reader.GetString("phone"),
                yearbh = reader.GetDateTime("yearbh"),
                Skidka = reader.GetString("Skidka")
            };
            _fitnes.Add(current);
        }

        Grid.ItemsSource = _fitnes;
    }

    private void filter_user()
    {
        _filters = new List<filters>();
        _connection = new MySqlConnection(connectionString);
        _connection.Open();
        
        string sql = "SELECT name, name FROM fitnes";
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read() && reader.HasRows)
        {
            var current = new filters()
            {
                fam = reader.GetString("fam"),
                name = reader.GetString("name"),
            };
            _filters.Add(current);
        }
        var combobox = this.Find<ComboBox>("Box");
        combobox.ItemsSource = _filters;
        _connection.Close();


    }

    private void Box_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var combobox = (ComboBox)sender;
        var current = combobox.SelectedItem as filters;
        var filter = _fitnes.Where(x => x.name == current.name).ToList();
        Grid.ItemsSource = filter;
    }

    private void Search_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        string sql1 = "SELECT fam, name, name, phone, yearbh, skidka FROM fitnes WHERE name LIKE '%"+search.Text+"%' OR last_name LIKE '%"+search.Text+"%' ";
        ShowTable(sql1);
    }

    private void A_Z_OnClick(object? sender, RoutedEventArgs e)
    {
        string sql = "SELECT fam, name, phone, yearbh, skidka FROM fitnes ORDER BY name desc";
        ShowTable(sql);
    }

    private void Back_OnClick(object? sender, RoutedEventArgs e)
    {
        bottom _mainWindow = new bottom();
        this.Hide();
        _mainWindow.Show();
    }
}