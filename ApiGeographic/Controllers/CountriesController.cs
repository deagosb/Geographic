using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ApiGeographic.Models;

namespace ApiGeographic.Controllers
{
    public class CountriesController : ApiController
    {
        private ApiGeographicContext db = new ApiGeographicContext();

        // GET: api/Countries
        public IQueryable<Country> GetCountries()
        {
            return db.Countries;
        }

        //// GET: api/Countries/5
        //[ResponseType(typeof(Country))]
        //public IHttpActionResult GetCountry(int id)
        //{
        //    Country country = db.Countries.Find(id);
        //    if (country == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(country);
        //}

        // GET: api/Countries/1
        [Route("api/Countries/{id}")]
        public IHttpActionResult GetCountry(int id)
        {

            var countries = (from c in db.Countries
                             join s in db.States on c.Id equals s.CountryId
                             join i in db.Cities on s.Id equals i.StateId
                             where c.Id.Equals(id)
                             select new
                             {
                                 IdCountry = c.Id,
                                 CountryName = c.Name,
                                 IdState = s.Id,
                                 StateName = s.Name,
                                 IdCity = i.Id,
                                 CityName = i.Name
                             }).ToList();

            return Ok(countries);
        }

        // GET: api/Countries/1/2
        [Route("api/Countries/{id}/{idState}")]
        public IHttpActionResult GetCountry(int id, int idState)
        {

            var countries = (from c in db.Countries
                             join s in db.States on c.Id equals s.CountryId
                             join i in db.Cities on s.Id equals i.StateId
                             where c.Id.Equals(id) 
                             && s.Id.Equals(idState)
                             select new
                             {
                                 IdCountry = c.Id,
                                 CountryName = c.Name,
                                 IdState = s.Id,
                                 StateName = s.Name,
                                 IdCity = i.Id,
                                 CityName = i.Name
                             }).ToList();

            return Ok(countries);
        }


        // GET: api/Countries/1/2/1
        [Route("api/Countries/{id}/{idState}/{idCity}")]
        public IHttpActionResult GetCountry(int id, int idState, int idCity)
        {

            var countries = (from c in db.Countries
                             join s in db.States on c.Id equals s.CountryId
                             join i in db.Cities on s.Id equals i.StateId
                             where c.Id.Equals(id)
                             && s.Id.Equals(idState)
                             && i.Id.Equals(idCity)
                             select new
                             {
                                 IdCountry = c.Id,
                                 CountryName = c.Name,
                                 IdState = s.Id,
                                 StateName = s.Name,
                                 IdCity = i.Id,
                                 CityName = i.Name
                             }).ToList();

            return Ok(countries);
        }


        // PUT: api/Countries/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCountry(int id, Country country)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != country.Id)
            {
                return BadRequest();
            }

            db.Entry(country).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Countries
        [ResponseType(typeof(Country))]
        public IHttpActionResult PostCountry(Country country)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Countries.Add(country);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = country.Id }, country);
        }

        // DELETE: api/Countries/5
        [ResponseType(typeof(Country))]
        public IHttpActionResult DeleteCountry(int id)
        {
            Country country = db.Countries.Find(id);
            if (country == null)
            {
                return NotFound();
            }

            db.Countries.Remove(country);
            db.SaveChanges();

            return Ok(country);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CountryExists(int id)
        {
            return db.Countries.Count(e => e.Id == id) > 0;
        }
    }
}