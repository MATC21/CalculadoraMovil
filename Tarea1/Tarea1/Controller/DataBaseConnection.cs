using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tarea1.Models;

namespace Tarea1.Controller
{
    public class DataBaseConnection
    {
        public SQLiteAsyncConnection Connection { get; set; }
        public DataBaseConnection(string path)
        {
            Connection = new SQLiteAsyncConnection(path);
            //Creamos una tabla Tarea
            Connection.CreateTableAsync<Operaciones>().Wait();
        }

        public async Task<int> Insertar(Operaciones item)
        {
            return await Connection.InsertAsync(item);
        }
        public async Task<List<Operaciones>> Listar()
        {
            return await Connection.Table<Operaciones>().ToListAsync();
        }
        public async Task<int> Eliminar(Operaciones item)
        {
            return await Connection.DeleteAsync(item);
        }
        public async Task<int> EditItem(Operaciones item)
        {
            return await Connection.UpdateAsync(item);
        }
    }
}
