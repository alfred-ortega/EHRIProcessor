﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using EHRIProcessor.Engine;

namespace EHRIProcessor.Model
{
    public partial class oluContext : DbContext
    {
        public oluContext()
        {
        }

        public oluContext(DbContextOptions<oluContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EhriStudHist> EhriStudHist { get; set; }
        public virtual DbSet<TrainingFileInfo> TrainingFileInfo { get; set; }
        public virtual DbSet<TrainingRecord> EhriTraining { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL(Config.Settings.OluDB);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<EhriStudHist>(entity =>
            {
                entity.HasKey(e => e.Emplid);

                entity.ToTable("ehri_stud_hist", "olu");

                entity.Property(e => e.Emplid)
                    .HasColumnName("emplid")
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Birthdate)
                    .HasColumnName("birthdate")
                    .HasColumnType("date");

                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");

                entity.Property(e => e.CreatedAt).HasColumnName("created_at");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(30)
                    .IsUnicode(false); 

                entity.Property(e => e.MiddleName)
                    .IsRequired()
                    .HasColumnName("middle_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);                                       


                entity.Property(e => e.Ssn)
                    .IsRequired()
                    .HasColumnName("ssn")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });
			
            modelBuilder.Entity<TrainingFileInfo>(entity =>
            {
                entity.HasKey(e => e.TrainingFileInfoId);

                entity.ToTable("ehri_trainingfileinfo", "olu");

                entity.Property(e => e.TrainingFileInfoId)
                    .HasColumnName("TrainingFileInfoID")
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.FileRecordCount).HasColumnType("int(11)");

                entity.Property(e => e.SavedRecordCount).HasColumnType("int(11)");
            });
			
            modelBuilder.Entity<TrainingRecord>(entity =>
            {
                entity.HasKey(e => e.EhriTrainingId);

                entity.ToTable("ehri_training", "olu");

                entity.Property(e => e.EhriTrainingId)
                    .HasColumnName("ehri_training_id")
                    .HasColumnType("int(10)");

                entity.Property(e => e.AccreditationIndicator)
                    .HasColumnName("ACCREDITATION_INDICATOR")
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.ContServiceAgreementSigned)
                    .HasColumnName("CONT_SERVICE_AGREEMENT_SIGNED")
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.CourseCompletionDate)
                    .HasColumnName("COURSE_COMPLETION_DATE")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CourseId)
                    .HasColumnName("COURSE_ID")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.CourseStartDate)
                    .HasColumnName("COURSE_START_DATE")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CourseTitle)
                    .HasColumnName("COURSE_TITLE")
                    .HasMaxLength(240)
                    .IsUnicode(false);

                entity.Property(e => e.CourseType)
                    .HasColumnName("COURSE_TYPE")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnName("CREATED_DATE");

                entity.Property(e => e.CreditDesignation)
                    .HasColumnName("CREDIT_DESIGNATION")
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.CreditType)
                    .HasColumnName("CREDIT_TYPE")
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.DutyHours)
                    .HasColumnName("DUTY_HOURS")
                    .HasColumnType("decimal(15,2)");

                entity.Property(e => e.EmailAddress)
                    .HasColumnName("EMAIL_ADDRESS")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeFirstName)
                    .HasColumnName("EMPLOYEE_FIRST_NAME")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeLastName)
                    .HasColumnName("EMPLOYEE_LAST_NAME")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeMiddleName)
                    .HasColumnName("EMPLOYEE_MIDDLE_NAME")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.ErrorMessage)
                    .HasColumnName("ERROR_MESSAGE")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ImpactOnPerformance)
                    .HasColumnName("IMPACT_ON_PERFORMANCE")
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdatedDate).HasColumnName("LAST_UPDATED_DATE");

                entity.Property(e => e.LrnInterfaceOutId)
                    .HasColumnName("LRN_INTERFACE_OUT_ID")
                    .HasColumnType("int(10)");

                entity.Property(e => e.MaterialCost)
                    .HasColumnName("MATERIAL_COST")
                    .HasColumnType("decimal(15,2)");

                entity.Property(e => e.NonDutyHours)
                    .HasColumnName("NON_DUTY_HOURS")
                    .HasColumnType("decimal(15,2)");

                entity.Property(e => e.NongovtContrCost)
                    .HasColumnName("NONGOVT_CONTR_COST")
                    .HasColumnType("decimal(15,2)");

                entity.Property(e => e.PerdiemCost)
                    .HasColumnName("PERDIEM_COST")
                    .HasColumnType("decimal(15,2)");

                entity.Property(e => e.PersonId)
                    .HasColumnName("PERSON_ID")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.PriorKnowledgeLevel)
                    .HasColumnName("PRIOR_KNOWLEDGE_LEVEL")
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.PriorSupvApprovalReceived)
                    .HasColumnName("PRIOR_SUPV_APPROVAL_RECEIVED")
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.ProcessStatus)
                    .HasColumnName("PROCESS_STATUS")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.RecommendTrngToOthers)
                    .HasColumnName("RECOMMEND_TRNG_TO_OTHERS")
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.RepaymentAgreementReqd)
                    .HasColumnName("REPAYMENT_AGREEMENT_REQD")
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.TrainingCredit)
                    .HasColumnName("TRAINING_CREDIT")
                    .HasColumnType("decimal(15,2)");

                entity.Property(e => e.TrainingDeliveryType)
                    .HasColumnName("TRAINING_DELIVERY_TYPE")
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.TrainingFileInfoId)
                    .HasColumnName("TrainingFileInfoID")
                    .HasColumnType("int(10)");

                entity.Property(e => e.TrainingLocation)
                    .HasColumnName("TRAINING_LOCATION")
                    .HasMaxLength(240)
                    .IsUnicode(false);

                entity.Property(e => e.TrainingPartOfIdp)
                    .HasColumnName("TRAINING_PART_OF_IDP")
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.TrainingPurpose)
                    .HasColumnName("TRAINING_PURPOSE")
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.TrainingSource)
                    .HasColumnName("TRAINING_SOURCE")
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.TrainingSubType)
                    .HasColumnName("TRAINING_SUB_TYPE")
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.TrainingTravelIndicator)
                    .HasColumnName("TRAINING_TRAVEL_INDICATOR")
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.TrainingType)
                    .HasColumnName("TRAINING_TYPE")
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.TravelCosts)
                    .HasColumnName("TRAVEL_COSTS")
                    .HasColumnType("decimal(15,2)");

                entity.Property(e => e.TutionAndFees)
                    .HasColumnName("TUTION_AND_FEES")
                    .HasColumnType("decimal(15,2)");

                entity.Property(e => e.TypeOfPayment)
                    .HasColumnName("TYPE_OF_PAYMENT")
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.VendorName)
                    .HasColumnName("VENDOR_NAME")
                    .HasMaxLength(240)
                    .IsUnicode(false);

                entity.Property(e => e.Birth_Date)
                    .HasColumnName("Birth_Date");

                entity.Property(e => e.SSN)
                    .HasColumnName("ssn")
                    .HasMaxLength(10); 
                    
            });
			
			
        }
    }
}