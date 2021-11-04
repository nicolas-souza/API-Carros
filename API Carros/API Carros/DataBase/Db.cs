using API_CARROS.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace API_CARROS.DataBase
{
    public class Db
    {
        private string sql { get; set; }

        String ConnectionString;
        SqlConnection _mConn;

        public Db()
        {

            
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            ConnectionString = configuration.GetSection("ConnectionString").GetSection("ServerConnection").Value;
        }

        public void Conectar()
        {
            try
            {
                _mConn = new SqlConnection(ConnectionString);
                _mConn.Open();
            }
            catch (SqlException ex)
            {

            }
        }

        public void Desconectar()
        {
            _mConn.Close();
        }

        public List<Carro> GetAllCarros()
        {
            DataTable dtCarros = new();

            sql = "SELECT * from carros";

            try
            {
                if (_mConn.State == ConnectionState.Open)
                {
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, ConnectionString);

                    sqlDataAdapter.Fill(dtCarros);

                }
            } catch (Exception ex)
            {

            }

            return DTOCarro(dtCarros);
        }

        public List<Carro> PostCarro(Carro carro)
        {

            try
            {
                if (_mConn.State == ConnectionState.Open)
                {
                    sql = "INSERT INTO carros (Placa, Fabricante, Modelo, Ano, Cor, Descricao) " +
                                 "VALUES(@placa, @fabricante, @modelo, @ano, @cor, @descricao)";


                    SqlCommand sqlCommand = new SqlCommand(sql, _mConn);
                    sqlCommand.Parameters.AddWithValue("@placa", carro.Placa);
                    sqlCommand.Parameters.AddWithValue("@fabricante", carro.Fabricante);
                    sqlCommand.Parameters.AddWithValue("@modelo", carro.Modelo);
                    sqlCommand.Parameters.AddWithValue("@ano", carro.Ano);
                    sqlCommand.Parameters.AddWithValue("@cor", carro.Cor);
                    sqlCommand.Parameters.AddWithValue("@descricao", carro.Descricao);


                    try
                    {
                        sqlCommand.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {

                    }

                    var car = GetByPlaca(carro.Placa);

                    return car;
                }


            }
            catch (Exception ex)
            {

            }
            return new List<Carro>();
        }

        public List<Carro> GetByPlaca(string placa)
        {
            try
            {
                if (_mConn.State == ConnectionState.Open)
                {
                    try
                    {
                        DataTable dtCarro = new DataTable();

                        sql = "SELECT * FROM carros WHERE Placa=@placa";

                        SqlCommand sqlCommand = new SqlCommand(sql, _mConn);
                        sqlCommand.Parameters.AddWithValue("@placa", placa);

                        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                        sqlDataAdapter.Fill(dtCarro);

                        return DTOCarro(dtCarro);

                    } catch (Exception ex)
                    {

                    }
                }
            } catch (Exception ex)
            {

            }

            return new List<Carro>();
        }

        public List<Carro> PutCarro (Carro carro)
        {
            try
            {
                if (_mConn.State == ConnectionState.Open)
                {
                    try
                    {
                        if (GetByPlaca(carro.Placa).Count() > 0)
                        {
                            sql = @"UPDATE carros SET Placa = @placa, Fabricante = @fabricante, Modelo = @modelo, 
                                Ano = @ano, Cor = @cor, Descricao=@descricao WHERE placa = @placa";

                            SqlCommand sqlCommand = new SqlCommand(sql, _mConn);

                            sqlCommand.Parameters.AddWithValue("@placa", carro.Placa);
                            sqlCommand.Parameters.AddWithValue("@fabricante", carro.Fabricante);
                            sqlCommand.Parameters.AddWithValue("@modelo", carro.Modelo);
                            sqlCommand.Parameters.AddWithValue("@ano", carro.Ano);
                            sqlCommand.Parameters.AddWithValue("@cor", carro.Cor);
                            sqlCommand.Parameters.AddWithValue("@descricao", carro.Descricao);

                            try
                            {
                                sqlCommand.ExecuteNonQuery();

                                return GetByPlaca(carro.Placa);
                            }
                            catch (Exception ex)
                            {

                            }

                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            } catch (Exception ex)
            {

            }

            return new List<Carro>();
        }

        public List<Carro> DeleteCarro (string placa)
        {
            try
            {
                if (_mConn.State == ConnectionState.Open)
                {
                    sql = "DELETE FROM carros WHERE Placa = @placa";

                    SqlCommand sqlCommand = new SqlCommand(sql, _mConn);

                    sqlCommand.Parameters.AddWithValue("@placa", placa);

                    try
                    {
                        var lst = GetByPlaca(placa);

                        sqlCommand.ExecuteNonQuery();

                        return lst;

                    } catch (Exception ex)
                    {

                    }

                }

            } catch (Exception ex)
            {

            }

            return new List<Carro>();
        }

        public List<Carro> DTOCarro(DataTable dtFilmes)
        {
            List<Carro> ListCarros = new();

            foreach (DataRow item in dtFilmes.Rows)
            {
                Carro carro = new Carro();

                carro.Placa= item.Field<string>("Placa");
                carro.Fabricante = item.Field<string>("Fabricante");
                carro.Modelo = item.Field<string>("Modelo");
                carro.Ano = item.Field<string>("Ano");
                carro.Cor = item.Field<string>("Cor");
                carro.Descricao = item.Field<string>("Descricao");

                ListCarros.Add(carro);
            }


            return ListCarros;
        }



    }
}
