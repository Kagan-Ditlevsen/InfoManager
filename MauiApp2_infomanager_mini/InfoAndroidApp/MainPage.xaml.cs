using InfoAndroidApp.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace InfoAndroidApp;

public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage()
    {
        InitializeComponent();
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);

        //var db = new Db();
        //using (var db = new Db())
        //{

        //}
        //SqlConnection conn = new SqlConnection(Db.SqlConnectionString);
        //SqlCommand cmd = conn.CreateCommand();
        //cmd.CommandText = "SELECT * FROM DailyType";
        //cmd.CommandTimeout= 10;
        //cmd.CommandType = CommandType.Text;
        //SqlDataAdapter adapter = new SqlDataAdapter(
        //    "SELECT * FROM Customers; SELECT * FROM Orders", conn);
        //adapter.TableMappings.Add("Table", "Customer");
        //adapter.TableMappings.Add("Table1", "Order");
        //DataSet ds = new DataSet();

        //adapter.Fill(ds);

        //DataTable dt = new DataTable();
        //dt.Load(cmd.ExecuteReader());
        //MauiProgram.sqlConnection.Close();
        //Task<DataSet> ds = Db.DataSetFromReaderAsync(new[] { "DailyType", "DailyTypeOption" });

        //LabelTxt.Text = $"Success {ds..Tables.Count} tables";
    }
}

