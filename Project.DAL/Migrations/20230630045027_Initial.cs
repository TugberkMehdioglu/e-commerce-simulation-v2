using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.DAL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUserProfiles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUserProfiles_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stock = table.Column<short>(type: "smallint", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NeighborHood = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AptNo = table.Column<byte>(type: "tinyint", nullable: false),
                    Flat = table.Column<byte>(type: "tinyint", nullable: true),
                    AppUserProfileId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_AppUserProfiles_AppUserProfileId",
                        column: x => x.AppUserProfileId,
                        principalTable: "AppUserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductAttributes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAttributes_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<short>(type: "smallint", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => new { x.ProductId, x.OrderId });
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4d7b3bc1-f3aa-48ce-b587-5e7dc5553134", "d132d8bc-aa7e-40a1-b5a4-0944e88a1b99", "Member", "MEMBER" },
                    { "4d7b3bc1-f3aa-48ce-b587-5e7dc5557634", "93317a9b-d1b0-4bc6-af52-87061671fa72", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedDate", "DeletedDate", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "ModifiedDate", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "5c8defd5-91f2-4256-9f16-e7fa7546dec4", 0, "22d689fc-48f4-4d8d-8d88-39d376bd5f8c", new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(5263), null, "Admin@gmail.com", true, true, null, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEAuKjMzP0+4gxkh13C6EoskkZ0N14fNnvBDNsy3TqHp4UpIFleCdcYdjJivCZCkOGw==", "05316453125", true, "ba0ba9ac-94c9-463d-8f09-9f173ded168e", 1, false, "Admin" },
                    { "5c8defd5-91f2-4256-9f16-e7fa7546fec5", 0, "6aa2dd6e-f1df-40ca-a745-bb0acf2f4d4e", new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(5276), null, "Member@gmail.com", false, true, null, null, "MEMBER@GMAIL.COM", "MEMBER", "AQAAAAEAACcQAAAAEA6WLQXRL+A20xfEhDX3tvbnI2j2u4fV2bvP+i4fM/cHoBfNPguUN4UCYrG2GwhA8g==", "05433470423", false, "eaa2d77a-320f-463f-9ff6-173a7811ca36", 1, false, "Member" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Description", "ModifiedDate", "Name", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(3624), null, "İşlemci fiyatları, modelleri ve güvenilir işlemci markaları uygun ödeme seçenekleriyle şimdi inceleyin ve hemen satın alın.", null, "İşlemci", 1 },
                    { 2, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(3628), null, "Ekran kartı fiyatları, modelleri ve güvenilir ekran kartı markaları uygun ödeme seçenekleriyle şimdi inceleyin ve hemen satın alın.", null, "Ekran Kartı", 1 },
                    { 3, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(3629), null, "Anakart fiyatları, modelleri ve güvenilir ekran kartı markaları uygun ödeme seçenekleriyle şimdi inceleyin ve hemen satın alın.", null, "Anakart", 1 },
                    { 4, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(3631), null, "Monitör fiyatları, modelleri ve güvenilir ekran kartı markaları uygun ödeme seçenekleriyle şimdi inceleyin ve hemen satın alın.", null, "Monitör", 1 },
                    { 5, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(3635), null, "SSD fiyatları, modelleri ve güvenilir ekran kartı markaları uygun ödeme seçenekleriyle şimdi inceleyin ve hemen satın alın.", null, "SSD", 1 },
                    { 6, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(3636), null, "Harici Disk fiyatları, modelleri ve güvenilir ekran kartı markaları uygun ödeme seçenekleriyle şimdi inceleyin ve hemen satın alın.", null, "Harici Disk", 1 },
                    { 7, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(3636), null, "Bilgisayar Kasası fiyatları, modelleri ve güvenilir ekran kartı markaları uygun ödeme seçenekleriyle şimdi inceleyin ve hemen satın alın.", null, "Bilgisayar Kasası", 1 }
                });

            migrationBuilder.InsertData(
                table: "AppUserProfiles",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "FirstName", "ImagePath", "LastName", "ModifiedDate", "Status" },
                values: new object[] { "5c8defd5-91f2-4256-9f16-e7fa7546dec4", new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(3545), null, "Tuğberk", null, "Mehdioğlu", null, 1 });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "4d7b3bc1-f3aa-48ce-b587-5e7dc5557634", "5c8defd5-91f2-4256-9f16-e7fa7546dec4" },
                    { "4d7b3bc1-f3aa-48ce-b587-5e7dc5553134", "5c8defd5-91f2-4256-9f16-e7fa7546fec5" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedDate", "DeletedDate", "Description", "ImagePath", "ModifiedDate", "Name", "Price", "Status", "Stock" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(5138), null, "AMD 3D V-Cache™ teknolojisi, en iyisini talep eden oyuncular için kararlı performans. Bu performansa AMD 3D V-Cache™ teknolojisine sahip bir AMD Ryzen 9 7950X3D işlemci ile ulaşın. PCIe® 5.0 depolama desteği gibi zaman kazandıran bağlantı teknolojileri ve özellikleri, ultra hızlı WiFi® 6E kablosuz bağlantı, 16 çekirdek/32 iş parçacığı ve özel video hızlandırıcılarına1 sahip olan AMD Ryzen™  9 7950X3D işlemci ile içerik üretme deneyiminizi bir üst seviyeye taşıyın. Güç. Performans. Olanak. AMD Socket AM5 anakart, oyuncular için DDR5 belleğin yüksek hızından AMD EXPO™ teknolojisine2 ve PCIe® 5.0'ın sunduğu artırılmış bant genişliğine kadar, 2025 yılına kadar desteklenen yeni özellikler sunuyor.", "7950X3D.jpg", null, "AMD Ryzen™ 9 7950X3D Soket AM5 4.2GHz 128MB 120W 5nm İşlemci", 23.470m, 1, (short)100 },
                    { 2, 1, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(5144), null, "Intel Core i9 12900K işlemcilerin hibrit performans mimarisi ile bir sonraki seviyeye çıkın1.. Oyun tutkunu, içerik üretici, yayıncı veya günlük kullanıcı olmanız fark etmeksizin istediğiniz yerde ihtiyacınız olan performansa ulaşın. İlginiz alanınız ne olursa olsun istediğiniz her yerde daha fazlasını yapın. Intel Core i9 12900K işlemciler—diğerlerine benzemeyen bir nesil. 12. Nesil Intel® Core™ işlemciler, benzersiz ve yeni hibrit performans mimarisi ile Performans ve Verimlilik çekirdeklerinin (P-çekirdek ve E-çekirdek) eşsiz bir kombinasyonunu sunar. Bu da ne yaparsanız yapın, sezgisel bir şekilde ölçeklenen gerçek kullanım performansı almanızı sağlar.1\r\n \r\nPerformans çekirdeği, Intel'in en yüksek performanslı CPU çekirdeğidir. Ayrıca oyun ve 3D tasarım gibi bilgi işlem bakımından yoğun iş yükleri için tek iş parçacıklı işlem performansını ve yanıt hızını en üst düzeye çıkarmak üzere tasarlanmıştır. Verimlilik çekirdeği, aynı anda yürütülebilecek görevler için çoklu iş parçacığı performansı sunar ve arka plan görevlerinin verimli bir şekilde boşaltılmasını sağlar. Bu sayede çoklu görev çalıştırmayı modern hale getirir.\r\n \r\nIntel, Performans çekirdekleri ve Verimlilik çekirdekleri işletim sistemiyle sorunsuz bir şekilde çalışsın diye Intel® Thread Director'ı doğrudan donanıma dahil etmiştir.2 Çalışma halindeyken otomatik olarak izleyen ve analiz yapan Intel® Thread Director, işletim sistemini yönlendirir ve doğru iş parçacığını doğru zamanda ve doğru çekirdeğe yerleştirmesine yardımcı olur. Ayrıca tüm bunları dinamik bir şekilde yapar, statik kurallar yerine gerçek bilgi işlem ihtiyaçlarını baz alarak planlama ile ilgili yönlendirmeleri değiştirir.\r\n \r\nIntel Core i9 12900K işlemciler, yeni nesil harici grafik kartları ve depolama cihazlarını destekler. Bu cihazlar, PCIe 5.0 ile artan iş hacminden ve DDR5 belleğin daha yüksek hızından ve bant genişliğinden faydalanır.\r\n \r\nIntel Core i9 12900K Soket 1700 işlemcilerin bir diğer standart özelliği olan Intel® Wi-Fi 6E (Gig+), eski cihazların sağlayamadığı özel ve yüksek hızlı kanallar sunar. Intel® Wi-Fi 6E ile girişim olmadan neredeyse üç kat daha hızlı bağlantı deneyimi elde edersiniz3. Bu da evden çalışmak, öğrenmek veya akıcı ve yüksek kaliteli yayınlar ile rahatlamak konusunda daha fazla özgürlük anlamına gelir.\r\n \r\nAyrıca bilgisayarınızı tek bir çift taraflı kablo ile birden fazla 4K ekrana ve diğer aksesuarlara bağlayabilirsiniz. En yenilikçi platformumuz olan 12. Nesil Intel® Core™ işlemci ailesi; en basit, en hızlı ve en güvenilir bağlantı olan Thunderbolt™ 4'ü destekler.\r\n \r\nIntel Core i9 12900K Soket 1700 işlemcilerin standart ve yerleşik özellikleri; gürültü bastırma, otomatik çerçeveleme ve oyun sırasında ağ genişliği ve video çözünürlüğünü optimize etme gibi işlevler sağlar. Bu da size zaman kazandırır ve şu ana kadar sadece hayal edebileceğiniz bir şekilde çoklu görev çalıştırmanızı sağlar.\r\n \r\nMuhteşem oyun oynama, internette gezinme ve yayın deneyimlerinden sıradaki başyapıtınızı üretmeye kadar her şey 12. Nesil Intel® Core™ işlemcilerle mümkün.", "12900K.jpg", null, "Intel Core i9 12900K Soket 1700 12. Nesil 3.20GHz 30MB Önbellek 10nm İşlemci", 22.340m, 1, (short)10 },
                    { 3, 2, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(5146), null, "Bellek Arayüzü: 384bit\r\n \r\nEkran Kartı Chipset Markası: NVIDIA\r\n \r\nVGA Slot: PCI Express 4.0\r\n \r\nBellek Tipi: GDDR6X\r\n \r\nBellek Kapasitesi: 24GB\r\n \r\nGrafik İşlemci: GeForce RTX 3090 Ti\r\n \r\nDirect X: 12\r\n \r\nOpenGL: 4.6", "3090Ti.jpg", null, "GIGABYTE GeForce RTX 3090 Ti GAMING 24GB GDDR6X 384Bit Nvidia Ekran Kartı", 81.146m, 1, (short)50 },
                    { 4, 2, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(5147), null, "\r\nSUPRIM ve logosunun konsepti, elmas kristali ve geometrisinden ilham aldı.\r\n \r\nMSI’ın ekran kartlarında ustalığının 20.yılı anısına SUPRIM serisi, prestiji öne çıkaran öğelerle üretildi.\r\n \r\nYüksek kaliteli malzeme ve üretimi öne çıkarmak için köşeler, çizgiler ve düzlemlerin geometrisi kullanıldı. GeForce RTX 40 Serisi ile Güçlendirildi\r\n \r\nNVIDIA® GeForce RTX™ 40 Serisi GPU'lar, DLSS 3 desteğiyle oyuncular ve içerik oluşturucular için hızlının ötesindedir. Bu GPU'lar, hem performansta hem de yapay zeka destekli grafikler açısından büyük bir atılım sağlayan ultra verimli NVIDIA Ada Lovelace mimarisi ile güçlendirilmiştir. Yeni Akış Çoklu İşlemcileri\r\n2 KATA KADAR performans artışı ve güç verimliliği\r\n \r\n4.Nesil Tensor Çekirdekleri\r\n2 KATA KADAR YZ Performansı\r\n \r\n3.Nesil RT Çekirdekleri\r\n2 KATA kadar Ray Tracing performansı. SUPRIM ile daha üstün bir deneyime geçin. Yüksek kaliteli malzeme ve oyun tutkunlarına yönelik özelliklerle tamamen yükseltilmiş bir ekran kartını duyun, görün ve hissedin. TORX FAN 5.0, heatsink üzerine gelen hava basıncını ve miktarını arttıran bir tasarım geliştirmesidir. Bu tasarım ile elde edilen hava akışı, TORX FAN 4.0 tasarımına göre en az %10, aksiyal fanlara göre ise en az %23 daha yoğundur. Her üç fan kanadını birbirine bağlayan bağlantı kemerleri hava akımını heatsink üzerine daha fazla yoğunlaştırır. Her bağlantı kemeri uçlarda içe doğru kıvrılarak türbülansı ve direnci azaltır. Kartın yüzeyinden hafifçe daha yüksek olan fan kapağı, havanın fanlar boyunca akışını rahatlatır. Bu kapak altında yer alan çıkıntılar havanın tekrar içeri girmesini engeller ve hava akışını kararlı tutarken gürültüyü azaltır. Buhar odası, düz yüzeylerde ısıyı sabit tutmak için mükemmel bir yöntemdir. GPU ve VRAM üzerinde yer alan buhar odası, ısıyı Core Pipe borular üzerine yönlendirir. Core Pipe soğutma boruları, mevcut boş alanı en verimli şekilde değerlendirecek şekilde ve en ince hesaplarla üretildi. Kare şeklindeki soğutma boruları GPU taban plakasına tam temas yaparak ısıyı heatsink boyunca dağıtır. Fan motorunun altında ve hava akışının az olduğu noktalarda yer alan dalga yapılı kenarların boyutları tekrar ayarlanarak ek verimlilik sağlandı. Hava akımının daha rahat geçişine olanak tanımak için V kesimli kanatlar stratejik olarak yerleştirildi. Eğim açısının optimize edilmesi ve dalgalanmanın arttırılması ile hava direnci merkeze doğru yönlendirildi. Böylece sıcak hava tıpkı bir namlu gibi hızla dışarı doğru itiliyor. Sağlam metal taban plakası altında yer alan termal yastıklar ek soğutma sunarken hava kanalları ile sıkışan ısının dışarı atılması sağlanır. Sıcaklık nispeten düşük ise aktif soğutma gerekmediği zamanlarda fanlar otomatik olarak durur ve ses tamamen kesilir. Oyun sırasında ısı arttığında fanlar tekrar dönmye başlar. İki set dayanıklı rulman sayesinde TORX FAN’lar yoğun ve uzun oyun seanslarınıza yıllar boyunca dayanır.", "RTX4090.jpg", null, "MSI GeForce RTX 4090 SUPRIM X 24GB GDDR6X 384Bit DLSS 3 Ekran Kartı", 71.984m, 1, (short)100 },
                    { 5, 3, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(5149), null, "En son teknolojiye, yüksek hızlı aktarıma, ultra performansa ve Sektörün ilk M-Vision Panosuna sahip MEG X670E GODLIKE, AMD X670 yonga setinin olağanüstü oyun potansiyelini ortaya çıkarmak için geliştirildi ve turnuvayı bir kez daha yönetmeye hazır. Dokunmatik 4,5 inçlik tam renkli IPS LCD, yalnızca donanım izleme ve hata ayıklama işlevini birleştiren oyun donanımınızın durumunu göstermekle kalmaz, aynı zamanda dokunmatik kontrolle sistemin çalıştırılmasına ve özelleştirilmesine olanak tanır.\r\n \r\nDashbaoard, sistem çalışmasını, OC ayarlamayı, donanım izlemeyi, hata ayıklama işlevini, canlı hava durumunu, sistem saatini, geri sayım sayacını ve dokunmatik kontrol ile çoklu işlevleri destekler. Ayrıca, müstakil mod, bilgisayarınızın çalışması için yeni bir yol sunar. VRM Soğutma: Genişletilimiş Heatsink tasarımı ve bobinler için ek 7W/mK termal yastıklar ile gelişmiş ısı giderimi.\r\n \r\nPCB Soğutma: Birinci sınıf 6 katmanlı sunucu sınıfı baskı devresi tasarımı, 2 oz. kalınlaştırılmış bakır ile ısı giderimini ve kararlılığı arttırır.\r\n \r\nSSD Soğutma: M.2 Shield Frozr, ısıya bağlı yavaşlamayı önler.\r\n \r\nSistem Soğutma: Stratejik olarak yerleştirilen pin başlıkları ve adanmış pompa fanı başlığı ile soğutma sisteminizi rahatça bağlayın ve senkronize edin. MSI PCI Express Steel Armor slotları ağır ekran kartlarına destek sağlamak için anakarta ek lehimlerle sabitlenmiştir. Oyunda her avantaj önemlidir. Bu yüzden Steel Armor aynı zamanda temas noktalarını elektromanyetik sinyal girişimine karşı da korur:\r\n \r\n* x16/x0/x4, x8/x8/x4 destekler\r\n \r\n** MSI SMT (yüzey montaj teknolojisi) güçlü lehimler ile sinyal saflığını arttırır.", "X670E.jpg", null, "MSI MEG X670E GODLIKE AMD X670 Soket AM5 DDR5 6600MHz(OC) M.2 Anakart", 57.568m, 1, (short)20 },
                    { 6, 3, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(5150), null, "AMD WRX80 Ryzen™ Threadripper™ PRO EATX iş istasyonu anakartı. Intel dual 10 G LAN, USB 3.2 Gen 2x2 Type-C girişi, 7 x PCIe 4.0 x16 yuvası, 3 x M.2 PCIe 4.0, ASMB9-iKVM, 2 x U.2 ve 16 güç aşaması\r\n \r\n\r\nAMD sWRX8 soket: AMD Ryzen™ Threadripper™ PRO Serisi İşlemciler için hazır\r\n \r\nUltra hızlı bağlantı seçenekleri: USB 3.2 Gen 2x2 Type-C portu, 10 x USB 3.2 Gen 2 portu, 3 x M.2 PCIe 4.0, HYPER M.2 x16 Gen 4 card ve Intel® X550-AT2 çift 10Gb Ethernet\r\n \r\nASMB9-iKVM ile uzaktan yönetim: Gelişmiş IT verimliliği için donanım düzeyinde daha iyi kontrol sunan bant dışı yönetim olanakları için IPMI mimarisine akıllı özellikler katan BMC yongası\r\n \r\nGüçlü performans: 16 güç aşaması, çoklu GPU desteği ve R-DIMM bellek desteği\r\n \r\nGüvenilir kararlılık: 7/24 güvenilirlik için testlerden geçirildi. Geniş uyumluluk sunar ve üst düzey güvenlik için SafeSlot’a sahiptir\r\n \r\nProfesyonel üretkenlik için üstün güç\r\nASUS Pro WS WRX80E-SAGE SE WIFI en yeni AMD® Ryzen® Threadripper™ Pro serisi işlemcilerin inanılmaz gücünü açığa çıkarmak için geliştirildi. Profesyonel yaratıcılık işleri için güvenilir bir temel oluşturan bu anakart, video düzenleme ve 3D görüntü işleme gibi çok çekirdekli CPU gerektiren görevlerde avantaj sağlıyor. SafeSlot özellikli yedi adet PCIe 4.0 yuvası da en yeni güçlü ekran kartlarıyla olağanüstü performans sağlıyor. Metalik siyah alüminyum çıkıntılar, parlak ayrıntılı paslanmaz çelik siyah file tasarım ve şeffaf duman grisi giriş çıkış korumasıyla, Pro WS WRX80E-SAGE SE WIFI içerik üreticileri için profesyonel ve düşük profilli bir görünüm sunar. Pro WS WRX80E-SAGE SE WIFI’da yüksek performanslı ekran kartları için yedi adet PCIe® 4.0 x16 yuvası yer alıyor. Bu sayede, profesyonel tasarım, modelleme, simülasyon ve görüntü işleme uygulamaları için üstün performans elde edilebiliyor.", "WRX80E-SAGE.jpg", null, "ASUS PRO WS WRX80E-SAGE SE 3200mhz (OC) M.2 Wi-Fi WRX80 e-ATX Anakart", 33.553m, 1, (short)100 },
                    { 7, 4, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(5151), null, "45\" WQHD (3440 x 1440) 21:9 Kavisli (800R) OLED Ekran\r\n \r\n240 Hz Yenileme Hızına Sahip OLED\r\n \r\n0,03 ms (GtG) Tepki Süresi\r\n \r\n1,5M:1 Kontrast Oranı\r\n \r\nNVIDIA® G-SYNC® Uyumlu\r\n \r\nAMD FreeSync™ Premium\r\n \r\n *Bu monitör, Aralık 2022 itibarıyla OLED oyun monitörlerinde en hızlı yenileme hızı ve yanıt süresi olan 240 Hz yenileme hızını ve 0,03 ms'yi destekler.\r\n*Aralık 2022 itibariyle, piyasada 240 Hz yenileme hızına sahip başka OLED monitör yoktur.\r\n*Görüntüler, özelliğin daha iyi anlaşılması için temsili olarak verilmiştir. Asıl kullanım şeklinden farklılık gösterebilir.\r\n**DCI-P3 Tipik %98,5, Minimum %90. Panoramik Oyun Görünümü\r\n \r\nHDR10 ve DCI-P3 %98,5 (Tipik) geniş renk gamına sahip 21:9 WQHD OLED ekran sayesinde, oyuncuların 45 inçlik geniş ekranda kendilerini oyunun merkezindeymiş gibi hissetmelerine yardımcı olabilir.\r\n \r\n*Görüntüler, özelliğin daha iyi anlaşılması için temsili olarak verilmiştir. Asıl kullanım şeklinden farklılık gösterebilir.\r\n ", "LG45.png", null, "LG 45\" 45GR95QE-B UltraGear 0,03Ms 240Hz WQHD OLED Curved Gaming Monitor", 61.832m, 1, (short)8 },
                    { 8, 4, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(5152), null, "49WL95C, tek ekranda iki katına çıkarılmış 27 inç 16:9 QHD piksel alanına sahip bir UltraWide 32:9 Dual QHD (5120x1440) monitördür. 32:9 FHD çözünürlüğe (3840x1080) kıyasla %70 daha fazla pikselle, birden fazla format ve birden fazla katmanla birçok görevi aynı anda gerçekleştirin. Referansları bulun ve ham görüntülerle kullanılmak üzere stok görüntüleri düzenleyin. Küçük resimler, geçerli pencere değiştirilmeden tek bir ekranda görüntülenebilir. Böylece, görüntü planlama aşamasında harcanan zamanı azaltabilirsiniz. Video film şeridini kontrol ederek, videolarınız için kaynak klipleri inceleyebilirsiniz. Pencereleri değiştirmeden, kaynak dosyaların küçük resimlerini görüntüleyebilir ve dosyaları zaman çizelgesine yükleyebilirsiniz. Beş dakikalık bir zaman çizelgesini kontrol edebilir ve kaydırma yapmadan bir düzenleme görevini gerçekleştirebilirsiniz. YouTube videolarında kullanılan müzik klipleri için görevler 1:1 video düzenleme ve bir mix aracı ile tamamlanabilir.", "LG49.jpg", null, "LG 49\" 49WL95C-WE 32:9, UltraGeniş 5Ms WQHD, HDR10, MaxxAudio® IPS Monitör", 58.298m, 1, (short)100 },
                    { 9, 5, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(5154), null, "Kompakt bir M.2 2280 form faktörü ile MP600 PRO LPX, konsolunuza doğrudan kolayca sığar ve hızlı ve kolay bir depolama yükseltmesi için tüm PS5* M.2 performans gereksinimlerini karşılar. İster 4, isterse 40 veya daha fazla oyununuz olsun; konsolunuzun depolama alanını neredeyse tüm oyun kitaplıklarının ihtiyaçlarını karşılayan, devasa 4TB'lık boyuta kadar genişletin. PCIe Gen4 teknolojisiyle 7.100 MB/sn sıralı okuma ve 6.800 MB/sn sıralı yazmaya varan ışık hızında hızlara ulaşarak, büyük oyun dosyalarının daha hızlı ve sorunsuz yüklenmesini sağlayın. Isıyı dağıtmaya ve kısıtlamayı azaltmaya yardımcı olan, önceden takılan düşük profilli alüminyum ısı dağıtıcıyı barındırır. Böylece oyun yoğunluğunuz ne olursa olsun SSD'niz yüksek performansını sürdürür. Sürücünüzün uzun yıllar boyunca en iyi şekilde performans göstermeye devam etmesi için gerekli performans ve dayanıklılığın ideal bir bileşimini sunmak için yüksek yoğunluklu 3D TLC NAND ile donatılmıştır.", "MP600.jpg", null, "CORSAIR 2TB MP600 PRO LPX Serisi NVMe M.2 SSD (7100MB Okuma / 6800MB Yazma)", 15.579m, 1, (short)8 },
                    { 10, 5, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(5156), null, "-", "M480.jpg", null, "MSI SPATIUM M480 PLAY PCIE 4.0 NVME M.2 2TB SSD (7000MB Okuma / 6850MB Yazma)", 13.263m, 1, (short)15 }
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "AppUserProfileId", "AptNo", "City", "Country", "CreatedDate", "DeletedDate", "District", "Flat", "ModifiedDate", "Name", "NeighborHood", "Status", "Street" },
                values: new object[] { 1, "5c8defd5-91f2-4256-9f16-e7fa7546dec4", (byte)21, "İstanbul", "Türkiye", new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(2320), null, "Kağıthane", (byte)8, null, "Ev", "Çeliktepe", 1, "Şairane" });

            migrationBuilder.InsertData(
                table: "ProductAttributes",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "ModifiedDate", "Name", "ProductId", "Status", "Value" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4881), null, null, "Çekirdek Sayısı", 1, 1, "16 çekirdek" },
                    { 2, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4886), null, null, "Frekans Hızı", 1, 1, "4.20 GHz" },
                    { 3, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4888), null, null, "Önbellek", 1, 1, "144 MB" },
                    { 4, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4889), null, null, "İşlemci Markası", 1, 1, "AMD" },
                    { 5, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4891), null, null, "Soket Tipi", 1, 1, "Soket AM5" },
                    { 6, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4892), null, null, "İşlemci Hızı", 1, 1, "4,2 GHz" },
                    { 7, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4893), null, null, "Garanti", 1, 1, "24" },
                    { 8, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4894), null, null, "Menşei", 1, 1, "Çin" },
                    { 9, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4896), null, null, "Çekirdek Sayısı", 2, 1, "16 çekirdek" },
                    { 10, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4914), null, null, "Frekans Hızı", 2, 1, "5,2 GHz" },
                    { 11, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4916), null, null, "Önbellek", 2, 1, "30 MB" },
                    { 12, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4917), null, null, "İşlemci Markası", 2, 1, "INTEL®" },
                    { 13, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4918), null, null, "Soket Tipi", 2, 1, "LGA 1700" },
                    { 14, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4919), null, null, "İşlemci Hızı", 2, 1, "5,2 GHz" },
                    { 15, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4920), null, null, "Garanti", 2, 1, "24" },
                    { 16, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4921), null, null, "Menşei", 2, 1, "Çin" },
                    { 17, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4922), null, null, "Ekran Kartı Chipset Marka", 3, 1, "NVIDIA®" },
                    { 18, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4923), null, null, "Grafik İşlemci", 3, 1, "GeForce RTX 3090 Ti" },
                    { 19, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4925), null, null, "Çekirdek Hızı Maks.", 3, 1, "1860 MHz" },
                    { 20, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4925), null, null, "Çekirdek Hücre Sayısı", 3, 1, "10752 Cuda" },
                    { 21, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4926), null, null, "Bellek Tipi", 3, 1, "GDDR6X" },
                    { 22, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4927), null, null, "Bellek Kapasitesi", 3, 1, "24GB" },
                    { 23, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4928), null, null, "Bellek Hızı", 3, 1, "21000 MHz" },
                    { 24, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4929), null, null, "Bellek Arayüzü", 3, 1, "384 Bit" },
                    { 25, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4929), null, null, "Ekran Kartı Chipset Marka", 4, 1, "NVIDIA®" },
                    { 26, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4930), null, null, "Grafik İşlemci", 4, 1, "GeForce RTX 4090" },
                    { 27, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4931), null, null, "Çekirdek Hızı Maks.", 4, 1, "2640 MHz" },
                    { 28, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4932), null, null, "Çekirdek Hücre Sayısı", 4, 1, "16384 Cuda" },
                    { 29, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4933), null, null, "Bellek Tipi", 4, 1, "GDDR6X" },
                    { 30, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4933), null, null, "Bellek Kapasitesi", 4, 1, "24GB" },
                    { 31, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4935), null, null, "Bellek Hızı", 4, 1, "21000 MHz" },
                    { 32, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4936), null, null, "Bellek Arayüzü", 4, 1, "384 Bit" },
                    { 33, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4937), null, null, "Soket Tipi", 5, 1, "Soket AM5" },
                    { 34, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4939), null, null, "Ram Tipi", 5, 1, "DDR5" },
                    { 35, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4939), null, null, "Anakart Markası", 5, 1, "MSI" },
                    { 36, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4940), null, null, "Anakart Chipseti", 5, 1, "AMD® X670" },
                    { 37, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4941), null, null, "Anakart Yapı", 5, 1, "ATX" },
                    { 38, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4942), null, null, "Ram Slot Sayısı", 5, 1, "4" },
                    { 39, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4945), null, null, "Maks. Ram Desteği", 5, 1, "128GB" },
                    { 40, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4945), null, null, "Desteklenen Ram Hızı", 5, 1, "4800MHz" },
                    { 41, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4946), null, null, "Soket Tipi", 6, 1, "Soket sWRX8" }
                });

            migrationBuilder.InsertData(
                table: "ProductAttributes",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "ModifiedDate", "Name", "ProductId", "Status", "Value" },
                values: new object[,]
                {
                    { 42, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4947), null, null, "Ram Tipi", 6, 1, "DDR4" },
                    { 43, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4948), null, null, "Anakart Markası", 6, 1, "ASUS" },
                    { 44, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4949), null, null, "Maks. Ram Desteği", 6, 1, "2048GB" },
                    { 45, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4950), null, null, "Anakart Yapı", 6, 1, "Extended ATX" },
                    { 46, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4950), null, null, "Ram Slot Sayısı", 6, 1, "8" },
                    { 47, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4952), null, null, "Maks. Ram Hızı (O.C.)", 6, 1, "3200mhz" },
                    { 48, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4953), null, null, "Desteklenen Ram Hızı", 6, 1, "3200mhz" },
                    { 49, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4953), null, null, "Yenileme Hızı", 7, 1, "240Hz" },
                    { 50, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4954), null, null, "Ekran Boyutu", 7, 1, "45 inch" },
                    { 51, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4955), null, null, "Çözünürlük (Piksel)", 7, 1, "3440 x 1440" },
                    { 52, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4956), null, null, "Görüntü Formatı", 7, 1, "21:9" },
                    { 53, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4957), null, null, "Ekran Renk", 7, 1, "1.07 Milyar" },
                    { 54, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4957), null, null, "Piksel Aralığı", 7, 1, "84 PPI" },
                    { 55, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4958), null, null, "İzleme Açısı", 7, 1, "178º(R/L), 178º(U/D)" },
                    { 56, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4959), null, null, "Sync Teknolojisi", 7, 1, "FreeSync / G-Sync" },
                    { 57, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4960), null, null, "Yerel açı oranı", 8, 1, "32:9" },
                    { 58, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4961), null, null, "Yenileme yanıt süresi", 8, 1, "5 ms" },
                    { 59, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4961), null, null, "Panel tipi", 8, 1, "AH-IPS" },
                    { 60, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4962), null, null, "En boy oranı", 8, 1, "32:9" },
                    { 61, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4963), null, null, "Ekran parlaklığı (tipik)", 8, 1, "350 cd/m²" },
                    { 62, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4964), null, null, "Görüntüleme teknolojisi", 8, 1, "LED" },
                    { 63, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4965), null, null, "HD türü", 8, 1, "UltraWide Quad HD" },
                    { 64, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4965), null, null, "Kontrast oranı", 8, 1, "1000:1" },
                    { 65, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4966), null, null, "Standart bağlantı", 9, 1, "PCI Express 4.0" },
                    { 66, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(4967), null, null, "Donanım şifreleme", 9, 1, "Var" },
                    { 67, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(5000), null, null, "TBW sınıfı", 9, 1, "1400" },
                    { 68, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(5003), null, null, "NVMe", 9, 1, "Var" },
                    { 69, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(5004), null, null, "Sıralı okuma hızı (CDM)", 9, 1, "7100 MB/s" },
                    { 70, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(5005), null, null, "Hafıza tipi", 9, 1, "3D TLC NAND" },
                    { 71, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(5005), null, null, "SSD form faktörü", 9, 1, "M.2" },
                    { 72, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(5006), null, null, "Sıralı yazma hızı (CDM)", 9, 1, "6800 MB/s" },
                    { 73, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(5007), null, null, "Standart bağlantı", 10, 1, "PCI Express 4.0" },
                    { 74, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(5008), null, null, "Donanım şifreleme", 10, 1, "Var" },
                    { 75, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(5009), null, null, "TBW sınıfı", 10, 1, "1400" },
                    { 76, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(5010), null, null, "NVMe", 10, 1, "Var" },
                    { 77, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(5011), null, null, "Sıralı okuma hızı (CDM)", 10, 1, "7100 MB/s" },
                    { 78, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(5012), null, null, "Hafıza tipi", 10, 1, "3D TLC NAND" },
                    { 79, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(5012), null, null, "SSD form faktörü", 10, 1, "M.2" },
                    { 80, new DateTime(2023, 6, 30, 7, 50, 27, 564, DateTimeKind.Local).AddTicks(5013), null, null, "Sıralı yazma hızı (CDM)", 10, 1, "6800 MB/s" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_AppUserProfileId",
                table: "Addresses",
                column: "AppUserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AddressId",
                table: "Orders",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AppUserId",
                table: "Orders",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributes_ProductId",
                table: "ProductAttributes",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "ProductAttributes");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "AppUserProfiles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
