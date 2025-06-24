using Microsoft.AspNetCore.Authorization;
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
        public IActionResult GetStampings(string? key)
        {
            var stamps = _repo.GetStamps(key);
            List<StampingDTOs> stampsDTOsList = new();
            foreach (var stamp in stamps)
            {
                stampsDTOsList.Add(new StampingDTOs
                {
                    Id=stamp.Id,
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

        [HttpDelete("{key}")]
        public bool deleteStamping(int key)
        {
            return _repo.RemoveStamp(key);
        }

        [HttpPut("{key}")]
        [Authorize]
        public bool UpdateStamping(int key, StampingWithoutIdDTOs stampDTOs)
        {
            return _repo.UpdateStamp(key, stampDTOs);
        }

        [HttpGet("{key}")]
        public IActionResult userPresence(string? key)
        {
            var stamps = _repo.GetStamps(key);

            return NotFound();
        }
    }
}
