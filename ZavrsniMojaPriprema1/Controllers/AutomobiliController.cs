using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ZavrsniMojaPriprema1.Interfaces;
using ZavrsniMojaPriprema1.Models;

namespace ZavrsniMojaPriprema1.Controllers
{
    public class AutomobiliController : ApiController
    {
        IAutomobilRepository _repository { get; set; }

        public AutomobiliController(IAutomobilRepository repository)
        {
            _repository = repository;
        }

        private bool isNumber(string num)
        {
            return int.TryParse(num, out int n);
        }


        public IEnumerable<AutomobilDTO> Get()
        {
            return _repository.GetAll().ProjectTo<AutomobilDTO>();
        }


        //[Authorize]
        [ResponseType(typeof(AutomobilDTO))]
        public IHttpActionResult Get(int id)
        {
            if (!isNumber(id.ToString()) || id < 1)
            {
                return BadRequest("Pogresan tip ili vrednost promenjive id.");
            }

            var automobil = _repository.GetById(id);
            if (automobil == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<AutomobilDTO>(automobil));
        }


        //[Authorize]
        [ResponseType(typeof(Automobil))]
        public IHttpActionResult Post(Automobil automobil)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _repository.Add(automobil);
            }
            catch (Exception e)
            {
                if (e.InnerException.InnerException.Message.Contains("duplicate"))
                {
                    return BadRequest("Vec postoji takav model u bazi!");
                }
                else
                {
                    return BadRequest(e.InnerException.InnerException.Message);
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = automobil.Id }, automobil);
        }


        //[Authorize]
        [ResponseType(typeof(Automobil))]
        public IHttpActionResult Put(int id, Automobil automobil)
        {
            if (!isNumber(id.ToString()) || id < 1)
            {
                return BadRequest("Pogresan tip ili vrednost promenjive id.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != automobil.Id)
            {
                return BadRequest();
            }

            try
            {
                _repository.Update(automobil);
            }
            catch (Exception e)
            {
                if (e.InnerException.InnerException.Message.Contains("duplicate"))
                {
                    return BadRequest("Vec postoji takav model u bazi!");
                }
                else
                {
                    return BadRequest(e.InnerException.InnerException.Message);
                }
            }

            return Ok(automobil);
        }


        //[Authorize]
        [ResponseType(typeof(void))]
        public IHttpActionResult Delete(int id)
        {
            if (!isNumber(id.ToString()) || id < 1)
            {
                return BadRequest("Pogresan tip ili vrednost promenjive id.");
            }

            var automobil = _repository.GetById(id);
            if (automobil == null)
            {
                return NotFound();
            }

            _repository.Delete(automobil);

            return Ok();
        }


        //[Authorize]
        [ResponseType(typeof(IQueryable<AutomobilDTO>))]
        [Route("api/automobili/pretraga")]
        public IHttpActionResult PostPretraga([FromBody] PretragaDTO pretraga)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(_repository.PretragaPoCeni(pretraga.minCena, pretraga.maxCena).ProjectTo<AutomobilDTO>());
        }


        //[Authorize]
        [ResponseType(typeof(IQueryable<AutomobilDTO>))]
        [Route("api/automobili/snaga")]

        public IHttpActionResult GetSnaga(int snaga)
        {
            if (!isNumber(snaga.ToString()) || snaga < 1)
            {
                return BadRequest("Pogresan tip ili vrednost promenjive snaga.");
            }

            return Ok(_repository.PrikaziPoSnazi(snaga).ProjectTo<AutomobilDTO>());
        }


        //[Authorize]
        [ResponseType(typeof(IQueryable<AutomobilDTO>))]
        [Route("api/automobili/filterbymarka")]
        public IHttpActionResult GetFilterByMarka(int id)
        {
            if (!isNumber(id.ToString()) || id < 1)
            {
                return BadRequest("Pogresan tip ili vrednost promenjive id.");
            }

            return Ok(_repository.PrikaziAutomobilePoIdKategorije(id).ProjectTo<AutomobilDTO>());
        }

    }
}
