using System;
using System.Collections.Generic;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public partial class DVLDContext : DbContext
{
    public DVLDContext(DbContextOptions<DVLDContext> options)
        : base(options)
    {
    }

    public virtual DbSet<clsApplication> Applications { get; set; }

    public virtual DbSet<clsApplicationType> ApplicationTypes { get; set; }

    public virtual DbSet<clsCountry> Countries { get; set; }

    public virtual DbSet<clsDetainedLicense> DetainedLicenses { get; set; }

    public virtual DbSet<clsDriver> Drivers { get; set; }


    public virtual DbSet<clsInternationalLicense> InternationalLicenses { get; set; }

    public virtual DbSet<clsLicense> Licenses { get; set; }

    public virtual DbSet<clsLicenseClass> LicenseClasses { get; set; }

    public virtual DbSet<clsLocalDrivingLicenseApplication> LocalDrivingLicenseApplications { get; set; }

    
    public virtual DbSet<clsPerson> People { get; set; }

    public virtual DbSet<clsTest> Tests { get; set; }

    public virtual DbSet<clsTestAppointment> TestAppointments { get; set; }


    public virtual DbSet<clsTestType> TestTypes { get; set; }

    public virtual DbSet<clsUser> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<clsApplication>(entity =>
        {
            entity.HasKey(e => e.ApplicationId);
            entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");
            entity.Property(e => e.ApplicantPersonId).HasColumnName("ApplicantPersonID");
            entity.Property(e => e.ApplicationDate).HasColumnType("datetime");
            entity.Property(e => e.ApplicationStatus)
                .HasDefaultValue((byte)1)
                .HasComment("1-New 2-Cancelled 3-Completed");
            entity.Property(e => e.ApplicationTypeId).HasColumnName("ApplicationTypeID");
            entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");
            entity.Property(e => e.LastStatusDate).HasColumnType("datetime");
            entity.Property(e => e.PaidFees).HasColumnType("smallmoney");

            entity.HasOne(d => d.ApplicantPerson).WithMany(p => p.Applications)
                .HasForeignKey(d => d.ApplicantPersonId)
                .HasConstraintName("FK_Applications_People");

            entity.HasOne(d => d.ApplicationType).WithMany(p => p.Applications)
                .HasForeignKey(d => d.ApplicationTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Applications_ApplicationTypes");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.Applications)
                .HasForeignKey(d => d.CreatedByUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Applications_Users");
        });

        modelBuilder.Entity<clsApplicationType>(entity =>
        {
            entity.HasKey(e => e.ApplicationTypeId);
            entity.Property(e => e.ApplicationTypeId).HasColumnName("ApplicationTypeID");
            entity.Property(e => e.ApplicationFees).HasColumnType("smallmoney");
            entity.Property(e => e.ApplicationTypeTitle).HasMaxLength(150);
            entity.Property(e => e.ApplicationTypeTitleArabic).HasMaxLength(150);
        });

        modelBuilder.Entity<clsCountry>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("PK__Countrie__10D160BFDBD6933F");

            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.CountryName).HasMaxLength(50);
            entity.Property(e => e.CountryNameArabic).HasMaxLength(50);
        });

        modelBuilder.Entity<clsDetainedLicense>(entity =>
        {
            entity.HasKey(e => e.DetainId);

            entity.Property(e => e.DetainId).HasColumnName("DetainID");
            entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");
            entity.Property(e => e.DetainDate).HasColumnType("smalldatetime");
            entity.Property(e => e.FineFees).HasColumnType("smallmoney");
            entity.Property(e => e.LicenseId).HasColumnName("LicenseID");
            entity.Property(e => e.ReleaseApplicationId).HasColumnName("ReleaseApplicationID");
            entity.Property(e => e.ReleaseDate).HasColumnType("smalldatetime");
            entity.Property(e => e.ReleasedByUserId).HasColumnName("ReleasedByUserID");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.DetainedLicenseCreatedByUsers)
                .HasForeignKey(d => d.CreatedByUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetainedLicenses_Users");

            entity.HasOne(d => d.License).WithMany(p => p.DetainedLicenses)
                .HasForeignKey(d => d.LicenseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetainedLicenses_Licenses");

            entity.HasOne(d => d.ReleaseApplication).WithMany(p => p.DetainedLicenses)
                .HasForeignKey(d => d.ReleaseApplicationId)
                .HasConstraintName("FK_DetainedLicenses_Applications");

            entity.HasOne(d => d.ReleasedByUser).WithMany(p => p.DetainedLicenseReleasedByUsers)
                .HasForeignKey(d => d.ReleasedByUserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_DetainedLicenses_Users1");
        });

        modelBuilder.Entity<clsDriver>(entity =>
        {
            entity.HasKey(e => e.DriverId).HasName("PK_Drivers_1");

            entity.Property(e => e.DriverId).HasColumnName("DriverID");
            entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");
            entity.Property(e => e.CreatedDate).HasColumnType("smalldatetime");
            entity.Property(e => e.PersonId).HasColumnName("PersonID");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.Drivers)
                .HasForeignKey(d => d.CreatedByUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Drivers_Users");

            entity.HasOne(d => d.Person).WithMany(p => p.Drivers)
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("FK_Drivers_People");
        });

        modelBuilder.Entity<clsInternationalLicense>(entity =>
        {
            entity.HasKey(e => e.IssuedUsingLocalLicenseId);
            entity.Property(e => e.InternationalLicenseId).HasColumnName("InternationalLicenseID");
            entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");
            entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");
            entity.Property(e => e.DriverId).HasColumnName("DriverID");
            entity.Property(e => e.ExpirationDate).HasColumnType("smalldatetime");
            entity.Property(e => e.IssueDate).HasColumnType("smalldatetime");
            entity.Property(e => e.IssuedUsingLocalLicenseId).HasColumnName("IssuedUsingLocalLicenseID");

            entity.HasOne(d => d.Application).WithMany(p => p.InternationalLicenses)
                .HasForeignKey(d => d.ApplicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InternationalLicenses_Applications");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.InternationalLicenses)
                .HasForeignKey(d => d.CreatedByUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InternationalLicenses_Users");

            entity.HasOne(d => d.Driver).WithMany(p => p.InternationalLicenses)
                .HasForeignKey(d => d.DriverId)
                .HasConstraintName("FK_InternationalLicenses_Drivers");

            entity.HasOne(d => d.IssuedUsingLocalLicense).WithMany(p => p.InternationalLicenses)
                .HasForeignKey(d => d.IssuedUsingLocalLicenseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InternationalLicenses_Licenses");
        });

        modelBuilder.Entity<clsLicense>(entity =>
        {
            entity.HasKey(e => e.LicenseId);
            entity.Property(e => e.LicenseId).HasColumnName("LicenseID");
            entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");
            entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");
            entity.Property(e => e.DriverId).HasColumnName("DriverID");
            entity.Property(e => e.ExpirationDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IssueDate).HasColumnType("datetime");
            entity.Property(e => e.IssueReason)
                .HasDefaultValue((byte)1)
                .HasComment("1-FirstTime, 2-Renew, 3-Replacement for Damaged, 4- Replacement for Lost.");
            entity.Property(e => e.LicenseClassId).HasColumnName("LicenseClassID");
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.PaidFees).HasColumnType("smallmoney");

            entity.HasOne(d => d.Application).WithMany(p => p.Licenses)
                .HasForeignKey(d => d.ApplicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Licenses_Applications");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.Licenses)
                .HasForeignKey(d => d.CreatedByUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Licenses_Users");

            entity.HasOne(d => d.Driver).WithMany(p => p.Licenses)
                .HasForeignKey(d => d.DriverId)
                .HasConstraintName("FK_Licenses_Drivers");

            entity.HasOne(d => d.LicenseClass).WithMany(p => p.Licenses)
                .HasForeignKey(d => d.LicenseClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Licenses_LicenseClasses");
        });

        modelBuilder.Entity<clsLicenseClass>(entity =>
        {
            entity.HasKey(e => e.LicenseClassId);
            entity.Property(e => e.LicenseClassId).HasColumnName("LicenseClassID");
            entity.Property(e => e.ClassDescription).HasMaxLength(500);
            entity.Property(e => e.ClassFees).HasColumnType("smallmoney");
            entity.Property(e => e.ClassName).HasMaxLength(50);
            entity.Property(e => e.ClassNameArabic).HasMaxLength(50);
            entity.Property(e => e.DefaultValidityLength)
                .HasDefaultValue((byte)1)
                .HasComment("How many years the licesnse will be valid.");
            entity.Property(e => e.MinimumAllowedAge)
                .HasDefaultValue((byte)18)
                .HasComment("Minmum age allowed to apply for this license");
        });

        modelBuilder.Entity<clsLocalDrivingLicenseApplication>(entity =>
        {
            entity.HasKey(e => e.LocalDrivingLicenseApplicationId).HasName("PK_DrivingLicsenseApplications");

            entity.Property(e => e.LocalDrivingLicenseApplicationId).HasColumnName("LocalDrivingLicenseApplicationID");
            entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");
            entity.Property(e => e.LicenseClassId).HasColumnName("LicenseClassID");

            entity.HasOne(d => d.Application).WithMany(p => p.LocalDrivingLicenseApplications)
                .HasForeignKey(d => d.ApplicationId)
                .HasConstraintName("FK_DrivingLicsenseApplications_Applications");

            entity.HasOne(d => d.LicenseClass).WithMany(p => p.LocalDrivingLicenseApplications)
                .HasForeignKey(d => d.LicenseClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DrivingLicsenseApplications_LicenseClasses");
        });

        
        modelBuilder.Entity<clsPerson>(entity =>
        {
            entity.HasIndex(e => e.NationalNo, "UQ_People_NationalNo").IsUnique();
            entity.HasKey(e => e.PersonId);
            entity.Property(e => e.PersonId).HasColumnName("PersonID");
            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(20);
            entity.Property(e => e.Gendor).HasComment("0 Male , 1 Femail");
            entity.Property(e => e.ImagePath).HasMaxLength(250);
            entity.Property(e => e.LastName).HasMaxLength(20);
            entity.Property(e => e.NationalNo).HasMaxLength(20);
            entity.Property(e => e.NationalityCountryId).HasColumnName("NationalityCountryID");
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.SecondName).HasMaxLength(20);
            entity.Property(e => e.ThirdName).HasMaxLength(20);

            entity.HasOne(d => d.NationalityCountry).WithMany(p => p.People)
                .HasForeignKey(d => d.NationalityCountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_People_Countries1");
        });

        modelBuilder.Entity<clsTest>(entity =>
        {
            entity.HasKey(e => e.TestId);
            entity.Property(e => e.TestId).HasColumnName("TestID");
            entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.TestAppointmentId).HasColumnName("TestAppointmentID");
            entity.Property(e => e.TestResult).HasComment("0 - Fail 1-Pass");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.Tests)
                .HasForeignKey(d => d.CreatedByUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tests_Users");

            entity.HasOne(d => d.TestAppointment).WithMany(p => p.Tests)
                .HasForeignKey(d => d.TestAppointmentId)
                .HasConstraintName("FK_Tests_TestAppointments");
        });

        modelBuilder.Entity<clsTestAppointment>(entity =>
        {
            entity.HasKey(e => e.TestAppointmentId);
            entity.Property(e => e.TestAppointmentId).HasColumnName("TestAppointmentID");
            entity.Property(e => e.AppointmentDate).HasColumnType("smalldatetime");
            entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");
            entity.Property(e => e.LocalDrivingLicenseApplicationId).HasColumnName("LocalDrivingLicenseApplicationID");
            entity.Property(e => e.PaidFees).HasColumnType("smallmoney");
            entity.Property(e => e.RetakeTestApplicationId).HasColumnName("RetakeTestApplicationID");
            entity.Property(e => e.TestTypeId).HasColumnName("TestTypeID");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.TestAppointments)
                .HasForeignKey(d => d.CreatedByUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TestAppointments_Users");

            entity.HasOne(d => d.LocalDrivingLicenseApplication).WithMany(p => p.TestAppointments)
                .HasForeignKey(d => d.LocalDrivingLicenseApplicationId)
                .HasConstraintName("FK_TestAppointments_LocalDrivingLicenseApplications");

            entity.HasOne(d => d.RetakeTestApplication).WithMany(p => p.TestAppointments)
                .HasForeignKey(d => d.RetakeTestApplicationId)
                .HasConstraintName("FK_TestAppointments_Applications");

            entity.HasOne(d => d.TestType).WithMany(p => p.TestAppointments)
                .HasForeignKey(d => d.TestTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TestAppointments_TestTypes");
        });

       
        modelBuilder.Entity<clsTestType>(entity =>
        {
            entity.HasKey(e => e.TestTypeId);
            entity.Property(e => e.TestTypeId).HasColumnName("TestTypeID");
            entity.Property(e => e.TestTypeDescription).HasMaxLength(500);
            entity.Property(e => e.TestTypeFees).HasColumnType("smallmoney");
            entity.Property(e => e.TestTypeTitle).HasMaxLength(100);
            entity.Property(e => e.TestTypeTitleArabic).HasMaxLength(100);
        });

        modelBuilder.Entity<clsUser>(entity =>
        {
            entity.HasKey(e => e.UserId);
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Password).HasMaxLength(20);
            entity.Property(e => e.PersonId).HasColumnName("PersonID");
            entity.Property(e => e.UserName).HasMaxLength(20);

            entity.HasOne(d => d.Person).WithMany(p => p.Users)
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("FK_Users_People");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
