﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using web_api.Data;

namespace web_api.Migrations
{
    [DbContext(typeof(AppDBContext))]
    partial class AppDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.4");

            modelBuilder.Entity("web_api.Models.Artist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Artists");
                });

            modelBuilder.Entity("web_api.Models.Publisher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Publishers");
                });

            modelBuilder.Entity("web_api.Models.Song", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Genre")
                        .HasColumnType("TEXT");

                    b.Property<int?>("PublisherId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PublisherId");

                    b.ToTable("Songs");
                });

            modelBuilder.Entity("web_api.Models.Song_Artist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ArtistId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SongId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.HasIndex("SongId");

                    b.ToTable("Songs_Artists");
                });

            modelBuilder.Entity("web_api.Models.Song", b =>
                {
                    b.HasOne("web_api.Models.Publisher", "Publisher")
                        .WithMany("Songs")
                        .HasForeignKey("PublisherId");

                    b.Navigation("Publisher");
                });

            modelBuilder.Entity("web_api.Models.Song_Artist", b =>
                {
                    b.HasOne("web_api.Models.Artist", "Artist")
                        .WithMany("Song_Artists")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("web_api.Models.Song", "Song")
                        .WithMany("Song_Artists")
                        .HasForeignKey("SongId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artist");

                    b.Navigation("Song");
                });

            modelBuilder.Entity("web_api.Models.Artist", b =>
                {
                    b.Navigation("Song_Artists");
                });

            modelBuilder.Entity("web_api.Models.Publisher", b =>
                {
                    b.Navigation("Songs");
                });

            modelBuilder.Entity("web_api.Models.Song", b =>
                {
                    b.Navigation("Song_Artists");
                });
#pragma warning restore 612, 618
        }
    }
}
