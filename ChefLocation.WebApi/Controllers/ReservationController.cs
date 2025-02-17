using ChefLocation.WebApi.Context;
using ChefLocation.WebApi.Contracts;
using ChefLocation.WebApi.Contracts.Reservation;
using ChefLocation.WebApi.Entities;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChefLocation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly ApiContext _context;

        public ReservationController(ApiContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation(CreateReservationRequest request)
        {
            var reservation = new Reservation
            {
                CountOfPeople = request.CountOfPeople,
                Email = request.Email,
                IsActive = true,
                Message = request.Message,
                NameSurname = request.NameSurname,
                PhoneNumber = request.PhoneNumber,
                ReservationDate = request.ReservationDate,
                ReservationTime = request.ReservationTime,
                ReservationStatus="Rezervasyon ulaştı"
            };
            _context.Reservations.Add(reservation);
            _context.SaveChanges();
            return Ok("Rezervasyon kayıt edildi.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReservation()
        {
            var reservation = await _context.Reservations
                .Select(a => new GetAllReservationResponse
                {
                    CountOfPeople = a.CountOfPeople,
                    Email = a.Email,
                    Message = a.Message,
                    NameSurname = a.NameSurname,
                    PhoneNumber = a.PhoneNumber,
                    ReservationDate = a.ReservationDate,
                    ReservationStatus = a.ReservationStatus,
                    ReservationTime = a.ReservationTime

                }).ToListAsync();
            return Ok(reservation);
        }
        [HttpPost("AcceptReservation")]
        public async Task<IActionResult> AcceptReservation(int id)
        {
            var findReservation = await _context.Reservations.FindAsync(id);
            var date = DateTime.Now;
            findReservation.ReservationStatus = $"Revervasyon {date} tarihinde işleme alındı.";

            if(findReservation.ReservationDate > DateTime.Now)
                findReservation.IsActive = false;
            await _context.SaveChangesAsync();
            return Ok("Rezervasyon kabul edildi..");
        }

        [HttpPost("NotAcceptReservation")]
        public async Task<IActionResult> NotAcceptReservation(int id)
        {
            var findReservation = await _context.Reservations.FindAsync(id);
            var date = DateTime.Now;
            findReservation.ReservationStatus = $"Rezervasyon {date} tarinde reddedildi.";
            findReservation.IsActive = false;
            await _context.SaveChangesAsync();
            return Ok("Rezervasyon reddedildi..");
        }
    }
}
