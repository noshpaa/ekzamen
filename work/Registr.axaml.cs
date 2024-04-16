using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace work;

public partial class regis : Window
{
    private MySqlConnection _connection;
    private string connectionString = "server=localhost;port=3301;database=abd;user id=root;password=Ghost45)";
    public regis()
    {
        InitializeComponent();
    }

    private void Add_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            _connection = new MySqlConnection(connectionString);
            _connection.Open();
            string insert = "INSERT INTO fitnes (fam, name, phone, yearbh, pol, skidka) VALUES ('"+text2.Text+"', '"+text3.Text+"', '"+text4.Text+"', '"+text5.Text+"''"+text6.Text+"', '"+text7.Text+"', )";
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

    private void Back_OnClick(object? sender, RoutedEventArgs e)
    {
        regis _mainWindow = new regis();
        _mainWindow.Show();
        this.Hide();
    }
}