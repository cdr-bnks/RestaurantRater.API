using RestaurantRater.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestaurantRater.Controllers
{
    public class RestaurantController : ApiController
    {
        private readonly RestaurantDbContext _context = new RestaurantDbContext(); 
        // Post (Create)
        // api/Restaurant
        [HttpPost]
        public async Task<IHttpActionResult> CreateRestaurant([FromBody]Restaurant model)
        {
            if (model == null)
            {
                return BadRequest("Your request body cannot be empty. ");
            }
            
            // If the model is valid
            if (ModelState.IsValid)
            {
                // Store the model in the database
                _context.Restaurants.Add(model);
               int changecount = await _context.SaveChangesAsync();

                return Ok("You're Restaurant was created!");
            }

            // The model is not valid, go ahead and reject it
            return BadRequest(ModelState);
        }

        // Get All
        // api/Restaurant
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Restaurant> restaurants = await _context.Restaurants.ToListAsync();
            return Ok(restaurants);
        }

        // Get by Id
        // api/Restaurant/{id}
        [HttpGet]
        public async Task<IHttpActionResult> GetById([FromUri] int id)
        {
            Restaurant restaurant = await _context.Restaurants.FindAsync(id);
            
            if(restaurant != null)
            {
                return Ok(restaurant);
            }

            return NotFound();
        }

        // Put (Update)
        // api/Restaurant/{id}
        [HttpPut]
        public async Task<IHttpActionResult> UpdateRestaurant([FromUri] int id, [FromBody] Restaurant updatedRestaurant)
        {
            // Check the ids if the match
            if(id != updatedRestaurant?.Id)
            {
                return BadRequest("ids do not match.");
            }

            // Check the ModelState
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Find the restaurant in the database
            Restaurant restaurant = await _context.Restaurants.FindAsync(id);

            // If the restaurant does not exist does not exist then request somthing else
            if(restaurant == null)
            {
                return NotFound();
            }

            // Update the properties
            restaurant.Name = updatedRestaurant.Name;
            restaurant.Address = updatedRestaurant.Address;

            // Save the changes
            await _context.SaveChangesAsync();

                return Ok("The restaurant was updated!");
        }

        // Delete (delete)
        // api/Restaurant/{id}
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteRestaurant([FromUri] int id)
        {
            Restaurant restaurant = await _context.Restaurants.FindAsync(id);

            if(restaurant == null)
            {
                return NotFound();
            }

            _context.Restaurants.Remove(restaurant);

            if(await _context.SaveChangesAsync() == 1)
            {
                return Ok("The restaurant was successfully deleted.");
            }

            return InternalServerError();
        }
    }
}
