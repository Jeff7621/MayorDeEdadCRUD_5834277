using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MayorDeEdadCRUD
{
    public class LocalDbService
    {
        private const string DB_NAME = "ley.db3";
        private readonly SQLiteAsyncConnection _connection;

        public LocalDbService()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));

            //le indica al sistema que cree la tabla de nuestro contexto
            _connection.CreateTableAsync<Determinar>();
        }

        //Metodo para listar los registos de nuestra tabla
        public async Task<List<Determinar>> GetResultado()
        {
            return await _connection.Table<Determinar>().ToListAsync();
        }

        //Metodo para listar los registro por id
        public async Task<Determinar> GetByid(int id)
        {
            return await _connection.Table<Determinar>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        //Metodo para crear regisro
        public async Task Create(Determinar determinar)
        {
            await _connection.InsertAsync(determinar);
        }

        //Metodo para actualizar
        public async Task Update(Determinar determinar)
        {
            await _connection.UpdateAsync(determinar);
        }

        //Metodo para eliminar
        public async Task Delete(Determinar determinar)
        {
            await _connection.DeleteAsync(determinar);
        }



    }
}
