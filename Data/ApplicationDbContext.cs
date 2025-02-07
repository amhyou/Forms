using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using forms.Models;

namespace forms.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{

    public DbSet<Form> Forms { get; set; }
    public DbSet<Topic> Topics { get; set; }
    public DbSet<AllowedUser> AllowedUsers { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<QuestionOption> QuestionOptions { get; set; }
    public DbSet<Response> Responses { get; set; }
    public DbSet<ResponseAnswer> ResponseAnswers { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<FormTag> FormTags { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // AllowedUser (Many-to-Many with Form and ApplicationUser)
        builder.Entity<AllowedUser>(allowedUser =>
        {
            allowedUser.HasKey(au => new { au.FormId, au.UserId });

            allowedUser.HasOne(au => au.Form)
              .WithMany(f => f.AllowedUsers)
              .HasForeignKey(au => au.FormId)
              .IsRequired();
        });

        // FormTag (Many-to-Many with Form and Tag)
        builder.Entity<FormTag>(formTag =>
        {
            formTag.HasKey(ft => new { ft.FormId, ft.TagId });

            formTag.HasOne(ft => ft.Form)
              .WithMany(f => f.FormTags)
              .HasForeignKey(ft => ft.FormId)
              .IsRequired();

            formTag.HasOne(ft => ft.Tag)
              .WithMany(t => t.FormTags)
              .HasForeignKey(ft => ft.TagId)
              .IsRequired();
        });

        // Question (One-to-Many with Form)
        builder.Entity<Question>(question =>
        {
            question.HasOne(q => q.Form)
              .WithMany(f => f.Questions)
              .HasForeignKey(q => q.FormId)
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

        // Response (One-to-Many with Form and ApplicationUser)
        builder.Entity<Response>(response =>
        {
            response.HasOne(r => r.Form)
              .WithMany(f => f.Responses)
              .HasForeignKey(r => r.FormId)
              .IsRequired();

            response.HasOne(r => r.User)
              .WithMany()
              .HasForeignKey(r => r.UserId)
              .IsRequired();
        });

        // ResponseAnswer (One-to-Many with Response and Question)
        builder.Entity<ResponseAnswer>(responseAnswer =>
        {
            responseAnswer.HasOne(ra => ra.Response)
              .WithMany(r => r.Answers)
              .HasForeignKey(ra => ra.ResponseId)
              .IsRequired();

            responseAnswer.HasOne(ra => ra.Question)
              .WithMany() // maybe add answers in question class
              .HasForeignKey(ra => ra.QuestionId)
              .IsRequired();
        });

        // Comment (One-to-Many with Form and ApplicationUser)
        builder.Entity<Comment>(comment =>
        {
            comment.HasOne(c => c.User)
              .WithMany()
              .HasForeignKey(c => c.UserId)
              .IsRequired();

            comment.HasOne(c => c.Form)
              .WithMany(f => f.Comments)
              .HasForeignKey(c => c.FormId)
              .IsRequired();
        });

        builder.Entity<Form>(form =>
        {
            form.HasOne(f => f.Author)
              .WithMany()
              .HasForeignKey(f => f.AuthorId)
              .IsRequired();

            form.HasOne(f => f.Topic)
              .WithMany()
              .HasForeignKey(f => f.TopicId)
              .IsRequired();

        });

    }
}
