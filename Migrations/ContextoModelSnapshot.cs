﻿// <auto-generated />
using System;
using JWForm.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace JWForm.Migrations
{
    [DbContext(typeof(Contexto))]
    partial class ContextoModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("JWForm.Models.Publicador", b =>
                {
                    b.Property<int>("PublicadorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PublicadorId"));

                    b.Property<bool>("EnviouORelatorio")
                        .HasColumnType("bit");

                    b.Property<string>("GrupoDeCampo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.HasKey("PublicadorId");

                    b.ToTable("Publicadores");
                });

            modelBuilder.Entity("JWForm.Models.Relatorio", b =>
                {
                    b.Property<int>("RelatorioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RelatorioId"));

                    b.Property<int>("EstudosBiblicos")
                        .HasColumnType("int");

                    b.Property<int>("Horas")
                        .HasColumnType("int");

                    b.Property<DateTime>("Mes")
                        .HasMaxLength(50)
                        .HasColumnType("datetime2");

                    b.Property<string>("Observacao")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Publicacoes")
                        .HasColumnType("int");

                    b.Property<int>("PublicadorId")
                        .HasColumnType("int");

                    b.Property<int>("Revisitas")
                        .HasColumnType("int");

                    b.Property<int>("Videos")
                        .HasColumnType("int");

                    b.HasKey("RelatorioId");

                    b.HasIndex("PublicadorId");

                    b.ToTable("Relatorios");
                });

            modelBuilder.Entity("JWForm.Models.Relatorio", b =>
                {
                    b.HasOne("JWForm.Models.Publicador", "Publicador")
                        .WithMany("Relatorios")
                        .HasForeignKey("PublicadorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Publicador");
                });

            modelBuilder.Entity("JWForm.Models.Publicador", b =>
                {
                    b.Navigation("Relatorios");
                });
#pragma warning restore 612, 618
        }
    }
}
