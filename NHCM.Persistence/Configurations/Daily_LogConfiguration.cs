using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHCM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Persistence.Configurations
{
    public class Daily_LogConfiguration : IEntityTypeConfiguration<DailyLog>
    {
        public void Configure(EntityTypeBuilder<DailyLog> builder)
        {
            builder.ToTable("daily_log", "att");

            builder.HasIndex(e => new { e.UserId, e.AttendanceDate })
                .HasName("daily_log_user_id_attendance_date_key")
                .IsUnique();

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("numeric")
                .HasDefaultValueSql("nextval('att.daily_log_id_seq'::regclass)");

            builder.Property(e => e.AttendanceDate)
                .HasColumnName("attendance_date")
                .HasColumnType("date");

            builder.Property(e => e.CheckIn).HasColumnName("check_in");

            builder.Property(e => e.CheckOut).HasColumnName("check_out");

            builder.Property(e => e.UserId).HasColumnName("user_id");
        }
    }
}
