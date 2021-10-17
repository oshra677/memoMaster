using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Repository.Models
{
    public partial class context : DbContext
    {
        public context()
        {
        }

        public context(DbContextOptions<context> options)
            : base(options)
        {
        }

        public virtual DbSet<BaseEvent> BaseEvent { get; set; }
        public virtual DbSet<CategoryCommunication> CategoryCommunication { get; set; }
        public virtual DbSet<CategoryEvent> CategoryEvent { get; set; }
        public virtual DbSet<CategoryMedicines> CategoryMedicines { get; set; }
        public virtual DbSet<CategoryMeetings> CategoryMeetings { get; set; }
        public virtual DbSet<CategoryPreparation> CategoryPreparation { get; set; }
        public virtual DbSet<CategoryShopping> CategoryShopping { get; set; }
        public virtual DbSet<Conversation> Conversation { get; set; }
        public virtual DbSet<MorePicture> MorePicture { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<Relative> Relative { get; set; }
        public virtual DbSet<Subjects> Subjects { get; set; }
        public virtual DbSet<TypeConversation> TypeConversation { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Words> Words { get; set; }
        public virtual DbSet<WordsTime> WordsTime { get; set; }
        public virtual DbSet<WordsToSubjects> WordsToSubjects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(" Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\This_User\\Documents\\Project\\Project\\Database.mdf;Integrated Security=True;Connect Timeout=30");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BaseEvent>(entity =>
            {
                entity.HasKey(e => e.IdBaseEvent);

                entity.Property(e => e.IdBaseEvent).HasColumnName("Id_BaseEvent");

                entity.Property(e => e.DateEvent)
                    .HasColumnName("Date_Event")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdConversation)
                    .HasColumnName("Id_Conversation")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Subject).HasMaxLength(10);

                entity.Property(e => e.TextEvent)
                    .HasColumnName("Text_Event")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Conversation)
                    .WithMany(p => p.BaseEvent)
                    .HasForeignKey(d => d.IdConversation)
                    .HasConstraintName("FK__BaseEvent__Id_Co__3BCADD1B");
            });

            modelBuilder.Entity<CategoryCommunication>(entity =>
            {
                entity.HasKey(e => e.IdCategoryCommunication);

                entity.ToTable("Category_Communication");

                entity.Property(e => e.IdCategoryCommunication)
                    .HasColumnName("Id_Category_Communication")
                    .ValueGeneratedNever();

                entity.Property(e => e.IdTypeConversation).HasColumnName("Id_Type_Conversation");

                entity.Property(e => e.WhatToSay)
                    .HasColumnName("What_to_say")
                    .HasMaxLength(30);

                entity.Property(e => e.WhomIdPerson).HasColumnName("Whom_Id_Person");

                entity.HasOne(d => d.BaseEvent)
                    .WithOne(p => p.CategoryCommunication)
                    .HasForeignKey<CategoryCommunication>(d => d.IdCategoryCommunication)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Category___Id_Ca__38EE7070");

                entity.HasOne(d => d.TypeConversation)
                    .WithMany(p => p.CategoryCommunication)
                    .HasForeignKey(d => d.IdTypeConversation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Category___Id_Ty__382F5661");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.CategoryCommunication)
                    .HasForeignKey(d => d.WhomIdPerson)
                    .HasConstraintName("FK__Category___Whom___1699586C");
            });

            modelBuilder.Entity<CategoryEvent>(entity =>
            {
                entity.HasKey(e => e.IdCategoryEvent);

                entity.ToTable("Category_Event");

                entity.Property(e => e.IdCategoryEvent)
                    .HasColumnName("Id_Category_Event")
                    .ValueGeneratedNever();

                entity.Property(e => e.City).HasMaxLength(20);

                entity.Property(e => e.Country).HasMaxLength(20);

                entity.Property(e => e.ItemToBring)
                    .HasColumnName("Item_To_Bring")
                    .HasMaxLength(30);

                entity.Property(e => e.Lat).HasMaxLength(20);

                entity.Property(e => e.Lng).HasMaxLength(20);

                entity.Property(e => e.Neighborhood).HasMaxLength(20);

                entity.Property(e => e.Num).HasMaxLength(5);

                entity.Property(e => e.Street).HasMaxLength(20);

                entity.Property(e => e.WhoseIdPerson).HasColumnName("Whose_Id_Person");

                entity.Property(e => e.WithWhoIdPerson).HasColumnName("With_who_Id_Person");

                entity.HasOne(d => d.BaseEvent)
                    .WithOne(p => p.CategoryEvent)
                    .HasForeignKey<CategoryEvent>(d => d.IdCategoryEvent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Category___Id_Ca__370627FE");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.CategoryEventPerson)
                    .HasForeignKey(d => d.WhoseIdPerson)
                    .HasConstraintName("FK__Category___Whose__23F3538A");

                entity.HasOne(d => d.PersonNavigation)
                    .WithMany(p => p.CategoryEventPersonNavigation)
                    .HasForeignKey(d => d.WithWhoIdPerson)
                    .HasConstraintName("FK__Category___With___22FF2F51");
            });

            modelBuilder.Entity<CategoryMedicines>(entity =>
            {
                entity.HasKey(e => e.IdCategoryMedicines);

                entity.ToTable("Category_Medicines");

                entity.Property(e => e.IdCategoryMedicines)
                    .HasColumnName("Id_Category_Medicines")
                    .ValueGeneratedNever();

                entity.Property(e => e.FrequencyDay).HasColumnName("Frequency_Day");

                entity.Property(e => e.FrequencyMonth).HasColumnName("Frequency_Month");

                entity.Property(e => e.FrequencyWeek).HasColumnName("Frequency_Week");

                entity.Property(e => e.NameMedicines)
                    .HasColumnName("Name_Medicines")
                    .HasMaxLength(20);

                entity.HasOne(d => d.BaseEvent)
                    .WithOne(p => p.CategoryMedicines)
                    .HasForeignKey<CategoryMedicines>(d => d.IdCategoryMedicines)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Category___Id_Ca__39E294A9");
            });

            modelBuilder.Entity<CategoryMeetings>(entity =>
            {
                entity.HasKey(e => e.IdCategoryMeetings);

                entity.ToTable("Category_Meetings");

                entity.Property(e => e.IdCategoryMeetings)
                    .HasColumnName("Id_Category_Meetings")
                    .ValueGeneratedNever();

                entity.Property(e => e.City).HasMaxLength(20);

                entity.Property(e => e.Country).HasMaxLength(20);

                entity.Property(e => e.ItemToBring)
                    .HasColumnName("Item_To_bring")
                    .HasMaxLength(30);

                entity.Property(e => e.Lat).HasMaxLength(20);

                entity.Property(e => e.Lng).HasMaxLength(20);

                entity.Property(e => e.Neighborhood).HasMaxLength(20);

                entity.Property(e => e.Num).HasMaxLength(5);

                entity.Property(e => e.Street).HasMaxLength(20);

                entity.Property(e => e.WhoseIdPerson).HasColumnName("Whose_Id_Person");

                entity.Property(e => e.WithWhoIdPerson).HasColumnName("With_who_Id_Person");

                entity.HasOne(d => d.BaseEvent)
                    .WithOne(p => p.CategoryMeetings)
                    .HasForeignKey<CategoryMeetings>(d => d.IdCategoryMeetings)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Category___Id_Ca__37FA4C37");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.CategoryMeetingsPerson)
                    .HasForeignKey(d => d.WhoseIdPerson)
                    .HasConstraintName("FK__Category___Whose__26CFC035");

                entity.HasOne(d => d.PersonNavigation)
                    .WithMany(p => p.CategoryMeetingsPersonNavigation)
                    .HasForeignKey(d => d.WithWhoIdPerson)
                    .HasConstraintName("FK__Category___With___25DB9BFC");
            });

            modelBuilder.Entity<CategoryPreparation>(entity =>
            {
                entity.HasKey(e => e.IdCategoryPreparation);

                entity.ToTable("Category_Preparation");

                entity.Property(e => e.IdCategoryPreparation)
                    .HasColumnName("Id_Category_Preparation")
                    .ValueGeneratedNever();

                entity.Property(e => e.Ingredients).HasMaxLength(20);

                entity.Property(e => e.WhatMake)
                    .HasColumnName("What_Make")
                    .HasMaxLength(20);

                entity.Property(e => e.WithWhoIdPerson).HasColumnName("With_who_Id_Person");

                entity.HasOne(d => d.BaseEvent)
                    .WithOne(p => p.CategoryPreparation)
                    .HasForeignKey<CategoryPreparation>(d => d.IdCategoryPreparation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Category___Id_Ca__351DDF8C");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.CategoryPreparation)
                    .HasForeignKey(d => d.WithWhoIdPerson)
                    .HasConstraintName("FK__Category___With___0FEC5ADD");
            });

            modelBuilder.Entity<CategoryShopping>(entity =>
            {
                entity.HasKey(e => e.IdCategoryShopping);

                entity.ToTable("Category_Shopping");

                entity.Property(e => e.IdCategoryShopping)
                    .HasColumnName("Id_Category_Shopping")
                    .ValueGeneratedNever();

                entity.Property(e => e.City).HasMaxLength(20);

                entity.Property(e => e.Country).HasMaxLength(20);

                entity.Property(e => e.ItemToBring)
                    .HasColumnName("Item_To_bring")
                    .HasMaxLength(30);

                entity.Property(e => e.Lat).HasMaxLength(20);

                entity.Property(e => e.Lng).HasMaxLength(20);

                entity.Property(e => e.NameShop)
                    .HasColumnName("Name_Shop")
                    .HasMaxLength(10);

                entity.Property(e => e.Neighborhood).HasMaxLength(20);

                entity.Property(e => e.Num).HasMaxLength(5);

                entity.Property(e => e.Street).HasMaxLength(20);

                entity.Property(e => e.WithWhoIdPerson).HasColumnName("With_who_Id_Person");

                entity.HasOne(d => d.BaseEvent)
                    .WithOne(p => p.CategoryShopping)
                    .HasForeignKey<CategoryShopping>(d => d.IdCategoryShopping)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Category___Id_Ca__361203C5");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.CategoryShopping)
                    .HasForeignKey(d => d.WithWhoIdPerson)
                    .HasConstraintName("FK__Category___With___10E07F16");
            });

            modelBuilder.Entity<Conversation>(entity =>
            {
                entity.HasKey(e => e.IdConversation);

                entity.Property(e => e.IdConversation).HasColumnName("Id_Conversation");

                entity.Property(e => e.DateConversation)
                    .HasColumnName("Date_Conversation")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdPerson).HasColumnName("Id_Person");

                entity.Property(e => e.IdTypeConversation).HasColumnName("Id_Type_Conversation");

                entity.Property(e => e.IdUser).HasColumnName("Id_User");

                entity.Property(e => e.Language).HasMaxLength(10);

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Conversation)
                    .HasForeignKey(d => d.IdPerson)
                    .HasConstraintName("FK__Conversat__Id_Pe__0E04126B");

                entity.HasOne(d => d.TypeConversation)
                    .WithMany(p => p.Conversation)
                    .HasForeignKey(d => d.IdTypeConversation)
                    .HasConstraintName("FK__Conversat__Id_Ty__0B91BA14");
            });

            modelBuilder.Entity<MorePicture>(entity =>
            {
                entity.HasKey(e => e.IdMorePicture);

                entity.ToTable("More_Picture");

                entity.Property(e => e.IdMorePicture).HasColumnName("Id_More_Picture");

                entity.Property(e => e.IdPerson).HasColumnName("Id_Person");

                entity.Property(e => e.Picture).HasColumnType("image");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.MorePicture)
                    .HasForeignKey(d => d.IdPerson)
                    .HasConstraintName("FK__More_Pict__Id_Pe__15A53433");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.IdPerson);

                entity.Property(e => e.IdPerson).HasColumnName("Id_Person");

                entity.Property(e => e.Address).HasMaxLength(30);

                entity.Property(e => e.BirthDate)
                    .HasColumnName("Birth_Date")
                    .HasColumnType("date");

                entity.Property(e => e.City).HasMaxLength(20);

                entity.Property(e => e.Email).HasMaxLength(30);

                entity.Property(e => e.FullName)
                    .HasColumnName("Full_Name")
                    .HasMaxLength(20);

                entity.Property(e => e.Id).HasMaxLength(9);

                entity.Property(e => e.IdRelative).HasColumnName("Id_Relative");

                entity.Property(e => e.IdUser).HasColumnName("Id_User");

                entity.Property(e => e.Neighborhood).HasMaxLength(20);

                entity.Property(e => e.Pelephon).HasMaxLength(10);

                entity.Property(e => e.Picture).HasColumnType("image");

                entity.Property(e => e.Telephon).HasMaxLength(10);

                entity.Property(e => e.VoicePrint)
                    .HasColumnName("Voice_Print")
                    .HasMaxLength(30);

                entity.HasOne(d => d.Relative)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.IdRelative)
                    .HasConstraintName("FK__Person__Id_Relat__0EF836A4");
            });

            modelBuilder.Entity<Relative>(entity =>
            {
                entity.HasKey(e => e.IdRelative);

                entity.Property(e => e.IdRelative).HasColumnName("Id_Relative");

                entity.Property(e => e.Description).HasMaxLength(10);

                entity.Property(e => e.LevelWarning)
                    .HasColumnName("Level_Warning")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Subjects>(entity =>
            {
                entity.HasKey(e => e.IdSubject);

                entity.Property(e => e.IdSubject).HasColumnName("Id_Subject");

                entity.Property(e => e.NameSubject)
                    .HasColumnName("Name_Subject")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<TypeConversation>(entity =>
            {
                entity.HasKey(e => e.IdTypeConversation);

                entity.ToTable("Type_Conversation");

                entity.Property(e => e.IdTypeConversation).HasColumnName("Id_Type_Conversation");

                entity.Property(e => e.Description).HasMaxLength(15);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                entity.Property(e => e.IdUser).HasColumnName("Id_User");

                entity.Property(e => e.BirthDate)
                    .HasColumnName("Birth_Date")
                    .HasColumnType("date");

                entity.Property(e => e.City).HasMaxLength(20);

                entity.Property(e => e.Country).HasMaxLength(30);

                entity.Property(e => e.Email).HasMaxLength(30);

                entity.Property(e => e.FirstName)
                    .HasColumnName("First_Name")
                    .HasMaxLength(20);

                entity.Property(e => e.Id).HasMaxLength(9);

                entity.Property(e => e.LastName)
                    .HasColumnName("Last_Name")
                    .HasMaxLength(15);

                entity.Property(e => e.Neighborhood).HasMaxLength(20);

                entity.Property(e => e.Num).HasMaxLength(10);

                entity.Property(e => e.Pelephon).HasMaxLength(10);

                entity.Property(e => e.Street).HasMaxLength(30);

                entity.Property(e => e.Telephon).HasMaxLength(10);
            });

            modelBuilder.Entity<Words>(entity =>
            {
                entity.HasKey(e => e.IdWord);

                entity.Property(e => e.IdWord)
                    .HasColumnName("Id_Word")
                    .ValueGeneratedNever();

                entity.Property(e => e.NameWord)
                    .HasColumnName("Name_Word")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<WordsTime>(entity =>
            {
                entity.HasKey(e => e.IdWordsTime);

                entity.ToTable("Words_Time");

                entity.Property(e => e.IdWordsTime).HasColumnName("Id_Words_Time");

                entity.Property(e => e.Argument).HasMaxLength(50);

                entity.Property(e => e.FuncHowMany)
                    .HasColumnName("Func_How_Many")
                    .HasMaxLength(50);

                entity.Property(e => e.Type).HasMaxLength(50);

                entity.Property(e => e.Word).HasMaxLength(50);
            });

            modelBuilder.Entity<WordsToSubjects>(entity =>
            {
                entity.HasKey(e => e.IdWordsToSubjects);

                entity.ToTable("Words_To_Subjects");

                entity.Property(e => e.IdWordsToSubjects)
                    .HasColumnName("Id_Words_To_Subjects")
                    .ValueGeneratedNever();

                entity.Property(e => e.FrequencyWordToSubject).HasColumnName("Frequency_Word_To_Subject");

                entity.Property(e => e.IdSubject).HasColumnName("Id_Subject");

                entity.Property(e => e.IdWord).HasColumnName("Id_Word");

                entity.HasOne(d => d.Subjects)
                    .WithMany(p => p.WordsToSubjects)
                    .HasForeignKey(d => d.IdSubject)
                    .HasConstraintName("FK__Words_To___Id_Su__7DCDAAA2");

                entity.HasOne(d => d.Words)
                    .WithMany(p => p.WordsToSubjects)
                    .HasForeignKey(d => d.IdWord)
                    .HasConstraintName("FK__Words_To___Id_Wo__7EC1CEDB");
            });
        }
    }
}
