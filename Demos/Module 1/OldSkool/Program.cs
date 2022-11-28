using System.Data.SqlClient;

namespace OldSkool;
class Program
{
    const string constr = @"Server=.\sqlexpress;Database=ProductCatalog;Trusted_Connection=True;";
    static void Main(string[] args)
    {
        var data = ReadData();
        foreach(var b in data)
            System.Console.WriteLine($"[{b.Id}] {b.Name}, {b.WebSite}");

    }

    private static IEnumerable<Brand> ReadData(string filter = "%")
    {
        SqlConnection con = new SqlConnection(constr);
        con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "Select * From Core.Brands Where Name like @val";
        cmd.Parameters.AddWithValue("@val", filter);
        SqlDataReader rdr = cmd.ExecuteReader(System.Data.CommandBehavior.SequentialAccess);
        //var brabds = new List<Brand>();
        while (rdr.Read())
        {
            var b = new Brand
            {
                Id = (long)rdr[0],
                Name = (string)rdr[1],
                WebSite = (string)rdr[2],
                TimeStamp = (byte[])rdr[3]
            };
            //brabds.Add(b);
            yield return b;

        }
        con.Close();
        //return brabds;
    }
}
