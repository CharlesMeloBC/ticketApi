﻿using Microsoft.EntityFrameworkCore;
using ticketApi.Data;
using ticketApi.Models;

namespace ticketApi.Services
{
    public class TicketService
    {
        private readonly AppDbContext _context;

        public TicketService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TicketDto>> GetAllTicketsAsyncActive()
        {
            var activeTickets = await _context.Tickets.Where(s => s.IsActive)
            .ToListAsync();

            var ticketDtos = activeTickets.Select(s => new TicketDto
            {
                Id = s.Id,
                Name = s.Name,
                IsActive = s.IsActive,
                Status = s.Status,
            }).ToList();

            return ticketDtos;
        }

        public async Task<List<TicketDto>> GetAllTicketsAsync(bool includeDeleted = false)
        {
            var allTickets = await _context.Tickets.ToListAsync();

            var ticketsDtos = allTickets.Select(t => new TicketDto
            {
                Id = t.Id,
                Name = t.Name,
                IsActive = t.IsActive,
                Status = t.Status,
                Created = t.Created,
                DeletedAT = includeDeleted ? t.DeletedAT : null,

            }).ToList();

            return ticketsDtos; 
        }

        public async Task<TicketModel?> GetTicketByIdAsync(int id)
        {
            return await _context.Tickets.FindAsync(id);
        }

        public async Task<TicketDto> CreateTicketAsync(TicketDto ticketDto)
        {
            var ticket = new TicketModel(ticketDto.Name);

            _context.Tickets.Add(ticket);

            await _context.SaveChangesAsync();

            ticketDto.Id = ticket.Id;
            ticketDto.Status = ticket.Status;
            ticketDto.IsActive = ticket.IsActive;
            ticketDto.Created = ticket.Created;

            return ticketDto;
        }

        public async Task<bool> DeleteTicketAsync(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);

            if (ticket == null)
                return false;

            ticket.Disable();

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
