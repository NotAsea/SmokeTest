﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmokeTestLogin.Data.Migrations;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Users",
            columns: table => new
            {
                Id = table.Column<long>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Name = table.Column<string>(type: "TEXT", nullable: false),
                UserName = table.Column<string>(type: "TEXT", nullable: false),
                Password = table.Column<string>(type: "TEXT", nullable: false),
                IsActived = table.Column<bool>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users", x => x.Id);
            });

        migrationBuilder.InsertData(
            table: "Users",
            columns: new[] { "Id", "IsActived", "Name", "Password", "UserName" },
            values: new object[,]
            {
                { 1L, true, "Toby Vandervort", "543E8085F01FB6823577F1C320512B9285554458EBA3E24DC066F0BC499396B6:33D1292506DC72FF6354D37D7562D0EF:500:SHA256", "Toby_Vandervort85" },
                { 2L, true, "Jerry Koch", "79548F7B57BCF369DB927EAE0E3CF48B20B11B569BD021A288005A00ED7AEF0B:8BDB7EA4DDAF26C5B42E763B0067AABA:500:SHA256", "Jerry59" },
                { 3L, true, "Mathew Pollich", "2B90AA803E31AB091B6407F35E2D9B8FF42738EF1B132FC31862CF63A011F712:7555BA262E7BE4F7A9AE39BF38382A6D:500:SHA256", "Mathew.Pollich" },
                { 4L, true, "Ruben Jerde", "6DDA87EC575812A63FAEBA56757EA01E538C0AD6B55C317DAB03C814D8FDB0E0:DCF257E298412A73E0790D158D16012B:500:SHA256", "Ruben32" },
                { 5L, true, "Phil Hane", "B7062AC611D8C1E1D3B54DDCEF5AE2DF61CF41F80E8B215FD7B53E397257BE2D:D9B0AD8CA3642AC7F369E406AC08DFA2:500:SHA256", "Phil_Hane35" },
                { 6L, true, "Enrique Swift", "905CD87F689C51631C361C3890B959685E7F4D5D1525812D4335E321AC6D79C0:23750D72628B27DA57643E2B3578C89D:500:SHA256", "Enrique.Swift66" },
                { 7L, true, "Dean Leffler", "6DBEAF14FE7ED8B9A80752FF6436937F853CE260474A84FDDA1432B29469E8B0:F7B0B49CAF9EA6359DB64FD4D39374D5:500:SHA256", "Dean.Leffler" },
                { 8L, true, "Beulah Hane", "7499E872D4FD27081AAD5CA2566DE6AB58EBBCD0B7CE5E8A337BE29CBEA340C0:19FC80F593B0287703F5CB153230AE2F:500:SHA256", "Beulah_Hane43" },
                { 9L, true, "Isaac Stanton", "7D258146332F35A3401DA0A37C7C4A21D605100851E7C01A17E7A29A21F4B315:49E95CB1EA2859EF15B4C316BFAA5960:500:SHA256", "Isaac_Stanton" },
                { 10L, true, "Owen Schamberger", "5FC7AE5003C8869386463B5B666DC4726F114A675575931A1EC8A3BA1F2041FC:2EB02FD5ECAF3A212B9C0B9A32BC6563:500:SHA256", "Owen24" },
                { 11L, true, "Judith Sporer", "548E26E96D4B822AEDA377EB8943438EA4613C7871814996196DB636F8A54ADB:475F533662BF7A53F7D82E04C059890C:500:SHA256", "Judith53" },
                { 12L, true, "Willard Roberts", "34F332DF324A20292D910FB6F677766064F6EA0FF2524375AF3D1391900C07D2:86FAA6C03EE8B6FD19087F898EA0D92D:500:SHA256", "Willard_Roberts31" },
                { 13L, true, "Peter Macejkovic", "FAAEFEF4F7BF83FC14A2291FB151443132E4523AC9AEF6CCBB1F8F1146CCEDF6:E25D59B7DF85CC11DEE343D6EFA3A684:500:SHA256", "Peter79" },
                { 14L, true, "Terence Turner", "FFDD8B5191D798E7B7EF46B9409E008BB9CD301BF27154E98FC04DEE283DE4A9:DB80E9C136D7FC52B94D64F8E64E993C:500:SHA256", "Terence_Turner" },
                { 15L, true, "Valerie Willms", "5CB25AA90A72AFF16D49D4125B44F130A201977967EC324C268B28A5E8427565:6E068CF3FDAAEA3CAA9526CE368A462E:500:SHA256", "Valerie.Willms" },
                { 16L, true, "Deborah Haag", "DA1A497EA26D7ECCE652AF46A8323FE93B1B6B5A29C24A054A54CF2846883336:81AEDFD4FE39BC4EDB48066B7A1D62EE:500:SHA256", "Deborah.Haag28" },
                { 17L, true, "Erin Lesch", "912D03C8E43589E4F4A00BE83AF0DFE1B7B903502439B147D5DC71985927B13F:EB67D4CD775172EBF7525630D7D5C43D:500:SHA256", "Erin.Lesch" },
                { 18L, true, "Beth Kulas", "ABD6DA8111AA47956E96AB4FE385A3D55756281F36E18CCCEE28652752D1B2B1:E6A510D0260B73EE28A83A69C65275AF:500:SHA256", "Beth55" },
                { 19L, true, "Tracy Jakubowski", "D080568F28834D18FEFC66E160FC0B142FC392E119C3F250FBCA1ABC6B52FD5A:F2F488498523573DC6B7B6FA05A9C256:500:SHA256", "Tracy_Jakubowski20" },
                { 20L, true, "Frankie Dietrich", "BC69593109D92A078A1C331CCE7DF7323C42B93A308CD38C15A335E9382A5B0A:C0FEC61CCE640137681CB4EA345A0BD2:500:SHA256", "Frankie70" },
                { 21L, true, "Rickey Streich", "0A0E141096DF36CF3CF727ACD57E448CF5AE38AA84B0E2916B65F7218B75999B:C221D7E1DDA8A113B07CF696C2A3715D:500:SHA256", "Rickey25" },
                { 22L, true, "Jaime Purdy", "B41B32F6FFEDC8342FDBBC9BFBA4DFAA72BEAE47D7753C5F060B389B3837ECCB:1DDF445B38C92D8A3A5066752BFC7FFB:500:SHA256", "Jaime70" },
                { 23L, true, "Toni Runte", "A6695EB51E3ACAF913BAFB9CB97FE801E64353063119693CFC05BCDD53A4B80F:E537FFD5E68F070CE21D6DD20637906E:500:SHA256", "Toni_Runte" },
                { 24L, true, "Sherry Harvey", "8B468559A51B13085989E41B17C5B3ECBEB666B087199ED1C0622F9CC8FB3930:AF73975DFFBCDAEF93DD1CFBBD9107ED:500:SHA256", "Sherry.Harvey35" },
                { 25L, true, "Bernard Sauer", "620551BAB4B1E701A0A49A2FFC65BF42B32CFF2C9864A8347C10D63AD668E2D5:2DEB8B87E13C88467F704A51ABD26364:500:SHA256", "Bernard.Sauer" },
                { 26L, true, "Kim Brekke", "4924FF7606FFFA71EF1F9686ACFA1B20109D58E34B066675D30182C6ACA0D607:B432A2B3F655534EFEFCAC78D7A9DD3D:500:SHA256", "Kim85" },
                { 27L, true, "Carrie Dickens", "6F81C4DE3756A4843AC2696869A2B93A80D6B98786B107D8BDD7F82B3E25F7D4:2AE3B96739D44EEC37A54F0FB50BA57C:500:SHA256", "Carrie2" },
                { 28L, true, "Brent Dickinson", "852FC1F05134A4D37461ED6D7AC37D251C2A9A0BB1B0F55C2C47DFCD017E2FA6:9FF016EFFDC1428EA09DDFD310CE428C:500:SHA256", "Brent_Dickinson37" },
                { 29L, true, "Beatrice Ryan", "BE39BDE441373171CCC43358ACCC3BB12B07123B2CA2D0D3642BDC75B490B40D:6D4E90197FF91FA256A7325255C1AFCE:500:SHA256", "Beatrice75" },
                { 30L, true, "Kara Torp", "7AC148FB3B4976C055E233A98611E5D8520129700806D91E0C84C0FFBE5054D8:FFED7DCEF6F87498FD51BFCB15104D2A:500:SHA256", "Kara_Torp" },
                { 31L, true, "Ruben Ruecker", "BF7F4028FFE24CBA0EAF48F8089DEC6B84C95532534CDA9F41265107D7D6FECB:985A42DCCF5835A63CE4E4DF7D11FD1A:500:SHA256", "Ruben_Ruecker" },
                { 32L, true, "Veronica Raynor", "4FD60EB5DF2FBE34103EA738CB773B03044BAD78E5D1600D17643090EC64EEF7:DCBE32F06B82EE4E57B59FCB1EF4056C:500:SHA256", "Veronica_Raynor" },
                { 33L, true, "Stella Haag", "7C8FDB0D56212EA4196CE4B5F3B6B5D99B07513805C15641F6B17C9007A4C159:677DBAFE75E5AE2D2DCC817367C65BF1:500:SHA256", "Stella10" },
                { 34L, true, "Brooke Leuschke", "50C728E97133088DE858178F248DF62CF1875E3C13C7A59E27B337F99CB05313:65E783EEAB7BCE49E39E4EF45E50422B:500:SHA256", "Brooke_Leuschke0" },
                { 35L, true, "Nicole Breitenberg", "9FF5E82AE6B68DDAE9214A5BFEAB55A00C9110F9F085F8162ED5D65DF6B3EC9C:CBAE7B642E2F0F4C82160FEB1BAD6F56:500:SHA256", "Nicole.Breitenberg18" },
                { 36L, true, "Kari O'Connell", "DC5C585B91D57E822621A4EDBA07260BB7B3DC7446A9344483AEB9CD1E10D9E2:780952AF9C2CF83ED4F515BFF35C1FCC:500:SHA256", "Kari.OConnell43" },
                { 37L, true, "Ramiro Fisher", "4B9AB0638373B4ED51719094420B3A6311D45B29888ACAFEB352AF5F7B0650B0:F7D7DA58F73FCCE40C04A3056047EC1A:500:SHA256", "Ramiro_Fisher" },
                { 38L, true, "Jessie Bosco", "85100A7E0CAA27E293BADCD6452DBB128A59470858E5EB99195F10B25D047A2C:B707E7C182B6631C7F8964C217127590:500:SHA256", "Jessie_Bosco" },
                { 39L, true, "Carroll Lehner", "F1CE078C53011B11DD383AB5AE269CA14C0B3B1AD7B09EE9F745BF120659ABA8:91D062DFF0D647D009EADF84D0C7F8B3:500:SHA256", "Carroll.Lehner74" },
                { 40L, true, "Spencer West", "5FA41268CF5777C8A2CA0D2167FB34D320AB7E87D5522FD1FD54B2686D2AE87F:497AB736E57C126C1435D10A93CA50F7:500:SHA256", "Spencer60" },
                { 41L, true, "Kerry Towne", "2CE81AD0FB37BCA9D725EC650517DAA690A59F0FFC1A92D7244FC74FF868B3F8:BED3A35CA2F34F826B5CF919BBB7B2C4:500:SHA256", "Kerry22" },
                { 42L, true, "Manuel Bogan", "9DF6D2186A716C9C8CA43F53E39DC08B719DA06A385E2E3E044BE22B46B2817B:F77DA13891A6F47C2335C2D62E575941:500:SHA256", "Manuel.Bogan66" },
                { 43L, true, "Carlos Langworth", "C1685976BDC9A4AEBE09987A2CDF057CC5BD13DF8D4A6C5FF466B1D4F95E6CEF:BCADFCE746630B14B256D8DFD4A44243:500:SHA256", "Carlos.Langworth49" },
                { 44L, true, "Jody Jaskolski", "BC842F9B3DDB848CB95095B9FD3315E3440EAEE309472BE7145B92DCAD5933C0:E42D00A1D6A6537D1F5974EF114D18EB:500:SHA256", "Jody6" },
                { 45L, true, "Curtis Crist", "190FB1683E00B74771C10A69A00DBED90CE8B830ACC374D9DB987AB21E1063A8:43088B6D1B5745A30FD0BD8CE182B4E2:500:SHA256", "Curtis.Crist" },
                { 46L, true, "Jon Tremblay", "78E7883EC1A30363233279B7D646CCBCFD431E6DAE03FAF30C46220512BB0E6A:20FC641F58AEC924AFFC635630E3CF6D:500:SHA256", "Jon55" },
                { 47L, true, "Tabitha Leuschke", "C11744137183657C96CBFB208734642C998262038D418EEB7C25C538E5A38709:6ADB0D3C3AE87EDB1F3B98D481DE9729:500:SHA256", "Tabitha15" },
                { 48L, true, "Antoinette Quigley", "44BF8E5C22AE6974CE54DBFAD8C9481D1BED51723E732D8B5BCC28EE40B904CD:F1D1CBAC9A424CF4C415EDA485D43DDC:500:SHA256", "Antoinette.Quigley" },
                { 49L, true, "Willie Fay", "6A66EC6F968E75E467A46DC654BD3FAE45433371A6A3E6BBC16064EABDF346C7:E88B829732BDEE469A95BD8123EC0316:500:SHA256", "Willie.Fay" },
                { 50L, true, "Terry Homenick", "93A30C13EBA60A03A305C0D004B970A08E340E5CADAFCB10C4A2B4A4065108C1:4E289B98138886E5BAA5E07AE7CAC249:500:SHA256", "Terry61" }
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Users");
    }
}
