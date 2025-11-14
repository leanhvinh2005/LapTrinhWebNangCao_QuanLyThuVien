using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Website.Models;

namespace Website.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> ACCOUNT_USER { get; set; }
        public DbSet<Member> DOCGIA { get; set; }
        public DbSet<Borrow> MUONTRA { get; set; }
        public DbSet<BookBorrow> JOIN_BOOKBORROW { get; set; }
        public DbSet<Book> SACH { get; set; }
        public DbSet<Collection> SUUTAP { get; set; }
        public DbSet<Tag> TAG { get; set; }
        public DbSet<Card> THETHUVIEN { get; set; }
        public DbSet<Librarian> THUTHU { get; set; }
        public DbSet<Chapter> TRANG { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>()
                .HasMany(b => b.tags)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "BookTag",
                    j => j.HasOne<Tag>().WithMany().HasForeignKey("idTag"),
                    j => j.HasOne<Book>().WithMany().HasForeignKey("idBook")
                );

            modelBuilder.Entity<Book>()
                .HasMany(b => b.chapters)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "BookChapter",
                    j => j.HasOne<Chapter>().WithMany().HasForeignKey("idChapter"),
                    j => j.HasOne<Book>().WithMany().HasForeignKey("idBook")
                );

            modelBuilder.Entity<BookBorrow>()
                .HasKey(bb => new { bb.idBorrow, bb.idBook });

            modelBuilder.Entity<Borrow>()
               .HasMany(b => b.bookborrows)
               .WithOne(bb => bb.borrow)
               .HasForeignKey(bb => bb.idBorrow)
               .IsRequired();

            modelBuilder.Entity<Borrow>()
                .HasOne(b => b.cardBorrow)
                .WithMany(c => c.borrows)
                .HasForeignKey(b => b.idCard)
                .IsRequired();

            modelBuilder.Entity<Librarian>()
                .HasOne(l => l.userLibrarian)
                .WithOne(u => u.librarian)
                .HasForeignKey<Librarian>(l => l.idUser);

            modelBuilder.Entity<Member>()
                .HasOne(m => m.userMember)
                .WithOne(u => u.member)
                .HasForeignKey<Member>(m => m.idUser);

            modelBuilder.Entity<Collection>()
                .HasMany(c => c.books)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "CollectionBook",
                    j => j.HasOne<Book>().WithMany().HasForeignKey("idBook"),
                    j => j.HasOne<Collection>().WithMany().HasForeignKey("idCollection")
                );
        }
    }
}
