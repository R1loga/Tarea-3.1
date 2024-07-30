using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarea_3._1.Models;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace Tarea_3._1.Services
{
    public class FirebaseService
    {
        private readonly FirebaseClient _firebaseClient;

        public FirebaseService()
        {
            _firebaseClient = new FirebaseClient("https://fir-tarea-a1636-default-rtdb.firebaseio.com/");
        }

        public async Task CrearProductoAsync(Producto producto)
        {
            try
            {
                await _firebaseClient
                    .Child("productos")
                    .PostAsync(JsonConvert.SerializeObject(producto));
            }
            catch (Exception ex)
            {
                
                throw new Exception("Error al crear el producto: " + ex.Message);
            }
        }

        public async Task<Producto> LeerProductoAsync(string id)
        {
            try
            {
                var productos = await _firebaseClient
                    .Child("productos")
                    .OnceAsync<Producto>();

                return productos.FirstOrDefault(p => p.Object.Id == id)?.Object;
            }
            catch (Exception ex)
            {
                
                throw new Exception("Error al leer el producto: " + ex.Message);
            }
        }

        public async Task<List<Producto>> LeerProductosAsync()
        {
            try
            {
                var productos = await _firebaseClient
                    .Child("productos")
                    .OnceAsync<Producto>();

                return productos.Select(p => p.Object).ToList();
            }
            catch (Exception ex)
            {
                
                throw new Exception("Error al leer los productos: " + ex.Message);
            }
        }

        public async Task ActualizarProductoAsync(Producto producto)
        {
            if (producto == null || string.IsNullOrEmpty(producto.Id))
            {
                throw new ArgumentException("Producto o ID no pueden ser nulos");
            }

           
            var productos = await _firebaseClient
                .Child("productos")
                .OnceAsync<Producto>();

           
            var productoToUpdate = productos
                .FirstOrDefault(p => p.Object.Id == producto.Id);

            if (productoToUpdate == null)
            {
                throw new Exception("Producto no encontrado para actualización.");
            }

           
            await _firebaseClient
                .Child("productos")
                .Child(productoToUpdate.Key)
                .PutAsync(JsonConvert.SerializeObject(producto));
        }

        public async Task EliminarProductoAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("ID del producto no puede ser nulo o vacío");
            }

            
            var productos = await _firebaseClient
                .Child("productos")
                .OnceAsync<Producto>();

            
            var productoToDelete = productos
                .FirstOrDefault(p => p.Object.Id == id);

            if (productoToDelete == null)
            {
                throw new Exception("Producto no encontrado para eliminación.");
            }

            await _firebaseClient
                .Child("productos")
                .Child(productoToDelete.Key)
                .DeleteAsync();
        }
    }
}
