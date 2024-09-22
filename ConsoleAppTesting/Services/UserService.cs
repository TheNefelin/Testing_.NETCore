using ConsoleAppTesting.Contexts;
using ConsoleAppTesting.Models;
using ConsoleAppTesting.Models.Dtos;
using ConsoleAppTesting.Models.Entities;
using ConsoleAppTesting.Utils;
using Microsoft.EntityFrameworkCore;
using static Dapper.SqlMapper;

namespace ConsoleAppTesting.Services
{
    internal class UserService
    {
        protected readonly string _key = "+dGyff+igbDcWHYGY3IVOrvaQAggRyorfbP4DfV9YvY=";
        protected readonly AppDbContext _context;

        public UserService()
        {
            _context = DbContextSingleton.Instance;
        }

        public bool GetAll()
        {
            try
            {
                var res = _context.Users.ToList();

                if (!res.Any())
                    return false;

                foreach (var data in res)
                    Console.WriteLine($"Id: {data.Id}, Email: {data.Email}, SqlToken: {data.SqlToken}");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se produjo un error: " + ex.Message);
                return false;
            }
        }

        public bool Add(string email, string clave, string claveC)
        {
            (string hash1, string salt1) = Password.HashPassword(clave);
            (string hash2, string salt2) = Password.HashPassword(claveC);

            UserEntity entity = new UserEntity()
            {
                Id = Guid.NewGuid().ToString(),
                Email = email,
                Hash1 = hash1,
                Salt1 = salt1,
                Hash2 = hash2,
                Salt2 = salt2,
            };

            try
            {
                _context.Users.Add(entity);
                _context.SaveChanges();

                return true;
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine("Error de actualización de la base de datos: " + dbEx.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se produjo un error: " + ex.Message);
                return false;
            }
        }

        public string Login(string email, string clave)
        {
            try
            {
                var userEntity = _context.Users.SingleOrDefault(e => e.Email == email);

                if (userEntity == null)
                    return null;

                if (!Password.VerifyPassword(clave, userEntity.Hash1, userEntity.Salt1))
                    return null;

                userEntity.SqlToken = Guid.NewGuid().ToString();

                _context.Users.Update(userEntity);
                _context.SaveChanges();

                return userEntity.SqlToken;
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine("Error de actualización de la base de datos: " + dbEx.Message);
                return "Error";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se produjo un error: " + ex.Message);
                return "Error";
            }
        }

        public ServiceResponse<object> SaveData(string id, string sqlToken, string clave, DataDTO dtoData)
        {
            var user = _context.Users.SingleOrDefault(e => e.Id == id && e.SqlToken == sqlToken);

            if (user == null) 
                return new ServiceResponse<object> { Status = false, Message = "Usuario no Existe o No ha Inciiado Sesion" };

            if (!Password.VerifyPassword(clave, user.Hash2, user.Salt2))
                return new ServiceResponse<object> { Status = false, Message = "Contraseña Incorrecta" };

            DataEncryptedDTO data1 = Cryptex.Encrypt(dtoData.Data1, clave);
            DataEncryptedDTO data2 = Cryptex.Encrypt(dtoData.Data2, clave);
            DataEncryptedDTO data3 = Cryptex.Encrypt(dtoData.Data3, clave);

            SecretEntity secretEntity = new()
            {
                Data1 = data1.Encrypted,
                IV1 = data1.IV,
                Data2 = data2.Encrypted,
                IV2 = data2.IV,
                Data3 = data3.Encrypted,
                IV3 = data3.IV,
                Id_User = user.Id,
            };

            _context.Add(secretEntity);
            _context.SaveChanges();

            return new ServiceResponse<object> { Status = true, Message = "Datos Guardados Correctamente" }; ;
        }

        public void GetData(string id, string sqlToken, string clave)
        {
            var data = _context.Secrets.ToList();
            List<DataEncryptedDTO> dtolistData;

            foreach (var item in data) {
                DataDTO dto = new DataDTO()
                {
                    Data1 = Cryptex.Decrypt(item.Data1, clave, item.IV1),
                    Data2 = Cryptex.Decrypt(item.Data2, clave, item.IV2),
                    Data3 = Cryptex.Decrypt(item.Data3, clave, item.IV3)
                };

                Console.WriteLine($"Data1: {dto.Data1}");
                Console.WriteLine($"Data2: {dto.Data2}");
                Console.WriteLine($"Data3: {dto.Data3}");
            }
        }

    }
}
