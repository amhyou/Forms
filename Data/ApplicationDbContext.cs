using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using forms.Models;

namespace forms.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{

  public DbSet<Template> Templates { get; set; }
  public DbSet<Topic> Topics { get; set; }
  public DbSet<Comment> Comments { get; set; }
  public DbSet<Question> Questions { get; set; }
  public DbSet<QuestionOption> QuestionOptions { get; set; }
  public DbSet<Form> Forms { get; set; }
  public DbSet<Response> Responses { get; set; }
  public DbSet<Tag> Tags { get; set; }


  protected override void OnModelCreating(ModelBuilder builder)
  {
    base.OnModelCreating(builder);

    // Question (One-to-Many with Template)
    builder.Entity<Question>(question =>
    {
      question.HasOne(q => q.Template)
          .WithMany(f => f.Questions)
          .HasForeignKey(q => q.TemplateId)
          .IsRequired();
    });

    // QuestionOption (One-to-Many with Question)
    builder.Entity<QuestionOption>(questionOption =>
    {
      questionOption.HasOne(qo => qo.Question)
              .WithMany(q => q.Options)
              .HasForeignKey(qo => qo.QuestionId)
              .IsRequired();
    });

    // Form (One-to-Many with Template and ApplicationUser)
    builder.Entity<Form>(form =>
    {
      form.HasOne(r => r.Template)
            .WithMany(f => f.Forms)
            .HasForeignKey(r => r.TemplateId)
            .IsRequired();

      form.HasOne(r => r.Author)
            .WithMany()
            .HasForeignKey(r => r.AuthorId)
            .IsRequired();
    });

    // Response (One-to-Many with Form and Question)
    builder.Entity<Response>(response =>
    {
      response.HasOne(ra => ra.Form)
            .WithMany(r => r.Responses)
            .HasForeignKey(ra => ra.FormId)
            .IsRequired();

      response.HasOne(ra => ra.Question)
            .WithMany() // #TODO maybe add responses in question class
            .HasForeignKey(ra => ra.QuestionId)
            .IsRequired();
    });

    // Comment (One-to-Many with Template and ApplicationUser)
    builder.Entity<Comment>(comment =>
    {
      comment.HasOne(c => c.User)
            .WithMany()
            .HasForeignKey(c => c.UserId)
            .IsRequired();

      comment.HasOne(c => c.Template)
            .WithMany(f => f.Comments)
            .HasForeignKey(c => c.TemplateId)
            .IsRequired();
    });

    // Comment (One-to-Many with Topic and ApplicationUser)
    builder.Entity<Template>(template =>
    {
      template.HasOne(f => f.Creator)
            .WithMany(c => c.Templates)
            .HasForeignKey(f => f.CreatorId)
            .IsRequired();

      template.HasOne(f => f.Topic)
            .WithMany()
            .HasForeignKey(f => f.TopicId)
            .IsRequired();


      template.HasMany(s => s.Tags)
        .WithMany(c => c.Templates)
        .UsingEntity(j => j.ToTable("Template_Tags"));

      template.HasMany(s => s.AllowedUsers)
        .WithMany(c => c.AllowedTemplates)
        .UsingEntity(j => j.ToTable("Template_Users"));

    });

  }
}
