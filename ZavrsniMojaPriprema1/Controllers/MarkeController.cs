using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ZavrsniMojaPriprema1.Interfaces;
using ZavrsniMojaPriprema1.Models;

namespace ZavrsniMojaPriprema1.Controllers
{
    public class MarkeController : ApiController
    {
        IMarkaRepository _repository { get; set; }

        public MarkeController(IMarkaRepository repository)
        {
            _repository = repository;
        }

        private bool isNumber(string num)
        {
            return int.TryParse(num, out int n);
        }


        public IEnumerable<MarkaDTO> Get()
        {
            return _repository.GetAll().ProjectTo<MarkaDTO>();
        }


        //[Authorize]
        [ResponseType(typeof(MarkaDTO))]
        public IHttpActionResult Get(int id)
        {
            if (!isNumber(id.ToString()) || id < 1)
            {
                return BadRequest("Pogresan tip ili vrednost promenjive id.");
            }

            var marka = _repository.GetById(id);
            if (marka == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<MarkaDTO>(marka));
        }


        //[Authorize]
        [ResponseType(typeof(Marka))]
        public IHttpActionResult Post(Marka marka)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _repository.Add(marka);
            }
            catch
            {
                return BadRequest("Marka istog naziva vec postoji u bazi.");
            }

            return CreatedAtRoute("DefaultApi", new { id = marka.Id }, marka);
        }


        //[Authorize]
        [ResponseType(typeof(Marka))]
        public IHttpActionResult Put(int id, Marka marka)
        {
            if (!isNumber(id.ToString()) || id < 1)
            {
                return BadRequest("Pogresan tip ili vrednost promenjive id.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != marka.Id)
            {
                return BadRequest();
            }

            try
            {
                _repository.Update(marka);
            }
            catch
            {
                throw;
            }

            return Ok(marka);
        }


        //[Authorize]
        [ResponseType(typeof(void))]
        public IHttpActionResult Delete(int id)
        {
            if (!isNumber(id.ToString()) || id < 1)
            {
                return BadRequest("Pogresan tip ili vrednost promenjive id.");
            }

            var marka = _repository.GetById(id);
            if (marka == null)
            {
                return NotFound();
            }

            _repository.Delete(marka);

            return Ok();
        }
    }
}
