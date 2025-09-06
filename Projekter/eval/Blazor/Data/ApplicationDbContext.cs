using Microsoft.EntityFrameworkCore;
using Blazor.Data.Models;

namespace Blazor.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    // DbSets for evalueringsplatformen
    public DbSet<Survey> Surveys { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Response> Responses { get; set; }
    public DbSet<ResponseData> ResponseData { get; set; }
    public DbSet<QuestionCondition> QuestionConditions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Survey konfiguration
        modelBuilder.Entity<Survey>(entity =>
        {
            entity.HasKey(s => s.Id);
            entity.Property(s => s.Title).IsRequired().HasMaxLength(200);
            entity.Property(s => s.Description).HasMaxLength(1000);
            entity.Property(s => s.AccessCode).IsRequired().HasMaxLength(4);
            entity.HasIndex(s => s.AccessCode).IsUnique();
            entity.Property(s => s.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        // Question konfiguration
        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(q => q.Id);
            entity.Property(q => q.Text).IsRequired().HasMaxLength(500);
            entity.Property(q => q.Description).HasMaxLength(1000);
            entity.Property(q => q.Options).HasColumnType("jsonb");
            entity.Property(q => q.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            
            entity.HasOne(q => q.Survey)
                  .WithMany(s => s.Questions)
                  .HasForeignKey(q => q.SurveyId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Response konfiguration
        modelBuilder.Entity<Response>(entity =>
        {
            entity.HasKey(r => r.Id);
            entity.Property(r => r.StudentName).HasMaxLength(100);
            entity.Property(r => r.StudentEmail).HasMaxLength(200);
            entity.Property(r => r.IpAddress).HasMaxLength(45); // IPv6 support
            entity.Property(r => r.SubmittedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            
            entity.HasOne(r => r.Survey)
                  .WithMany(s => s.Responses)
                  .HasForeignKey(r => r.SurveyId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // ResponseData konfiguration
        modelBuilder.Entity<ResponseData>(entity =>
        {
            entity.HasKey(rd => rd.Id);
            entity.Property(rd => rd.Data).IsRequired().HasColumnType("jsonb");
            entity.Property(rd => rd.TextValue).HasMaxLength(1000);
            entity.Property(rd => rd.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            
            entity.HasOne(rd => rd.Response)
                  .WithMany(r => r.ResponseData)
                  .HasForeignKey(rd => rd.ResponseId)
                  .OnDelete(DeleteBehavior.Cascade);
                  
            entity.HasOne(rd => rd.Question)
                  .WithMany(q => q.ResponseData)
                  .HasForeignKey(rd => rd.QuestionId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // QuestionCondition konfiguration
        modelBuilder.Entity<QuestionCondition>(entity =>
        {
            entity.HasKey(qc => qc.Id);
            entity.Property(qc => qc.Value).IsRequired().HasMaxLength(500);
            entity.Property(qc => qc.Operator).HasMaxLength(50);
            entity.Property(qc => qc.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            
            entity.HasOne(qc => qc.Question)
                  .WithMany(q => q.Conditions)
                  .HasForeignKey(qc => qc.QuestionId)
                  .OnDelete(DeleteBehavior.Cascade);
                  
            entity.HasOne(qc => qc.ParentQuestion)
                  .WithMany(q => q.ParentConditions)
                  .HasForeignKey(qc => qc.ParentQuestionId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
