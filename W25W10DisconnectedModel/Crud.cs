﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;

namespace W25W10DisconnectedModel
{
    public class Crud
    {
        private SqlConnection conn;
        private SqlDataAdapter adp;
        private SqlCommandBuilder cmdBuilder;
        private DataSet ds;
        private DataTable tbl;

        public Crud()
        {
            string query = "select ProductID, ProductName, UnitPrice, UnitsInStock from Products";

            conn = new SqlConnection(Data.GetConnectionString());
            adp = new SqlDataAdapter(query, conn);
            cmdBuilder = new SqlCommandBuilder(adp);

            FillDataSet();
        }

        private void FillDataSet()
        {
            ds = new DataSet();
            adp.Fill(ds);
            tbl = ds.Tables[0];

            // define the primary key
            DataColumn[] pk = new DataColumn[1];
            pk[0] = tbl.Columns["ProductID"];
            pk[0].AutoIncrement = true;
            tbl.PrimaryKey = pk;
        }

        public DataTable GetAllProducts()
        {
            return tbl;
        }

        public DataRow? GetProductById(int id)
        {
            var row = tbl.Rows.Find(id);
            return row;
        }
    }
}
