using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using rilevazioniPresenza.Reps.UserFiles;
using rilevazioniPresenzaData.Models;
using rilevazioniPresenze.DTOs;
using rilevazioniPresenze.Reps.StampingFiles;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace rilevazioniPresenze.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StampingController : ControllerBase
    {
        private readonly IStampingRepository _repo;
        private readonly IUserRepository _repoUser;
        public StampingController(IStampingRepository repo, IUserRepository repoUser)
        {
            _repo = repo;
            _repoUser = repoUser;
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
                    Id = stamp.Id,
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
        public IActionResult userPresence(string key)
        {
            var stamps = _repo.GetStamps(key);
            List<StampingDTOs> stampsDTOsList = new();
            foreach (var stamp in stamps)
            {
                stampsDTOsList.Add(new StampingDTOs
                {
                    Id = stamp.Id,
                    ShiftType = stamp.ShiftType,
                    Orario = stamp.Orario,
                    IdMatricola = stamp.IdMatricola
                });
            }

            var shifts = _repoUser.GetShiftsByKey(key);

            var groupedStamps = stampsDTOsList.GroupBy(stamp => stamp.Orario.Date).ToList();

            List<StampingRespectDTOs> stampRespects = new();

            //StampingDTOs[] couples = new StampingDTOs[2];

            //foreach (var group in groupedStamps)
            //{
            //    var respect = false;
            //    var _group = group.ToList();
            //    var usedStampIds = new HashSet<int>();
            //    foreach (var stamp in group)
            //    {
            //        if (usedStampIds.Contains(stamp.Id))
            //            continue;
            //        DayOfWeek day = stamp.Orario.DayOfWeek;
            //        TimeSpan orarioTimbratura = stamp.Orario.TimeOfDay;
            //        var shift = shifts.FirstOrDefault(s => s.Giorno == day);
            //        if (shift == null)
            //            continue;
            //        if (stamp.ShiftType == 0)
            //        {
            //            couples = new StampingDTOs[2];
            //            TimeSpan entrataMattina = shift.T1.Value.ToTimeSpan();
            //            TimeSpan differenzaMattina = (orarioTimbratura - entrataMattina).Duration();
            //            TimeSpan entrataPomeriggio = shift.T2.Value.ToTimeSpan();
            //            TimeSpan differenzaPomeriggio = (orarioTimbratura - entrataPomeriggio).Duration();


            //            var outStamp = group.FirstOrDefault(s => (int)s.ShiftType == 1 && s.Orario > stamp.Orario);
            //            if (outStamp != null)
            //            {
            //                usedStampIds.Add(outStamp.Id);
            //                TimeSpan orarioTimbraturaUscita = outStamp.Orario.TimeOfDay;
            //                TimeSpan uscitaMattina = shift.FT1.Value.ToTimeSpan();
            //                TimeSpan differenzaUscitaMattina = (orarioTimbraturaUscita - uscitaMattina).Duration();
            //                TimeSpan uscitaPomeriggio = shift.FT2.Value.ToTimeSpan();
            //                TimeSpan differenzaUscitaPomeriggio = (orarioTimbraturaUscita - uscitaPomeriggio).Duration();
            //                if (differenzaMattina <= differenzaPomeriggio)
            //                    if (differenzaMattina <= TimeSpan.FromMinutes(30) || differenzaUscitaMattina <= TimeSpan.FromMinutes(30))
            //                        respect = true;
            //                    else
            //                    if (differenzaPomeriggio <= TimeSpan.FromMinutes(30) || differenzaUscitaPomeriggio <= TimeSpan.FromMinutes(30))
            //                        respect = true;
            //                couples[0] = stamp;
            //                couples[1] = outStamp;

            //                stampRespects.Add(new StampingRespectDTOs
            //                {
            //                    Couple = couples,
            //                    Respect = respect
            //                });
            //            }
            //            else
            //            {
            //                couples[0] = stamp;
            //                couples[1] = null;
            //                stampRespects.Add(new StampingRespectDTOs
            //                {
            //                    Couple = couples,
            //                    Respect = respect
            //                });
            //            }

            //        }
            //        else
            //        {
            //            couples[0] = null;
            //            couples[1] = stamp;
            //            stampRespects.Add(new StampingRespectDTOs
            //            {
            //                Couple = couples,
            //                Respect = respect
            //            });
            //        }
            //    }
            //}

            foreach (var group in groupedStamps)
            {
                var inStamps = group.Where(s => s.ShiftType == 0).ToList();
                var outStamps = group.Where(s => (int)s.ShiftType == 1).ToList();
                DayOfWeek day = group.Key.DayOfWeek;
                var shift = shifts.FirstOrDefault(s => s.Giorno == day);
                if (shift == null)
                    continue;

                var foundCouples = new List<(StampingDTOs? In, StampingDTOs? Out)>();

                foreach (var outStamp in outStamps)
                {
                    var closestIn = inStamps
                    .Where(i => i.Orario < outStamp.Orario)
                    .LastOrDefault();

                    if (closestIn == null)
                        foundCouples.Add((null, outStamp));
                    else
                    {
                        foundCouples.Add((closestIn, outStamp));
                        inStamps.Remove(closestIn);
                    }
                }

                foreach (var plusIn in inStamps)
                    foundCouples.Add((plusIn, null));

                foreach (var couple in foundCouples)
                {
                    bool respect = false;

                    if (couple.In != null && couple.Out != null && shift.T1 != null && shift.FT1 != null && shift.T2 != null && shift.FT2 != null )
                    {
                        TimeSpan orarioTimbratura = couple.In.Orario.TimeOfDay;
                        TimeSpan entrataMattina = shift.T1.Value.ToTimeSpan();
                        TimeSpan differenzaMattina = (orarioTimbratura - entrataMattina).Duration();
                        TimeSpan entrataPomeriggio = shift.T2.Value.ToTimeSpan();
                        TimeSpan differenzaPomeriggio = (orarioTimbratura - entrataPomeriggio).Duration();
                        
                        TimeSpan orarioTimbraturaUscita = couple.Out.Orario.TimeOfDay;
                        TimeSpan uscitaMattina = shift.FT1.Value.ToTimeSpan();
                        TimeSpan differenzaUscitaMattina = (orarioTimbraturaUscita - uscitaMattina).Duration();
                        TimeSpan uscitaPomeriggio = shift.FT2.Value.ToTimeSpan();
                        TimeSpan differenzaUscitaPomeriggio = (orarioTimbraturaUscita - uscitaPomeriggio).Duration();

                        if (differenzaMattina <= differenzaPomeriggio)
                        {
                            if (differenzaMattina <= TimeSpan.FromMinutes(30) || differenzaPomeriggio <= TimeSpan.FromMinutes(30))
                                respect = true;
                        }
                        else
                            if (differenzaUscitaPomeriggio <= TimeSpan.FromMinutes(30) || differenzaUscitaPomeriggio <= TimeSpan.FromMinutes(30))
                            respect = true;
                    }

                    stampRespects.Add(new StampingRespectDTOs
                    {
                        Couple = [couple.In, couple.Out],
                        Respect = respect
                    });
                }
            }
            return Ok(stampRespects);
        }
    }
}
