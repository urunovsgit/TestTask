using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestTask.Database.Migrations
{
    /// <inheritdoc />
    public partial class Populate_DelieveryOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"

                INSERT INTO DeliveryOrders (Weight, CityRegion, DeliveryDate) VALUES
                (5.0, 'Central', '2024-10-23 08:00:00'),
                (3.2, 'North', '2024-10-23 08:05:00'),
                (2.5, 'South', '2024-10-23 08:10:00'),
                (4.1, 'Central', '2024-10-23 08:15:00'),
                (1.8, 'West', '2024-10-23 08:20:00'),
                (3.0, 'East', '2024-10-23 08:25:00'),
                (2.7, 'North', '2024-10-23 08:30:00'),
                (4.4, 'South', '2024-10-23 08:35:00'),
                (5.2, 'Central', '2024-10-23 08:40:00'),
                (2.6, 'West', '2024-10-23 08:45:00'),
                (3.1, 'East', '2024-10-23 08:50:00'),
                (1.9, 'North', '2024-10-23 08:55:00'),
                (3.4, 'South', '2024-10-23 09:00:00'),
                (5.3, 'Central', '2024-10-23 09:05:00'),
                (2.1, 'West', '2024-10-23 09:10:00'),
                (4.2, 'East', '2024-10-23 09:15:00'),
                (3.6, 'North', '2024-10-23 09:20:00'),
                (2.9, 'South', '2024-10-23 09:25:00'),
                (4.5, 'Central', '2024-10-23 09:30:00'),
                (1.7, 'West', '2024-10-23 09:35:00'),
                (3.3, 'East', '2024-10-23 09:40:00'),
                (2.8, 'North', '2024-10-23 09:45:00'),
                (4.0, 'South', '2024-10-23 09:50:00'),
                (5.1, 'Central', '2024-10-23 09:55:00'),
                (2.4, 'West', '2024-10-23 10:00:00'),
                (3.7, 'East', '2024-10-23 10:05:00'),
                (1.5, 'North', '2024-10-23 10:10:00'),
                (3.9, 'South', '2024-10-23 10:15:00'),
                (5.4, 'Central', '2024-10-23 10:20:00'),
                (2.0, 'West', '2024-10-23 10:25:00'),
                (4.3, 'East', '2024-10-23 10:30:00'),
                (3.8, 'North', '2024-10-23 10:35:00'),
                (2.2, 'South', '2024-10-23 10:40:00'),
                (4.6, 'Central', '2024-10-23 10:45:00'),
                (1.6, 'West', '2024-10-23 10:50:00'),
                (3.5, 'East', '2024-10-23 10:55:00'),
                (2.3, 'North', '2024-10-23 11:00:00'),
                (4.8, 'South', '2024-10-23 11:05:00');


            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DELETE FROM DeliveryOrders;");
        }
    }
}
