﻿// <auto-generated />
using System;
using Bibliotheque.MVC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Bibliotheque.MVC.Migrations
{
    [DbContext(typeof(BibliothequeContext))]
    [Migration("20210714142139_MigrationInitiale")]
    partial class MigrationInitiale
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.7");

            modelBuilder.Entity("Bibliotheque.MVC.Models.Emprunt", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateEmprunt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateRetour")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateRetourLimite")
                        .HasColumnType("TEXT");

                    b.Property<int>("LivreID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UsagerID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("LivreID");

                    b.HasIndex("UsagerID");

                    b.ToTable("Emprunt");
                });

            modelBuilder.Entity("Bibliotheque.MVC.Models.Livre", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Auteurs")
                        .HasColumnType("TEXT");

                    b.Property<string>("Categorie")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CodeUnique")
                        .HasColumnType("TEXT");

                    b.Property<string>("Isbn10")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Isbn13")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Prix")
                        .HasColumnType("REAL");

                    b.Property<int>("Quantite")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Titre")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("Isbn10");

                    b.HasIndex("Isbn13");

                    b.ToTable("Livre");
                });

            modelBuilder.Entity("Bibliotheque.MVC.Models.Usager", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Courriel")
                        .HasColumnType("TEXT");

                    b.Property<int>("Defaillance")
                        .HasColumnType("INTEGER");

                    b.Property<int>("No")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("Statut")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.ToTable("Usager");
                });

            modelBuilder.Entity("Bibliotheque.MVC.Models.Emprunt", b =>
                {
                    b.HasOne("Bibliotheque.MVC.Models.Livre", "Livre")
                        .WithMany("Emprunts")
                        .HasForeignKey("LivreID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bibliotheque.MVC.Models.Usager", "Usager")
                        .WithMany("Emprunts")
                        .HasForeignKey("UsagerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Livre");

                    b.Navigation("Usager");
                });

            modelBuilder.Entity("Bibliotheque.MVC.Models.Livre", b =>
                {
                    b.Navigation("Emprunts");
                });

            modelBuilder.Entity("Bibliotheque.MVC.Models.Usager", b =>
                {
                    b.Navigation("Emprunts");
                });
#pragma warning restore 612, 618
        }
    }
}
