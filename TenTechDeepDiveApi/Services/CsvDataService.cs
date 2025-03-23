using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper; // Install-Package CsvHelper
using CsvHelper.Configuration;
using TenTechDeepDiveApi.Data;
using TenTechDeepDiveApi.Models;

namespace TenTechDeepDiveApi.Services
{
    public class CsvDataService
    {
        private readonly ApplicationDbContext _context;

        public CsvDataService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void SeedMembersFromCsv(Stream csvStream)
        {
            using (var reader = new StreamReader(csvStream))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.Read(); // Skip header row
                csv.ReadHeader();
                while (csv.Read())
                {
                    var member = new Member
                    {
                        Name = csv.GetField("name"),
                        Surname = csv.GetField("surname"),
                        BookingCount = csv.GetField<int>("booking_count"),
                        DateJoined = DateTime.Parse(csv.GetField("date_joined"))
                    };
                    _context.Members.Add(member);
                }
                _context.SaveChanges();
            }
        }

        public void SeedInventoryFromCsv(Stream csvStream)
        {
            using (var reader = new StreamReader(csvStream))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.Read(); // Skip header row
                csv.ReadHeader();
                while (csv.Read())
                {
                    var inventoryItem = new InventoryItem
                    {
                        Title = csv.GetField("title"),
                        Description = csv.GetField("description"),
                        RemainingCount = csv.GetField<int>("remaining_count"),
                        ExpirationDate = DateTime.ParseExact(csv.GetField("expiration_date"), "dd/MM/yyyy", CultureInfo.InvariantCulture)
                    };
                    _context.InventoryItems.Add(inventoryItem);
                }
                _context.SaveChanges();
            }
        }
    }
}
