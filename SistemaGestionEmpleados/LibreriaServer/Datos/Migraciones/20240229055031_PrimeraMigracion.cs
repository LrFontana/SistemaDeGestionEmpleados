using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibreriaServer.Datos.Migraciones
{
    /// <inheritdoc />
    public partial class PrimeraMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AplicacionUsuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCompelto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contraseña = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AplicacionUsuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DepartamentoGeneral",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartamentoGeneral", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InfoTokenActualizado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoTokenActualizado", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medicos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiagnosticoMedico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecomendacionesMEdicas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdEmpleado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroARchivo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Otro = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pais",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolUsuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdRol = table.Column<int>(type: "int", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolUsuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SistemaDeRol",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SistemaDeRol", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoDeSanciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoDeSanciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoDeVacaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoDeVacaciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoHorasExtras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoHorasExtras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departamento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartamentoGeneralId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departamento_DepartamentoGeneral_DepartamentoGeneralId",
                        column: x => x.DepartamentoGeneralId,
                        principalTable: "DepartamentoGeneral",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ciudad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaisId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ciudad", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ciudad_Pais_PaisId",
                        column: x => x.PaisId,
                        principalTable: "Pais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sanciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Castigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CastigoFecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoDeSancionId = table.Column<int>(type: "int", nullable: true),
                    IdEmpleado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroARchivo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Otro = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sanciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sanciones_TipoDeSanciones_TipoDeSancionId",
                        column: x => x.TipoDeSancionId,
                        principalTable: "TipoDeSanciones",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Vacaiones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumeroDeDias = table.Column<int>(type: "int", nullable: false),
                    TipoDeVacacionesId = table.Column<int>(type: "int", nullable: false),
                    IdEmpleado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroARchivo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Otro = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacaiones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vacaiones_TipoDeVacaciones_TipoDeVacacionesId",
                        column: x => x.TipoDeVacacionesId,
                        principalTable: "TipoDeVacaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HorasExtras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoHorasExtrasId = table.Column<int>(type: "int", nullable: false),
                    IdEmpleado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroARchivo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Otro = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HorasExtras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HorasExtras_TipoHorasExtras_TipoHorasExtrasId",
                        column: x => x.TipoHorasExtrasId,
                        principalTable: "TipoHorasExtras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rama",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartamentoId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rama", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rama_Departamento_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pueblo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CiudadId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pueblo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pueblo_Ciudad_CiudadId",
                        column: x => x.CiudadId,
                        principalTable: "Ciudad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Empleado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEmpleado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroArchivo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreCompleto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreTrabajo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Foto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Otro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RamaId = table.Column<int>(type: "int", nullable: false),
                    PuebloId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Empleado_Pueblo_PuebloId",
                        column: x => x.PuebloId,
                        principalTable: "Pueblo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Empleado_Rama_RamaId",
                        column: x => x.RamaId,
                        principalTable: "Rama",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ciudad_PaisId",
                table: "Ciudad",
                column: "PaisId");

            migrationBuilder.CreateIndex(
                name: "IX_Departamento_DepartamentoGeneralId",
                table: "Departamento",
                column: "DepartamentoGeneralId");

            migrationBuilder.CreateIndex(
                name: "IX_Empleado_PuebloId",
                table: "Empleado",
                column: "PuebloId");

            migrationBuilder.CreateIndex(
                name: "IX_Empleado_RamaId",
                table: "Empleado",
                column: "RamaId");

            migrationBuilder.CreateIndex(
                name: "IX_HorasExtras_TipoHorasExtrasId",
                table: "HorasExtras",
                column: "TipoHorasExtrasId");

            migrationBuilder.CreateIndex(
                name: "IX_Pueblo_CiudadId",
                table: "Pueblo",
                column: "CiudadId");

            migrationBuilder.CreateIndex(
                name: "IX_Rama_DepartamentoId",
                table: "Rama",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Sanciones_TipoDeSancionId",
                table: "Sanciones",
                column: "TipoDeSancionId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacaiones_TipoDeVacacionesId",
                table: "Vacaiones",
                column: "TipoDeVacacionesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AplicacionUsuario");

            migrationBuilder.DropTable(
                name: "Empleado");

            migrationBuilder.DropTable(
                name: "HorasExtras");

            migrationBuilder.DropTable(
                name: "InfoTokenActualizado");

            migrationBuilder.DropTable(
                name: "Medicos");

            migrationBuilder.DropTable(
                name: "RolUsuario");

            migrationBuilder.DropTable(
                name: "Sanciones");

            migrationBuilder.DropTable(
                name: "SistemaDeRol");

            migrationBuilder.DropTable(
                name: "Vacaiones");

            migrationBuilder.DropTable(
                name: "Pueblo");

            migrationBuilder.DropTable(
                name: "Rama");

            migrationBuilder.DropTable(
                name: "TipoHorasExtras");

            migrationBuilder.DropTable(
                name: "TipoDeSanciones");

            migrationBuilder.DropTable(
                name: "TipoDeVacaciones");

            migrationBuilder.DropTable(
                name: "Ciudad");

            migrationBuilder.DropTable(
                name: "Departamento");

            migrationBuilder.DropTable(
                name: "Pais");

            migrationBuilder.DropTable(
                name: "DepartamentoGeneral");
        }
    }
}
