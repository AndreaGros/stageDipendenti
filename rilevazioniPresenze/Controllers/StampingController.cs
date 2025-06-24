using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using rilevazioniPresenzaData.Models;
using rilevazioniPresenze.DTOs;
using rilevazioniPresenze.Reps.StampingFiles;

namespace rilevazioniPresenze.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StampingController : ControllerBase
    {
        private readonly IStampingRepository _repo;
        public StampingController(IStampingRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetStampings()
        {
            var stamps = _repo.GetStamps();
            List<StampingDTOs> stampsDTOsList = new();
            foreach (var stamp in stamps)
            {
                stampsDTOsList.Add(new StampingDTOs
                {
                    ShiftType = stamp.ShiftType,
                    Orario = stamp.Orario,
                    IdMatricola = stamp.IdMatricola
                });
            }
            return Ok(stampsDTOsList);
        }

        [HttpPost]
        public bool AddStamping(StampingDTOs stampDTOs)
        {
            Stamping stamp = new Stamping
            {
                ShiftType = stampDTOs.ShiftType,
                Orario = stampDTOs.Orario,
                IdMatricola = stampDTOs.IdMatricola
            };
            return _repo.AddStamp(stamp);
        }

        [HttpDelete("{idMatricola}/{shiftType}/{orario}")]
        public bool deleteStamping(string idMatricola, ShiftType shiftType, DateTime orario)
        {
            return _repo.RemoveStamp(idMatricola, shiftType, orario);
        }

        [HttpGet("{idMatricola}/{shiftType}/{orario}")]
        public IActionResult GetStampingByKey(string idMatricola, ShiftType shiftType, DateTime orario)
        {
            
            Stamping? stamp = _repo.GetStampByKey(idMatricola, shiftType, orario);
            if (stamp == null)
                return NotFound();

            StampingDTOs stampDTOs = new StampingDTOs
            {
                ShiftType = stamp.ShiftType,
                Orario = stamp.Orario,
                IdMatricola = stamp.IdMatricola
            };


            return Ok(stampDTOs);
        }

        //[HttpPut]
        //public bool UpdateStamping(ShiftType shiftType, DateTime orario)
        //{
        //    return _repo.UpdateStamp(shiftType, orario);
        //}
    }
}
